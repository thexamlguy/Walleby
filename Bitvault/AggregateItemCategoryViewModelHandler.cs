using Toolkit.Foundation;

namespace Bitvault;

public class AggregateItemCategoryViewModelHandler(IEnumerable<IConfigurationDescriptor<ItemConfiguration>> descriptors,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<AggerateEventArgs<ItemCategoryNavigationViewModel>>
{
    public Task Handle(AggerateEventArgs<ItemCategoryNavigationViewModel> args)
    {
        bool selected = true;
        foreach (IConfigurationDescriptor<ItemConfiguration> descriptor in descriptors)
        {
            if (serviceFactory.Create<ItemCategoryNavigationViewModel>(descriptor.Name, selected)
                is ItemCategoryNavigationViewModel viewModel)
            {
                publisher.Publish(Create.As(viewModel), nameof(ItemCategoryCollectionViewModel));
                selected = false;
            }
        }

        return Task.CompletedTask;
    }
}
