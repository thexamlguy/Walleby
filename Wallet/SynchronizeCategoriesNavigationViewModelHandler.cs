using Toolkit.Foundation;

namespace Wallet;

public class SynchronizeCategoriesNavigationViewModelHandler(IItemConfigurationCollection configurations,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<SynchronizeEventArgs<CategoryNavigationViewModel>>
{
    public Task Handle(SynchronizeEventArgs<CategoryNavigationViewModel> args)
    {
        foreach (KeyValuePair<string, Func<ItemConfiguration>> configuration in configurations)
        {
            if (serviceFactory.Create<CategoryNavigationViewModel>(args => args.Initialize(),
                configuration.Key)
                is CategoryNavigationViewModel viewModel)
            {
                publisher.Publish(Create.As(viewModel), nameof(CategoriesNavigationViewModel));
            }
        }

        return Task.CompletedTask;
    }
}
