using Toolkit.Foundation;

namespace Bitvault;

public class AggerateItemContentViewModelHandler(IValueStore<Item<(Guid, string)>> valueStore,
    IMediator mediator,
    IServiceFactory serviceFactory,
    IPublisher publisher) :
    INotificationHandler<AggerateEventArgs<IItemEntryViewModel>>
{
    public Task Handle(AggerateEventArgs<IItemEntryViewModel> args)
    {
        var d = valueStore;
        //if (serviceFactory.Create<ItemHeaderViewModel>(false) is ItemHeaderViewModel viewModel)
        //{
        //    publisher.Publish(Create.As<IItemEntryViewModel>(viewModel), nameof(ItemViewModel));
        //}

        return Task.CompletedTask;
    }
}