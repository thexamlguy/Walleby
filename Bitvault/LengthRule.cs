namespace Bitvault;

public class LengthRule :
    IPasswordRule
{
    public int CalculateScore(string password) => 
        password.Length >= 8 ? 5 : 0;
}
