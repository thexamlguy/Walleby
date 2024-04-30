namespace Bitvault;

public interface IVaultStorage
{
    bool Create(string name, VaultKey key);
}