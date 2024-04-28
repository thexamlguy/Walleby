using Toolkit.Foundation;

namespace Bitvault
{
    public interface IVaultFactory
    {
        IComponentHost? Create(string name, 
            string password, 
            VaultConfiguration configuration);
    }
}