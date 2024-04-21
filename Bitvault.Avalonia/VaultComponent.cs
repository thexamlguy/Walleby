using Toolkit.Foundation;

namespace Bitvault.Avalonia;

public class VaultComponent :
    IVaultComponent
{
    public IComponentBuilder Create() =>
        ComponentBuilder.Create()
            .AddServices(services =>
            {
       
            });
}
