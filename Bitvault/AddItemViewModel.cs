using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class AddItemViewModel : 
    ObservableCollectionViewModel<IItemViewModel>,
    INotificationHandler<Test>
{
    [ObservableProperty]
    private string named;

    public AddItemViewModel(IServiceProvider provider, 
        IServiceFactory factory, 
        IMediator mediator,
        IPublisher publisher, 
        ISubscriber subscriber, 
        IDisposer disposer,
        IContentTemplate template,
        NamedComponent named) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;
        Named = $"{named}";

        Add<ItemHeaderViewModel>();
    }

    public IContentTemplate Template { get; set; }

    public Task<bool> Confirm()
    {
        ItemConfiguration configuration = new();
        foreach (IItemViewModel item in this)
        {
            item.Invoke(configuration);
        }

        return Task.FromResult(true);
    }

    public Task Handle(Test args, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
