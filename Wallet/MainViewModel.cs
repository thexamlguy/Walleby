﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Wallet;

public partial class MainViewModel :
    ObservableCollection<INavigationViewModel>,
    INotificationHandler<SelectionEventArgs<IWalletNavigationViewModel>>
{
    [ObservableProperty]
    private FooterViewModel footer;

    public MainViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template,
        FooterViewModel footer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Footer = footer;
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(SelectionEventArgs<IWalletNavigationViewModel> args)
    {
        if (args.Sender is WalletNavigationViewModel wallet)
        {
            SelectedItem = null;

            Reset(args => args.SetSource(wallet), false);
            SelectedItem = wallet;
        }
        else
        {
            Clear(false);
        }

        return Task.CompletedTask;
    }
}