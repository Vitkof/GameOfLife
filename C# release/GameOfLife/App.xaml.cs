using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using GameOfLife.Implement;
using GameOfLife.ViewModels;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Init();
        }

        public IServiceProvider Container { get; private set; }

        void Init()
        {
            var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.UseMicrosoftDependencyResolver();
                    var reso = Locator.CurrentMutable;
                    reso.InitializeSplat();
                    reso.InitializeReactiveUI();
                    ConfigureServices(services);
                })
                .UseEnvironment(Environments.Development)
                .Build();

            Container = host.Services;
            Container.UseMicrosoftDependencyResolver();

        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IViewFor<MainViewModel>, MainWindow>();
            services.AddSingleton<IViewFor<GridViewModel>, MyGridView>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<Game>(sp => Game.Default);
            services.AddSingleton<Rules>(sp => Rules.Instance);
            
        }
    }
}
