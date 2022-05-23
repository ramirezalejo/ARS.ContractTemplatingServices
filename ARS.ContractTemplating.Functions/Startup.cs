using System.Net.Http;
using ARS.ContractTemplating.Domain.Contracts.Infrastructure;
using ARS.ContractTemplating.Domain.Contracts.Services;
using ARS.ContractTemplating.Infrastructure.Blobs;
using ARS.ContractTemplating.Infrastructure.CognitiveServices;
using ARS.ContractTemplating.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(ARS.ContractTemplating.Functions.Startup))]
namespace ARS.ContractTemplating.Functions;

/// <summary>
/// Startup for functions to initialize dependencies
/// </summary>
public class Startup : FunctionsStartup
{
    /// <summary>
    /// Configure
    /// </summary>
    /// <param name="builder"></param>
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
        builder.Services.AddSingleton(configuration);
        builder.Services.AddHttpClient();
        builder.Services.AddLogging();
        builder.Services.AddScoped<ICognitiveServicesClient>(x => 
            new CognitiveServicesClient(
                x.GetRequiredService<IHttpClientFactory>(),
                configuration.GetValue<string>("CognitiveSearchEndpoint"),
                configuration.GetValue<string>("CognitiveSearchSubscriptionKey"),
                x.GetRequiredService<ILogger>()));
        /*builder.Services.AddSingleton<IQueueClient>(x =>
            new QueueClient(configuration.GetValue<string>("StorageConnectionString"), "ContractRequest"));*/
        builder.Services.AddSingleton<IBlobClient>(x =>
            new BlobClient(configuration.GetValue<string>("StorageConnectionString")));
        builder.Services.AddTransient<IContractsService, ContractsService>();

    }
}