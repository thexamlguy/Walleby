using Toolkit.Foundation;

namespace Bitvault;

public class VaultComponentFactory(IComponentFactory componentFactory) : 
    IVaultComponentFactory
{
    public IComponentHost? Create(string name)
    {
        if (componentFactory.Create<IVaultComponent, VaultConfiguration>($"Vault:{name}", 
            new VaultConfiguration { Name = name }) is IComponentHost host)
        {
            return host;
        }

        return default;
    }
}
