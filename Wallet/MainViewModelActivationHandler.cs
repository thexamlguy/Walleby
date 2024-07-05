using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Wallet;

public class MainViewModelActivationHandler(IPublisher publisher,
    IWalletHostCollection Wallets) :
    INotificationHandler<ActivationEventArgs<IMainNavigationViewModel>>
{
    public Task Handle(ActivationEventArgs<IMainNavigationViewModel> args)
    {
        bool selected = true;

        foreach (IComponentHost Wallet in Wallets.OrderBy(x => x.Services.GetRequiredService<IConfigurationDescriptor<WalletConfiguration>>()
            is IConfigurationDescriptor<WalletConfiguration> descriptor ? descriptor.Name : null))
        {
            if (Wallet.Services.GetRequiredService<IConfigurationDescriptor<WalletConfiguration>>() 
                is IConfigurationDescriptor<WalletConfiguration> configuration)
            {
                if (Wallet.Services.GetRequiredService<IServiceFactory>() is IServiceFactory factory)
                {
                    IDecoratorService<ProfileImage<IImageDescriptor>> profileImageDecorator =
                        Wallet.Services.GetRequiredService<IDecoratorService<ProfileImage<IImageDescriptor>>>();
                    ProfileImage<IImageDescriptor>? profileImage = profileImageDecorator.Value;

                    if (factory.Create<WalletNavigationViewModel>(args => args.Initialize(), configuration.Name, profileImage?.Value ?? null, selected) 
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