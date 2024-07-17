using System.Text.RegularExpressions;

namespace Wallet;

public partial class LowercaseRule :
    IPasswordRule
{
    public int CalculateScore(string password) =>
        Regex().IsMatch(password) ? 2 : 0;

    [GeneratedRegex(@"[a-z]")]
    private static partial Regex Regex();
}