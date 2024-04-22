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

                services.AddTransient<IVaultComponent, VaultComponent>();
                services.AddInitializer<VaultComponentsInitializer>();

                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
                {
                    services.AddTemplate<MainWindowViewModel, MainWindow>("MainWindow");
                }

                services.AddSingleton<IVaultHostCollection, VaultHostCollection>();

                services.AddTemplate<MainViewModel, MainView>("Main");
                services.AddHandler<MainViewModelHandler>();

                services.AddConfiguration<VaultConfiguration>(args => args.Name = "Personal", "Vault:Personal");
            })
        .Build();

        await host.RunAsync();
    }
}
