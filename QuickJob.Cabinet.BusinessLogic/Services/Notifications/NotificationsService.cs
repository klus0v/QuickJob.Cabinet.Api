using System.Net;
using QuickJob.Cabinet.BusinessLogic.Services.Users;
using QuickJob.Cabinet.DataModel.Exceptions;
using QuickJob.Users.Client;
using QuickJob.Users.Client.Clients;
using Vostok.Logging.Abstractions;

namespace QuickJob.Cabinet.BusinessLogic.Services.Notifications;

public sealed class NotificationsService : INotificationsService
{
    private readonly IUsersClient usersClient;
    private readonly INotificationsClient notificationsClient;
    private readonly ILog log;

    public NotificationsService(IQuickJobUsersClient usersClient, ILog log, INotificationsClient notificationsClient)
    {
        this.notificationsClient = notificationsClient;
        this.log = log.ForContext(nameof(UsersService));
        this.usersClient = usersClient.Users;
    }

    public async Task SendEmail(string email, Dictionary<string, string> variables, string templateId)
    {
        var result = await notificationsClient.SendEmail(email, variables, templateId);
        if (!result.IsSuccessful)
        {
            log.Error($"Error send message for email: {email}, error: {result.ErrorResult.Message}");
            throw new CustomHttpException(HttpStatusCode.ServiceUnavailable, HttpErrors.Notifications(result.ErrorResult.Message));
        }
    }
}