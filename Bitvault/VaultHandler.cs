using Toolkit.Foundation;

namespace Bitvault;

public class VaultHandler(IVaultFactory factory) : INotificationHandler<Create<Vault>>
{
    public async Task Handle(Create<Vault> args, 
        CancellationToken cancellationToken = default)
    {
        if (args.Value is Vault vault)
        {
            await factory.CreateAsync($"Vault:{vault.Name}", new VaultConfiguration { Name = vault.Name });
        }
    }
}