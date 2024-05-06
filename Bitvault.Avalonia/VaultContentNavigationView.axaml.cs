using Avalonia.Controls;

namespace Bitvault.Avalonia
{
    public partial class VaultContentNavigationView : ListBoxItem
    {
        public VaultContentNavigationView()
        {
            InitializeComponent();
            Tapped += VaultContentNavigationView_Tapped;
        }

        private void VaultContentNavigationView_Tapped(object? sender, global::Avalonia.Input.TappedEventArgs e)
        {

        }
    }
}
