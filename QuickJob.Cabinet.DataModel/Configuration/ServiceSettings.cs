namespace QuickJob.Cabinet.DataModel.Configuration;

public class ServiceSettings
{
    public string UsersApiKey { get; set; }
    public string UsersApiBaseUrl { get; set; }
    public string NotificationsApiKey { get; set; }
    public string NotificationsApiBaseUrl { get; set; }
    public List<string> Origins { get; set; }
    public KeycloackSettings KeycloackSettings { get; set; }
}

public class KeycloackSettings
{
    public string Authority { get; set; }
    public string Audience { get; set; } 
}