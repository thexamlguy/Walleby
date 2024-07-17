using Toolkit.Foundation;

namespace Wallet;

public class CreateProfileImageHandler(IFileProvider fileProvider,
    IImageReader imageReader) :
    IHandler<CreateEventArgs<ProfileImage>, IImageDescriptor?>
{
    public async Task<IImageDescriptor?> Handle(CreateEventArgs<ProfileImage> args,
        CancellationToken cancellationToken)
    {
        if (await fileProvider.SelectFiles(new FileFilter("Image files", ["jpg", "jpeg", "png"]))
            is { Count: 1 } files)
        {
            if (files.FirstOrDefault() is string file)
            {
                using FileStream stream = File.OpenRead(file);
                return imageReader.Get(stream, 200, 200, true);
            }
        }

        return default;
    }
}