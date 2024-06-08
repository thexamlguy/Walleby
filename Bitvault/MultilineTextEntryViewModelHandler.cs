using Toolkit.Foundation;

namespace Bitvault;

public class MultilineTextEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<MultilineTextEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<MultilineTextEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is MultilineTextEntryConfiguration configuration)
        {
            if (serviceFactory.Create<MultilineTextEntryViewModel>([.. args.Parameters, configuration, configuration.Label, configuration.Value ?? ""])
                is MultilineTextEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}

