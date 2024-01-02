using QuickJob.Cabinet.DataModel.Configuration;
using QuickJob.Notifications.Client;
using IConfigurationProvider = Vostok.Configuration.Abstractions.IConfigurationProvider;


namespace QuickJob.Cabinet.Api.DI;

internal class NotificationsClientFactory
{
    private readonly IConfigurationProvider configuration;

    public NotificationsClientFactory(IConfigurationProvider configuration) => 
        this.configuration = configuration;
    
    public QuickJobNotificationsClient GetClient()
    {
        var settings = configuration.Get<ServiceSettings>();

        return new QuickJobNotificationsClient(settings.NotificationsApiBaseUrl, settings.NotificationsApiKey);
    }
}