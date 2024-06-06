using Toolkit.Foundation;

namespace Bitvault;

public class ItemMaskedTextEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<MaskedTextEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<MaskedTextEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is MaskedTextEntryConfiguration configuration)
        {
            if (serviceFactory.Create<ItemMaskedTextEntryViewModel>([.. args.Parameters, configuration, configuration.Pattern, configuration.Label, configuration.Value])
                is ItemMaskedTextEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
