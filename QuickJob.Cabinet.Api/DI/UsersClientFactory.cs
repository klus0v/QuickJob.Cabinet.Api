using QuickJob.Cabinet.DataModel.Configuration;
using QuickJob.Users.Client;
using IConfigurationProvider = Vostok.Configuration.Abstractions.IConfigurationProvider;


namespace QuickJob.Cabinet.Api.DI;

internal class UsersClientFactory
{
    private readonly IConfigurationProvider configuration;

    public UsersClientFactory(IConfigurationProvider configuration) => 
        this.configuration = configuration;
    
    public QuickJobUsersClient GetClient()
    {
        var settings = configuration.Get<ServiceSettings>();

        return new QuickJobUsersClient(settings.UsersApiBaseUrl, settings.UsersApiKey);
    }
}