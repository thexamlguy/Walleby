using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;


[Enumerate(nameof(VaultViewModel))]
public partial class VaultViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template,
    string? filter = null) : ObservableCollectionViewModel<VaultContentNavigationViewModel>(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<Vault<Filter<string>>>
{
    [ObservableProperty]
    private string? filter = filter;

    public IContentTemplate Template { get; set; } = template;

    public override async Task Activated()
    {
        await Publisher.Publish(Vault.As<Activated>());
        await base.Activated();
    }

    public override async Task Deactivated()
    {
        await Publisher.Publish(Vault.As<Deactivated>());
        await base.Deactivated();
    }

    protected override IEnumerate PrepareEnumeration(object? key) => 
        Enumerate<VaultContentNavigationViewModel>.With(new VaultViewModelOptions { Filter = Filter }) with { Key = key };

    public async Task Handle(Vault<Filter<string>> args, 
        CancellationToken cancellationToken = default)
    {
        if (args.Value is Filter<string> filter)
        {
            Filter = filter.Value;
            await Enumerate();
        }
    }
}