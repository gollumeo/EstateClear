using ExecutorBoard.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace ExecutorBoard.SystemTests.TestHost;

/// <summary>
/// Boots the API with a system-bootstrap connection string override for tests.
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Runeforge>
{
    private readonly string _systemConnectionString;

    public CustomWebApplicationFactory(string systemConnectionString)
    {
        _systemConnectionString = systemConnectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("SystemTests");
        builder.ConfigureAppConfiguration((_, configurationBuilder) =>
        {
            configurationBuilder.AddJsonFile("appsettings.SystemTests.json", optional: true);
            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SystemBootstrap:ConnectionString"] = _systemConnectionString
            });
        });
    }
}
