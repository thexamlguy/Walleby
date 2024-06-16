using Toolkit.Foundation;

namespace Wallet;

public class MaskedTextEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<MaskedTextEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<MaskedTextEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is MaskedTextEntryConfiguration configuration)
        {
            string? label = configuration.Label;
            string? value = $"{configuration.Value}" ?? "";
            double? width = configuration.Width;

            if (serviceFactory.Create<MaskedTextEntryViewModel>(args => args.Initialize(), 
                [.. args.Parameters, configuration, configuration.Pattern, label, value, width])
                is MaskedTextEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
