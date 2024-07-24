using Toolkit.Foundation;

namespace Wallet;

public class CreateFileAttachmentHandler(IFileProvider fileProvider) :
    IHandler<CreateEventArgs<FileAttachment>, IReadOnlyCollection<IFileDescriptor>>
{
    public async Task<IReadOnlyCollection<IFileDescriptor>> Handle(CreateEventArgs<FileAttachment> args,
        CancellationToken cancellationToken)
    {
        List<IFileDescriptor> attachments = [];
        if (await fileProvider.SelectFiles(new FileFilter("All files", [], true))
            is { Count: > 0 } files)
        {
            foreach (string file in files)
            {
                FileInfo fileInfo = new(file);
                if (fileInfo.Exists)
                {
                    attachments.Add(new FileDescriptor(fileInfo.Name, fileInfo.FullName, (int)fileInfo.Length));
                }
            }
        }

        return attachments;
    }
}