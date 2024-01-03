using System.Net;
using QuickJob.Cabinet.BusinessLogic.Services.Users;
using QuickJob.Cabinet.DataModel.Exceptions;
using QuickJob.Notifications.Client;
using QuickJob.Notifications.Client.Models.API.Requests.Email;
using Vostok.Logging.Abstractions;

namespace QuickJob.Cabinet.BusinessLogic.Services.Notifications;

public sealed class NotificationsService : INotificationsService
{
    private readonly IQuickJobNotificationsClient notificationsClient;
    private readonly ILog log;

    public NotificationsService(ILog log, IQuickJobNotificationsClient notificationsClient)
    {
        this.notificationsClient = notificationsClient;
        this.log = log.ForContext(nameof(UsersService));
    }

    public async Task SendEmail(string email, Dictionary<string, string> variables, string templateName)
    {
        var request = new SendEmailRequest
        {
            Email = email,
            TemplateName = templateName,
            Variables = variables
        };
        var result = await notificationsClient.Email.SendEmailAsync(request);
        if (!result.IsSuccessful)
        {
            log.Error($"Error send message for email: {email}, error: {result.ErrorResult.Message}");
            throw new CustomHttpException(HttpStatusCode.ServiceUnavailable, HttpErrors.Notifications(result.ErrorResult.Message));
        }
    }
}