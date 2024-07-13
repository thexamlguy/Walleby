using Toolkit.Foundation;

namespace Wallet;

public class DropdownEntryCollectionViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<DropdownEntryCollectionConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<DropdownEntryCollectionConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is DropdownEntryCollectionConfiguration configuration)
        {
            List<DropdownEntryViewModel> values = [];
            values.Add(serviceFactory.Create<DropdownEntryViewModel>());

            foreach (string item in configuration.Values)
            {
                values.Add(serviceFactory.Create<DropdownEntryViewModel>(item));
            }

            string? label = configuration.Label;
            object? value = configuration.Value ?? "";
            double? width = configuration.Width;

            DropdownEntryViewModel? selected = values.FirstOrDefault(x => x.Value is not null && x.Value.Equals($"{value}"));

            if (serviceFactory.Create<DropdownEntryCollectionViewModel>(args => args.Initialize(),
                [values, .. args.Parameters, configuration, label, value, false, false, width, selected ?? null])
                is DropdownEntryCollectionViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
