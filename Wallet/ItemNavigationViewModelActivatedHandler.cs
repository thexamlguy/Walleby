using Toolkit.Foundation;

namespace Wallet;

public class ItemNavigationViewModelActivatedHandler(IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<ActivationEventArgs<ItemNavigationViewModel, Guid>>
{
    public async Task Handle(ActivationEventArgs<ItemNavigationViewModel, Guid> args)
    {
        Guid id = args.Value;
        IImageDescriptor? imageDescriptor = await mediator.Handle<RequestEventArgs<ItemImage<Guid>>,
            IImageDescriptor>(Request.As(new ItemImage<Guid>(id)));

        if (imageDescriptor is not null)
        {
            publisher.Publish(Notify.As(new Item<IImageDescriptor>(imageDescriptor)));
        }
    }
}