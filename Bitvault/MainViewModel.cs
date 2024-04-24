using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Notification(nameof(MainViewModel))]
public partial class MainViewModel : 
    ObservableCollectionViewModel<IMainNavigationViewModel>
{
    [ObservableProperty]
    private FooterViewModel footer;

    public MainViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template,
        FooterViewModel footer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {
        Template = template;
        Footer = footer;
    }

    public IContentTemplate Template { get; set; }
}
