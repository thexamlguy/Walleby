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
            string? label = configuration.Label;
            object? value = configuration.Value ?? "";
            double? width = configuration.Width;

            if (serviceFactory.Create<TextEntryViewModel>([.. args.Parameters, configuration, label, value, width])
                is TextEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
