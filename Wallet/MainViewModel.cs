﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

[Notification(typeof(CreateEventArgs<IMainNavigationViewModel>), nameof(MainViewModel))]
[Notification(typeof(InsertEventArgs<IMainNavigationViewModel>), nameof(MainViewModel))]
public partial class MainViewModel :
    ObservableCollection<IMainNavigationViewModel>
{
    [ObservableProperty]
    private FooterViewModel footer;

    public MainViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        FooterViewModel footer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Footer = footer;
    }

    public IContentTemplate Template { get; set; }
}