﻿using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Wallet;

public partial class DismissItemActionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) : Observable(provider, factory, mediator, publisher, subscriber, disposer)
{
    [RelayCommand]
    private void Invoke() => Publisher.Publish(Cancel.As<Item>());
}