namespace TranslationManagement.Application.Translator.Commands.AddTranslator;

public class AddTranslatorCommand : IRequest<string>
{
    public string Name { get; set; }
    public int HourlyRate { get; set; }
    public TranslatorType Type { get; set; }
    public string CreditCardNumber { get; set; }
    
    public class AddTranslatorCommandHandler : IRequestHandler<AddTranslatorCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IApDbContext _dbContext;

        public AddTranslatorCommandHandler(
            IMapper mapper,
            IApDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<string> Handle(AddTranslatorCommand request, CancellationToken cancellationToken)
        {
            var translator = _mapper.Map<TranslatorEntity>(request);

            await _dbContext.Translators.AddAsync(translator, cancellationToken);

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result > 0
                ? translator.Id
                : throw new ApplicationLayerException("Error while creating translator.");
        }
    }
}