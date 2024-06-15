using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using Toolkit.Foundation;

namespace Wallet;

public partial class CreateWalletViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IPublisher publisher,
    IMediator mediator,
    ISubscriber subscriber,
    IDisposer disposer) :
    Observable(provider, factory, mediator, publisher, subscriber, disposer),
    IPrimaryConfirmation
{
    [MaybeNull]
    [ObservableProperty]
    private string name;

    [MaybeNull]
    [ObservableProperty]
    private string password;

    public async Task<bool> ConfirmPrimary()
    {
        using (await new ActivityLock(this))
        {
            return await Mediator.Handle<CreateEventArgs<Wallet<(string, string)>>,
                bool>(Create.As(new Wallet<(string, string)>((Name, Password))));
        }
    }
}