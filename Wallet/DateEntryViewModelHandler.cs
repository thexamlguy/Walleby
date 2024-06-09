using Toolkit.Foundation;

namespace Wallet;

public class DateEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<DateEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<DateEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is DateEntryConfiguration configuration)
        {
            string? label = configuration.Label;

            if (!DateTimeOffset.TryParse($"{configuration.Value}", out DateTimeOffset value))
            {
                value = DateTimeOffset.Now;
            }

            double? width = configuration.Width;

            if (serviceFactory.Create<DateEntryViewModel>([.. args.Parameters, configuration, label, value, width])
                is DateEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}