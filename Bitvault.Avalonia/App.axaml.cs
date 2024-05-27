using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Bitvault.Data;
using HotAvalonia;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Toolkit.Avalonia;
using Toolkit.Foundation;

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
            .AddConfiguration<LockerConfiguration>("Locker:*")
            .AddConfiguration<ItemConfiguration>("Item:*")
            //.AddConfiguration<ItemConfiguration>(args => args.Name = "Bank Account", "Item:Bank Account")
            .AddConfiguration("Item:Credit Card", ItemConfiguration.CreditCard)
            //.AddConfiguration<ItemConfiguration>(args => args.Name = "Document", "Item:Document")
            //.AddConfiguration<ItemConfiguration>(args => args.Name = "Driving Licence", "Item:Driving Licence")
            //.AddConfiguration<ItemConfiguration>(args => args.Name = "Identity", "Item:Identity")
            //.AddConfiguration<ItemConfiguration>(args => args.Name = "Login", "Item:Login")
            //.AddConfiguration<ItemConfiguration>(args => args.Name = "Note", "Item:Note")
            //.AddConfiguration<ItemConfiguration>(args => args.Name = "Password", "Item:Password")
            .ConfigureServices((context, services) =>
            {
                services.AddAvalonia();
                services.AddHandler<AppHandler>();

                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
                {
                    services.AddTemplate<MainWindowViewModel, MainWindow>("MainWindow");
                }

                services.AddScoped<IProxyService<IEnumerable<ItemConfiguration>>>(provider =>
                    new ProxyService<IEnumerable<ItemConfiguration>>(provider.GetRequiredService<IEnumerable<ItemConfiguration>>()));

                services.AddHandler<LockerActivatedHandler>();

                services.AddTransient<ILockerComponent>(provider => Component.Create<LockerComponent>(provider, args =>
                {
                    args.AddServices(services =>
                    {
                        services.AddTransient<IComparer<Item>>(provider => Comparer<Item>.Create((x, z) =>
                            StringComparer.CurrentCultureIgnoreCase.Compare(x.Name, z.Name)));

                        services.AddCache<Item>();

                        services.AddTransient(_ =>
                            provider.GetRequiredService<IProxyService<IEnumerable<ItemConfiguration>>>());

                        services.AddTransient<IKeyGenerator, KeyGenerator>();
                        services.AddTransient<IEncryptor, AesEncryptor>();
                        services.AddTransient<IDecryptor, AesDecryptor>();

                        services.AddTransient<IPasswordHasher, PasswordHasher>();
                        services.AddTransient<IKeyDeriver, KeyDeriver>();

                        services.AddTransient<ISecurityKeyFactory, SecurityKeyFactory>();
                        services.AddTransient<ILockerStorageFactory, LockerStorageFactory>();
                        services.TryAddSingleton<IValueStore<SecurityKey>, ValueStore<SecurityKey>>();
                        services.TryAddSingleton<IValueStore<LockerConnection>, ValueStore<LockerConnection>>();

                        services.AddDbContextFactory<LockerContext>((provider, args) =>
                        {
                            if (provider.GetRequiredService<IValueStore<LockerConnection>>()
                                is IValueStore<LockerConnection> connection)
                            {
                                args.UseSqlite($"{connection.Value}");
                            }
                        });

                        services.AddHandler<QueryLockerHandler>();
                        services.AddHandler<CreateItemHandler>();
                        services.AddHandler<UpdateItemHander>();
                        services.AddHandler<UpdateItemStateHandler>();

                        services.AddHandler<OpenLockerHandler>();

                        services.AddTemplate<LockerNavigationViewModel, LockerNavigationView>();
                        services.AddTemplate<AllNavigationViewModel, AllNavigationView>();
                        services.AddTemplate<StarredNavigationViewModel, StarredNavigationView>();
                        services.AddTemplate<CategoriesNavigationViewModel, CategoriesNavigationView>();
                        services.AddTemplate<ArchiveNavigationViewModel, ArchiveNavigationView>();

                        services.AddTemplate<OpenLockerViewModel, OpenView>("OpenLocker");

                        services.AddScoped<LockerViewModelConfiguration>();

                        services.AddTemplate<LockerViewModel, LockerView>("Locker");
                        services.AddTemplate<ItemCollectionViewModel, ItemCollectionView>("ContentItemCollection");
                        services.AddHandler<AggerateLockerItemViewModelHandler>();

                        services.AddTemplate<SearchLockerActionViewModel, SearchLockerActionView>();
                        services.AddTemplate<LockerHeaderViewModel, LockerHeaderView>("LockerHeader");

                        services.AddTemplate<CreateItemActionViewModel, CreateItemActionView>();
                        services.AddTemplate<ItemCategoryCollectionViewModel, ItemCategoryCollectionView>("LockerItemCategoryCollection");
                        services.AddTemplate<ItemCategoryNavigationViewModel, ItemCategoryNavigationView>();
                       
                        services.AddHandler<AggerateLockerCategoryViewModelHandler>();

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
                        services.AddTemplate<ItemContentViewModel, ItemContentView>();
                        services.AddTemplate<AddItemNavigationViewModel, AddItemNavigationView>();

                        services.AddScoped<IValueStore<Item>, ValueStore<Item>>();

                        services.AddHandler<ConfirmItemHandler>();
                        services.AddHandler<ArchiveItemHandler>();
                        services.AddHandler<UnarchiveItemHandler>();
                        services.AddHandler<FavouriteItemHandler>();
                        services.AddHandler<UnfavouriteItemHandler>();

                        services.AddHandler<CreatedItemHandler>(ServiceLifetime.Singleton);
                        services.AddHandler<ModifiedItemHandler>(ServiceLifetime.Singleton);
                    });
                })!);

                services.AddTransient<ILockerFactory, LockerFactory>();
                services.AddHandler<CreateLockerHandler>();

                services.AddSingleton<ILockerHostCollection, LockerHostCollection>();
                services.AddInitializer<LockerInitializer>();

                services.AddTemplate<MainViewModel, MainView>("Main");
                services.AddHandler<AggerateMainViewModelHandler>();

                services.AddTransient<FooterViewModel>();

                services.AddTemplate<ManageNavigationViewModel, ManageNavigationView>();
                services.AddTemplate<ManageViewModel, ManageView>("Manage");

                services.AddTemplate<CreateLockerNavigationViewModel, CreateLockerNavigationView>();
                services.AddTemplate<CreateLockerViewModel, CreateLockerView>("CreateLocker");
            })
        .Build();

        await host.RunAsync();
    }
}