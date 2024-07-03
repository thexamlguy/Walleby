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
                IImageDescriptor? imageDescriptor = headerConfiguration.ImageDescriptor;
                Guid id = Guid.NewGuid();

                await mediator.Handle<CreateEventArgs<(Guid, string, string, IImageDescriptor?,
                    ItemConfiguration)>, bool>(new CreateEventArgs<(Guid, string, string, IImageDescriptor?, 
                    ItemConfiguration)>((id, name, category, imageDescriptor ?? default, itemConfiguration)));

                Item<(Guid, string, string)> item = new((id, name, category));

                publisher.Publish(Created.As(item));
                publisher.Publish(Changed.As<Item>());
            }
        }
    }
}
