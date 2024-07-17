using Toolkit.Foundation;

namespace Wallet;

public class HyperlinkEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<HyperlinkEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<HyperlinkEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is HyperlinkEntryConfiguration configuration)
        {
            string? label = configuration.Label;
            string? value = $"{configuration.Value}" ?? "";
            double? width = configuration.Width;

            if (serviceFactory.Create<HyperlinkEntryViewModel>(args => args.Initialize(),
                [.. args.Parameters, configuration, label, value, false, false, width])
                is HyperlinkEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}