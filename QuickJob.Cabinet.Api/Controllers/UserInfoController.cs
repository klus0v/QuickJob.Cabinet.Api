using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickJob.Cabinet.Api.Validation.Attributes;
using QuickJob.Cabinet.BusinessLogic.Managers.UsersInfo;
using QuickJob.Cabinet.DataModel.API.Requests.Info;
using QuickJob.Cabinet.DataModel.API.Responses;

namespace QuickJob.Cabinet.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserInfoController : ControllerBase
{
    private readonly IUsersManager usersManager;

    public UserInfoController(IUsersManager usersManager) => 
        this.usersManager = usersManager;

    [AllowAnonymous]
    [HttpGet("base")]
    [ProducesResponseType(typeof(BaseInfoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBaseUserInfo([FromQuery] Guid? userId = null) => 
        Ok(await usersManager.GetBaseUserInfo(userId));

    [HttpPatch("base")]
    [ProducesResponseType(typeof(BaseInfoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> PatchBaseUserInfo([FromBody, ValidSetInfoRequest] SetBaseInfoRequest request) => 
        Ok(await usersManager.PatchBaseUserInfo(request));

    [HttpGet("additional")]
    [ProducesResponseType(typeof(AdditionalInfoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetaAdditionalUserInfo() => 
        Ok(await usersManager.GetAdditionalUserInfo());

    [HttpPatch("additional")]
    [ProducesResponseType(typeof(AdditionalInfoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> PatchAdditionalUserInfo([FromBody, ValidSetInfoRequest] SetAdditionalInfoRequest request) => 
        Ok(await usersManager.PatchAdditionalUserInfo(request));
}
