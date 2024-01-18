using System.Net;
using QuickJob.Cabinet.BusinessLogic.Mappers;
using QuickJob.Cabinet.BusinessLogic.Services.Users;
using QuickJob.Cabinet.DataModel.API.Requests.Info;
using QuickJob.Cabinet.DataModel.API.Responses;
using QuickJob.Cabinet.DataModel.Context;
using QuickJob.Cabinet.DataModel.Exceptions;

namespace QuickJob.Cabinet.BusinessLogic.Managers.UsersInfo;

public sealed class UsersManager : IUsersManager
{
    private readonly IUsersService usersService;

    public UsersManager(IUsersService usersService)
    {
        this.usersService = usersService;
    }

    public async Task<BaseInfoResponse> GetBaseUserInfo(Guid? id = null)
    {
        if (id is null && !RequestContext.ClientInfo.IsUserAuthenticated)
            throw new CustomHttpException(HttpStatusCode.NotFound, HttpErrors.NotFound(string.Empty));
        
        var userId = id ?? RequestContext.ClientInfo.UserId;
        var userRepresentation = await usersService.GetById(userId);
        return userRepresentation.MapToBaseInfo();
    }

    public async Task<AdditionalInfoResponse> GetAdditionalUserInfo()
    {
        var userRepresentation = await usersService.GetById(RequestContext.ClientInfo.UserId);
        return userRepresentation.MapToAdditionalInfo();
    }

    public async Task<BaseInfoResponse> PatchBaseUserInfo(SetBaseInfoRequest request)
    {
        var user = await usersService.GetById(RequestContext.ClientInfo.UserId); 
        user.SetBaseInfo(request);
        var result = await usersService.PatchById(RequestContext.ClientInfo.UserId, user);
        return result.MapToBaseInfo();
    }

    public async Task<AdditionalInfoResponse> PatchAdditionalUserInfo(SetAdditionalInfoRequest request)
    {
        var user = await usersService.GetById(RequestContext.ClientInfo.UserId); 
        user.SetAdditionalInfo(request);
        var result = await usersService.PatchById(RequestContext.ClientInfo.UserId, user);
        return result.MapToAdditionalInfo();
    }
}