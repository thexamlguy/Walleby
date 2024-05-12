using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class ContainerActivatedHandler(IContainerHostCollection containers,
    IPublisher publisher) : 
    INotificationHandler<ActivatedEventArgs<IComponentHost>>
{
    public async Task Handle(ActivatedEventArgs<IComponentHost> args,
        CancellationToken cancellationToken = default)
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
                        await publisher.Publish(new InsertEventArgs<IMainNavigationViewModel>(index, viewModel),
                            nameof(MainViewModel), cancellationToken);
                    }
                }
            }
        }
    }
}

public class ItemActivatedHandler(IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<ActivatedEventArgs<ItemConfiguration>>
{
    public async Task Handle(ActivatedEventArgs<ItemConfiguration> args,
        CancellationToken cancellationToken = default)
    {

    }
}