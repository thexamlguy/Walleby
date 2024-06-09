using Toolkit.Foundation;

namespace Wallet;

public class PasswordEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<PasswordEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<PasswordEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is PasswordEntryConfiguration configuration)
        {
            string? label = configuration.Label;
            object? value = configuration.Value ?? "";
            double? width = configuration.Width;

            if (serviceFactory.Create<PasswordEntryViewModel>([.. args.Parameters, configuration, label, value, width])
                is PasswordEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
