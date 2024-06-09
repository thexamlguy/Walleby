using Toolkit.Foundation;

namespace Wallet;

public class ConfirmCreateItemHandler(IMediator mediator,
    IDecoratorService<ItemConfiguration> itemConfigurationDecorator,
    IDecoratorService<ItemHeaderConfiguration> itemHeaderConfiguration,
    IPublisher publisher) :
    INotificationHandler<ConfirmEventArgs<Item>>
{
    public async Task Handle(ConfirmEventArgs<Item> args)
    {
        if (itemHeaderConfiguration.Service is ItemHeaderConfiguration headerConfiguration &&
            itemConfigurationDecorator.Service is ItemConfiguration itemConfiguration)
        {
            string? name = headerConfiguration?.Name;
            if (name is not null)
            {
                Guid id = Guid.NewGuid();
                publisher.Publish(Created.As(new Item<(Guid, string)>((id, name))));

                await mediator.Handle<CreateEventArgs<(Guid, string, string,
                    ItemConfiguration)>, bool>(new CreateEventArgs<(Guid, string, string, 
                    ItemConfiguration)>((id, name, "", itemConfiguration)));
            }
        }
    }
}
