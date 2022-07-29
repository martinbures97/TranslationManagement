namespace TranslationManagement.Application.TranslationJob.Queries.GetTranslationJob;

public class GetTranslationJobQuery : IRequest<TranslationJobDto?>
{
    public string Id { get; set; }
    
    public class GetTranslationJobQueryHandler : IRequestHandler<GetTranslationJobQuery, TranslationJobDto?>
    {
        private readonly IMapper _mapper;
        private readonly IApDbContext _dbContext;

        public GetTranslationJobQueryHandler(
            IMapper mapper,
            IApDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<TranslationJobDto?> Handle(GetTranslationJobQuery request, CancellationToken cancellationToken)
        {
            var job = await _dbContext.TranslationJobs
                .Where(j => j.Id == request.Id && !j.IsDeleted)
                .ProjectTo<TranslationJobDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (job is null)
                throw new NotFoundException("Translation job");

            return job;
        }
    }
}