using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;

public partial class AddVaultContentViewModel : 
    ObservableCollectionViewModel<IVaultContentEntryViewModel>
{
    [ObservableProperty]
    private string named;

    public AddVaultContentViewModel(IServiceProvider provider, 
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

        Add<VaultContentHeaderViewModel>();
    }

    public IContentTemplate Template { get; set; }

    public Task<bool> Confirm()
    {
        VaultContentConfiguration configuration = new();
        foreach (IVaultContentEntryViewModel item in this)
        {
            item.Invoke(configuration);
        }

        return Task.FromResult(true);
    }
}
