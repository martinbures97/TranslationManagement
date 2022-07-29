namespace TranslationManagement.Application.TranslationJob.Queries;

public class TranslationJobDto
{
    public string Id { get; set; }
    public string CustomerName { get; set; }
    public string OriginalContent { get; set; }
    public string TranslatedContent { get; set; }
    public TranslationJobStatus Status { get; set; }
    public double Price { get; set; }
    public string? TranslatorId { get; set; }
}