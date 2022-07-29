

namespace TranslationManagement.Infrastructure.Configurations;

public class TranslatorEntityTypeConfiguration : EntityTypeConfigurationBase<TranslatorEntity, string>
{
    public static string CertifiedTranslatorId = Guid.NewGuid().ToString();
    
    public override void Configure(EntityTypeBuilder<TranslatorEntity> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.Name)
            .HasMaxLength(TranslatorEntity.NameMaxLength);

        builder.Property(b => b.CreditCardNumber)
            .HasMaxLength(TranslatorEntity.CreditCardNumberMaxLength);
        
        builder.Metadata
            .FindNavigation(nameof(TranslatorEntity.Jobs))
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder.HasMany(b => b.Jobs)
            .WithOne(b => b.Translator)
            .HasForeignKey(b => b.TranslatorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new List<TranslatorEntity>()
        {
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Mylie Ritter",
                Type = TranslatorType.Applicant,
                HourlyRate = 500,
                CreditCardNumber = "4590181712697982"
            },
            new()
            {
                Id = CertifiedTranslatorId,
                Name = "Evica Johansson",
                Type = TranslatorType.Certified,
                HourlyRate = 1000,
                CreditCardNumber = "4590182781315688"
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Kerstin Bazhaev",
                Type = TranslatorType.Applicant,
                HourlyRate = 150,
                CreditCardNumber = "4590181640630931",
                IsDeleted = true
            }
        });
    }
}