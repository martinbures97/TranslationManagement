using External.ThirdParty.Services;

namespace TranslationManagement.Application.TranslationJob.EventHandlers.TranslationJobCreated;

public class TranslationJobCreatedEventHandler : INotificationHandler<TranslationJobCreatedEvent>
{
    private readonly INotificationService _notificationService;

    public TranslationJobCreatedEventHandler(
        INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    public async Task Handle(TranslationJobCreatedEvent notification, CancellationToken cancellationToken)
    {
        var result = false;
        var maximumTries = 3;

        for (var i = 0; i < maximumTries; i++)
        {
            try
            {
                result = await _notificationService.SendNotification("Job created: " + notification.Job.Id);
            }
            catch (Exception e)
            {
                // ignored
            }

            if (result)
                break;
        }
    }
}