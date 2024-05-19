using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class AggerateMainViewModelHandler(IPublisher publisher,
    IContainerHostCollection containers) :
    INotificationHandler<AggerateEventArgs<IMainNavigationViewModel>>
{
    public Task Handle(AggerateEventArgs<IMainNavigationViewModel> args)
    {
        foreach (IComponentHost container in containers.OrderBy(x => x.GetConfiguration<ContainerConfiguration>() 
            is ContainerConfiguration configuration ? configuration.Name : null))
        {
            if (container.Services.GetRequiredService<ContainerConfiguration>() is ContainerConfiguration configuration)
            {
                if (container.Services.GetRequiredService<IServiceFactory>() is IServiceFactory factory)
                {
                    if (factory.Create<ContainerNavigationViewModel>(configuration.Name) is ContainerNavigationViewModel viewModel)
                    {
                        publisher.Publish(Create.As<IMainNavigationViewModel>(viewModel),
                            nameof(MainViewModel));
                    }
                }
            }
        }

        return Task.CompletedTask;
    }
}