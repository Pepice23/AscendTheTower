using Velopack;

namespace AscendTheTower.Helper;

public class AutoUpdater
{
    public static async Task UpdateMyApp()
    {
        var mgr = new UpdateManager(@"A:\ATTUpdates");

        // check for new version
        var newVersion = await mgr.CheckForUpdatesAsync();
        if (newVersion == null)
            return; // no update available

        // download new version
        await mgr.DownloadUpdatesAsync(newVersion);

        // install new version and restart app
        mgr.ApplyUpdatesAndRestart(newVersion);
    }
}