using Toolkit.Foundation;

namespace Bitvault;

public class AggregateItemContentViewModelHandler(IMediator mediator,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<AggerateEventArgs<IItemEntryViewModel>>
{
    public Task Handle(AggerateEventArgs<IItemEntryViewModel> args)
    {
        //wModel>(false) is ItemHeaderViewModel viewModel)
        //{
        //    publisher.Publish(Create.As<IItemEntryViewModel>(viewModel), nameof(ItemViewModel));
        //}

        return Task.CompletedTask;
    }
}
