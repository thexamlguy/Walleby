using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Wallet;

public class CreateWalletHandler(IHostEnvironment environment,
    IWalletHostFactory componentFactory,
    IWalletHostCollection wallets,
    IPublisher publisher) :
    IHandler<CreateEventArgs<Wallet<(string, string, IImageDescriptor?)>>, Result>
{
    public async Task<Result> Handle(CreateEventArgs<Wallet<(string, string, IImageDescriptor?)>> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is not Wallet<(string, string, IImageDescriptor?)> wallet)
        {
            return Result.Failure(Error.Failure);
        }

        (string name, string password, IImageDescriptor? imageDescriptor) = wallet.Value;
        name = name.Trim();

        if (Directory.Exists(Path.Combine(environment.ContentRootPath, "Wallet", name)))
        {
            return Result.Failure(Error.Duplicated);
        }

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
        {
            return Result.Failure(Error.Null);
        }

        if (componentFactory.Create(name) is IComponentHost host)
        {
            IWalletFactory walletFactory = host.Services.GetRequiredService<IWalletFactory>();
            if (await walletFactory.Create(name, password, imageDescriptor))
            {
                wallets.Add(host);
                host.Start();

                publisher.Publish(Activated.As(new Wallet<IComponentHost>(host)));
            }
        }

        return Result.Success();
    }
}