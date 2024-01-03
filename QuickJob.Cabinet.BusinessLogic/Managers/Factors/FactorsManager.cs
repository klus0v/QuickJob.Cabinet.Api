using System.Net;
using QuickJob.Cabinet.BusinessLogic.Helpers;
using QuickJob.Cabinet.BusinessLogic.Mappers;
using QuickJob.Cabinet.BusinessLogic.Services.Notifications;
using QuickJob.Cabinet.BusinessLogic.Services.Users;
using QuickJob.Cabinet.DataModel.Context;
using QuickJob.Cabinet.DataModel.Exceptions;
using QuickJob.Notifications.Client.Constants;

namespace QuickJob.Cabinet.BusinessLogic.Managers.Factors;

public sealed class FactorsManager : IFactorsManager
{
    private readonly IUsersService usersService;
    private readonly INotificationsService notificationsService;

    public FactorsManager(IUsersService usersService, INotificationsService notificationsService)
    {
        this.usersService = usersService;
        this.notificationsService = notificationsService;
    }

    public async Task InitSetUserEmail(string email)
    {
        var userId = RequestContext.ClientInfo.UserId;
        var user = await usersService.GetById(userId);
        if (user.Email == email)
            throw new CustomHttpException(HttpStatusCode.Conflict, HttpErrors.EmailAlreadySet);

        var code = SecretCodeGenerator.GenerateCode();
        user.AddOrUpdateAttribute(email, code);
        await usersService.PatchById(userId, user);

        var emailVariables = new Dictionary<string, string> { { "code", code } };
        await notificationsService.SendEmail(email, emailVariables, NtfConstants.EmailConfirmTemplate);
    }

    public async Task ConfirmSetUserEmail(string email, string code)
    {
        var userId = RequestContext.ClientInfo.UserId;
        var user = await usersService.GetById(userId);
        if (user.Email == email)
            throw new CustomHttpException(HttpStatusCode.Conflict, HttpErrors.EmailAlreadySet);
        if (code != user.GetAttributeOrNull(email))
            throw new CustomHttpException(HttpStatusCode.Forbidden, HttpErrors.IncorrectCode);

        user.Email = email;
        user.Username = email;
        user.DeleteAttribute(email);
        await usersService.PatchById(userId, user);
    }
}