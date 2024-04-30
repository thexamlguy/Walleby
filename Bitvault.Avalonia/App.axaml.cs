using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
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
        //var connectionString = new SqliteConnectionStringBuilder(@"Filename=C:\\Users\\dan_c\\source\\repos\\Bitvault\\Bitvault.Avalonia.Desktop\\bin\\Debug\\net8.0\\SQssLite.sql")
        //{
        //    Mode = SqliteOpenMode.ReadWriteCreate,
        //    Password = "Test123"
        //}.ToString();

        //var connection = new SqliteConnection(connectionString);
        //connection.Open();

        //var command = connection.CreateCommand();
        //command.CommandText = "SELECT quote($newPassword);";
        //command.Parameters.AddWithValue("$newPassword", "Test123");
        //var quotedNewPassword = (string)command.ExecuteScalar();

        //command.CommandText = "PRAGMA rekey = " + quotedNewPassword;
        //command.Parameters.Clear();
        //command.ExecuteNonQuery();


        IHost? host = DefaultHostBuilder.Create()
            .AddConfiguration<VaultConfiguration>(args => args.Name = "Personal",
                   "Vault:*")
            .ConfigureServices((context, services) =>
            {
                services.AddAvalonia();
                services.AddHandler<AppHandler>();

                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
                {
                    services.AddTemplate<MainWindowViewModel, MainWindow>("MainWindow");
                }

                services.AddTransient<IVaultComponent> (provider => Component.Create<VaultComponent>(provider, args =>
                {
                    args.AddServices(services =>
                    {
                        services.AddTransient<IKeyGenerator, KeyGenerator>();
                        services.AddTransient<IEncryptor, AesEncryptor>();
                        services.AddTransient<IDecryptor, AesDecryptor>();

                        services.AddTransient<IPasswordHasher, PasswordHasher>();
                        services.AddTransient<IKeyDeriver, KeyDeriver>();

                        services.AddTransient<IVaultFactory, VaultFactory>();
                        services.AddTransient<IVaultKeyGenerator, VaultKeyGenerator>();
                        services.AddTransient<IVaultStorage, VaultStorage>();

                        services.AddDbContextFactory<VaultDbContext>(args =>
                        {
                            args.UseSqlite();
                        });

                        services.AddDbContextFactory<VaultDbContext>();

                        services.AddHandler<OpenVaultHandler>();

                        services.AddTemplate<VaultNavigationViewModel, VaultNavigationView>();
                        services.AddTemplate<AllNavigationViewModel, AllNavigationView>();
                        services.AddTemplate<StarredNavigationViewModel, StarredNavigationView>();
                        services.AddTemplate<CategoriesNavigationViewModel, CategoriesNavigationView>();
                        services.AddTemplate<ArchiveNavigationViewModel, ArchiveNavigationView>();

                        services.AddTemplate<VaultViewModel, VaultView>("Vault");
                        services.AddTemplate<LockViewModel, LockView>("Lock");
                    });
                })!);

                services.AddTransient<IVaultComponentFactory,  VaultComponentFactory>();
                services.AddHandler<CreateVaultHandler>();

                services.AddSingleton<IVaultHostCollection, VaultHostCollection>();
                services.AddInitializer<VaultCollectionInitializer>();

                services.AddTemplate<MainViewModel, MainView>("Main");
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