using AscendTheTower.Helper;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;

namespace AscendTheTower;


public partial class Form1 : Form
{
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
}
