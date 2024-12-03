using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AscendTheTower.Services;
using AscendTheTower.Configuration;
using Microsoft.Extensions.Configuration;

namespace AscendTheTower;

public static class Startup
{
    public static IServiceProvider? Services { get; private set; }

    public static void Init()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<GameConfig>(context.Configuration.GetSection("GameConfig"));
                services.AddWindowsFormsBlazorWebView();
                services.AddSingleton<ArmorService>();
                services.AddSingleton<BattleService>();
                services.AddSingleton<EnemyService>();
                services.AddSingleton<PlayerService>();
                services.AddSingleton<UpgradeService>();
                services.AddSingleton<WeaponService>();

#if DEBUG
                services.AddBlazorWebViewDeveloperTools();
#endif
            })
            .Build();
        Services = host.Services;
    }
}
