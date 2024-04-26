using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class VaultInitializer(IServiceProvider provider,
    IProxyService<IPublisher> publisher) : IInitializer
{
    public async Task Initialize()
    {
        if (provider.GetService<IComponentHost>() is IComponentHost vault)
        {
            if (vault.Services.GetRequiredService<VaultConfiguration>() is VaultConfiguration configuration)
            {
                if (vault.Services.GetRequiredService<IServiceFactory>() is IServiceFactory factory)
                {
                    if (factory.Create<VaultNavigationViewModel>(configuration.Name) is VaultNavigationViewModel viewModel)
                    {
                        await publisher.Proxy.Publish(new Create<IMainNavigationViewModel>(viewModel),
                            nameof(MainViewModel));
                    }
                }
            }
        }
    }
}