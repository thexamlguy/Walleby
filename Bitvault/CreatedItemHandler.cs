using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class CreatedItemHandler(IServiceProvider serviceProvider,
    ICache<Item> cache,
    IPublisher publisher) :
    INotificationHandler<CreatedEventArgs<Item>>
{
    public Task Handle(CreatedEventArgs<Item> args)
    {
        if (args.Value is Item item)
        {
            IServiceScope serviceScope = serviceProvider.CreateScope();
            IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();
            IValueStore<Item> valueStore = serviceScope.ServiceProvider.GetRequiredService<IValueStore<Item>>();

            if (serviceFactory.Create<ItemNavigationViewModel>(item.Id, item.Name, "Description", true)
                is ItemNavigationViewModel viewModel)
            {
                cache.Add(item);

                int index = cache.IndexOf(item);
                valueStore.Set(item);

                publisher.Publish(Insert.As(index, viewModel), nameof(ContainerViewModel));
            }
        }

        return Task.CompletedTask;
    }
}