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
            if (headerConfiguration.Name is { Length: > 0 } name &&
                headerConfiguration.Category is { Length: > 0 } category)
            {
                Guid id = Guid.NewGuid();

                Item<(Guid, string)> item = new((id, name));
                publisher.Publish(Created.As(item));

                await mediator.Handle<CreateEventArgs<(Guid, string, string,
                    ItemConfiguration)>, bool>(new CreateEventArgs<(Guid, string, string, 
                    ItemConfiguration)>((id, name, category, itemConfiguration)));

                publisher.Publish(Changed.As(item));
            }
        }
    }
}
