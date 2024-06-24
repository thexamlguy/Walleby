using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Wallet;

public class WalletActivatedHandler(IWalletHostCollection Wallets,
    IPublisher publisher) :
    INotificationHandler<ActivatedEventArgs<Wallet<IComponentHost>>>
{
    public Task Handle(ActivatedEventArgs<Wallet<IComponentHost>> args)
    {
        if (args.Sender is Wallet<IComponentHost> wallet && wallet.Value is IComponentHost host)
        {
            List<IComponentHost> sortedWallets = [.. Wallets, host];
            sortedWallets = [.. sortedWallets.OrderBy(x => x.Services.GetRequiredService<IConfigurationDescriptor<WalletConfiguration>>() is 
                IConfigurationDescriptor<WalletConfiguration> descriptor ? descriptor.Name : null)];

            int index = sortedWallets.IndexOf(host);

            if (host.Services.GetRequiredService<IConfigurationDescriptor<WalletConfiguration>>() is 
                ConfigurationDescriptor<WalletConfiguration> descriptor)
            {
                if (host.Services.GetRequiredService<IServiceFactory>() is IServiceFactory serviceFactory)
                {
                    if (serviceFactory.Create<WalletNavigationViewModel>(args => args.Initialize(), 
                        descriptor.Name, false) 
                        is WalletNavigationViewModel viewModel)
                    {
                        publisher.Publish(Insert.As<IMainNavigationViewModel>(index, viewModel),
                            nameof(MainViewModel));
                    }
                }
            }
        }

        return Task.CompletedTask;
    }
}