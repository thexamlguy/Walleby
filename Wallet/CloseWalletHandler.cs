using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;
using Wallet.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Wallet;

public class CloseWalletHandler(IDecoratorService<WalletConnection> walletConnectionDecorator,
    IDbContextFactory<WalletContext> dbContextFactory) :
    IHandler<CloseEventArgs<Wallet>, bool>
{
    public async Task<bool> Handle(CloseEventArgs<Wallet> args, 
        CancellationToken cancellationToken)
    {
        walletConnectionDecorator.Set(null);

        return true;
    }
}
