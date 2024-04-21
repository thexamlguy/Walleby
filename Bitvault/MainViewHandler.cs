using Toolkit.Foundation;

namespace Bitvault;

public class MainViewHandler(IPublisher publisher,
    IServiceFactory factory) :
    INotificationHandler<Enumerate<IMainNavigationViewModel>>
{
    public async Task Handle(Enumerate<IMainNavigationViewModel> args,
        CancellationToken cancellationToken = default)
    {

    }
}