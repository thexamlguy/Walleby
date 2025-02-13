﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class AttachmentEntryViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    Guid id,
    DateTimeOffset created,
    int size,
    string name) : Observable(provider, factory, mediator, publisher, subscriber, disposer),
    IAttachmentEntryViewModel
{
    [ObservableProperty]
    private DateTimeOffset? created = created;

    [ObservableProperty]
    private Guid id = id;

    [ObservableProperty]
    private int size = size;

    [ObservableProperty]
    private string name = name;
}