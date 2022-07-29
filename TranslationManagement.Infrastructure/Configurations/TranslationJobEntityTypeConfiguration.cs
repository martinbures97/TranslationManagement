namespace TranslationManagement.Infrastructure.Configurations;

public class TranslationJobEntityTypeConfiguration : EntityTypeConfigurationBase<TranslationJobEntity, string>
{
    public override void Configure(EntityTypeBuilder<TranslationJobEntity> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.CustomerName)
            .HasMaxLength(TranslationJobEntity.CustomerNameMaxLength);
        
        builder.Property(b => b.OriginalContent)
            .HasMaxLength(TranslationJobEntity.OriginalContentMaxLength);

        builder.HasData(new List<TranslationJobEntity>()
        {
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Status = TranslationJobStatus.New,
                OriginalContent = "Text to translate",
                Price = TranslationJobEntity.CalculatePrice("Text to translate"),
                CustomerName = "Netflix",
                IsDeleted = false,
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Status = TranslationJobStatus.InProgress,
                OriginalContent = "Text to translate",
                Price = TranslationJobEntity.CalculatePrice("Text to translate"),
                CustomerName = "Microsoft",
                IsDeleted = false,
                TranslatorId = TranslatorEntityTypeConfiguration.CertifiedTranslatorId
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Status = TranslationJobStatus.Completed,
                OriginalContent = "Text to translate",
                Price = TranslationJobEntity.CalculatePrice("Text to translate"),
                CustomerName = "Xiaomi",
                IsDeleted = false,
                TranslatorId = TranslatorEntityTypeConfiguration.CertifiedTranslatorId
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Status = TranslationJobStatus.New,
                OriginalContent = "Text to translate",
                Price = TranslationJobEntity.CalculatePrice("Text to translate"),
                CustomerName = "Apple",
                IsDeleted = true
            }
        });
    }
}