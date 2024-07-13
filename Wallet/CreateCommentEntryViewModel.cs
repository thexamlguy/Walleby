using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public record Comment<TValue>(TValue Value);

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
        Publisher.Publish(Create.As(new Comment<string?>(Value)));
        Value = null;
    }
}