﻿using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class AggerateItemViewModelHandler(IMediator mediator,
    IServiceProvider serviceProvider,
    ICache<Item<(Guid, string)>> cache,
    IPublisher publisher,
    LockerViewModelConfiguration dd) :
    INotificationHandler<AggerateEventArgs<ItemNavigationViewModel, 
        LockerViewModelConfiguration>>
{
    public async Task Handle(AggerateEventArgs<ItemNavigationViewModel,
        LockerViewModelConfiguration> args)
    {
        if (args.Value is LockerViewModelConfiguration configuration)
        {
            cache.Clear();
            bool selected = true;

            if (await mediator.Handle<RequestEventArgs<QueryLockerConfiguration>,
                IReadOnlyCollection<(Guid Id, string Name, string Category, bool Favourite, bool Archived)>>(Request.As(new QueryLockerConfiguration
                {
                    Filter = configuration.Filter,
                    Query = configuration.Query
                })) is IReadOnlyCollection<(Guid Id, string Name, string Category, bool Favourite, bool Archived)> results)
            {
                foreach ((Guid Id, string Name, string Category, bool Favourite, bool Archived) in results)
                {
                    IServiceScope serviceScope = serviceProvider.CreateScope();
                    IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();
                    IValueStore<Item<(Guid, string)>> valueStore = serviceScope.ServiceProvider.GetRequiredService<IValueStore<Item<(Guid, string)>>>();

                    if (serviceFactory.Create<ItemNavigationViewModel>(Id, Name, "Description", Category, selected, Favourite, Archived) is ItemNavigationViewModel viewModel)
                    {
                        Item<(Guid, string)> item = new((Id, Name));

                        valueStore.Set(item);

                        cache.Add(item);
                        publisher.Publish(Create.As(viewModel), nameof(ItemCollectionViewModel));
                    }

                    selected = false;
                }
            }
        }
    }
}