using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class MainViewModelHandler(IPublisher publisher,
    IContainerHostCollection containers) :
    INotificationHandler<EnumerateEventArgs<IMainNavigationViewModel>>
{
    public Task Handle(EnumerateEventArgs<IMainNavigationViewModel> args)
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
                        publisher.Publish(new CreateEventArgs<IMainNavigationViewModel>(viewModel),
                            nameof(MainViewModel));
                    }
                }
            }
        }

        return Task.CompletedTask;
    }
}