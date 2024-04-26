using FluentAvalonia.UI.Windowing;
using Toolkit.Foundation;

namespace Bitvault.Avalonia;

[NavigationTarget("Window")]
public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
    }
}