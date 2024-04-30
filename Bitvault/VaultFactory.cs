namespace Bitvault;

public class VaultFactory(IVaultKeyGenerator keyGenerator, 
    IVaultStorage vaultStorageFactory) :
    IVaultFactory
{
    public bool Create(string name,
        string password)
    {
        VaultKey key = keyGenerator.Create(password);   
        return vaultStorageFactory.Create(name, key);
    }
}
