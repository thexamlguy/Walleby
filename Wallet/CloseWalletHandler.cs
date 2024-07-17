using Toolkit.Foundation;

namespace Wallet;

public class CloseWalletHandler(IDecoratorService<WalletConnection> walletConnectionDecorator) :
    IHandler<CloseEventArgs<Wallet>, bool>
{
    public Task<bool> Handle(CloseEventArgs<Wallet> args,
        CancellationToken cancellationToken)
    {
        walletConnectionDecorator.Set(null);
        return Task.FromResult(true);
    }
}