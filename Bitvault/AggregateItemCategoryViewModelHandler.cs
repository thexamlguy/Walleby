using Toolkit.Foundation;

namespace Bitvault;

public class AggregateItemCategoryViewModelHandler(IItemConfigurationCollection configurations,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<AggerateEventArgs<ItemCategoryNavigationViewModel>>
{
    public Task Handle(AggerateEventArgs<ItemCategoryNavigationViewModel> args)
    {
        bool selected = true;
        foreach (KeyValuePair<string, Func<ItemConfiguration>> configuration in configurations)
        {
            if (serviceFactory.Create<ItemCategoryNavigationViewModel>(configuration.Key, selected)
                is ItemCategoryNavigationViewModel viewModel)
            {
                publisher.Publish(Create.As(viewModel), nameof(ItemCategoryCollectionViewModel));
                selected = false;
            }
        }

        return Task.CompletedTask;
    }
}
