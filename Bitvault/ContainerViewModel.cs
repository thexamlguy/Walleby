using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;


[Enumerate(nameof(ContainerViewModel))]
public partial class ContainerViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    IContentTemplate template,
    NamedComponent named,
    string? filter = null) : ObservableCollectionViewModel<ItemNavigationViewModel>(provider, factory, mediator, publisher, subscriber, disposer),
    INotificationHandler<SecureStorage<Filter<string>>>
{
    [ObservableProperty]
    private string? filter = filter;

    [ObservableProperty]
    private string named = $"{named}";

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

        public async Task Handle(SecureStorage<Filter<string>> args,
            CancellationToken cancellationToken = default)
        {
            if (args.Value is Filter<string> filter)
            {
                Filter = filter.Value;
                await Enumerate();
            }
        }

        protected override IEnumerate PrepareEnumeration(object? key) =>
            Enumerate<ItemNavigationViewModel>.With(new ContainerViewModelConfiguration { Filter = Filter }) with { Key = key };
}