using Toolkit.Foundation;

namespace Wallet;

public class SynchronizeItemCategoryViewModelHandler(IItemConfigurationCollection configurations,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<SynchronizeEventArgs<ItemCategoryNavigationViewModel>>
{
    public Task Handle(SynchronizeEventArgs<ItemCategoryNavigationViewModel> args)
    {
        bool selected = true;
        foreach (KeyValuePair<string, Func<ItemConfiguration>> configuration in configurations)
        {
            if (serviceFactory.Create<ItemCategoryNavigationViewModel>(args => args.OnInitialize(), 
                configuration.Key, selected)
                is ItemCategoryNavigationViewModel viewModel)
            {
                publisher.Publish(Create.As(viewModel), nameof(ItemCategoryCollectionViewModel));
                selected = false;
            }
        }

        return Task.CompletedTask;
    }
}
