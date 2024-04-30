namespace Bitvault
{
    public interface IVaultFactory
    {
        bool Create(string name, string password);
    }
}