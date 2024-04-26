using Toolkit.Foundation;

namespace Bitvault;


public class VaultStorageHandler :
    INotificationHandler<Create<VaultStorage>>
{
    public Task Handle(Create<VaultStorage> args,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public record VaultStorage(string Name);

public class VaultHandler(IVaultFactory factory) :
    IHandler<Create<Vault>, bool>
{
    //public async Task Handle(Create<Vault> args, 
    //    CancellationToken cancellationToken = default)
    //{
    //    if (args.Value is Vault vault)
    //    {
    //        if (factory.Create($"Vault:{vault.Name}", new VaultConfiguration { Name = vault.Name }) is IComponentHost host)
    //        {
    //            await host.StartAsync(cancellationToken);
    //        }
    //    }
    //}
    public Task<bool> Handle(Create<Vault> args, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}