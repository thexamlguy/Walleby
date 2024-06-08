using Toolkit.Foundation;

namespace Bitvault;

public class MaskedTextEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<MaskedTextEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<MaskedTextEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is MaskedTextEntryConfiguration configuration)
        {
            if (serviceFactory.Create<MaskedTextEntryViewModel>([.. args.Parameters, configuration, configuration.Pattern, configuration.Label, configuration.Value])
                is MaskedTextEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
