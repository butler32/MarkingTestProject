using MarkingTestProject.Core.Interfaces;
using MarkingTestProject.Core.Services;
using MarkingTestProject.Infrastructure;
using MarkingTestProject.Infrastructure.Interfaces;
using MarkingTestProject.Infrastructure.Repositories;
using MarkingTestProject.Interfaces;
using MarkingTestProject.Models;
using MarkingTestProject.Services;
using MarkingTestProject.Utilities;
using MarkingTestProject.ViewModels;
using MarkingTestProject.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace MarkingTestProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IHost Host { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton(x =>
                    {
                        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "appsettings.json");
                        var config = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath))
                            ?? throw new Exception("cannot read appsettings.json file");
                        return config;
                    });

                    services.AddDbContext<TestProjDbContext>((provider, x) =>
                    {
                        var config = provider.GetRequiredService<Config>();
                        x.UseNpgsql(config.ConnectionString);
                    }, ServiceLifetime.Transient);

                    services.AddSingleton(x =>
                    {
                        return x.GetRequiredService<IAPIService>().GetData();
                    });

                    services.AddTransient<ICancellationTokenService, CancellationTokenService>();
                    services.AddSingleton<IAPIService, APIService>();
                    services.AddTransient<IDataBaseService, DataBaseService>();

                    services.AddTransient<MainWindow>();
                    services.AddTransient<CurrentTaskView>();
                    services.AddTransient<ProductView>();
                    services.AddTransient<BoxView>();
                    services.AddTransient<PalletView>();

                    services.AddTransient<NavigationViewModel>();
                    services.AddTransient<CurrentTaskViewModel>();
                    services.AddTransient<ProductViewModel>();
                    services.AddTransient<BoxViewModel>();
                    services.AddTransient<PalletViewModel>();

                    services.AddTransient<IRepositoryService, RepositoryService>();
                    services.AddTransient<IDTOConversionService, DTOConversionService>();

                    services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
                    services.AddTransient(typeof(IBottleRepository<>), typeof(BottleRepository<>));
                    services.AddTransient(typeof(IBoxRepository<>), typeof(BoxRepository<>));
                    services.AddTransient(typeof(IPalletRepository<>), typeof(PalletRepository<>));

                }).Build();

            var mainWindow = Host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
