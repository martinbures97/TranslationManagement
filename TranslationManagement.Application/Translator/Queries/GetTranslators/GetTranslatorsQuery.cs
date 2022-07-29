namespace TranslationManagement.Application.Translator.Queries.GetTranslators;

public class GetTranslatorsQuery : IRequest<IEnumerable<TranslatorDto>>
{
    public class GetTranslatorsQueryHandler : IRequestHandler<GetTranslatorsQuery, IEnumerable<TranslatorDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApDbContext _dbContext;

        public GetTranslatorsQueryHandler(
            IMapper mapper,
            IApDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public Task<IEnumerable<TranslatorDto>> Handle(GetTranslatorsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Translators
                .Include(t => t.Jobs.Where(j => !j.IsDeleted))
                .Where(t => !t.IsDeleted)
                .ProjectTo<TranslatorDto>(_mapper.ConfigurationProvider)
                .AsEnumerable());
        }
    }
}