using System.Text.RegularExpressions;

namespace Bitvault;

public partial class DigitRule : 
    IPasswordRule
{
    public int CalculateScore(string password) =>
        Regex().IsMatch(password) ? 2 : 0;

    [GeneratedRegex(@"\d")]
    private static partial Regex Regex();
}
