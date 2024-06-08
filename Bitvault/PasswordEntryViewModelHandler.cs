using Toolkit.Foundation;

namespace Bitvault;

public class PasswordEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<PasswordEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<PasswordEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is PasswordEntryConfiguration configuration)
        {
            if (serviceFactory.Create<PasswordEntryViewModel>([.. args.Parameters, configuration, configuration.Label, configuration.Value ?? ""])
                is PasswordEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
