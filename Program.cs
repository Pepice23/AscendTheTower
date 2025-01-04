using AscendTheTower.Helper;
using Velopack;

namespace AscendTheTower;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        VelopackApp.Build().Run();
        MainAsync().GetAwaiter().GetResult();
    }

    private static async Task MainAsync()
    {
        Startup.Init();
        await CheckForUpdates();
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
        
    }
    
    private static async Task CheckForUpdates()
    {
        await AutoUpdater.UpdateMyApp();
    }

}