using Toolkit.Foundation;

namespace Bitvault;

public class OpenVaultHandler(IMediator mediator) :
    IHandler<Open<Vault>, bool>
{
    public async Task<bool> Handle(Open<Vault> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is Vault vault)
        {
            if (vault.Password is { Length: > 0 } password)
            {
                //if (await mediator.Handle<Open<VaultStorage>, bool>(Open.As(new VaultStorage("Personal", password)), cancellationToken))
                //{
                //    return true;
                //}
            }
        }

        return false;
    }
}