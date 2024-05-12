namespace Bitvault;

public interface IContainerStorageFactory
{
    Task<bool> Create(string name, SecurityKey key);
}