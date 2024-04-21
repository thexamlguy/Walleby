﻿using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class VaultComponentsInitializer(IServiceProvider provider,
    IProxyServiceCollection<IComponentBuilder> proxy,
    IEnumerable<IConfiguration<VaultConfiguration>> configurations,
    IVaultHostCollection vaults) : IInitializer
{
    public Task Initialize()
    {
        foreach (IConfiguration<VaultConfiguration> configuration in configurations)
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
                });

                builder.AddConfiguration(configuration.Section, configuration.Value);
                IComponentHost host = builder.Build();
                host.StartAsync();

                vaults.Add(host);
            }
        }

        return Task.CompletedTask;
    }
}
