using FS.Keycloak.RestApiClient.Model;
using QuickJob.Cabinet.DataModel.ServerDataModel.Constants;

namespace QuickJob.Cabinet.BusinessLogic.Mappers;

public static class UserRepresentationExtensions
{

    #region Avatar

    public static string? GetAvatar(this UserRepresentation user) => 
        user.GetAttributeOrNull(KeycloackConstants.Avatar);
    
    public static void AddOrUpdateAvatar(this UserRepresentation user, string url) => 
        user.AddOrUpdateAttribute(KeycloackConstants.Avatar, url);

    public static void DeleteAvatar(this UserRepresentation user) => 
        user.DeleteAttribute(KeycloackConstants.Avatar);

    #endregion

    public static string? GetAttributeOrNull(this UserRepresentation user, string key) => 
        user.Attributes?.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();

    public static List<string>? GetAttributesOrNull(this UserRepresentation user, string key) =>
        user.Attributes?.FirstOrDefault(x => x.Key == key).Value;

    public static void AddOrUpdateAttribute(this UserRepresentation user, string key, string value)
    {
        user.Attributes ??= new Dictionary<string, List<string>>();
        
        user.Attributes.Remove(key);
        user.Attributes.Add(key, new List<string> {value});
    } 
    
    public static void AddOrUpdateAttribute(this UserRepresentation user, string key, List<string> values)
    {
        user.Attributes ??= new Dictionary<string, List<string>>();
        
        user.Attributes.Remove(key);
        user.Attributes.Add(key, values);
    } 
    
    public static void DeleteAttribute(this UserRepresentation user, string key) => 
        user.Attributes?.Remove(key);

    public static void DeleteAttributes(this UserRepresentation user, List<string> keys)
    {
        if (user.Attributes is null || keys.Count == 0) 
            return;

        foreach (var key in keys)
            user.Attributes.Remove(key);
    }
}