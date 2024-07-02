using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class FavouriteItemActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    bool value = false) : Observable<bool>(provider, factory, mediator, publisher, subscriber, disposer, value),
    IRemovable
{
    [RelayCommand]
    private void Invoke()
    {
        if (!Value)
        {
            Value = true;
            Publisher.Publish(Favourite.As<Item>());
        }
        else
        {
            Value = false;
            Publisher.Publish(Unfavourite.As<Item>());
        }
    }
}