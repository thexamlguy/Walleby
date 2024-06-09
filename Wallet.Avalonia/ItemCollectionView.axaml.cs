using Avalonia.Controls;

namespace Wallet.Avalonia;

public partial class ItemCollectionView :
    UserControl
{
    public ItemCollectionView()
    {
        InitializeComponent();

        foo.SelectionChanged += Foo_SelectionChanged;
    }

    private void Foo_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
    }
}
