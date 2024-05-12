using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class MainViewModelHandler(IPublisher publisher,
    IContainerHostCollection vaults) :
    INotificationHandler<Enumerate<IMainNavigationViewModel>>
{
    public async Task Handle(Enumerate<IMainNavigationViewModel> args,
        CancellationToken cancellationToken = default)
    {
        foreach (IComponentHost vault in vaults)
        {
            if (vault.Services.GetRequiredService<ContainerConfiguration>() is ContainerConfiguration configuration)
            {
                if (vault.Services.GetRequiredService<IServiceFactory>() is IServiceFactory factory)
                {
                    if (factory.Create<ContainerNavigationViewModel>(configuration.Name) is ContainerNavigationViewModel viewModel)
                    {
                        await publisher.Publish(new Create<IMainNavigationViewModel>(viewModel),
                            nameof(MainViewModel), cancellationToken);
                    }
                }
            }
        }
    }
}