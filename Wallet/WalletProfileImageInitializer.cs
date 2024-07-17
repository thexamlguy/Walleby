using Microsoft.Extensions.Hosting;
using Toolkit.Foundation;

namespace Wallet;

public class WalletProfileImageInitializer(IHostEnvironment environment,
    IImageReader reader,
    IDecoratorService<ProfileImage<IImageDescriptor>> profileImageDecorator) :
    IInitialization
{
    public void Initialize()
    {
        string file = Path.Combine(environment.ContentRootPath, "Thumbnail.png");
        if (File.Exists(file))
        {
            using FileStream stream = File.OpenRead(file);
            IImageDescriptor imageDescriptor = reader.Get(stream, 200, 200);

            profileImageDecorator.Set(new ProfileImage<IImageDescriptor>(imageDescriptor));
        }
    }
}