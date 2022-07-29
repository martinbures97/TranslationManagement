namespace TranslationManagement.Application.TranslationJob.EventHandlers.TranslationJobCreated;

public class TranslationJobCreatedEvent : INotification
{
    public TranslationJobEntity Job { get; }

    public TranslationJobCreatedEvent(TranslationJobEntity job)
    {
        Job = job;
    }    
}