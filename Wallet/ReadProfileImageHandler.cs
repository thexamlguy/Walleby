using Toolkit.Foundation;

namespace Wallet;

public class ReadProfileImageHandler(IFileProvider fileProvider, 
    IImageReader imageReader) : 
    IHandler<ReadEventArgs<ProfileImage>, IImageDescriptor?>
{
    public async Task<IImageDescriptor?> Handle(ReadEventArgs<ProfileImage> args,
        CancellationToken cancellationToken)
    {
        if (await fileProvider.SelectFiles(new FileFilter("Image files", ["jpg", "jpeg", "png"])) is {  Count: 1 } files)
        {
            if (files.FirstOrDefault() is string file)
            {
                await using FileStream stream = File.OpenRead(file);
                return await imageReader.Get(stream, 200, 200, true);
            }
        }

        return default;
    }
}
