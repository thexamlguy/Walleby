using Toolkit.Foundation;

namespace Bitvault;

public class VaultViewModelHandler(IServiceFactory factory,
    IPublisher publisher) :
    INotificationHandler<Enumerate<LockerNavigationViewModel>>
{
    public async Task Handle(Enumerate<LockerNavigationViewModel> args,
        CancellationToken cancellationToken = default)
    {
        for (int i = 0; i < 5000;  i++)
        {
            if (factory.Create<LockerNavigationViewModel>() is LockerNavigationViewModel viewModel)
            {
                await publisher.Publish(new Create<LockerNavigationViewModel>(viewModel),
                    nameof(VaultViewModel), cancellationToken);
            }
        }
    }
}