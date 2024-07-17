using System.Text.RegularExpressions;

namespace Wallet;

public partial class UppercaseRule :
    IPasswordRule
{
    public int CalculateScore(string password) =>
        Regex().IsMatch(password) ? 2 : 0;

    [GeneratedRegex(@"[A-Z]")]
    private static partial Regex Regex();
}