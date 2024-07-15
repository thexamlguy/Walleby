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
            List<Comment> value =   new List<Comment>();
            double? width = configuration.Width;

            if (serviceFactory.Create<CommentEntryCollectionViewModel>(args => args.Initialize(), 
                [.. args.Parameters, configuration, label, value, false, false, width])
                is CommentEntryCollectionViewModel viewModel)
            {

                viewModel.Add<CreateCommentEntryViewModel>();
                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}

