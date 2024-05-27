using Toolkit.Foundation;

namespace Bitvault;

public class AggerateContainerCategoryViewModelHandler(IProxyService<IEnumerable<ItemConfiguration>> proxyConfigurations,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<AggerateEventArgs<ItemCategoryNavigationViewModel>>
{
    public Task Handle(AggerateEventArgs<ItemCategoryNavigationViewModel> args)
    {
        if (proxyConfigurations.Value is IEnumerable<ItemConfiguration> configurations)
        {
            foreach (ItemConfiguration configuration in configurations)
            {
                if (serviceFactory.Create<ItemCategoryNavigationViewModel>(configuration.Name)
                    is ItemCategoryNavigationViewModel viewModel)
                {
                    publisher.Publish(Create.As(viewModel), nameof(ItemCategoryCollectionViewModel));
                }
            }
        }

        return Task.CompletedTask;
    }
}
