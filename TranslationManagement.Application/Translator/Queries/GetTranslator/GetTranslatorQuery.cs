

namespace TranslationManagement.Application.Translator.Queries.GetTranslator;

public class GetTranslatorQuery : IRequest<TranslatorDto>
{
    public string Id { get; set; }
    
    public class GetTranslatorQueryHandler : IRequestHandler<GetTranslatorQuery, TranslatorDto>
    {
        private readonly IMapper _mapper;
        private readonly IApDbContext _dbContext;

        public GetTranslatorQueryHandler(
            IMapper mapper,
            IApDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<TranslatorDto> Handle(GetTranslatorQuery request, CancellationToken cancellationToken)
        {
            var translator = await _dbContext.Translators
                .Include(t => t.Jobs.Where(j => !j.IsDeleted))
                .Where(t => t.Id == request.Id && !t.IsDeleted)
                .ProjectTo<TranslatorDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (translator is null)
                throw new NotFoundException("Translator");

            return translator;
        }
    }
}