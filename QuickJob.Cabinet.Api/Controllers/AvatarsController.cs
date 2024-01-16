using Microsoft.AspNetCore.Authorization;
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
    [ProducesResponseType(typeof(SetUserAvatarResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetUserAvatar(IFormFile content)
    {
        var response = new SetUserAvatarResponse(await avatarsManager.SetAvatar(content));
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserAvatar()
    { 
        await avatarsManager.DeleteAvatar();
        return Ok();
    }
}
