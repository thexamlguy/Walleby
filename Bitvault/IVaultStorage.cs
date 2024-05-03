namespace Bitvault;

public interface IVaultStorage
{
    Task<bool> Create(string name, VaultKey key);
}