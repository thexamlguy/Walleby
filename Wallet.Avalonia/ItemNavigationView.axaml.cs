using Avalonia.Controls;
using System;

namespace Wallet.Avalonia;

public partial class ItemNavigationView : 
    ListBoxItem
{
    public ItemNavigationView() => 
        InitializeComponent();

    protected override Type StyleKeyOverride =>
        typeof(ListBoxItem);
}