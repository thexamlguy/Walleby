namespace Bitvault;

public interface IKeyGenerator
{
    byte[] Generate(int size);
}