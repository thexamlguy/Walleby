using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Wallet.Data;
using HotAvalonia;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using Toolkit.Avalonia;
using Toolkit.Foundation;
using FluentAvalonia.Core;

namespace Wallet.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        this.EnableHotReload();
        FAUISettings.SetAnimationsEnabledAtAppLevel(false);

        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        IHost? host = DefaultHostBuilder.Create()
            .AddConfiguration<WalletConfiguration>("Wallet:*")
            .AddConfiguration<ItemConfiguration>("Item:*")
            .AddConfiguration("Item:Bank Account", ItemConfiguration.BankAccount)
            .AddConfiguration("Item:Credit Card", ItemConfiguration.CreditCard)
            .AddConfiguration("Item:Document", ItemConfiguration.Document)
            .AddConfiguration("Item:Driving Licence", ItemConfiguration.DrivingLicence)
            .AddConfiguration("Item:Identity", ItemConfiguration.Identity)
            .AddConfiguration("Item:Login", ItemConfiguration.Login)
            .AddConfiguration("Item:Note", ItemConfiguration.Note)
            .AddConfiguration("Item:Password", ItemConfiguration.Password)
            .AddConfiguration("Item:Passport", ItemConfiguration.Passport)
            .AddConfiguration("Item:API Credentials", ItemConfiguration.ApiCredentials)
            .AddConfiguration("Item:Software License", ItemConfiguration.SoftwareLicense)
            .AddConfiguration("Item:Crypto", ItemConfiguration.CryptoWallet)
            .AddConfiguration("Item:Database", ItemConfiguration.Database)
            .AddConfiguration("Item:Membership", ItemConfiguration.Membership)
            .AddConfiguration("Item:Insurance Documents", ItemConfiguration.InsuranceDocuments)
            .AddConfiguration("Item:Utility", ItemConfiguration.Utility)
            .AddConfiguration("Item:Server", ItemConfiguration.Server)
            .AddConfiguration("Item:Education Record", ItemConfiguration.EducationRecord)
            .AddConfiguration("Item:Travel Documents", ItemConfiguration.TravelDocuments)
            .AddConfiguration("Item:Concert Ticket", ItemConfiguration.ConcertTicket)            
            .ConfigureServices((context, services) =>
            {
                services.AddAvalonia();
                services.AddHandler<AppHandler>();

                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
                {
                    services.AddTemplate<MainWindowViewModel, MainWindow>("MainWindow");
                }

                services.AddHandler<WalletActivatedHandler>();
                services.AddTransient<IWalletComponent>(provider => Component.Register<WalletComponent>(provider, args =>
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
                        services.AddTransient<IWalletStorageFactory, WalletStorageFactory>();

                        services.AddTransient<IItemConfigurationCollection, ItemConfigurationCollection>(provider =>
                        {
                            IEnumerable<IConfigurationDescriptor<ItemConfiguration>> items =
                                provider.GetServices<IConfigurationDescriptor<ItemConfiguration>>().OrderBy(x => x.Name) ??
                                Enumerable.Empty<IConfigurationDescriptor<ItemConfiguration>>();

                            return new ItemConfigurationCollection(items.ToDictionary(x => x.Name, x => (Func<ItemConfiguration>)(() => x.Value)));
                        });

                        services.TryAddSingleton<IDecoratorService<SecurityKey>, DecoratorService<SecurityKey>>();
                        services.TryAddSingleton<IDecoratorService<WalletConnection>, DecoratorService<WalletConnection>>();

                        services.AddDbContextFactory<WalletContext>((provider, args) =>
                        {
                            if (provider.GetRequiredService<IDecoratorService<WalletConnection>>()
                                is IDecoratorService<WalletConnection> connection)
                            {
                                args.UseSqlite($"{connection.Service}");
                            }
                        });

                        services.AddHandler<QueryWalletHandler>();
                        services.AddHandler<RequestItemHandler>();
                        services.AddHandler<CreateItemHandler>();
                        services.AddHandler<DeleteItemHandler>();
                        services.AddHandler<UpdateItemHander>();
                        services.AddHandler<UpdateItemStateHandler>();
                        services.AddHandler<CountCategoriesHandler>();

                        services.AddHandler<OpenWalletHandler>();

                        services.AddTemplate<WalletNavigationViewModel, WalletNavigationView>();

                        services.AddTemplate<CreateItemNavigationViewModel, CreateItemNavigationView>();
                        services.AddTemplate<AllNavigationViewModel, AllNavigationView>();
                        services.AddTemplate<FavouritesNavigationViewModel, FavouritesNavigationView>();
                        services.AddTemplate<CategoriesNavigationViewModel, CategoriesNavigationView>();
                        services.AddTemplate<CategoryNavigationViewModel, CategoryNavigationView>();
                        services.AddTemplate<ArchiveNavigationViewModel, ArchiveNavigationView>();

                        services.AddTemplate<OpenWalletViewModel, OpenWalletView>("OpenWallet");

                        services.AddScoped<ItemCollectionConfiguration>();

                        services.AddTemplate<WalletViewModel, WalletView>("Wallet");
                        services.AddTemplate<ItemCollectionViewModel, ItemCollectionView>("ItemCollection");

                        services.AddHandler<SynchronizeItemCollectionViewModelHandler>();

                        services.AddTemplate<WalletHeaderViewModel, WalletHeaderView>("WalletHeader");
                        services.AddTemplate<BackActionViewModel, BackActionView>();
                        services.AddTemplate<SearchWalletActionViewModel, SearchWalletActionView>();

                        services.AddTemplate<ItemCategoryCollectionViewModel, ItemCategoryCollectionView>("ItemCategoryCollection");
                        services.AddTemplate<ItemCategoryNavigationViewModel, ItemCategoryNavigationView>();

                        services.AddHandler<SynchronizeCategoriesNavigationViewModelHandler>();

                        services.AddHandler<SynchronizeItemCategoryViewModelHandler>();

                        services.AddScoped<IDecoratorService<Item<(Guid, string)>>, DecoratorService<Item<(Guid, string)>>>();

                        services.AddTemplate<AddItemNavigationViewModel, AddItemNavigationView>();

                        services.AddTemplate<ItemNavigationViewModel, ItemNavigationView>();
                        services.AddTemplate<EmptyItemCollectionViewModel, EmptyItemCollectionView>("EmptyItemCollection");

                        services.AddScoped<IDecoratorService<ItemHeaderConfiguration>, DecoratorService<ItemHeaderConfiguration>>();
                        services.AddScoped<IDecoratorService<ItemConfiguration>, DecoratorService<ItemConfiguration>>();

                        services.AddTemplate<ItemViewModel, ItemView>("Item");
                        services.AddHandler<CreateItemViewModelHandler>("Item");

                        services.AddTemplate<ItemHeaderViewModel, ItemHeaderView>();
                        services.AddTemplate<ItemContentViewModel, ItemContentView>();

                        services.AddHandler<SynchronizeItemContentViewModelHandler>();
                        services.AddHandler<SynchronizeItemContentFromCategoryViewModelHandler>();

                        services.AddTemplate<ItemSectionViewModel, ItemSectionView>();

                        services.AddTemplate<TextEntryViewModel, TextEntryView>();
                        services.AddTemplate<MultilineTextEntryViewModel, MultilineTextEntryView>();
                        services.AddTemplate<PasswordEntryViewModel, PasswordEntryView>();
                        services.AddTemplate<MaskedTextEntryViewModel, MaskedTextEntryView>();
                        services.AddTemplate<DropdownEntryViewModel, DropdownEntryView>();
                        services.AddTemplate<DateEntryViewModel, DateEntryView>();
                        services.AddTemplate<HyperlinkEntryViewModel, HyperlinkEntryView>();
                        services.AddTemplate<PinEntryViewModel, PinEntryView>();

                        services.AddTemplate<ItemCommandHeaderViewModel, ItemCommandHeaderView>("ItemCommandHeader");

                        services.AddTemplate<FavouriteItemActionViewModel, FavouriteItemActionView>();
                        services.AddTemplate<ConfirmItemActionViewModel, ConfirmItemActionView>();
                        services.AddTemplate<DismissItemActionViewModel, DismissItemActionView>();
                        services.AddTemplate<ArchiveItemActionViewModel, ArchiveItemActionView>();
                        services.AddTemplate<UnarchiveItemActionViewModel, UnarchiveItemActionView>();
                        services.AddTemplate<EditItemActionViewModel, EditItemActionView>();
                        services.AddTemplate<DeleteItemActionViewModel, DeleteItemActionView>();

                        services.AddHandler<ConfirmUpdateItemHandler>(nameof(ItemState.Write));
                        services.AddHandler<ConfirmCreateItemHandler>(nameof(ItemState.New));
                        services.AddHandler<ConfirmDeleteItemHandler>();
    
                        services.AddHandler<HyperlinkHandler>();

                        services.AddHandler<ItemChangedHandler>(ServiceLifetime.Singleton);

                        services.AddHandler<ArchiveItemHandler>();
                        services.AddHandler<UnarchiveItemHandler>();
                        services.AddHandler<FavouriteItemHandler>();
                        services.AddHandler<UnfavouriteItemHandler>();

                        services.AddHandler<TextEntryViewModelHandler>(nameof(TextEntryConfiguration));
                        services.AddHandler<MultilineTextEntryViewModelHandler>(nameof(MultilineTextEntryConfiguration));
                        services.AddHandler<PasswordEntryViewModelHandler>(nameof(PasswordEntryConfiguration));
                        services.AddHandler<MaskedTextEntryViewModelHandler>(nameof(MaskedTextEntryConfiguration));
                        services.AddHandler<DropdownEntryViewModelHandler>(nameof(DropdownEntryConfiguration));
                        services.AddHandler<DateEntryViewModelHandler>(nameof(DateEntryConfiguration));
                        services.AddHandler<HyperlinkEntryViewModelHandler>(nameof(HyperlinkEntryConfiguration));
                        services.AddHandler<PinEntryViewModelHandler>(nameof(PinEntryConfiguration));

                        services.AddHandler<ItemCreatedHandler>(ServiceLifetime.Singleton);
                        services.AddHandler<ItemModifiedHandler>(ServiceLifetime.Singleton);
                    });
                })!);

                services.AddTransient<IWalletFactory, WalletFactory>();
                services.AddHandler<CreateWalletHandler>();

                services.AddSingleton<IWalletHostCollection, WalletHostCollection>();
                services.AddInitializer<WalletInitializer>();

                services.AddTemplate<MainViewModel, MainView>("Main");
                services.AddHandler<SynchronizeMainViewModelHandler>();

                services.AddTransient<FooterViewModel>();

                services.AddTemplate<ManageNavigationViewModel, ManageNavigationView>();
                services.AddTemplate<ManageViewModel, ManageView>("Manage");

                services.AddTemplate<CreateWalletNavigationViewModel, CreateWalletNavigationView>();
                services.AddTemplate<CreateWalletViewModel, CreateWalletView>("CreateWallet");
            })
        .Build();

        await host.RunAsync();
    }
}