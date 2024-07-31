using Microsoft.AspNetCore.Components.WebView.WindowsForms;

namespace AscendTheTower;

using AutoUpdaterDotNET;

public partial class Form1 : Form
{
    public Form1()
    {
        AutoUpdater.Start("https://github.com/Pepice23/AscendTheTower/blob/master/Updater.xml");
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
}
