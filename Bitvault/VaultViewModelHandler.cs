using Toolkit.Foundation;

namespace Bitvault;

public class VaultViewModelHandler(IServiceFactory factory,
    IPublisher publisher) :
    INotificationHandler<Enumerate<LockerNavigationViewModel, VaultViewModelOptions>>
{
    public async Task Handle(Enumerate<LockerNavigationViewModel, VaultViewModelOptions> args,
        CancellationToken cancellationToken = default)
    {
        if (args.Options?.Filter is "All")
        {
            for (int i = 0; i < 100; i++)
            {
                if (factory.Create<LockerNavigationViewModel>() is LockerNavigationViewModel viewModel)
                {
                    await publisher.Publish(new Create<LockerNavigationViewModel>(viewModel),
                        nameof(VaultViewModel), cancellationToken);
                }
            }
        }

        if (args.Options?.Filter is "Starred")
        {
            for (int i = 0; i < 10; i++)
            {
                if (factory.Create<LockerNavigationViewModel>() is LockerNavigationViewModel viewModel)
                {
                    await publisher.Publish(new Create<LockerNavigationViewModel>(viewModel),
                        nameof(VaultViewModel), cancellationToken);
                }
            }
        }

        if (args.Options?.Filter is "Archive")
        {
            for (int i = 0; i < 100000; i++)
            {
                if (factory.Create<LockerNavigationViewModel>() is LockerNavigationViewModel viewModel)
                {
                    await publisher.Publish(new Create<LockerNavigationViewModel>(viewModel),
                        nameof(VaultViewModel), cancellationToken);
                }
            }
        }
    }
}