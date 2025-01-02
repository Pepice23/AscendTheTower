using AscendTheTower.Helper;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;

namespace AscendTheTower;


public partial class Form1 : Form
{
    private readonly AutoUpdater _autoUpdater = new AutoUpdater(
        "https://gist.githubusercontent.com/Pepice23/582a32e578ea048ec7dc9c9c510fb59a/raw/dddb534d5b30fa7f9a0dee41c2ae2a5de871d6fc/updater.json",
        "2.0.1");
    public Form1()
    {
        InitializeComponent();
        Text = "Ascend The Tower";

        var blazor = new BlazorWebView()
        {
            Dock = DockStyle.Fill,
            HostPage = "wwwroot/index.html",
            Services = Startup.Services!,
            StartPath = "/"
        };

        blazor.RootComponents.Add<Main>("#app");
        Controls.Add(blazor);
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await _autoUpdater.CheckForUpdates();
    }
}
