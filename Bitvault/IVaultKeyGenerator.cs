namespace Bitvault;

public interface IVaultKeyGenerator
{
    VaultKey Create(string password);
}