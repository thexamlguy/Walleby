using Toolkit.Foundation;

namespace Bitvault;

public class ItemDropdownEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<DropdownEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<DropdownEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is DropdownEntryConfiguration configuration)
        {
            List<ItemDropdownValueViewModel> values = [];
            foreach (string item in configuration.Values)
            {
                values.Add(serviceFactory.Create<ItemDropdownValueViewModel>(item));
            }

            string? label = configuration.Label;
            object? value = configuration.Value;

            ItemDropdownValueViewModel? selected = values.FirstOrDefault(x => x.Value is not null && x.Value.Equals($"{value}"));

            if (serviceFactory.Create<ItemDropdownEntryViewModel>([values, .. args.Parameters, configuration, label, value ?? "", selected])
                is ItemDropdownEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}
