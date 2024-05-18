using Avalonia.Controls;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Windowing;

namespace Bitvault.Avalonia;

public partial class ContainerView : UserControl
{
    public ContainerView() => InitializeComponent();

    protected override void OnLoaded(RoutedEventArgs args)
    {
        base.OnLoaded(args);

        if (VisualRoot is AppWindow appWindow)
        {
            Title.ColumnDefinitions[1].Width = new GridLength(appWindow.TitleBar.RightInset, GridUnitType.Pixel);
        }
    }
}