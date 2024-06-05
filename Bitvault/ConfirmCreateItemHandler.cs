using Toolkit.Foundation;

namespace Bitvault;

public class ConfirmCreateItemHandler(IMediator mediator,
    IDecoratorService<ItemConfiguration> decoratorItemConfiguration,
    IPublisher publisher) :
    INotificationHandler<ConfirmEventArgs<Item>>
{
    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        if (decoratorItemConfiguration.Value is ItemConfiguration configuration)
        {
            string? name = await mediator.Handle<ConfirmEventArgs<ItemHeader>, string>(Confirm.As<ItemHeader>());
            if (name is not null)
            {
                Guid id = Guid.NewGuid();
                publisher.Publish(Created.As(new Item<(Guid, string)>((id, name))));

                await mediator.Handle<CreateEventArgs<(Guid, string, string,
                    ItemConfiguration)>, bool>(new CreateEventArgs<(Guid, string, string, ItemConfiguration)>((id, name, "", configuration)));
            }
        }
    }
}
