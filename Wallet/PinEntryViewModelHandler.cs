using Toolkit.Foundation;

namespace Wallet;

public class PinEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<PinEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<PinEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is PinEntryConfiguration configuration)
        {
            string? label = configuration.Label;
            string? value = $"{configuration.Value}" ?? "";
            double? width = configuration.Width;

            if (serviceFactory.Create<PinEntryViewModel>([.. args.Parameters, configuration, label, value, width])
                is PinEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
