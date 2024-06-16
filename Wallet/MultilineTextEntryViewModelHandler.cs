using Toolkit.Foundation;

namespace Wallet;

public class MultilineTextEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<MultilineTextEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<MultilineTextEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is MultilineTextEntryConfiguration configuration)
        {
            string? label = configuration.Label;
            string? value = $"{configuration.Value}" ?? "";
            double? width = configuration.Width;

            if (serviceFactory.Create<MultilineTextEntryViewModel>(args => args.OnInitialize(), 
                [.. args.Parameters, configuration, label, value, width])
                is MultilineTextEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}

