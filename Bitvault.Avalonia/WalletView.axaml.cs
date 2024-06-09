using Avalonia.Controls;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Windowing;

namespace Bitvault.Avalonia;

public partial class WalletView : UserControl
{
    public WalletView() => InitializeComponent();

    protected override void OnLoaded(RoutedEventArgs args)
    {
        base.OnLoaded(args);

        if (VisualRoot is AppWindow appWindow)
        {
            Container.ColumnDefinitions[3].Width = new GridLength(appWindow.TitleBar.RightInset, GridUnitType.Pixel);
        }
    }
}