using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickJob.Cabinet.Api.Validation.Attributes;
using QuickJob.Cabinet.BusinessLogic.Managers.UsersInfo;
using QuickJob.Cabinet.DataModel.API.Requests;

namespace QuickJob.Cabinet.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserInfoController : ControllerBase
{
    private readonly IUsersManager usersManager;

    public UserInfoController(IUsersManager usersManager) => 
        this.usersManager = usersManager;

    [HttpGet("base")]
    public async Task<IActionResult> GetBaseUserInfo([FromQuery] Guid? userId = null) => 
        Ok(await usersManager.GetBaseUserInfo(userId));

    [HttpPatch("base")]
    public async Task<IActionResult> PatchBaseUserInfo([FromBody, ValidSetInfoRequest] SetBaseInfoRequest request) => 
        Ok(await usersManager.PatchBaseUserInfo(request));

    [HttpGet("additional")]
    public async Task<IActionResult> GetaAdditionalUserInfo() => 
        Ok(await usersManager.GetAdditionalUserInfo());

    [HttpPatch("additional")]
    public async Task<IActionResult> PatchAdditionalUserInfo([FromBody, ValidSetInfoRequest] SetAdditionalInfoRequest request) => 
        Ok(await usersManager.PatchAdditionalUserInfo(request));
}
