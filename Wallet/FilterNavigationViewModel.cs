﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

[Notification(typeof(NotifyEventArgs<Item<int>>), nameof(Value))]
public abstract partial class FilterNavigationViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    int key,
    string value) : 
    ObservableCollection<IWalletNavigationViewModel, int, string>(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    IWalletNavigationViewModel,
    INotificationHandler<ActivatedEventArgs<Wallet>>,
    INotificationHandler<DeactivatedEventArgs<Wallet>>,
    INotificationHandler<NotifyEventArgs<Item<int>>>
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private bool selected;

    public Task Handle(NotifyEventArgs<Item<int>> args)
    {
        if (args.Sender is Item<int> item)
        {
            Key = item.Value;
        }

        return Task.CompletedTask;
    }

    public Task Handle(DeactivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = true);
}

[Notification(typeof(NotifyEventArgs<Item<int>>), nameof(Value))]
public abstract partial class FilterNavigationViewModel<TWalletNavigation>(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    int key,
    string value) :
    ObservableCollection<TWalletNavigation, int, string>(provider, factory, mediator, publisher, subscriber, disposer, key, value),
    IWalletNavigationViewModel,
    INotificationHandler<ActivatedEventArgs<Wallet>>,
    INotificationHandler<DeactivatedEventArgs<Wallet>>
    where TWalletNavigation :
    IWalletNavigationViewModel
{
    [ObservableProperty]
    private bool activated;

    [ObservableProperty]
    private bool selected;

    public Task Handle(DeactivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = false);

    public Task Handle(ActivatedEventArgs<Wallet> args) =>
        Task.FromResult(Activated = true);
}