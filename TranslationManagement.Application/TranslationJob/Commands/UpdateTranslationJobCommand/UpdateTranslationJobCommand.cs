namespace TranslationManagement.Application.TranslationJob.Commands.UpdateTranslationJobCommand;

public class UpdateTranslationJobCommand : IRequest<Unit>
{
    public string Id { get; set; }
    public string OriginalContent { get; set; }
    public TranslationJobStatus Status { get; set; }
    public string? TranslatorId { get; set; }

    public class UpdateTranslationJobCommandHandler : IRequestHandler<UpdateTranslationJobCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IApDbContext _dbContext;

        public UpdateTranslationJobCommandHandler(
            IMapper mapper,
            IApDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<Unit> Handle(UpdateTranslationJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _dbContext.TranslationJobs
                .Include(j => j.Translator)
                .FirstOrDefaultAsync(j =>
                    j.Id == request.Id
                    && !j.IsDeleted,
                    cancellationToken
                );

            if (job is null)
                throw new NotFoundException("Translation job");

            if (job.Status == TranslationJobStatus.InProgress && job.OriginalContent != request.OriginalContent)
                throw new ApplicationLayerException(
                    $"Can't modify content while job is '{nameof(TranslationJobStatus.InProgress)}'");

            var canModifyStatus = request.Status >= job.Status;
            if (!canModifyStatus)
                throw new ApplicationLayerException("Can't put job into previous state.");
            
            if(!string.IsNullOrEmpty(request.TranslatorId) && request.TranslatorId != job.TranslatorId)
            {
                var translator = await _dbContext.Translators
                    .FirstOrDefaultAsync(t =>
                            t.Id == request.TranslatorId
                            && !t.IsDeleted,
                        cancellationToken
                    );

                if (translator is null)
                    throw new NotFoundException("Translator");

                if (translator.Type != TranslatorType.Certified)
                    throw new ApplicationLayerException(
                        "Can't assign translator to job. Translator isn't certified.");

                if (request.Status == TranslationJobStatus.InProgress)
                    throw new ApplicationLayerException("Can't change translator while job is in progress.");

                job.Translator = translator;
            } 
            else if (string.IsNullOrEmpty(request.TranslatorId) && !string.IsNullOrEmpty(job.TranslatorId))
            {
                if (request.Status == TranslationJobStatus.InProgress)
                    throw new ApplicationLayerException("Can't unassign translator while job is in progress.");
                
                job.TranslatorId = null;
            }

            _mapper.Map(request, job);

            job.Price = TranslationJobEntity.CalculatePrice(job.OriginalContent);

            _dbContext.TranslationJobs.Update(job);

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0
                ? Unit.Value
                : throw new ApplicationLayerException("Error while updating job.");
        }
    }
}