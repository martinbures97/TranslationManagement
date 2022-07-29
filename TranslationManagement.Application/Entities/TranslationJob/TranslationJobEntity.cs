namespace TranslationManagement.Application.Entities.TranslationJob;

public class TranslationJobEntity : EntityBase<string>
{
    public const double PricePerCharacter = 0.01;

    public const int CustomerNameMaxLength = 50;
    public const int OriginalContentMaxLength = 2000;
    
    
    public string? CustomerName { get; set; }
    public TranslationJobStatus Status { get; set; }
    public string OriginalContent { get; set; }
    public string? TranslatedContent { get; set; }
    public double Price { get; set; }
    
    public string? TranslatorId { get; set; }
    public TranslatorEntity? Translator { get; set; }

    public static double CalculatePrice(string contentToTranslate)
    {
        return contentToTranslate.Length * PricePerCharacter;
    }
}