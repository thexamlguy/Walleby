using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Wallet;

public class SynchronizeMainViewModelHandler(IPublisher publisher,
    IWalletHostCollection Wallets) :
    INotificationHandler<SynchronizeEventArgs<IMainNavigationViewModel>>
{
    public Task Handle(SynchronizeEventArgs<IMainNavigationViewModel> args)
    {
        bool selected = true;

        foreach (IComponentHost Wallet in Wallets.OrderBy(x => x.Services.GetRequiredService<IConfigurationDescriptor<WalletConfiguration>>()
            is IConfigurationDescriptor<WalletConfiguration> descriptor ? descriptor.Name : null))
        {
            if (Wallet.Services.GetRequiredService<IConfigurationDescriptor<WalletConfiguration>>() 
                is IConfigurationDescriptor<WalletConfiguration> descriptor)
            {
                if (Wallet.Services.GetRequiredService<IServiceFactory>() is IServiceFactory factory)
                {
                    if (factory.Create<WalletNavigationViewModel>(descriptor.Name, selected) 
                        is WalletNavigationViewModel viewModel)
                    {
                        publisher.Publish(Create.As<IMainNavigationViewModel>(viewModel),
                            nameof(MainViewModel));

                        selected = false;
                    }
                }
            }
        }

        return Task.CompletedTask;
    }
}