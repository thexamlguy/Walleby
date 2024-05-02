namespace Bitvault;

public interface IVaultKeyFactory
{
    VaultKey Create(string password);
}
