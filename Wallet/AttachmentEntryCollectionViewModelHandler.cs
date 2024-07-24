using Toolkit.Foundation;

namespace Wallet;

public class AttachmentEntryCollectionViewModelHandler(IServiceFactory serviceFactory) :
    IHandler<CreateEventArgs<AttachmentEntryCollectionConfiguration>, IItemEntryViewModel?>
{
    public Task<IItemEntryViewModel?> Handle(CreateEventArgs<AttachmentEntryCollectionConfiguration> args,
        CancellationToken cancellationToken)
    {
        if (args.Sender is AttachmentEntryCollectionConfiguration configuration)
        {
            string? label = configuration.Label;
            List<Attachment> values = configuration.Value is not null ? new List<Attachment>(configuration.Value) : [];
            double? width = configuration.Width;

            if (serviceFactory.Create<AttachmentEntryCollectionViewModel>(args => args.Initialize(),
                [.. args.Parameters, configuration, label, values, false, false, width])
                is AttachmentEntryCollectionViewModel viewModel)
            {
                //foreach (Comment value in values.OrderByDescending(x => x.DateTime))
                //{
                //    viewModel.Add<CommentEntryViewModel>(value.DateTime, value.Text);
                //}

                return Task.FromResult<IItemEntryViewModel?>(viewModel);
            }
        }

        return Task.FromResult<IItemEntryViewModel?>(default);
    }
}