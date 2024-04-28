using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;


public class VaultHandler(IComponentFactory factory) :
    IHandler<Create<Vault>, bool>
{
    public async Task<bool> Handle(Create<Vault> args, 
        CancellationToken cancellationToken)
    {
        if (args.Value is Vault vault)
        {
            if (factory.Create<IVaultComponent>($"Vault:{vault.Name}", new VaultConfiguration { Name = vault.Name }) is IComponentHost host)
            {
                if (host.Services.GetRequiredService<IMediator>() is IMediator mediator)
                {
                    if (await mediator.Handle<Create<VaultStorage>, bool>(Create.As(new VaultStorage(vault.Name, vault.Password)), cancellationToken))
                    {
                        await host.StartAsync(cancellationToken);
                    }
                }
            }
        }

        return false;
    }
}