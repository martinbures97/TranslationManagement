namespace TranslationManagement.Application.TranslationJob.Queries.GetTranslationJobs;

public class GetTranslationJobsQuery : IRequest<IEnumerable<TranslationJobDto>>
{
    public class GetTranslationJobsQueryHandler : IRequestHandler<GetTranslationJobsQuery, IEnumerable<TranslationJobDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApDbContext _dbContext;

        public GetTranslationJobsQueryHandler(
            IMapper mapper,
            IApDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public Task<IEnumerable<TranslationJobDto>> Handle(GetTranslationJobsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _dbContext.TranslationJobs
                .Where(j => !j.IsDeleted)
                .ProjectTo<TranslationJobDto>(_mapper.ConfigurationProvider)
                .AsEnumerable()
            );
        }
    }
}