using Toolkit.Foundation;

namespace Bitvault;

public class ItemPasswordEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<PasswordEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<PasswordEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is PasswordEntryConfiguration configuration)
        {
            if (serviceFactory.Create<ItemPasswordEntryViewModel>([.. args.Parameters, configuration, configuration.Label, configuration.Value ?? ""])
                is ItemPasswordEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
