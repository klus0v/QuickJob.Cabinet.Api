using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickJob.Cabinet.BusinessLogic.Managers.Avatars;
using QuickJob.Cabinet.DataModel.API.Responses;

namespace QuickJob.Cabinet.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AvatarsController : ControllerBase
{
    private readonly IAvatarsManager avatarsManager;

    public AvatarsController(IAvatarsManager avatarsManager) => 
        this.avatarsManager = avatarsManager;

    [HttpPut]
    public async Task<SetUserAvatarResponse> SetUserAvatar(IFormFile content) => 
        new(await avatarsManager.SetAvatar(content));

    [HttpDelete]
    public async Task DeleteUserAvatar() => 
        await avatarsManager.DeleteAvatar();
}
