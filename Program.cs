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
        MainAsync().GetAwaiter().GetResult();
    }

    private static async Task MainAsync()
    {
        VelopackApp.Build().Run();
        Startup.Init();
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
        await CheckForUpdates();
    }
    
    private static async Task CheckForUpdates()
    {
        await Helper.AutoUpdater.UpdateMyApp();
    }

}