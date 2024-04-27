using Toolkit.Foundation;

namespace Bitvault;

public class VaultNavigationViewModelHandler(IPublisher publisher,
    IServiceFactory factory,
    IEnumerable<IConfigurationDescriptor<VaultConfiguration>> descriptors) :
    INotificationHandler<Enumerate<IMainNavigationViewModel>>
{
    public async Task Handle(Enumerate<IMainNavigationViewModel> args,
        CancellationToken cancellationToken = default)
    {
        foreach (IConfigurationDescriptor<VaultConfiguration> descriptor in descriptors)
        {
            if (factory.Create<VaultNavigationViewModel>(descriptor.Value.Name) is VaultNavigationViewModel viewModel)
            {
                await publisher.Publish(new Create<IMainNavigationViewModel>(viewModel),
                    nameof(MainViewModel), cancellationToken);
            }
        }
    }
}