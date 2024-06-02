using Toolkit.Foundation;

namespace Bitvault;

public class ItemDropdownEntryViewModelHandler(IServiceFactory serviceFactory) : 
    IHandler<DropdownEntryConfiguration, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(DropdownEntryConfiguration args, 
        CancellationToken cancellationToken)
    {
        if (serviceFactory.Create<ItemDropdownEntryViewModel>(args.Label, args.Value ?? new object()) is ItemDropdownEntryViewModel viewModel)
        {
            return Task.FromResult<IItemEntryViewModel?>(viewModel);
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
