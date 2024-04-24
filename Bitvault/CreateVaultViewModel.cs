using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolkit.Foundation;

namespace Bitvault;

public partial class CreateVaultViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableViewModel(serviceProvider, serviceFactory, publisher, subscriber, disposer),
    IPrimaryConfirmation
{
    [MaybeNull]
    [ObservableProperty]
    private string name;

    public async Task<bool> Confirm()
    {
        await Publisher.Publish(Create.As(new Vault(Name)));
        return true;
    }
}