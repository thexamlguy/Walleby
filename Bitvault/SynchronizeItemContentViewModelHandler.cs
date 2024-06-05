using Toolkit.Foundation;

namespace Bitvault;

public class SynchronizeItemContentViewModelHandler(IMediator mediator,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<SynchronizeEventArgs<IItemEntryViewModel>>
{
    public Task Handle(SynchronizeEventArgs<IItemEntryViewModel> args)
    {
        //wModel>(false) is ItemHeaderViewModel viewModel)
        //{
        //    publisher.Publish(Create.As<IItemEntryViewModel>(viewModel), nameof(ItemViewModel));
        //}

        return Task.CompletedTask;
    }
}
