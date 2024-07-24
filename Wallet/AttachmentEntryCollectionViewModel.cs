using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class AttachmentEntryCollectionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template,
    ItemState state,
    IItemEntryConfiguration<ICollection<Attachment>> configuration,
    string key,
    ICollection<Attachment> value,
    bool isConcealed,
    bool isRevealed,
    double width) : ItemEntryCollectionViewModel<IAttachmentEntryViewModel, ICollection<Attachment>>(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, isConcealed, isRevealed, width)
{
    public IContentTemplate Template { get; set; } = template;

    [RelayCommand]
    private async Task Invoke()
    {
        if (await Mediator.Handle<CreateEventArgs<FileAttachment>, IReadOnlyCollection<IFileDescriptor>>(Create.As<FileAttachment>()) 
            is IReadOnlyCollection<IFileDescriptor> fileDescriptors)
        {
            foreach (IFileDescriptor file in fileDescriptors)
            {
                string path = file.Path;
                DateTimeOffset created = DateTimeOffset.Now;
                string name = file.Name;
                long size = file.Size;

                Attachment attachment = new()
                {
                    Name = name,
                    Path = path,
                    DateTime = created
                };

                Add<LocalAttachmentEntryViewModel>(path, created, size, name);
                Value.Add(attachment);
            }
        }
    }
}