using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Wallet;

public class WalletActivatedHandler(IWalletHostCollection wallets,
    IPublisher publisher) :
    INotificationHandler<ActivatedEventArgs<Wallet<IComponentHost>>>
{
    public Task Handle(ActivatedEventArgs<Wallet<IComponentHost>> args)
    {
        if (args.Sender is Wallet<IComponentHost> wallet && wallet.Value is IComponentHost host)
        {
            List<IComponentHost> sortedWallets =
            [
                .. wallets.OrderBy(wallet =>
                {
                    var descriptor = wallet.Services.GetRequiredService<IConfigurationDescriptor<WalletConfiguration>>();
                    return descriptor?.Name;
                },
                Comparer<string?>.Create((x, y) =>
                {
                    bool xIsNumeric = int.TryParse(x, out int xNum);
                    bool yIsNumeric = int.TryParse(y, out int yNum);

                    return (xIsNumeric, yIsNumeric) switch
                    {
                        (true, true) => xNum.CompareTo(yNum),
                        (true, false) => -1,
                        (false, true) => 1,
                        _ => string.Compare(x, y, StringComparison.Ordinal)
                    };
                })),
            ];

            int index = sortedWallets.IndexOf(host);

            if (host.Services.GetRequiredService<IConfigurationDescriptor<WalletConfiguration>>() is 
                ConfigurationDescriptor<WalletConfiguration> descriptor)
            {
                if (host.Services.GetRequiredService<IServiceFactory>() is IServiceFactory serviceFactory)
                {
                    IDecoratorService<ProfileImage<IImageDescriptor>> profileImageDecorator =
                        host.Services.GetRequiredService<IDecoratorService<ProfileImage<IImageDescriptor>>>();
                    
                    ProfileImage<IImageDescriptor>? profileImage = profileImageDecorator.Value;
                    if (serviceFactory.Create<WalletNavigationViewModel>(args => args.Initialize(), 
                        descriptor.Name, profileImage?.Value, false) 
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