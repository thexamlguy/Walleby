using Wallet.Data;

namespace Wallet;

public record WalletConnection(string Value) : 
    IConnection
{
    public override string ToString() => Value;
}