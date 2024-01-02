using FS.Keycloak.RestApiClient.Model;

namespace QuickJob.Cabinet.BusinessLogic.Services.Users;

public interface IUsersService
{
    Task<UserRepresentation> GetById(Guid userId);
    Task<UserRepresentation> PatchById(Guid userId, UserRepresentation patchRequest);
}