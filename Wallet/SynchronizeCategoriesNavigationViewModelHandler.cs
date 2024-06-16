﻿using Toolkit.Foundation;

namespace Wallet;

public class SynchronizeCategoriesNavigationViewModelHandler(IMediator mediator,
    IItemConfigurationCollection configurations,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<SynchronizeEventArgs<CategoryNavigationViewModel>>
{
    public async Task Handle(SynchronizeEventArgs<CategoryNavigationViewModel> args)
    {
        IReadOnlyCollection<(string Name, int Count)>? counts = await mediator.Handle<CountEventArgs<Item>,
            IReadOnlyCollection<(string, int)>>(Count.As<Item>());

        foreach (KeyValuePair<string, Func<ItemConfiguration>> configuration in configurations)
        {
            int count = counts?.FirstOrDefault(x => x.Name == configuration.Key).Count ?? 0;
            string name = configuration.Key;

            if (serviceFactory.Create<CategoryNavigationViewModel>(args => args.OnInitialize(), count, name)
                is CategoryNavigationViewModel viewModel)
            {
                publisher.Publish(Create.As(viewModel), nameof(CategoriesNavigationViewModel));
            }
        }
    }
}
