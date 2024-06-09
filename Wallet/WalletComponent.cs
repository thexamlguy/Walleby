using Toolkit.Foundation;

namespace Wallet;

public class WalletComponent(IComponentBuilder builder) : Component(builder),
    IWalletComponent;