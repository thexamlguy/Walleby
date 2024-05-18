﻿using Toolkit.Foundation;

namespace Bitvault;

public partial class ContainerHeaderViewModel : 
    ObservableCollection
{
    public ContainerHeaderViewModel(IServiceProvider provider, 
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;

        Add<SearchContainerActionViewModel>();

        //Add<CreateItemActionViewModel>(scope: true);
    }

    public IContentTemplate Template { get; set; }

    //public Task Handle(RequestEventArgs<Filter<string>> args)
    //{
    //    if (args.Value is Filter<string> filter)
    //    {
    //        Value = filter.Value;
    //    }

    //    return Task.CompletedTask;
    //}
}
