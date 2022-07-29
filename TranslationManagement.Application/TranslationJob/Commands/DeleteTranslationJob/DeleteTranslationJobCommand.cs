namespace TranslationManagement.Application.TranslationJob.Commands.DeleteTranslationJob;

public class DeleteTranslationJobCommand : IRequest<Unit>
{
    public string Id { get; set; }
    
    public class DeleteTranslationJobCommandHandler : IRequestHandler<DeleteTranslationJobCommand, Unit>
    {
        private readonly IApDbContext _dbContext;

        public DeleteTranslationJobCommandHandler(
            IApDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Unit> Handle(DeleteTranslationJobCommand request, CancellationToken cancellationToken)
        {
            var translationJob = await _dbContext.TranslationJobs
                .FirstOrDefaultAsync(j =>
                    j.Id == request.Id 
                    && !j.IsDeleted, 
                    cancellationToken
                );

            if (translationJob is null)
                throw new NotFoundException("Translation job");

            if (translationJob.Status == TranslationJobStatus.InProgress)
                throw new ApplicationLayerException(
                    $"Can't delete translation job which is '{nameof(TranslationJobStatus.InProgress)}'");

            translationJob.IsDeleted = true;

            _dbContext.TranslationJobs.Update(translationJob);

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0
                ? Unit.Value
                : throw new ApplicationLayerException("Error while deleting Translation job");
        }
    }
}