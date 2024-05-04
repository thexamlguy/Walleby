using Toolkit.Foundation;

namespace Bitvault;

public class VaultViewModelHandler(IServiceFactory factory,
    IPublisher publisher) :
    INotificationHandler<Enumerate<LockerNavigationViewModel>>
{
    public async Task Handle(Enumerate<LockerNavigationViewModel> args,
        CancellationToken cancellationToken = default)
    {
        Random rnd = new Random();
        int d = rnd.Next(5, 10);
        for (int i = 0; i < 2;  i++)
        {
            if (factory.Create<LockerNavigationViewModel>() is LockerNavigationViewModel viewModel)
            {
                await publisher.Publish(new Create<LockerNavigationViewModel>(viewModel),
                    nameof(VaultViewModel), cancellationToken);
            }
        }
    }
}