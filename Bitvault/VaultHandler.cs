using LiteDB;
using Toolkit.Foundation;

namespace Bitvault;

public class VaultStorageHandler :
    IHandler<Create<VaultStorage>, bool>
{
    public Task<bool> Handle(Create<VaultStorage> args, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class DatabaseFactory
{

}

public record VaultStorage(string Name);

public class VaultHandler(IVaultFactory factory) :
    IHandler<Create<Vault>, bool>
{
    public async Task<bool> Handle(Create<Vault> args, CancellationToken cancellationToken)
    {
        if (args.Value is Vault vault)
        {
            if (factory.Create($"Vault:{vault.Name}", new VaultConfiguration { Name = vault.Name }) is IComponentHost host)
            {
                await host.StartAsync(cancellationToken);
            }
        }

        return true;
    }
}