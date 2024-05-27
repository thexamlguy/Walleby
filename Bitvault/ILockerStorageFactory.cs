namespace Bitvault;

public interface ILockerStorageFactory
{
    Task<bool> Create(string name, SecurityKey key);
}