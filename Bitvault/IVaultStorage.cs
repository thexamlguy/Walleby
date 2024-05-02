namespace Bitvault;

public interface IVaultStorage
{
    Task<bool> CreateAsync(string name, VaultKey key);
}