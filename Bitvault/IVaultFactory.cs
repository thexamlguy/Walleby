
namespace Bitvault
{
    public interface IVaultFactory
    {
        Task CreateAsync(string name, VaultConfiguration configuration);
    }
}