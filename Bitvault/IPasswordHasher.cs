namespace Bitvault;

public interface IPasswordHasher
{
    string HashPassword(string password, int iterations = 10000);
}