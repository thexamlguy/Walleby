using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class WalletActivatedHandler(IWalletHostCollection Wallets,
    IPublisher publisher) :
    INotificationHandler<ActivatedEventArgs<IComponentHost>>
{
    public Task Handle(ActivatedEventArgs<IComponentHost> args)
    {
        if (args.Value is IComponentHost Wallet)
        {
            List<IComponentHost> sortedWallets = [.. Wallets, Wallet];
            sortedWallets = [.. sortedWallets.OrderBy(x => x.Services.GetRequiredService<IConfigurationDescriptor<WalletConfiguration>>() is 
                IConfigurationDescriptor<WalletConfiguration> descriptor ? descriptor.Name : null)];

            int index = sortedWallets.IndexOf(Wallet);

            if (Wallet.Services.GetRequiredService<ConfigurationDescriptor<WalletConfiguration>>() is ConfigurationDescriptor<WalletConfiguration> descriptor)
            {
                if (Wallet.Services.GetRequiredService<IServiceFactory>() is IServiceFactory serviceFactory)
                {
                    if (serviceFactory.Create<WalletNavigationViewModel>(descriptor.Name) is WalletNavigationViewModel viewModel)
                    {
                        publisher.Publish(new InsertEventArgs<IMainNavigationViewModel>(index, viewModel),
                            nameof(MainViewModel));
                    }
                }
            }
        }

        return Task.CompletedTask;
    }
}