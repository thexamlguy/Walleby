using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class CreateVaultHandler(IComponentFactory componentFactory) :
    IHandler<Create<Vault>, bool>
{
    public async Task<bool> Handle(Create<Vault> args, 
        CancellationToken cancellationToken)
    {
        if (args.Value is Vault vault)
        {
            if (vault.Name is { Length: > 0 } name && vault.Password is { Length: > 0 } password)
            {
                if (componentFactory.Create<IVaultComponent, VaultConfiguration>($"Vault:{name}", new VaultConfiguration { Name = name }) is IComponentHost host)
                {
                    if (host.Services.GetRequiredService<IMediator>() is IMediator mediator)
                    {
                        if (await mediator.Handle<Create<VaultStorage>, bool>(Create.As(new VaultStorage(name, password)), cancellationToken))
                        {
                            await host.StartAsync(cancellationToken);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}