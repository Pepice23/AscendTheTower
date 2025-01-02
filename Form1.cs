using AscendTheTower.Helper;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;

namespace AscendTheTower;


public partial class Form1 : Form
{
    private readonly AutoUpdater _autoUpdater = new AutoUpdater(
        "https://raw.githubusercontent.com/ascend-the-tower/ascend-the-tower/main/update.json",
        "1.0.0");
    public Form1()
    {
        InitializeComponent();

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
