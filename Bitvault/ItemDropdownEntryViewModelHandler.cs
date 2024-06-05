using Toolkit.Foundation;

namespace Bitvault;

public class ItemDropdownEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<DropdownEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<DropdownEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is DropdownEntryConfiguration configuration)
        {
            if (serviceFactory.Create<ItemDropdownEntryViewModel>([.. args.Parameters, configuration, configuration.Label, configuration.Value ?? ""])
                is ItemDropdownEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
