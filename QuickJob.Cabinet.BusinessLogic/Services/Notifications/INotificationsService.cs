namespace QuickJob.Cabinet.BusinessLogic.Services.Notifications;

public interface INotificationsService
{
    Task SendEmail(string email, Dictionary<string, string> variables, string templateName);
}