using System;
using Toolkit.UI.Controls.Avalonia;

namespace Wallet.Avalonia;

public partial class WalletNavigationView :
    OverflowItem
{
    public WalletNavigationView() =>
        InitializeComponent();

    protected override Type StyleKeyOverride =>
        typeof(OverflowItem);
}