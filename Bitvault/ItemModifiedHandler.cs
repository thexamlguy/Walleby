using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class ItemModifiedHandler(IServiceProvider serviceProvider,
    IPublisher publisher) :
    INotificationHandler<ModifiedEventArgs<Item<(Guid, string)>>>
{
    public Task Handle(ModifiedEventArgs<Item<(Guid, string)>> args)
    {
        Item<(Guid, string)> oldItem = args.OldView;
        Item<(Guid, string)> newItem = args.NewValue;

        ICache<Item<(Guid, string)>> cache = serviceProvider.GetRequiredService<ICache<Item<(Guid, string)>>>();
        if (cache.TryGetValue(oldItem, out Item<(Guid, string)>? cachedItem))
        {
            if (cachedItem is not null)
            {
                IServiceScope serviceScope = serviceProvider.CreateScope();
                IValueStore<Item<(Guid, string)>> valueStore = serviceScope.ServiceProvider.GetRequiredService<IValueStore<Item<(Guid, string)>>>();

                int oldIndex = cache.IndexOf(cachedItem);
                cache.Remove(cachedItem);

                cache.Add(newItem);

                int newIndex = cache.IndexOf(newItem);
                valueStore.Set(newItem);

                publisher.Publish(MoveTo.As<ItemNavigationViewModel>(oldIndex, newIndex),
                    nameof(ItemCollectionViewModel));
            }
        }

        return Task.CompletedTask;
    }
}