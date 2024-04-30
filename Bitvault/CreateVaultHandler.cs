using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateVaultHandler(IVaultComponentFactory vaultComponentFactory) :
    IHandler<Create<Vault>, bool>
{
    public Task<bool> Handle(Create<Vault> args, 
        CancellationToken cancellationToken)
    {
        if (args.Value is Vault vault && vault.Name is { Length: > 0 } name && vault.Password is { Length: > 0 } password)
        {
            if (vaultComponentFactory.Create(name) is IComponentHost host)
            {
                IVaultFactory factory = host.Services.GetRequiredService<IVaultFactory>();
                factory.Create(name, password);

                return Task.FromResult(true);
            }
        }

        return Task.FromResult(false);
    }
}