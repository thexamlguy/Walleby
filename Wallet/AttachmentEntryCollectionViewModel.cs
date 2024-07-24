using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class AttachmentEntryCollectionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    ItemState state,
    IItemEntryConfiguration<ICollection<Attachment>> configuration,
    string key,
    ICollection<Attachment> value,
    bool isConcealed,
    bool isRevealed,
    double width) : ItemEntryCollectionViewModel<AttachmentEntryViewModel, ICollection<Attachment>>(provider, factory, mediator, publisher, subscriber, disposer, state, configuration, key, value, isConcealed, isRevealed, width)
{
    [RelayCommand]
    private async Task Invoke()
    {
        if (await Mediator.Handle<CreateEventArgs<FileAttachment>, IReadOnlyCollection<IFileDescriptor>>(Create.As<FileAttachment>()) 
            is IReadOnlyCollection<IFileDescriptor> fileDescriptors)
        {
            foreach (IFileDescriptor file in fileDescriptors)
            {
                Attachment attachment = new()
                {
                    Name = file.Name,
                    Path = file.Path,
                    DateTime = DateTimeOffset.Now
                };

                Add<AttachmentEntryViewModel>(attachment);
                Value.Add(attachment);
            }
        }
    }
}