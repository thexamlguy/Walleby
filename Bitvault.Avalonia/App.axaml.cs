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
            .AddConfiguration<VaultConfiguration>(args => args.Name = "Personal",
                   "Vault:*")
            .ConfigureServices((context, services) =>
            {
                services.AddAvalonia();
                services.AddHandler<AppHandler>();

                services.AddTransient<IVaultComponent, VaultComponent>();
                services.AddTransient<IVaultFactory, VaultFactory>();
                services.AddInitializer<VaultsInitializer>();

                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
                {
                    services.AddTemplate<MainWindowViewModel, MainWindow>("MainWindow");
                }

                services.AddSingleton<IVaultHostCollection, VaultHostCollection>();
                services.AddSingleton<IVaultFactory, VaultFactory>();
                services.AddHandler<VaultHandler>();

                services.AddTemplate<MainViewModel, MainView>("Main");

                services.AddTemplate<VaultNavigationViewModel, VaultNavigationView>();
                services.AddHandler<VaultNavigationViewModelHandler>();

                services.AddTransient<FooterViewModel>();

                services.AddTemplate<ManageNavigationViewModel, ManageNavigationView>();
                services.AddTemplate<ManageViewModel, ManageView>("Manage");

                services.AddTemplate<CreateVaultNavigationViewModel, CreateVaultNavigationView>();
                services.AddTemplate<CreateVaultViewModel, CreateVaultView>("CreateVault");
            })
        .Build();

        await host.RunAsync();
    }
}