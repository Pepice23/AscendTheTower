namespace AscendTheTower.Helper;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.Json;

public class AutoUpdater(string updateUrl, string currentVersion)
{
    private readonly string _applicationPath = Application.ExecutablePath;
    
    public class UpdateInfo
    {
        public string? Version { get; init; }
        public string? DownloadUrl { get; init; }
        public string? ReleaseNotes { get; init; }
    }

    public async Task CheckForUpdates()
    {
        try
        {
            using (var client = new HttpClient())
            {
                // Get update information from server
                var response = await client.GetStringAsync(updateUrl);
                var updateInfo = JsonSerializer.Deserialize<UpdateInfo>(response);

                if (IsNewVersionAvailable(updateInfo?.Version))
                {
                    var result = MessageBox.Show(
                        $"A new version ({updateInfo?.Version}) is available. Would you like to update?\n\nRelease Notes:\n{updateInfo?.ReleaseNotes}",
                        "Update Available",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        await DownloadAndInstallUpdate(updateInfo?.DownloadUrl);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error checking for updates: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private bool IsNewVersionAvailable(string? newVersion)
    {
        Version current = Version.Parse(currentVersion);
        if (newVersion != null)
        {
            Version latest = Version.Parse(newVersion);
            return latest > current;
        }

        return false;
    }

    private async Task DownloadAndInstallUpdate(string? downloadUrl)
    {
        try
        {
            // Create temp directory for update
            string tempPath = Path.Combine(Path.GetTempPath(), "AppUpdate");
            Directory.CreateDirectory(tempPath);

            // Download new version
            using (var client = new HttpClient())
            {
                var updateFile = Path.Combine(tempPath, "update.zip");
                var response = await client.GetByteArrayAsync(downloadUrl);
                await File.WriteAllBytesAsync(updateFile, response);

                // Create updater batch script
                string updaterScript = CreateUpdaterScript(updateFile, _applicationPath);
                string batchPath = Path.Combine(tempPath, "update.bat");
                await File.WriteAllTextAsync(batchPath, updaterScript);

                // Start updater script and close application
                Process.Start(new ProcessStartInfo
                {
                    FileName = batchPath,
                    UseShellExecute = true,
                    CreateNoWindow = true
                });

                Application.Exit();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error downloading update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private string CreateUpdaterScript(string updateFile, string applicationPath)
    {
        return @$"
@echo off
timeout /t 2 /nobreak > nul
taskkill /f /im ""{Path.GetFileName(applicationPath)}""
timeout /t 2 /nobreak > nul
powershell -command ""Expand-Archive -Force '{updateFile}' '{Path.GetDirectoryName(applicationPath)}'""
start """" ""{applicationPath}""
del ""%~f0""
";
    }
}