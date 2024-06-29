using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Wallet;

public class WalletComponent(IHostEnvironment environment,
    IComponentBuilder builder) :
    Component(builder)
{
    public override IComponentBuilder Configuring(string key,
        IComponentBuilder builder)
    {
        builder.SetComponentConfiguration(args =>
        {
            args.ContentRoot = Path.Combine(environment.ContentRootPath, key.Replace(":", "\\"));
        });

        return base.Configuring(key, builder);
    }
}