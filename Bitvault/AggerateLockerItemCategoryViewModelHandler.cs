using Toolkit.Foundation;

namespace Bitvault;

public class AggerateLockerItemCategoryViewModelHandler(IEnumerable<IConfigurationDescriptor<ItemConfiguration>> descriptors,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<AggerateEventArgs<ItemCategoryNavigationViewModel>>
{
    public Task Handle(AggerateEventArgs<ItemCategoryNavigationViewModel> args)
    {
        foreach (IConfigurationDescriptor<ItemConfiguration> descriptor in descriptors)
        {
            if (serviceFactory.Create<ItemCategoryNavigationViewModel>(descriptor.Name)
                is ItemCategoryNavigationViewModel viewModel)
            {
                publisher.Publish(Create.As(viewModel), nameof(ItemCategoryCollectionViewModel));
            }
        }

        return Task.CompletedTask;
    }
}
