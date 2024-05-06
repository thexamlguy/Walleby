using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

[Enumerate(nameof(MainViewModel))]
public partial class MainViewModel :
    ObservableCollectionViewModel<IMainNavigationViewModel>
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