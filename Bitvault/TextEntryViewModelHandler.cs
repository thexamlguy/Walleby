using Toolkit.Foundation;

namespace Bitvault;

public class TextEntryViewModelHandler(IServiceFactory serviceFactory) : 
    IHandler<CreateEventArgs<TextEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<TextEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is TextEntryConfiguration configuration)
        {
            if (serviceFactory.Create<TextEntryViewModel>([.. args.Parameters, configuration, configuration.Label, configuration.Value ?? ""])
                is TextEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
