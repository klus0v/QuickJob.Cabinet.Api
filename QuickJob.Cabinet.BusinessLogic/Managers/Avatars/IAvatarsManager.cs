using Microsoft.AspNetCore.Http;

namespace QuickJob.Cabinet.BusinessLogic.Managers.Avatars;

public interface IAvatarsManager
{
    Task<string> SetAvatar(IFormFile content);
    Task DeleteAvatar();
}