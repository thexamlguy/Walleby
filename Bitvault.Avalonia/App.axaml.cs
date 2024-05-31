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
            .AddConfiguration("Item:Bank Account", ItemConfiguration.BankAccount)
            .AddConfiguration("Item:Credit Card", ItemConfiguration.CreditCard)
            .AddConfiguration("Item:Document", ItemConfiguration.Document)
            .AddConfiguration("Item:Driving Licence", ItemConfiguration.DrivingLicence)
            .AddConfiguration("Item:Identity", ItemConfiguration.Identity)
            .AddConfiguration("Item:Login", ItemConfiguration.Login)
            .AddConfiguration("Item:Note", ItemConfiguration.Note)
            .AddConfiguration("Item:Password", ItemConfiguration.Password)
            .ConfigureServices((context, services) =>
            {
                services.AddAvalonia();
                services.AddHandler<AppHandler>();

                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
                {
                    services.AddTemplate<MainWindowViewModel, MainWindow>("MainWindow");
                }

                services.AddHandler<LockerActivatedHandler>();

                services.AddTransient<ILockerComponent>(provider => Component.Create<LockerComponent>(provider, args =>
                {
                    args.AddServices(services =>
                    {
                        services.AddTransient<IComparer<Item<(Guid, string)>>>(provider => Comparer<Item<(Guid, string)>>.Create((x, z) =>
                            StringComparer.CurrentCultureIgnoreCase.Compare(x.Value.Item2, z.Value.Item2)));

                        services.AddCache<Item<(Guid, string)>>();

                        services.AddTransient(_ =>
                            provider.GetServices<IConfigurationDescriptor<ItemConfiguration>>());

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

                        services.AddTemplate<OpenLockerViewModel, OpenLockerView>("OpenLocker");

                        services.AddScoped<LockerViewModelConfiguration>();

                        services.AddTemplate<LockerViewModel, LockerView>("Locker");
                        services.AddTemplate<ItemCollectionViewModel, ItemCollectionView>("ContentItemCollection");
                        services.AddHandler<AggerateItemViewModelHandler>();

                        services.AddTemplate<LockerHeaderViewModel, LockerHeaderView>("LockerHeader");
                        services.AddTemplate<BackActionViewModel, BackActionView>();
                        services.AddTemplate<CreateItemActionViewModel, CreateItemActionView>();
                        services.AddTemplate<SearchLockerActionViewModel, SearchLockerActionView>();

                        services.AddTemplate<ItemCategoryCollectionViewModel, ItemCategoryCollectionView>("ItemCategoryCollection");
                        services.AddTemplate<ItemCategoryNavigationViewModel, ItemCategoryNavigationView>();
                       
                        services.AddHandler<AggerateItemCategoryViewModelHandler>();

                        services.AddTemplate<ItemNavigationViewModel, ItemNavigationView>();
                        services.AddTemplate<ItemViewModel, ItemView>("Item");

                        services.AddHandler<AggerateItemContentViewModelHandler>();

                        services.AddTemplate<ItemCommandHeaderViewModel, ItemCommandHeaderView>("ItemCommandHeader");

                        services.AddTemplate<FavouriteItemActionViewModel, FavouriteItemActionView>();
                        services.AddTemplate<ConfirmItemActionViewModel, ConfirmItemActionView>();
                        services.AddTemplate<DismissItemActionViewModel, DismissItemActionView>();
                        services.AddTemplate<ArchiveItemActionViewModel, ArchiveItemActionView>();
                        services.AddTemplate<UnarchiveItemActionViewModel, UnarchiveItemActionView>();
                        services.AddTemplate<EditItemActionViewModel, EditItemActionView>();
                        services.AddTemplate<DeleteItemActionViewModel, DeleteItemActionView>();

                        services.AddTemplate<EmptyItemCollectionViewModel, EmptyItemCollectionView>("EmptyItemCollection");
                        services.AddTemplate<ItemHeaderViewModel, ItemHeaderView>();
                        services.AddTemplate<ItemContentViewModel, ItemContentView>();
                        services.AddTemplate<AddItemNavigationViewModel, AddItemNavigationView>();

                        services.AddScoped<IValueStore<Item<(Guid, string)>>, ValueStore<Item<(Guid, string)>>>();

                        services.AddHandler<ConfirmUpdateItemHandler>(nameof(Update));
                        services.AddHandler<ConfirmCreateItemHandler>(nameof(Create));

                        services.AddHandler<ArchiveItemHandler>();
                        services.AddHandler<UnarchiveItemHandler>();
                        services.AddHandler<FavouriteItemHandler>();
                        services.AddHandler<UnfavouriteItemHandler>();

                        services.AddHandler<ItemCreatedHandler>(ServiceLifetime.Singleton);
                        services.AddHandler<ItemModifiedHandler>(ServiceLifetime.Singleton);
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