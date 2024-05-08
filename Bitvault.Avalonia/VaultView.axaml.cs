using Avalonia.Controls;
using Toolkit.Foundation;

namespace Bitvault.Avalonia;

[NavigationTarget("Content")]
[NavigationTarget("ContentHeader")]
public partial class VaultView : UserControl
{
    public VaultView() => InitializeComponent();
}