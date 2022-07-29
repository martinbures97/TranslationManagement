using TranslationManagement.Application.TranslationJob.EventHandlers.TranslationJobCreated;

namespace TranslationManagement.Application.TranslationJob.Commands.AddTranslationJob;

public class AddTranslationJobCommand : IRequest<string>
{
    public string CustomerName { get; set; }
    public string OriginalContent { get; set; }
    
    public string? TranslatorId { get; set; }
    
    public class AddTranslationJobCommandHandler : IRequestHandler<AddTranslationJobCommand, string>
    {
        private readonly IPublisher _publisher;
        private readonly IApDbContext _dbContext;

        public AddTranslationJobCommandHandler(
            IPublisher publisher,
            IApDbContext dbContext)
        {
            _publisher = publisher;
            _dbContext = dbContext;
        }

        public async Task<string> Handle(AddTranslationJobCommand request, CancellationToken cancellationToken)
        {
            var translationJob = new TranslationJobEntity()
            {
                OriginalContent = request.OriginalContent,
                Price = request.OriginalContent.Length * TranslationJobEntity.PricePerCharacter,
                Status = TranslationJobStatus.New,
                CustomerName = request.CustomerName
            };
            
            if(!string.IsNullOrEmpty(request.TranslatorId))
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

                translationJob.Translator = translator;
            }

            await _dbContext.TranslationJobs.AddAsync(translationJob, cancellationToken);

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (result <= 0) throw new ApplicationLayerException("Error while creating translation job.");

            _publisher.Publish(new TranslationJobCreatedEvent(translationJob), cancellationToken)
                .ConfigureAwait(false);
            
            return translationJob.Id;
        }
    }
}