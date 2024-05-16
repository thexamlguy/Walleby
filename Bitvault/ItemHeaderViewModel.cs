﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class ItemHeaderViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    bool immutable,
    string? value = null) : ObservableViewModel<string, string>(provider, factory, mediator, publisher, subscriber, disposer, value),
    IHandler<ValidationEventArgs<Item>, bool>,
    IHandler<ConfirmEventArgs<Item>, ItemHeaderConfiguration>
{
    [ObservableProperty]
    private bool immutable = immutable;

    public Task<bool> Handle(ValidationEventArgs<Item> args, 
        CancellationToken cancellationToken)
    {
        // we need to work on the local validation layer
        return Task.FromResult(true);
    }

    public Task<ItemHeaderConfiguration> Handle(ConfirmEventArgs<Item> args,
        CancellationToken cancellationToken) => Task.FromResult(new ItemHeaderConfiguration { Name = Value });
}
