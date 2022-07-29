namespace TranslationManagement.Application.Translator.Commands.DeleteTranslator;

public class DeleteTranslatorCommand : IRequest<Unit>
{
    public string Id { get; set; }
    
    public class DeleteTranslatorCommandHandler : IRequestHandler<DeleteTranslatorCommand, Unit>
    {
        private readonly IApDbContext _dbContext;

        public DeleteTranslatorCommandHandler(
            IApDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Unit> Handle(DeleteTranslatorCommand request, CancellationToken cancellationToken)
        {
            var translator = await _dbContext.Translators
                .Include(t => t.Jobs.Where(j => !j.IsDeleted))
                .FirstOrDefaultAsync(t => 
                        t.Id == request.Id && 
                        !t.IsDeleted,
                    cancellationToken
                );

            if (translator is null)
                throw new NotFoundException("Translator");

            if (translator.Jobs.Any(j =>
                    j.Status == TranslationJobStatus.InProgress || j.Status == TranslationJobStatus.New))
                throw new ApplicationLayerException("Can't delete translator, while having active jobs.");

            translator.IsDeleted = true;
            
            _dbContext.Translators.Update(translator);

            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            
            return result > 0
                ? Unit.Value
                : throw new ApplicationLayerException("Error while deleting translator.");
        }
    }
}