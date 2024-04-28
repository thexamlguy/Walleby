using Toolkit.Foundation;

namespace Bitvault;

public interface IVaultComponent : IComponent;

public class VaultComponent(IComponentBuilder builder) : Component(builder),
    IVaultComponent
{
}