using Toolkit.Foundation;

namespace Bitvault.Avalonia;

public class VaultComponent :
    IVaultComponent
{
    public IComponentBuilder Create() =>
        ComponentBuilder.Create()
            .AddServices(services =>
            {
                services.AddTemplate<VaultNavigationViewModel, VaultNavigationView>();
                services.AddTemplate<AllNavigationViewModel, AllNavigationView>();
                services.AddTemplate<StarredNavigationViewModel, StarredNavigationView>();
                services.AddTemplate<CategoriesNavigationViewModel, CategoriesNavigationView>();
                services.AddTemplate<ArchiveNavigationViewModel, ArchiveNavigationView>();

                services.AddTemplate<VaultViewModel, VaultView>("Vault");
            });
}
