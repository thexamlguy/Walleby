using Toolkit.Foundation;

namespace Bitvault;

public class ContainerComponent(IComponentBuilder builder) : Component(builder),
    IContainerComponent;