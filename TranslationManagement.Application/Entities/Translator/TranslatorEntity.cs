namespace TranslationManagement.Application.Entities.Translator;

public class TranslatorEntity : EntityBase<string>
{
    private readonly List<TranslationJobEntity> _jobs;

    public const int NameMaxLength = 50;
    public const int CreditCardNumberMaxLength = 19;
    
    public string Name { get; set; }
    public int HourlyRate { get; set; }
    public TranslatorType Type { get; set; }
    public string CreditCardNumber { get; set; }
    
    public IReadOnlyCollection<TranslationJobEntity> Jobs => _jobs.AsReadOnly();
}