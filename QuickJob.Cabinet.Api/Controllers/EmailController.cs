using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickJob.Cabinet.BusinessLogic.Managers.Factors;
using QuickJob.Cabinet.DataModel.API.Requests.Email;

namespace QuickJob.Cabinet.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly IFactorsManager factorsManager;

    public EmailController(IFactorsManager factorsManager) => 
        this.factorsManager = factorsManager;

    [HttpPost]
    public async Task SetUserEmail(SetEmailRequest request) => 
        await factorsManager.InitSetUserEmail(request.Email);

    [HttpPost]
    public async Task ConfirmUserEmail(ConfirmEmailRequest request) => 
        await factorsManager.ConfirmSetUserEmail(request.Email, request.Code);
}
