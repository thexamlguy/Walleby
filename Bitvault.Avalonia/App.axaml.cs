using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolkit.Avalonia;
using Toolkit.Foundation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using HotAvalonia;
using Bitvault.Data;

namespace Bitvault.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        this.EnableHotReload();
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        IHost? host = DefaultHostBuilder.Create()
            .AddConfiguration<ContainerConfiguration>(args => args.Name = "Personal",
                   "Vault:*")
            .ConfigureServices((context, services) =>
            {
                services.AddAvalonia();
                services.AddHandler<AppHandler>();

                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
                {
                    services.AddTemplate<MainWindowViewModel, MainWindow>("MainWindow");
                }

                services.AddTransient<IContainerComponent> (provider => Component.Create<ContainerComponent>(provider, args =>
                {
                    args.AddServices(services =>
                    {
                        services.AddTransient<IKeyGenerator, KeyGenerator>();
                        services.AddTransient<IEncryptor, AesEncryptor>();
                        services.AddTransient<IDecryptor, AesDecryptor>();

                        services.AddTransient<IPasswordHasher, PasswordHasher>();
                        services.AddTransient<IKeyDeriver, KeyDeriver>();

                        services.AddTransient<ISecurityKeyFactory, SecurityKeyFactory>();
                        services.AddTransient<IContainer, ContainerFactory>();
                        services.TryAddSingleton<IContainer<SecurityKey>, Container<SecurityKey>>();
                        services.TryAddSingleton<IContainer<ContainerConnection>, Container<ContainerConnection>>();

                        services.AddDbContextFactory<ContainerDbContext>((provider, args) =>
                        {
                            if (provider.GetRequiredService<IContainer<ContainerConnection>>() 
                                is IContainer<ContainerConnection> connection)
                            {
                                args.UseSqlite($"{connection.Value}");
                            }
                        });

                        services.AddHandler<OpenContainerHandler>();

                        services.AddTemplate<ContainerNavigationViewModel, ContainerNavigationView>();
                        services.AddTemplate<AllNavigationViewModel, AllNavigationView>();
                        services.AddTemplate<StarredNavigationViewModel, StarredNavigationView>();
                        services.AddTemplate<CategoriesNavigationViewModel, CategoriesNavigationView>();
                        services.AddTemplate<ArchiveNavigationViewModel, ArchiveNavigationView>();

                        services.AddTemplate<OpenContainerViewModel, OpenView>("OpenContainer");

                        services.AddTemplate<ContainerViewModel, ContainerView>("Container");
                        services.AddHandler<ContainerViewModelHandler>();

                        services.AddTemplate<SearchHeaderViewModel, SearchHeaderView>("SearchHeader");
                        services.AddTemplate<ContainerHeaderViewModel, ContainerHeaderView>("ContainerHeader");
                        services.AddTemplate<AddItemActionViewModel, AddItemActionView>();

                        services.AddTemplate<ItemNavigationViewModel, ItemNavigationView>();
                        services.AddTemplate<ItemViewModel, ItemView>("Item");

                        services.AddTemplate<AddItemViewModel, AddItemView>("AddItem");
                        services.AddTemplate<AddItemCommandHeaderViewModel, AddItemCommandHeaderView>("AddVaultContentCommandHeader");

                        services.AddTemplate<ConfirmItemActionViewModel, ConfirmItemActionView>();
                        services.AddTemplate<DismissItemActionViewModel, DismissItemActionView>();

                        services.AddTemplate<ItemHeaderViewModel, ItemHeaderView>();

                        services.AddHandler<ItemConfigurationHandler>();
                    });
                })!);

                services.AddTransient<IContainerComponentFactory,  ContainerComponentFactory>();
                services.AddHandler<CreateContainerHandler>();

                services.AddSingleton<IContainerHostCollection, ContainerHostCollection>();
                services.AddInitializer<ContainerInitializer>();

                services.AddTemplate<MainViewModel, MainView>("Main");
                services.AddHandler<MainViewModelHandler>();

                services.AddTransient<FooterViewModel>();

                services.AddTemplate<ManageNavigationViewModel, ManageNavigationView>();
                services.AddTemplate<ManageViewModel, ManageView>("Manage");

                services.AddTemplate<CreateContainerNavigationViewModel, CreateContainerNavigationView>();
                services.AddTemplate<CreateContainerViewModel, CreateContainerView>("CreateContainer");
            })
        .Build();

        await host.RunAsync();
    }
}