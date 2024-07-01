using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Wallet;

public class ItemCreatedHandler(IServiceProvider serviceProvider,
    ICache<Item<(Guid, string)>> cache,
    IPublisher publisher) :
    INotificationHandler<CreatedEventArgs<Item<(Guid, string, string, IImageDescriptor?)>>>
{
    public Task Handle(CreatedEventArgs<Item<(Guid, string, string, IImageDescriptor?)>> args)
    {
        if (args.Sender is Item<(Guid, string, string, IImageDescriptor?)> item)
        {
            (Guid id, string name, string category, IImageDescriptor? imageDescriptor) = item.Value;

            IServiceScope serviceScope = serviceProvider.CreateScope();
            IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();
            IDecoratorService<Item<(Guid, string)>> decoratorService = serviceScope.ServiceProvider.GetRequiredService<IDecoratorService<Item<(Guid, string)>>>();

            if (serviceFactory.Create<ItemNavigationViewModel>(args => args.Initialize(),
                id, name, "Description", category, imageDescriptor, true)
                is ItemNavigationViewModel viewModel)
            {
                Item<(Guid, string)> cachedItem = new((id, name));
                cache.Add(cachedItem);

                int index = cache.IndexOf(cachedItem);
                decoratorService.Set(cachedItem);

                publisher.Publish(Insert.As(index, viewModel), 
                    nameof(ItemCollectionViewModel));
            }
        }

        return Task.CompletedTask;
    }
}