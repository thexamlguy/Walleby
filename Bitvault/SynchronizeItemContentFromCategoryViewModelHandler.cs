using System.Reflection;
using Toolkit.Foundation;

namespace Bitvault;

public class SynchronizeItemContentFromCategoryViewModelHandler(IItemConfigurationCollection configurations,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<SynchronizeEventArgs<IItemEntryViewModel,
        (string, ISynchronizationCollection<ItemSectionViewModel>)>>
{
    public async Task Handle(SynchronizeEventArgs<IItemEntryViewModel, (string, ISynchronizationCollection<ItemSectionViewModel>)> args)
    {
        (string category, ISynchronizationCollection<ItemSectionViewModel> synchronization) = args.Value;
        if (configurations.TryGetValue(category, out Func<ItemConfiguration>? factory))
        {
            if (factory.Invoke() is ItemConfiguration configuration)
            {
                foreach (ItemSectionConfiguration configurationSection in configuration.Sections)
                {
                    string section = $"{nameof(ItemSection)}:{Guid.NewGuid()}";
                    if (serviceFactory.Create<ItemSectionViewModel>(synchronization, section)
                        is ItemSectionViewModel sectionViewModel)
                    {
                        publisher.Publish(Create.As(sectionViewModel), nameof(ItemContentViewModel));
                        foreach (IItemEntryConfiguration entryConfiguration in configurationSection.Entries)
                        {
                            Type messageType = typeof(CreateEventArgs<>).MakeGenericType(entryConfiguration.GetType());
                            ConstructorInfo? constructor = messageType.GetConstructor([entryConfiguration.GetType(), typeof(object[])]);

                            if (constructor?.Invoke(new object[] { entryConfiguration, new object[] { sectionViewModel } }) is object message)
                            {
                                if (await mediator.Handle<object, IItemEntryViewModel?>(message, entryConfiguration.GetType().Name) is IItemEntryViewModel entryViewModel)
                                {
                                    publisher.Publish(Create.As(entryViewModel), section);
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
