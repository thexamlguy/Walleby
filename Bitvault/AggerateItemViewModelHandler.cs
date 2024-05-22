using Toolkit.Foundation;

namespace Bitvault;

public class AggerateItemViewModelHandler(IMediator mediator,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<AggerateEventArgs<IItemEntryViewModel>>
{
    public Task Handle(AggerateEventArgs<IItemEntryViewModel> args)
    {
        //if (serviceFactory.Create<ItemHeaderViewModel>(false) is ItemHeaderViewModel viewModel)
        //{
        //    publisher.Publish(Create.As<IItemEntryViewModel>(viewModel), nameof(ItemViewModel));
        //}

        return Task.CompletedTask;
    }
}
