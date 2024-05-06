using Toolkit.Foundation;

namespace Bitvault;

public class VaultViewModelHandler(IServiceFactory factory,
    IPublisher publisher) :
    INotificationHandler<Enumerate<VaultContentNavigationViewModel, VaultViewModelOptions>>
{
    public async Task Handle(Enumerate<VaultContentNavigationViewModel, VaultViewModelOptions> args,
        CancellationToken cancellationToken = default)
    {
        if (args.Options?.Filter is "All")
        {
            for (int i = 0; i < 100; i++)
            {
                if (factory.Create<VaultContentNavigationViewModel>("Name " + i, "Description " + 1) is VaultContentNavigationViewModel viewModel)
                {
                    await publisher.Publish(new Create<VaultContentNavigationViewModel>(viewModel),
                        nameof(VaultViewModel), cancellationToken);
                }
            }
        }

        if (args.Options?.Filter is "Starred")
        {
            for (int i = 0; i < 10; i++)
            {
                if (factory.Create<VaultContentNavigationViewModel>("Name " + i, "Description " + 1) is VaultContentNavigationViewModel viewModel)
                {
                    await publisher.Publish(new Create<VaultContentNavigationViewModel>(viewModel),
                        nameof(VaultViewModel), cancellationToken);
                }
            }
        }

        if (args.Options?.Filter is "Archive")
        {
            for (int i = 0; i < 1000; i++)
            {
                if (factory.Create<VaultContentNavigationViewModel>("Name " + i, "Description " + 1) is VaultContentNavigationViewModel viewModel)
                {
                    await publisher.Publish(new Create<VaultContentNavigationViewModel>(viewModel),
                        nameof(VaultViewModel), cancellationToken);
                }
            }
        }
    }
}