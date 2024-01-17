using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QuickJob.Cabinet.Api.Middlewares;
using QuickJob.Cabinet.BusinessLogic.Managers.Avatars;
using QuickJob.Cabinet.BusinessLogic.Managers.Factors;
using QuickJob.Cabinet.BusinessLogic.Managers.UsersInfo;
using QuickJob.Cabinet.BusinessLogic.Services.Notifications;
using QuickJob.Cabinet.BusinessLogic.Services.S3;
using QuickJob.Cabinet.BusinessLogic.Services.Users;
using QuickJob.Cabinet.DataModel.Configuration;
using QuickJob.Notifications.Client;
using QuickJob.Users.Client;
using Vostok.Configuration.Sources.Json;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;
using Vostok.Logging.File;
using Vostok.Logging.File.Configuration;
using ConfigurationProvider = Vostok.Configuration.ConfigurationProvider;
using IConfigurationProvider = Vostok.Configuration.Abstractions.IConfigurationProvider;

namespace QuickJob.Cabinet.Api.DI;

internal static class ServiceCollectionExtensions
{
    private const string FrontSpecificOrigins = "_frontSpecificOrigins";

    public static void AddServiceCors(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var serviceProperties = serviceProvider.GetRequiredService<IConfigurationProvider>().Get<ServiceSettings>();

         services
            .AddCors(option => option
                //.AddPolicy(FrontSpecificOrigins, builder => builder.WithOrigins(serviceSettings.Origins.ToArray())
                .AddPolicy(FrontSpecificOrigins, builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
    }
    
    public static void UseServiceCors(this IApplicationBuilder builder) => 
        builder.UseCors(FrontSpecificOrigins);

    public static void AddServiceAuthentication(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var keycloackSettings = serviceProvider.GetRequiredService<IConfigurationProvider>().Get<ServiceSettings>().KeycloackSettings;
        
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = keycloackSettings.Authority;
                options.Audience = keycloackSettings.Audience;
            });
    }

    public static void AddServiceSwaggerDocument(this IServiceCollection services)
    {
        services.AddSwaggerDocument(doc =>
        {
            doc.Title = "QuickJob.Cabinet.Api";
            doc.AddSecurity("Bearer", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
            {
                Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });
        });
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }

    public static void AddSettings(this IServiceCollection services)
    {
        var provider = new ConfigurationProvider();

        provider.SetupSourceFor<ServiceSettings>(new JsonFileSource($"QuickJob.Settings/{nameof(ServiceSettings)}.json"));
        provider.SetupSourceFor<S3Settings>(new JsonFileSource($"QuickJob.Settings/{nameof(S3Settings)}.json"));

        services.AddSingleton<IConfigurationProvider>(provider);
    }

    public static void AddSystemServices(this IServiceCollection services) => services
        .AddDistributedMemoryCache()
        .AddSingleton<IAvatarsManager, AvatarsManager>()
        .AddSingleton<IUsersManager, UsersManager>()
        .AddSingleton<IFactorsManager, FactorsManager>()
        .AddSingleton<IS3Storage, AWSStorage>()
        .AddSingleton<IUsersService, UsersService>()
        .AddSingleton<INotificationsService, NotificationsService>();

    public static void AddExternalServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ILog>(new CompositeLog(new ConsoleLog(), new FileLog(new FileLogSettings())));
        services
            .AddSingleton<AWSClientFactory>()
            .TryAddSingleton(x => x.GetRequiredService<AWSClientFactory>().GetClient());
        services
            .AddSingleton<UsersClientFactory>()
            .TryAddSingleton<IQuickJobUsersClient>(x => x.GetRequiredService<UsersClientFactory>().GetClient());
        services
            .AddSingleton<NotificationsClientFactory>()
            .TryAddSingleton<IQuickJobNotificationsClient>(x => x.GetRequiredService<NotificationsClientFactory>().GetClient());
    }
    
    public static void AddAuthMiddleware(this IServiceCollection services) =>
        services.AddSingleton<UserAuthMiddleware>();

    public static void UseAuthMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<UserAuthMiddleware>();

    public static void UseUnhandledExceptionMiddleware(this IApplicationBuilder builder) => 
        builder.UseMiddleware<UnhandledExceptionMiddleware>();
}
