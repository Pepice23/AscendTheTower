﻿using System;
using AscendTheTower.Services;
using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;

namespace AscendTheTower
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

            appBuilder.Services
                .AddLogging();

            // register custom services
            appBuilder.Services
                .AddSingleton<PlayerService>();
            appBuilder.Services
                .AddSingleton<EnemyService>();
            appBuilder.Services.AddSingleton<BattleService>();
            appBuilder.Services.AddSingleton<WeaponService>();

            // register root component and selector
            appBuilder.RootComponents.Add<App>("app");

            var app = appBuilder.Build();

            // customize window
            app.MainWindow
                .SetIconFile("favicon.ico")
                .SetTitle("Ascend the Tower");

            AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
            {
                app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
            };

            app.Run();
        }
    }
}
