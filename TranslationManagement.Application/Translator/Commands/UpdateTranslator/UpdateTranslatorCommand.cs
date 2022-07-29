

namespace TranslationManagement.Application.Translator.Commands.UpdateTranslator;

public class UpdateTranslatorCommand : IRequest<Unit>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int HourlyRate { get; set; }
    public string CreditCardNumber { get; set; }
    public TranslatorType Type { get; set; }
    
    public class UpdateTranslatorCommandHandler : IRequestHandler<UpdateTranslatorCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IApDbContext _dbContext;

        public UpdateTranslatorCommandHandler(
            IMapper mapper,
            IApDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<Unit> Handle(UpdateTranslatorCommand request, CancellationToken cancellationToken)
        {
            var translator = await _dbContext.Translators
                .FirstOrDefaultAsync(t => 
                        t.Id == request.Id && 
                        !t.IsDeleted,
                    cancellationToken
                );

            if (translator is null)
                throw new NotFoundException("Translator");

            _mapper.Map(request, translator);

            _dbContext.Translators.Update(translator);

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0
                ? Unit.Value
                : throw new ApplicationLayerException("Error while updating translation job.");
        }
    } 
}