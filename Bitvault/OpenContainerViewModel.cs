﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Toolkit.Foundation;

namespace Bitvault;

public partial class OpenContainerViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer) :
    Observable(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private string? password;

    [RelayCommand]
    private async Task Invoke()
    {
        if (Password is { Length: > 0 })
        {
            if (await Mediator.Handle<ActivateEventArgs<Container>, bool>(Activate.As(new Container(Password))))
            {
                Publisher.Publish(Opened.As<Container>());
            }
        }
    }
}