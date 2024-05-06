using Toolkit.Foundation;

namespace Bitvault;

public partial class VaultHeaderViewModel : ObservableCollectionViewModel<string, IDisposable>,
    INotificationHandler<Vault<Filter<string>>>
{
    public VaultHeaderViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;

        Add<AddVaultContentActionViewModel>();
    }

    public IContentTemplate Template { get; set; }

    public Task Handle(Vault<Filter<string>> args,
        CancellationToken cancellationToken = default)
    {
        if (args.Value is Filter<string> filter)
        {
            Value = filter.Value;
        }

        return Task.CompletedTask;
    }
}
