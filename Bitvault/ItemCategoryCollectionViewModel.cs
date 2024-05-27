﻿using CommunityToolkit.Mvvm.ComponentModel;
using Toolkit.Foundation;

namespace Bitvault;
[Aggerate(nameof(ItemCategoryCollectionViewModel))]
public partial class ItemCategoryCollectionViewModel(IServiceProvider provider,
    IServiceFactory factory,
    IMediator mediator,
    IPublisher publisher,
    ISubscription subscriber,
    IDisposer disposer,
    IContentTemplate template) :
    ObservableCollection<ItemCategoryNavigationViewModel>(provider, factory, mediator, publisher, subscriber, disposer)
{
    [ObservableProperty]
    private IContentTemplate template = template;
}
