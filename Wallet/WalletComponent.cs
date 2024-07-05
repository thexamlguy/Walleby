using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Xml.Linq;
using Toolkit.Foundation;

namespace Wallet;

public class WalletComponent(IHostEnvironment environment,
    IComponentBuilder builder) :
    Component(builder)
{
    public override IComponentBuilder Configuring(string key,
        IComponentBuilder builder)
    {
        string path = Path.Combine(environment.ContentRootPath, key.Replace(":", "\\"));
        builder.SetComponentConfiguration(args =>
        {
            args.ContentRoot = Path.Combine(path);
        });

        return base.Configuring(key, builder);
    }
}