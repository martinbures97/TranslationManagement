namespace TranslationManagement.Application.Common.Interfaces;

public interface IApDbContext
{
    DbSet<TranslatorEntity> Translators { get; }
    DbSet<TranslationJobEntity> TranslationJobs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}