namespace Wallet;

public interface IPasswordRule
{
    int CalculateScore(string password);
}
