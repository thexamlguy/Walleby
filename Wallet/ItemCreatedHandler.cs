﻿using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Wallet;

public class ItemCreatedHandler(IServiceProvider serviceProvider,
    ICache<Item<(Guid, string)>> cache,
    IPublisher publisher) :
    INotificationHandler<CreatedEventArgs<Item<(Guid, string)>>>
{
    public Task Handle(CreatedEventArgs<Item<(Guid, string)>> args)
    {
        if (args.Sender is Item<(Guid, string)> item)
        {
            (Guid id, string name) = item.Value;

            IServiceScope serviceScope = serviceProvider.CreateScope();
            IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();
            IDecoratorService<Item<(Guid, string)>> decoratorService = serviceScope.ServiceProvider.GetRequiredService<IDecoratorService<Item<(Guid, string)>>>();

            if (serviceFactory.Create<ItemNavigationViewModel>(args => args.Initialize(),
                id, name, "Description", true)
                is ItemNavigationViewModel viewModel)
            {
                cache.Add(item);

                int index = cache.IndexOf(item);
                decoratorService.Set(item);

                publisher.Publish(Insert.As(index, viewModel), 
                    nameof(ItemCollectionViewModel));
            }
        }

        return Task.CompletedTask;
    }
}