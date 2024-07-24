using Toolkit.Foundation;

namespace Wallet;

public class CommentEntryCollectionViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<CommentEntryCollectionConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<CommentEntryCollectionConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is CommentEntryCollectionConfiguration configuration)
        {
            string? label = configuration.Label;
            List<Comment> values = configuration.Value is not null ? new List<Comment>(configuration.Value) : [];
            double? width = configuration.Width;

            if (serviceFactory.Create<CommentEntryCollectionViewModel>(args => args.Initialize(),
                [.. args.Parameters, configuration, label, values, false, false, width])
                is CommentEntryCollectionViewModel viewModel)
            {
                foreach (Comment value in values.OrderByDescending(x => x.Created))
                {
                    viewModel.Add<CommentEntryViewModel>(value.Created, value.Text);
                }

                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}