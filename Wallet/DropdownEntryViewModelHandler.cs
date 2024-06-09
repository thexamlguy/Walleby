using Toolkit.Foundation;

namespace Wallet;

public class DropdownEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<DropdownEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<DropdownEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is DropdownEntryConfiguration configuration)
        {
            List<DropdownValueViewModel> values = [];
            foreach (string item in configuration.Values)
            {
                values.Add(serviceFactory.Create<DropdownValueViewModel>(item));
            }

            string? label = configuration.Label;
            object? value = configuration.Value ?? "";
            double? width = configuration.Width;

            DropdownValueViewModel? selected = values.FirstOrDefault(x => x.Value is not null && x.Value.Equals($"{value}"));

            if (serviceFactory.Create<DropdownEntryViewModel>([values, .. args.Parameters, configuration, label, value, width, selected])
                is DropdownEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
