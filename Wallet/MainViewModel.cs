using Avalonia.Xaml.Interactions.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template,
        FooterViewModel footer) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Footer = footer;
    }

    public IContentTemplate Template { get; set; }
}