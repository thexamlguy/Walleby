﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ArchiveNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableViewModel(provider, factory, mediator, publisher, subscriber, disposer),
    IVaultNavigationViewModel
{
    [ObservableProperty]
    private bool selected;

    [RelayCommand]
    public void Invoke() => Publisher.Publish(Changed.As(new VaultCategory("Archive")));
}