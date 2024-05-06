using Avalonia.Controls;
using Toolkit.Foundation;

namespace Bitvault.Avalonia;

[NavigationTarget("Header")]
[NavigationTarget("Content")]
public partial class VaultView : UserControl
{
    public VaultView() => InitializeComponent();
}