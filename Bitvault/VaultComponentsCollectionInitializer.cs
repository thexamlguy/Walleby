using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class VaultComponentsCollectionInitializer(IServiceProvider provider,
    IProxyServiceCollection<IComponentBuilder> proxy,
    VaultConfigurationCollection configurations) : IInitializer
{
    public Task Initialize()
    {
        //for (int index = 0; index < configurations.Count; index++)
        //{
        //    VaultConfiguration configuration = configurations[index];
        //    if (provider.GetRequiredService<IVaultComponent>() is IVaultComponent component)
        //    {
        //        IComponentBuilder builder = component.Create();
        //        builder.AddServices(services =>
        //        {
        //            services.AddTransient(_ =>
        //                provider.GetRequiredService<IProxyService<IPublisher>>());

        //            services.AddTransient(_ =>
        //                provider.GetRequiredService<IProxyService<IComponentHostCollection>>());

        //            services.AddScoped(_ =>
        //                provider.GetRequiredService<INavigationContextCollection>());

        //            services.AddScoped(_ =>
        //                provider.GetRequiredService<INavigationContextProvider>());

        //            services.AddScoped(_ =>
        //                provider.GetRequiredService<IComponentScopeCollection>());

        //            services.AddTransient(_ =>
        //                provider.GetRequiredService<IComponentScopeProvider>());

        //            services.AddRange(proxy.Services);
        //        });

        //        builder.AddConfiguration<VaultConfiguration>(name: $"{nameof(VaultConfigurationCollection)}:{configuration.Name}");

        //        IComponentHost host = builder.Build();
        //        host.StartAsync();
        //    }
        //}

        return Task.CompletedTask;
    }
}
