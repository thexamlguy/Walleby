using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class CreatedItemHandler(IServiceProvider serviceProvider,
    ICache<Item<(Guid, string)>> cache,
    IPublisher publisher) :
    INotificationHandler<CreatedEventArgs<Item<(Guid, string)>>>
{
    public Task Handle(CreatedEventArgs<Item<(Guid, string)>> args)
    {
        if (args.Value is Item<(Guid, string)> item)
        {
            (Guid id, string name) = item.Value;

            IServiceScope serviceScope = serviceProvider.CreateScope();
            IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();
            IValueStore<Item<(Guid, string)>> valueStore = serviceScope.ServiceProvider.GetRequiredService<IValueStore<Item<(Guid, string)>>>();

            if (serviceFactory.Create<ItemNavigationViewModel>(id, name, "Description", true)
                is ItemNavigationViewModel viewModel)
            {
                cache.Add(item);

                int index = cache.IndexOf(item);
                valueStore.Set(item);

                publisher.Publish(Insert.As(index, viewModel), 
                    nameof(ItemCollectionViewModel));
            }
        }

        return Task.CompletedTask;
    }
}