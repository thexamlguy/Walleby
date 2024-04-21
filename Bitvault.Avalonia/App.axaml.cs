using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolkit.Avalonia;
using Toolkit.Foundation;

namespace Bitvault.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        IHost? host = DefaultBuilder.Create()
            .ConfigureServices((context, services) =>
            {
                services.AddAvalonia();
                services.AddHandler<AppHandler>();

                //services.AddTransient<IVaultComponent, VaultComponent>();
                //services.AddInitializer<VaultComponentsCollectionInitializer>();

                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
                {
                    services.AddTemplate<MainWindowViewModel, MainWindow>("MainWindow");
                }

                services.AddTemplate<MainViewModel, MainView>("Main");
                services.AddHandler<MainViewHandler>();

                services.AddTemplate<VaultNavigationViewModel, VaultNavigationView>();
                services.AddTemplate<AllNavigationViewModel, AllNavigationView>();
                services.AddTemplate<StarredNavigationViewModel, StarredNavigationView>();
                services.AddTemplate<CategoriesNavigationViewModel, CategoriesNavigationView>();
                services.AddTemplate<ArchiveNavigationViewModel, ArchiveNavigationView>();

                services.AddTemplate<VaultViewModel, VaultView>("Vault");

                services.AddConfiguration<VaultConfiguration>(args => args.Name = "foo1", $"{nameof(VaultConfiguration)}:Personal");
                services.AddConfiguration<VaultConfiguration>(args => args.Name = "foo2", $"{nameof(VaultConfiguration)}:Test");

            })
        .Build();

        await host.RunAsync();
    }
}
