using System.Net;
using FS.Keycloak.RestApiClient.Model;
using QuickJob.Cabinet.DataModel.Exceptions;
using QuickJob.Users.Client;
using QuickJob.Users.Client.Clients;
using Vostok.Logging.Abstractions;

namespace QuickJob.Cabinet.BusinessLogic.Services.Users;

public sealed class UsersService : IUsersService
{
    private readonly IUsersClient usersClient;
    private readonly ILog log;

    public UsersService(IQuickJobUsersClient usersClient, ILog log)
    {
        this.log = log.ForContext(nameof(UsersService));
        this.usersClient = usersClient.Users;
    }

    public async Task<UserRepresentation> GetById(Guid userId)
    {
        var result = await usersClient.GetUserAsync(userId);
        if (result.IsSuccessful)
            return result.Response;
        log.Error($"Error get user: {userId}, error: {result.ErrorResult.Message}");
        throw new CustomHttpException(HttpStatusCode.ServiceUnavailable, HttpErrors.Users(result.ErrorResult.Message));
    }

    public async Task<UserRepresentation> PatchById(Guid userId, UserRepresentation patchRequest)
    {
        var result = await usersClient.PatchUserAsync(userId, patchRequest);
        if (result.IsSuccessful)
            return result.Response;
        log.Error($"Error get user: {userId}, error: {result.ErrorResult.Message}");
        throw new CustomHttpException(HttpStatusCode.ServiceUnavailable, HttpErrors.Users(result.ErrorResult.Message));

    }
}