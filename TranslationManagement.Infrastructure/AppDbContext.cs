namespace TranslationManagement.Infrastructure
{
    public class AppDbContext : DbContext, IApDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TranslatorEntity> Translators { get; private set; }
        public DbSet<TranslationJobEntity> TranslationJobs { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}