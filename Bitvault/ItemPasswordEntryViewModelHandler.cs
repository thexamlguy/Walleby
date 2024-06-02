using Toolkit.Foundation;

namespace Bitvault;

public class ItemPasswordEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<PasswordEntryConfiguration, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(PasswordEntryConfiguration args,
        CancellationToken cancellationToken)
    {
        if (serviceFactory.Create<ItemPasswordEntryViewModel>(args.Label, args.Value ?? new object()) is ItemPasswordEntryViewModel viewModel)
        {
            return Task.FromResult<IItemEntryViewModel?>(viewModel);
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
