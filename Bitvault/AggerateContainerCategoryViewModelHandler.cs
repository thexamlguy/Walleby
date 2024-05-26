using Toolkit.Foundation;

namespace Bitvault;

public class AggerateContainerCategoryViewModelHandler(IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<AggerateEventArgs<ItemNavigationViewModel,
        ContainerViewModelConfiguration>>
{
    public Task Handle(AggerateEventArgs<ItemNavigationViewModel,
    ContainerViewModelConfiguration> args)
    {
        if (serviceFactory.Create<ItemNavigationViewModel>() 
            is ItemNavigationViewModel viewModel)
        {
            publisher.Publish(Create.As(viewModel), nameof(ContainerCategoryCollectionViewModel));
        }

        return Task.CompletedTask;
    }
}
