namespace TranslationManagement.Application.Translator.Queries;

public class TranslatorDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int HourlyRate { get; set; }
    public string CreditCardNumber { get; set; }
    public TranslatorType Type { get; set; }
    public ICollection<string> JobIds { get; set; }
}