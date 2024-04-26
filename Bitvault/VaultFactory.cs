using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class VaultFactory(IServiceProvider provider,
    IProxyServiceCollection<IComponentBuilder> proxy,
    IComponentScopeCollection scopes,
    IVaultHostCollection vaults) : IVaultFactory
{
    public IComponentHost? Create(string name,
        VaultConfiguration configuration)
    {
        if (provider.GetRequiredService<IVaultComponent>() is IVaultComponent component)
        {
            IComponentBuilder builder = component.Create();
            builder.AddServices(services =>
            {
                services.AddTransient(_ =>
                    provider.GetRequiredService<IProxyService<IPublisher>>());

                services.AddTransient(_ =>
                    provider.GetRequiredService<IProxyService<IComponentHostCollection>>());

                services.AddScoped(_ =>
                    provider.GetRequiredService<INavigationContextCollection>());

                services.AddScoped(_ =>
                    provider.GetRequiredService<INavigationContextProvider>());

                services.AddScoped(_ =>
                    provider.GetRequiredService<IComponentScopeCollection>());

                services.AddTransient(_ =>
                    provider.GetRequiredService<IComponentScopeProvider>());

                services.AddRange(proxy.Services);
                services.AddSingleton(new ComponentScope(name));
            });

            builder.AddConfiguration(name, configuration);
            IComponentHost host = builder.Build();

            scopes.Add(new ComponentScopeDescriptor(name,
                 host.Services.GetRequiredService<IServiceProvider>()));

            vaults.Add(host);
            return host;
        }

        return default;
    }
}
