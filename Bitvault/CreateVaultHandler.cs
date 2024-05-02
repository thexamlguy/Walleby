using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateVaultHandler(IVaultComponentFactory componentFactory) :
    IHandler<Create<Vault>, bool>
{
    public async Task<bool> Handle(Create<Vault> args, 
        CancellationToken cancellationToken)
    {
        if (args.Value is Vault vault && vault.Name is { Length: > 0 } name &&
            vault.Password is { Length: > 0 } password)
        {
            if (componentFactory.Create(name) is IComponentHost host)
            {
                IVaultInitializer initializer = host.Services.GetRequiredService<IVaultInitializer>();
                if (await initializer.Initialize(name, password))
                {
                    host.Start();
                }
            }
        }

        return false;
    }
}