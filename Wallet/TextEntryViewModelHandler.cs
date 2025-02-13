﻿using Toolkit.Foundation;

namespace Wallet;

public class TextEntryViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<TextEntryConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<TextEntryConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is TextEntryConfiguration configuration)
        {
            string? label = configuration.Label;
            string? value = $"{configuration.Value}" ?? "";
            double? width = configuration.Width;

            if (serviceFactory.Create<TextEntryViewModel>(args => args.Initialize(),
                [.. args.Parameters, configuration, label, value, false, false, width])
                is TextEntryViewModel viewModel)
            {
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}