using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        
    }
}