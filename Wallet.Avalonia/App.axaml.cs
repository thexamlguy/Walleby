using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FluentAvalonia.Core;
using HotAvalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using Toolkit.Avalonia;
using Toolkit.Foundation;
using Wallet.Data;

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
                    services.AddTemplate<MainWindowViewModel, 
                        MainWindow>("MainWindow");
                }

                services.AddHandler<WalletActivatedHandler>();
                services.AddTransient(provider => Component.Create<WalletComponent>(provider, args =>
                {
                    args.AddServices(services =>
                    {
                        services.AddSingleton<IUserInteraction, UserInteraction>();

                        services.AddTransient<IComparer<Item<(Guid, string)>>>(provider => Comparer<Item<(Guid Id, string Name)>>.Create((x, z) =>
                            StringComparer.CurrentCultureIgnoreCase.Compare(x.Value.Name, z.Value.Name)));

                        services.AddCache<Item<(Guid, string)>>();

                        services.AddTransient(_ =>
                            provider.GetServices<IConfigurationDescriptor<ItemConfiguration>>());

                        services.AddInitializer<WalletInactivity>();

                        services.AddTransient<IWalletFactory, WalletFactory>();

                        services.AddTransient<IKeyGenerator, KeyGenerator>();
                        services.AddTransient<IEncryptor, AesEncryptor>();
                        services.AddTransient<IDecryptor, AesDecryptor>();

                        services.AddTransient<IPasswordHasher, PasswordHasher>();
                        services.AddTransient<IKeyDeriver, KeyDeriver>();

                        services.AddTransient<ISecurityKeyFactory, SecurityKeyFactory>();
                        services.AddTransient<IWalletDatabaseFactory, WalletDatabaseFactory>();
                        services.AddTransient<IWalletConnectionFactory, WalletConnectionFactory>();

                        services.AddTransient<IInitialization, WalletProfileImageInitializer>();

                        services.AddTransient<IItemConfigurationCollection, ItemConfigurationCollection>(provider =>
                        {
                            IEnumerable<IConfigurationDescriptor<ItemConfiguration>> items =
                                provider.GetServices<IConfigurationDescriptor<ItemConfiguration>>().OrderBy(x => x.Name) ??
                                Enumerable.Empty<IConfigurationDescriptor<ItemConfiguration>>();

                            return new ItemConfigurationCollection(items.ToDictionary(x => x.Name,
                                x => (Func<ItemConfiguration>)(() => x.Value)));
                        });

                        services.TryAddSingleton<IDecoratorService<ProfileImage<IImageDescriptor>>,
                            DecoratorService<ProfileImage<IImageDescriptor>>>();

                        services.TryAddSingleton<IDecoratorService<SecurityKey>, DecoratorService<SecurityKey>>();
                        services.TryAddSingleton<IDecoratorService<WalletConnection>, DecoratorService<WalletConnection>>();

                        services.AddTransient<IConnection>(provider =>
                            provider.GetRequiredService<IDecoratorService<WalletConnection>>().Value!);

                        services.AddDbContextFactory<WalletContext>();

                        services.AddHandler<CreateFileAttachmentHandler>();
                        services.AddHandler<CreateProfileImageHandler>();

                        services.AddHandler<QueryWalletHandler>();

                        services.AddHandler<ItemHandler>();
                        services.AddHandler<ItemImageHandler>();

                        services.AddHandler<CreateItemHandler>();
                        services.AddHandler<DeleteItemHandler>();
                        services.AddHandler<UpdateItemHander>();
                        services.AddHandler<UpdateItemStateHandler>();
                        services.AddHandler<CountCategoriesHandler>();

                        services.AddHandler<OpenWalletHandler>();
                        services.AddHandler<CloseWalletHandler>();

                        services.AddTemplate<WalletNavigationViewModel, WalletNavigationView>();

                        services.AddTemplate<CreateItemNavigationViewModel, CreateItemNavigationView>();
                        services.AddTemplate<AllNavigationViewModel, AllNavigationView>();
                        services.AddTemplate<FavouritesNavigationViewModel, FavouritesNavigationView>();
                        services.AddTemplate<CategoriesNavigationViewModel, CategoriesNavigationView>();
                        services.AddTemplate<CategoryNavigationViewModel, CategoryNavigationView>();
                        services.AddTemplate<ArchiveNavigationViewModel, ArchiveNavigationView>();

                        services.AddTemplate<OpenWalletViewModel, OpenWalletView>("OpenWallet");

                        services.AddScoped<ItemNavigationCollectionConfiguration>();

                        services.AddTemplate<WalletViewModel, WalletView>("Wallet");
                        services.AddTemplate<ItemNavigationCollectionViewModel, ItemNavigationCollectionView>("ItemCollection");

                        services.AddHandler<ItemNavigationCollectionViewModelActivatedHandler>();

                        services.AddTemplate<WalletHeaderViewModel, WalletHeaderView>("WalletHeader");
                        services.AddTemplate<BackActionViewModel, BackActionView>();
                        services.AddTemplate<SearchWalletActionViewModel, SearchWalletActionView>();

                        services.AddTemplate<ItemCategoryNavigationCollectionViewModel,
                            ItemCategoryNavigationCollectionView>("ItemCategoryCollection");
                        services.AddTemplate<ItemCategoryNavigationViewModel, ItemCategoryNavigationView>();

                        services.AddHandler<CategoriesNavigationViewModelActivationHandler>();

                        services.AddHandler<ItemCategoryViewModelActivatedHandler>();

                        services.AddScoped<IDecoratorService<Item<(Guid, string)>>, 
                            DecoratorService<Item<(Guid, string)>>>();

                        services.AddTemplate<AddItemNavigationViewModel, AddItemNavigationView>();

                        services.AddTemplate<ItemNavigationViewModel, ItemNavigationView>();
                        services.AddHandler<ItemNavigationViewModelActivatedHandler>();

                        services.AddTemplate<EmptyItemCollectionViewModel, 
                            EmptyItemCollectionView>("EmptyItemCollection");

                        services.AddScoped<IDecoratorService<ItemHeaderConfiguration>, 
                            DecoratorService<ItemHeaderConfiguration>>();
                        services.AddScoped<IDecoratorService<ItemConfiguration>,
                            DecoratorService<ItemConfiguration>>();

                        services.AddTemplate<ItemViewModel, ItemView>("Item");

                        services.AddTemplate<ItemHeaderViewModel, ItemHeaderView>();
                        services.AddTemplate<ItemContentViewModel, ItemContentView>();

                        services.AddHandler<ItemContentViewModelActivationHandler>();
                        services.AddHandler<ItemContentFromCategoryViewModelActivationHandler>();

                        services.AddTemplate<ItemSectionViewModel, ItemSectionView>();

                        services.AddTemplate<CreateCommentEntryViewModel, CreateCommentEntryView>();
                        services.AddTemplate<CommentEntryViewModel, CommentEntryView>();
                        services.AddTemplate<CommentEntryCollectionViewModel, CommentEntryCollectionView>();

                        services.AddTemplate<AttachmentEntryCollectionViewModel, AttachmentEntryCollectionView>();

                        services.AddTemplate<TextEntryViewModel, TextEntryView>();
                        services.AddTemplate<PasswordEntryViewModel, PasswordEntryView>();
                        services.AddTemplate<MaskedTextEntryViewModel, MaskedTextEntryView>();
                        services.AddTemplate<DropdownEntryCollectionViewModel, DropdownEntryCollectionView>();
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

                        services.AddHandler<AttachmentEntryCollectionViewModelHandler>(nameof(AttachmentEntryCollectionConfiguration));
                        services.AddHandler<TextEntryViewModelHandler>(nameof(TextEntryConfiguration));
                        services.AddHandler<CommentEntryCollectionViewModelHandler>(nameof(CommentEntryCollectionConfiguration));
                        services.AddHandler<PasswordEntryViewModelHandler>(nameof(PasswordEntryConfiguration));
                        services.AddHandler<MaskedTextEntryViewModelHandler>(nameof(MaskedTextEntryConfiguration));
                        services.AddHandler<DropdownEntryCollectionViewModelHandler>(nameof(DropdownEntryCollectionConfiguration));
                        services.AddHandler<DateEntryViewModelHandler>(nameof(DateEntryConfiguration));
                        services.AddHandler<HyperlinkEntryViewModelHandler>(nameof(HyperlinkEntryConfiguration));
                        services.AddHandler<PinEntryViewModelHandler>(nameof(PinEntryConfiguration));

                        services.AddHandler<ItemCreatedHandler>(ServiceLifetime.Singleton);
                        services.AddHandler<ItemModifiedHandler>(ServiceLifetime.Singleton);
                    });
                })!);

                services.AddTransient<IWalletHostFactory, WalletHostFactory>();

                services.AddSingleton<IWalletHostCollection, WalletHostCollection>();
                services.AddInitializer<WalletCollectionInitializer>();

                services.AddHandler<CreateWalletHandler>();
                services.AddHandler<CreateProfileImageHandler>();

                services.AddTemplate<MainViewModel, MainView>("Main");

                services.AddTemplate<WalletNavigationCollectionViewModel, WalletNavigationCollectionView>("Wallets");
                services.AddHandler<WalletNavigationCollectionViewModelActivationHandler>();

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