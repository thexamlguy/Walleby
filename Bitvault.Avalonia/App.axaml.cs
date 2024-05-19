﻿using Avalonia;
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
using System.Collections.Generic;

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

                services.AddHandler<ContainerActivatedHandler>();

                services.AddTransient<IContainerComponent> (provider => Component.Create<ContainerComponent>(provider, args =>
                {
                    args.AddServices(services =>
                    {
                        services.AddTransient<IComparer<Item>>(provider => Comparer<Item>.Create((x, z) => 
                            x.Name!.CompareTo(z.Name) == 0 ? 1 : x.Name!.CompareTo(z.Name)));

                        services.AddCache<Item>();

                        services.AddTransient<IKeyGenerator, KeyGenerator>();
                        services.AddTransient<IEncryptor, AesEncryptor>();
                        services.AddTransient<IDecryptor, AesDecryptor>();

                        services.AddTransient<IPasswordHasher, PasswordHasher>();
                        services.AddTransient<IKeyDeriver, KeyDeriver>();

                        services.AddTransient<ISecurityKeyFactory, SecurityKeyFactory>();
                        services.AddTransient<IContainerStorageFactory, ContainerStorageFactory>();
                        services.TryAddSingleton<IValueStore<SecurityKey>, ValueStore<SecurityKey>>();
                        services.TryAddSingleton<IValueStore<ContainerConnection>, ValueStore<ContainerConnection>>();

                        services.AddDbContextFactory<ContainerDbContext>((provider, args) =>
                        {
                            if (provider.GetRequiredService<IValueStore<ContainerConnection>>() 
                                is IValueStore<ContainerConnection> connection)
                            {
                                args.UseSqlite($"{connection.Value}");
                            }
                        }); 

                        services.AddHandler<QueryContainerHandler>();
                        services.AddHandler<CreateItemHander>();

                        services.AddHandler<OpenContainerHandler>();

                        services.AddTemplate<ContainerNavigationViewModel, ContainerNavigationView>();
                        services.AddTemplate<AllNavigationViewModel, AllNavigationView>();
                        services.AddTemplate<StarredNavigationViewModel, StarredNavigationView>();
                        services.AddTemplate<CategoriesNavigationViewModel, CategoriesNavigationView>();
                        services.AddTemplate<ArchiveNavigationViewModel, ArchiveNavigationView>();

                        services.AddTemplate<OpenContainerViewModel, OpenView>("OpenContainer");

                        services.AddScoped<ContainerViewModelConfiguration>();

                        services.AddTemplate<ContainerViewModel, ContainerView>("Container");
                        services.AddHandler<AggerateContainerViewModelHandler>();

                        services.AddTemplate<SearchContainerActionViewModel, SearchContainerActionView>();
                        services.AddTemplate<ContainerHeaderViewModel, ContainerHeaderView>("ContainerHeader");
                        services.AddTemplate<CreateItemActionViewModel, CreateItemActionView>();

                        services.AddTemplate<ItemNavigationViewModel, ItemNavigationView>();
                        services.AddTemplate<ItemViewModel, ItemView>("Item");
                        services.AddHandler<AggerateItemViewModelHandler>();

                        services.AddTemplate<ItemCommandHeaderViewModel, ItemCommandHeaderView>("ItemCommandHeader");

                        services.AddTemplate<FavouriteItemActionViewModel, FavouriteItemActionView>();
                        services.AddTemplate<ConfirmItemActionViewModel, ConfirmItemActionView>();
                        services.AddTemplate<DismissItemActionViewModel, DismissItemActionView>();
                        services.AddTemplate<ArchiveItemActionViewModel, ArchiveItemActionView>();
                        services.AddTemplate<UnarchiveItemActionViewModel, UnarchiveItemActionView>();
                        services.AddTemplate<EditItemActionViewModel, EditItemActionView>();
                        services.AddTemplate<DeleteItemActionViewModel, DeleteItemActionView>();

                        services.AddTemplate<ItemHeaderViewModel, ItemHeaderView>();

                        services.AddScoped<IValueStore<Item>, ValueStore<Item>>();

                        services.AddHandler<ConfirmItemHandler>(ServiceLifetime.Scoped);
                        services.AddHandler<ArchiveItemHandler>(ServiceLifetime.Scoped);
                        services.AddHandler<UnarchiveItemHandler>(ServiceLifetime.Scoped);
                        services.AddHandler<FavouriteItemHandler>(ServiceLifetime.Scoped);
                        services.AddHandler<UnfavouriteItemHandler>(ServiceLifetime.Scoped);

                        services.AddHandler<ItemActivatedHandler>(ServiceLifetime.Singleton);
                    });
                })!);

                services.AddTransient<IContainerFactory,  ContainerFactory>();
                services.AddHandler<CreateContainerHandler>();

                services.AddSingleton<IContainerHostCollection, ContainerHostCollection>();
                services.AddInitializer<ContainerInitializer>();

                services.AddTemplate<MainViewModel, MainView>("Main");
                services.AddHandler<AggerateMainViewModelHandler>();

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