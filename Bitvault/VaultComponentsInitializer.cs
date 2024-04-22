using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class VaultComponentsInitializer(IServiceProvider provider,
    IProxyServiceCollection<IComponentBuilder> proxy,
    IEnumerable<IConfigurationDescriptor<VaultConfiguration>> configurations,
    IComponentScopeCollection scopes,
    IVaultHostCollection vaults) : IInitializer
{
    public async Task Initialize()
    {
        foreach (IConfigurationDescriptor<VaultConfiguration> configuration in configurations)
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

                    services.AddSingleton(new ComponentScope(configuration.Section));
                });

                builder.AddConfiguration(configuration.Section, configuration.Value);
                IComponentHost host = builder.Build();

                scopes.Add(new ComponentScopeDescriptor(configuration.Section,
                     host.Services.GetRequiredService<IServiceProvider>()));

                vaults.Add(host);
                await host.StartAsync();
            }
        }
    }
}
