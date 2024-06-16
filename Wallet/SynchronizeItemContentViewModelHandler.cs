﻿using System.Reflection;
using Toolkit.Foundation;

namespace Wallet;

public class SynchronizeItemContentViewModelHandler(IDecoratorService<Item<(Guid, string)>> itemDecorator,
    IDecoratorService<ItemConfiguration> itemConfigurationDecorator,
    IMediator mediator,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<SynchronizeEventArgs<ItemSectionViewModel>>
{
    public async Task Handle(SynchronizeEventArgs<ItemSectionViewModel> args)
    {
        if (itemDecorator.Service is Item<(Guid, string)> item)
        {
            if (item.Value is (Guid Id, _))
            {
                (_, _, _, _, ItemConfiguration? configuration) = await mediator.Handle<RequestEventArgs<Item<Guid>>, 
                    (Guid, string, string?, string, ItemConfiguration?)>(Request.As(new Item<Guid>(Id)));

                if (configuration is not null)
                {
                    itemConfigurationDecorator.Set(configuration);
                    foreach (ItemSectionConfiguration configurationSection in configuration.Sections)
                    {
                        string id = $"{nameof(ItemSection)}:{Guid.NewGuid()}";
                        if (serviceFactory.Create<ItemSectionViewModel>(args => args.OnInitialize(), id)
                            is ItemSectionViewModel sectionViewModel)
                        {
                            publisher.Publish(Create.As(sectionViewModel), nameof(ItemContentViewModel));
                            foreach (IItemEntryConfiguration entryConfiguration in configurationSection.Entries)
                            {
                                Type messageType = typeof(CreateEventArgs<>).MakeGenericType(entryConfiguration.GetType());
                                ConstructorInfo? constructor = messageType.GetConstructor([entryConfiguration.GetType(), typeof(object[])]);

                                if (constructor?.Invoke(new object[] { entryConfiguration, new object[] { ItemState.Read } }) is object message)
                                {
                                    if (await mediator.Handle<object, IItemEntryViewModel?>(message,
                                        entryConfiguration.GetType().Name) is IItemEntryViewModel entryViewModel)
                                    {
                                        publisher.Publish(Create.As(entryViewModel), id);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
