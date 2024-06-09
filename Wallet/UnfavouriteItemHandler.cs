using Toolkit.Foundation;

namespace Wallet;
public class UnfavouriteItemHandler(IDecoratorService<Item<(Guid, string)>> decoratorService,
    IMediator mediator) :
    INotificationHandler<UnfavouriteEventArgs<Item>>
{
    public async Task Handle(UnfavouriteEventArgs<Item> args)
    {
        try
        {
            if (decoratorService.Service is Item<(Guid, string)> item)
            {
                (Guid id, string name) = item.Value;
                await mediator.Handle<UpdateEventArgs<(Guid, int)>, bool>(new UpdateEventArgs<(Guid, int)>((id, 0)));
            }
        }
        catch
        {
        }
    }
}