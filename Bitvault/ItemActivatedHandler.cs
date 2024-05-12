using FluentAvalonia.Core;
using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class ItemActivatedHandler(IServiceProvider serviceProvider,
    ICache<Item> cache,
    IPublisher publisher) :
    INotificationHandler<ActivatedEventArgs<Item>>
{
    public async Task Handle(ActivatedEventArgs<Item> args,
        CancellationToken cancellationToken = default)
    {
        if (args.Value is Item item)
        {
            IServiceScope serviceScope = serviceProvider.CreateScope();
            IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();

            cache.Add(item);
            int index = cache.IndexOf(item);

            if (serviceFactory.Create<ItemNavigationViewModel>(item.Id, item.Name, "Description " + 1) is ItemNavigationViewModel viewModel)
            {
                await publisher.Publish(Insert.As(index, viewModel), nameof(ContainerViewModel), cancellationToken);
            }
        }
    }
}