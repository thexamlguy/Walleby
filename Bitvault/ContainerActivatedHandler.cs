using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public record ContainerActivatedHandler(IPublisher publisher) : 
    INotificationHandler<Activated<IComponentHost>>
{
    public async Task Handle(Activated<IComponentHost> args, CancellationToken cancellationToken = default)
    {
        if (args.Value is IComponentHost container)
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