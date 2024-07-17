using Toolkit.Foundation;

namespace Wallet;

public class ItemCategoryViewModelActivatedHandler(IItemConfigurationCollection configurations,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<ActivationEventArgs<ItemCategoryNavigationViewModel>>
{
    public Task Handle(ActivationEventArgs<ItemCategoryNavigationViewModel> args)
    {
        bool selected = true;
        foreach (KeyValuePair<string, Func<ItemConfiguration>> configuration in configurations)
        {
            if (serviceFactory.Create<ItemCategoryNavigationViewModel>(args => args.Initialize(),
                configuration.Key, selected)
                is ItemCategoryNavigationViewModel viewModel)
            {
                publisher.Publish(Create.As(viewModel), nameof(ItemCategoryNavigationCollectionViewModel));
                selected = false;
            }
        }

        return Task.CompletedTask;
    }
}