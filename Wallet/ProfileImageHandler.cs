using Toolkit.Foundation;

namespace Wallet;

public class ProfileImageHandler(IFileProvider fileProvider, 
    IImageProvider imageProvider) : 
    IHandler<RequestEventArgs<ProfileImage>, IImageDescriptor?>
{
    public async Task<IImageDescriptor?> Handle(RequestEventArgs<ProfileImage> args,
        CancellationToken cancellationToken)
    {
        if (await fileProvider.SelectFiles(new FileFilter("Image files", ["jpg", "jpeg", "png"])) is {  Count: 1 } files)
        {
            if (files.FirstOrDefault() is string file)
            {
                return await imageProvider.Get(file, 100, 100, true);
            }
        }

        return default;
    }
}
