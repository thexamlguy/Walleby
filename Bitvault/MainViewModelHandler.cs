using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class MainViewModelHandler(IPublisher publisher,
    IContainerHostCollection containers) :
    INotificationHandler<Enumerate<IMainNavigationViewModel>>
{
    public async Task Handle(Enumerate<IMainNavigationViewModel> args,
        CancellationToken cancellationToken = default)
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
                        await publisher.Publish(new Create<IMainNavigationViewModel>(viewModel),
                            nameof(MainViewModel), cancellationToken);
                    }
                }
            }
        }
    }
}