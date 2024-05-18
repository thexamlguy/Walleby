using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class ContainerActivatedHandler(IContainerHostCollection containers,
    IPublisher publisher) : 
    INotificationHandler<ActivatedEventArgs<IComponentHost>>
{
    public Task Handle(ActivatedEventArgs<IComponentHost> args)
    {
        if (args.Value is IComponentHost container)
        {
            List<IComponentHost> sortedContainers = [.. containers, container];
            sortedContainers = [.. sortedContainers.OrderBy(x => x.GetConfiguration< ContainerConfiguration>() is ContainerConfiguration configuration ? configuration.Name : null)];

            int index = sortedContainers.IndexOf(container);

            if (container.Services.GetRequiredService<ContainerConfiguration>() is ContainerConfiguration configuration)
            {
                if (container.Services.GetRequiredService<IServiceFactory>() is IServiceFactory serviceFactory)
                {
                    if (serviceFactory.Create<ContainerNavigationViewModel>(configuration.Name) is ContainerNavigationViewModel viewModel)
                    {
                        publisher.Publish(new InsertEventArgs<IMainNavigationViewModel>(index, viewModel),
                            nameof(MainViewModel));
                    }
                }
            }
        }

        return Task.CompletedTask;
    }
}