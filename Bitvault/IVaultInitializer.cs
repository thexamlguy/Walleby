namespace Bitvault;

public interface IVaultInitializer
{
    Task<bool> Initialize(string name, string password);
}
