using Toolkit.Foundation;

namespace Bitvault;

public class ItemTextEntryViewModelHandler(IServiceFactory serviceFactory) : 
    IHandler<CreateEventArgs<TextEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<TextEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is TextEntryConfiguration configuration)
        {
            if (serviceFactory.Create<ItemTextEntryViewModel>([.. args.Parameters, configuration, configuration.Label, configuration.Value ?? ""])
                is ItemTextEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
