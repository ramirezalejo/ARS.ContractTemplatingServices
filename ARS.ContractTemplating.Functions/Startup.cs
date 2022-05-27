using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using ARS.ContractTemplating.Domain.Interfaces.Infrastructure;
using ARS.ContractTemplating.Domain.Interfaces.Services;
using ARS.ContractTemplating.Infrastructure.Blobs;
using ARS.ContractTemplating.Infrastructure.CognitiveServices;
using ARS.ContractTemplating.Infrastructure.Queues;
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
[ExcludeFromCodeCoverage]
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
        builder.Services.AddSingleton<IDocumentAnalysisClient>
        (new DocumentAnalysisClient(configuration.GetValue<string>("FormRecognizerSubscriptionKey"),
            configuration.GetValue<string>("FormRecognizerEndpoint")));
        /*builder.Services.AddSingleton<IQueueClient>(x =>
            new QueueClient(configuration.GetValue<string>("StorageConnectionString"), "ContractRequest"));*/
        builder.Services.AddSingleton<IBlobClient>(x =>
            new BlobClient(configuration.GetValue<string>("StorageConnectionString")));
        builder.Services.AddTransient<IContractsService, ContractsService>();
        
    }
}