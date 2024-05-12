using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class ContainerActivatedHandler(IContainerHostCollection containers,
    IPublisher publisher) : 
    INotificationHandler<Activated<IComponentHost>>
{
    public async Task Handle(Activated<IComponentHost> args, CancellationToken cancellationToken = default)
    {
        if (args.Value is IComponentHost container)
        {
            List<IComponentHost> sortedContainers = [.. containers, container];
            sortedContainers = [.. sortedContainers.OrderBy(x => x.GetConfiguration< ContainerConfiguration>() is ContainerConfiguration configuration ? configuration.Name : null)];

            int index = sortedContainers.IndexOf(container);

            if (container.Services.GetRequiredService<ContainerConfiguration>() is ContainerConfiguration configuration)
            {
                if (container.Services.GetRequiredService<IServiceFactory>() is IServiceFactory factory)
                {
                    if (factory.Create<ContainerNavigationViewModel>(configuration.Name) is ContainerNavigationViewModel viewModel)
                    {
                        await publisher.Publish(new Insert<IMainNavigationViewModel>(index, viewModel),
                            nameof(MainViewModel), cancellationToken);
                    }
                }
            }
        }
    }
}