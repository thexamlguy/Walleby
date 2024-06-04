using Toolkit.Foundation;

namespace Bitvault;

public class ItemTextEntryViewModelHandler(IServiceFactory serviceFactory) : 
    IHandler<TextEntryConfiguration, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(TextEntryConfiguration args,
        CancellationToken cancellationToken)
    {
        if (serviceFactory.Create<ItemTextEntryViewModel>(args, args.Label, args.Value ?? "") 
            is ItemTextEntryViewModel viewModel)
        {
            return Task.FromResult<IItemEntryViewModel?>(viewModel);
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
