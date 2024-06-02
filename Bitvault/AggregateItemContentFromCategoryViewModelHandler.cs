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
                    int index = 0;

                    foreach (ItemSectionConfiguration section in configuration.Sections)
                    {
                        if (serviceFactory.Create<ItemSectionViewModel>($"{nameof(ItemSection)}{index}") is ItemSectionViewModel sectionViewModel)
                        {
                            publisher.Publish(Create.As(sectionViewModel), nameof(ItemContentViewModel));
                            foreach (ItemEntryConfiguration entryConfiguration in section.Entries)
                            {
                                if (await mediator.Handle<ItemEntryConfiguration, IItemEntryViewModel?>(entryConfiguration,
                                    entryConfiguration.GetType().Name) is IItemEntryViewModel entryViewModel)
                                {
                                    publisher.Publish(Create.As(entryViewModel), $"{nameof(ItemSection)}{index}");
                                }
                            }
                        }
                    }
                }
            }    
        }
    }
}
