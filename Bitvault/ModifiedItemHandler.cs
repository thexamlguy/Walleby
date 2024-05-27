using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class ModifiedItemHandler(IServiceProvider serviceProvider,
    IPublisher publisher) :
    INotificationHandler<ModifiedEventArgs<Item>>
{
    public Task Handle(ModifiedEventArgs<Item> args)
    {
        Item oldItem = args.OldView;
        Item newItem = args.NewValue;

        ICache<Item> cache = serviceProvider.GetRequiredService<ICache<Item>>();
        if (cache.TryGetValue(oldItem, out Item? cachedItem))
        {
            if (cachedItem is not null)
            {
                IServiceScope serviceScope = serviceProvider.CreateScope();
                IValueStore<Item> valueStore = serviceScope.ServiceProvider.GetRequiredService<IValueStore<Item>>();

                int oldIndex = cache.IndexOf(cachedItem);
                cache.Remove(cachedItem);

                cache.Add(newItem);

                int newIndex = cache.IndexOf(newItem);
                valueStore.Set(newItem);

                publisher.Publish(MoveTo.As<ItemNavigationViewModel>(oldIndex, newIndex), nameof(LockerViewModel));
            }
        }

        return Task.CompletedTask;
    }
}