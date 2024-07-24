using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class CreateCommentEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) : Observable<string>(provider, factory, mediator, publisher, subscriber, disposer),
    ICommentEntryViewModel
{
    [RelayCommand]
    private void Invoke()
    {
        Publisher.Publish(Create.As(new Comment { Text = Value, Created = DateTimeOffset.Now }));
        Value = null;
    }
}