using System.Text.RegularExpressions;

namespace Bitvault;

public partial class DiversityBonusRule : 
    IPasswordRule
{
    public int CalculateScore(string password)
    {
        bool hasUppercase = UppercaseRegex().IsMatch(password);
        bool hasLowercase = LowercaseRegex().IsMatch(password);
        bool hasDigit = DigitRegex().IsMatch(password);
        bool hasSpecialChar = SpecialCharRegex().IsMatch(password);

        return (hasUppercase && hasLowercase && hasDigit && hasSpecialChar) ? 2 : 0;
    }

    [GeneratedRegex(@"[A-Z]")]
    private static partial Regex UppercaseRegex();

    [GeneratedRegex(@"[a-z]")]
    private static partial Regex LowercaseRegex();

    [GeneratedRegex(@"\d")]
    private static partial Regex DigitRegex();

    [GeneratedRegex(@"[^a-zA-Z0-9]")]
    private static partial Regex SpecialCharRegex();
}