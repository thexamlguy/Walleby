﻿using Toolkit.Foundation;

namespace Bitvault;

public partial class ManageViewModel :
    ObservableCollection,
    IMainNavigationViewModel
{
    public ManageViewModel(IServiceProvider provider,
        IServiceFactory factory,
        IMediator mediator,
        IPublisher publisher,
        ISubscription subscriber,
        IDisposer disposer,
        IContentTemplate template) : base(provider, factory, mediator, publisher, subscriber, disposer)
    {
        Template = template;

        Add<CreateLockerNavigationViewModel>();
    }

    public IContentTemplate Template { get; set; }
}