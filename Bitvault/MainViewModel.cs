using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

[Notification(typeof(CreateEventArgs<IMainNavigationViewModel>), nameof(MainViewModel))]
public partial class MainViewModel :
    ObservableCollection<IMainNavigationViewModel>
{
    [ObservableProperty]
    private FooterViewModel footer;

    public MainViewModel(ICollectionSynchronizer synchronizer, 
        IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template,
        FooterViewModel footer) : base(synchronizer, provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Footer = footer;
    }

    public IContentTemplate Template { get; set; }
}