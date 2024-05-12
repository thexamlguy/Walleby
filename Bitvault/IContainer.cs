namespace Bitvault;

public interface IContainer
{
    Task<bool> Create(string name, SecurityKey key);
}