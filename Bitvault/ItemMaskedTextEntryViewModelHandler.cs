﻿using Toolkit.Foundation;

namespace Bitvault;

public class ItemMaskedTextEntryViewModelHandler(IServiceFactory serviceFactory) : 
    IHandler<MaskedTextEntryConfiguration, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(MaskedTextEntryConfiguration args,
        CancellationToken cancellationToken)
    {
        if (serviceFactory.Create<ItemMaskedTextEntryViewModel>(args, args.Label, args.Value ?? "") is 
            ItemMaskedTextEntryViewModel viewModel)
        {
            return Task.FromResult<IItemEntryViewModel?>(viewModel);
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
