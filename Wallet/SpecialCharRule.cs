using System.Text.RegularExpressions;

namespace Wallet;

public partial class SpecialCharRule : IPasswordRule
{
    public int CalculateScore(string password) => 
        Regex().IsMatch(password) ? 2 : 0;

    [GeneratedRegex(@"[^a-zA-Z0-9]")]
    private static partial Regex Regex();
}