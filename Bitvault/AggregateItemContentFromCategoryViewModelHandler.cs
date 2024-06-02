using Toolkit.Foundation;

namespace Bitvault;

public class AggregateItemContentFromCategoryViewModelHandler(IItemConfigurationCollection configurations,
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
                    foreach (ItemSectionConfiguration section in configuration.Sections)
                    {
                        foreach (ItemEntryConfiguration entryConfiguration in section.Entries)
                        {
                            var dod = await mediator.Handle<ItemEntryConfiguration, IItemEntryViewModel?>(entryConfiguration,
                                entryConfiguration.GetType().Name);
                        }
                    }
                }
            }    
        }
    }
}
