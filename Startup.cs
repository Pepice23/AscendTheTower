using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AscendTheTower.Services;

namespace AscendTheTower;

public static class Startup
{
    public static IServiceProvider? Services { get; private set; }

    public static void Init()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(WireupServices)
            .Build();
        Services = host.Services;
    }

    private static void WireupServices(IServiceCollection services)
    {
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
    }
}
