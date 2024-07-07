using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Wallet;

public class CreateWalletHandler(IWalletHostFactory componentFactory,
    IWalletHostCollection wallets,
    IPublisher publisher) :
    IHandler<CreateEventArgs<Wallet<(string, string, IImageDescriptor?)>>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<Wallet<(string, string, IImageDescriptor?)>> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is Wallet<(string, string, IImageDescriptor?)> wallet)
        {
            (string name, string password, IImageDescriptor? imageDescriptor) = wallet.Value;

            if (name is { Length: > 0 } &&
                password is { Length: > 0 })
            {
                if (componentFactory.Create(name) is IComponentHost host)
                {
                    IWalletFactory walletFactory = host.Services.GetRequiredService<IWalletFactory>();
                    if (await walletFactory.Create(name, password, imageDescriptor))
                    {
                        wallets.Add(host);
                        host.Start();

                        publisher.Publish(Activated.As(new Wallet<IComponentHost>(host)));
                        return true;
                    }
                }
            }
        }

        return false;
    }
}