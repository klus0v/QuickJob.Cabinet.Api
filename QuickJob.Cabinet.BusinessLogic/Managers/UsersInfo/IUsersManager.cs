using QuickJob.Cabinet.DataModel.API.Requests;
using QuickJob.Cabinet.DataModel.API.Responses;

namespace QuickJob.Cabinet.BusinessLogic.Managers.UsersInfo;

public interface IUsersManager
{
    Task<BaseInfoResponse> GetBaseUserInfo(Guid? userId = null);
    Task<AdditionalInfoResponse> GetAdditionalUserInfo();
    Task<BaseInfoResponse> PatchBaseUserInfo(SetBaseInfoRequest request);
    Task<AdditionalInfoResponse> PatchAdditionalUserInfo(SetAdditionalInfoRequest request);
}