using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class ItemActivatedHandler(IServiceProvider serviceProvider,
    IProxyService<IPublisher> proxyPublisher) :
    INotificationHandler<ActivatedEventArgs<Item>>
{
    public async Task Handle(ActivatedEventArgs<Item> args,
        CancellationToken cancellationToken = default)
    {
        IServiceScope serviceScope = serviceProvider.CreateScope();
        IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();

        if (serviceFactory.Create<ItemNavigationViewModel>(2, "efesf", "Description " + 1) is ItemNavigationViewModel viewModel)
        {
            // somehow, we need to get back out of the scope back to the compoment level, this currently doesnt work, and we need a better and cleaner way
            await proxyPublisher.Proxy.Publish(new CreateEventArgs<ItemNavigationViewModel>(viewModel),
                nameof(ContainerViewModel), cancellationToken);
        }
    }
}