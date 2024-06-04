using Toolkit.Foundation;

namespace Bitvault;

public class AggregateItemContentFromCategoryViewModelHandler(IItemConfigurationCollection configurations,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IPublisher publisher) :
    INotificationHandler<AggerateEventArgs<IItemEntryViewModel, string>>
{
    public async Task Handle(AggerateEventArgs<IItemEntryViewModel, string> args)
    {
        if (args.Value is string category)
        {
            if (configurations.TryGetValue(category, out Func<ItemConfiguration>? factory))
            {
                if (factory.Invoke() is ItemConfiguration configuration)
                {
                    foreach (ItemSectionConfiguration configurationSection in configuration.Sections)
                    {
                        string section = $"{nameof(ItemSection)}:{Guid.NewGuid}";
                        if (serviceFactory.Create<ItemSectionViewModel>(section) 
                            is ItemSectionViewModel sectionViewModel)
                        {
                            publisher.Publish(Create.As(sectionViewModel), nameof(ItemContentViewModel));
                            foreach (ItemEntryConfiguration entryConfiguration in configurationSection.Entries)
                            {
                                if (await mediator.Handle<ItemEntryConfiguration, IItemEntryViewModel?>(entryConfiguration,
                                    entryConfiguration.GetType().Name) is IItemEntryViewModel entryViewModel)
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
