namespace Bitvault;

public interface IContainerFactory
{
    Task<bool> Create(string name, SecurityKey key);
}