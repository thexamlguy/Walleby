using System.Reflection;
using Toolkit.Foundation;

namespace Wallet;

public class SynchronizeItemContentFromCategoryViewModelHandler(IItemConfigurationCollection configurations,
    IDecoratorService<ItemConfiguration> itemConfigurationDecorator,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<SynchronizeEventArgs<IItemEntryViewModel, string>>
{
    public async Task Handle(SynchronizeEventArgs<IItemEntryViewModel, string> args)
    {
        if (args.Value is string category)
        {
            if (configurations.TryGetValue(category, out Func<ItemConfiguration>? configurationFactory))
            {
                if (configurationFactory.Invoke() is ItemConfiguration configuration)
                {
                    itemConfigurationDecorator.Set(configuration);
                    foreach (ItemSectionConfiguration configurationSection in configuration.Sections)
                    {
                        string id = $"{nameof(ItemSection)}:{Guid.NewGuid()}";
                        if (serviceFactory.Create<ItemSectionViewModel>(id)
                            is ItemSectionViewModel sectionViewModel)
                        {
                            publisher.Publish(Create.As(sectionViewModel), nameof(ItemContentViewModel));
                            foreach (IItemEntryConfiguration entryConfiguration in configurationSection.Entries)
                            {
                                Type messageType = typeof(CreateEventArgs<>).MakeGenericType(entryConfiguration.GetType());
                                ConstructorInfo? constructor = messageType.GetConstructor([entryConfiguration.GetType(), typeof(object[])]);

                                if (constructor?.Invoke(new object[] { entryConfiguration, new object[] { ItemState.New } }) is object message)
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
