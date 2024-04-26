using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class VaultNavigationViewModelHandler(IPublisher publisher,
    IVaultHostCollection vaults) :
    INotificationHandler<Enumerate<IMainNavigationViewModel>>
{
    public async Task Handle(Enumerate<IMainNavigationViewModel> args,
        CancellationToken cancellationToken = default)
    {
        foreach (IComponentHost vault in vaults)
        {
            if (vault.Services.GetRequiredService<VaultConfiguration>() is VaultConfiguration configuration)
            {
                if (vault.Services.GetRequiredService<IServiceFactory>() is IServiceFactory factory)
                {
                    if (factory.Create<VaultNavigationViewModel>(configuration.Name) is VaultNavigationViewModel viewModel)
                    {
                        await publisher.Publish(new Create<IMainNavigationViewModel>(viewModel),
                            nameof(MainViewModel), cancellationToken);
                    }
                }
            }
        }
    }
}
