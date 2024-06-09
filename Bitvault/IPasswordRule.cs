namespace Bitvault;

public interface IPasswordRule
{
    int CalculateScore(string password);
}
