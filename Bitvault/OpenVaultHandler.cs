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
          
            }
        }

        return false;
    }
}