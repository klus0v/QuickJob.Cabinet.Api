using FS.Keycloak.RestApiClient.Model;
using QuickJob.Cabinet.DataModel.API.Requests.Info;
using QuickJob.Cabinet.DataModel.API.Responses;
using QuickJob.Cabinet.DataModel.ServerDataModel;
using QuickJob.Cabinet.DataModel.ServerDataModel.Constants;

namespace QuickJob.Cabinet.BusinessLogic.Mappers;

public static class UserMapper
{
    public static BaseInfoResponse MapToBaseInfo(this UserRepresentation user)
    {
        return new BaseInfoResponse
        {
            Id = Guid.Parse(user.Id),
            Email = user.Email,
            Fio = user.GetAttributeOrNull(KeycloackConstants.Fio),
            Phone = user.GetAttributeOrNull(KeycloackConstants.Phone),
            Address = user.GetAttributeOrNull(KeycloackConstants.Address),
            Telegram = new Telegram(
                user.GetAttributeOrNull(KeycloackConstants.TgId), 
                user.GetAttributeOrNull(KeycloackConstants.TgUsername))
        };
    }

    public static AdditionalInfoResponse MapToAdditionalInfo(this UserRepresentation user)
    {
        return new AdditionalInfoResponse()
        { 
            Id = Guid.Parse(user.Id),
            Institute = user.GetAttributeOrNull(KeycloackConstants.Institute),
            BirthDate = user.GetAttributeOrNull(KeycloackConstants.BirthDate),
            Course = int.TryParse(user.GetAttributeOrNull(KeycloackConstants.Course), out var course) ? course : null,
            Citizenship = user.GetAttributeOrNull(KeycloackConstants.Citizenship),
            Preferences = user.GetAttributesOrNull(KeycloackConstants.Preferences)
        };
    }
    
    public static void SetAdditionalInfo(this UserRepresentation user, SetAdditionalInfoRequest additionalInfo)
    {

        user.Attributes ??= new Dictionary<string, List<string>>();

        if (additionalInfo.Institute is not null)
            user.AddOrUpdateAttribute(KeycloackConstants.Institute, additionalInfo.Institute);
        if (additionalInfo.BirthDate is not null)
            user.AddOrUpdateAttribute(KeycloackConstants.BirthDate, additionalInfo.BirthDate);
        if (additionalInfo.Course is not null)
            user.AddOrUpdateAttribute(KeycloackConstants.Course, additionalInfo.Course.ToString() ?? "0");
        if (additionalInfo.Citizenship is not null)
            user.AddOrUpdateAttribute(KeycloackConstants.Citizenship, additionalInfo.Citizenship);
        if (additionalInfo.Preferences is not null)
            user.AddOrUpdateAttribute(KeycloackConstants.Preferences, additionalInfo.Preferences);

        if (additionalInfo.Deletes is not null)
            user.DeleteAttributes(additionalInfo.Deletes);
    }
    
    public static void SetBaseInfo(this UserRepresentation user, SetBaseInfoRequest baseInfo)
    {

        user.Attributes ??= new Dictionary<string, List<string>>();

        if (baseInfo.Fio is not null)
            user.AddOrUpdateAttribute(KeycloackConstants.Fio, baseInfo.Fio);
        if (baseInfo.Phone is not null)
            user.AddOrUpdateAttribute(KeycloackConstants.Phone, baseInfo.Phone);
        if (baseInfo.Address is not null)
            user.AddOrUpdateAttribute(KeycloackConstants.Address, baseInfo.Address);
        
        if (baseInfo.Deletes is not null)
            user.DeleteAttributes(baseInfo.Deletes);
    }
}