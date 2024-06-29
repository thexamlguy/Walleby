using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Wallet;

public class CreateWalletHandler(IWalletHostFactory componentFactory,
    IPublisher publisher) :
    IHandler<CreateEventArgs<Wallet<(string, string, IImageDescriptor?)>>, bool>
{
    public async Task<bool> Handle(CreateEventArgs<Wallet<(string, string, IImageDescriptor?)>> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is Wallet <(string, string, IImageDescriptor?)> Wallet)
        {
            if (Wallet.Value is (string name, string password,
                IImageDescriptor thumbnail) && 
                name is { Length: > 0 } &&
                password is { Length: > 0 })
            {
                if (componentFactory.Create(name) is IComponentHost host)
                {
                    IWalletFactory factory = host.Services.GetRequiredService<IWalletFactory>();
                    if (await factory.Create(name, password, thumbnail))
                    {
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