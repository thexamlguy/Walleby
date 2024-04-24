using Toolkit.Foundation;

namespace Bitvault;

public class LockViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer) :
    ObservableViewModel(serviceProvider, serviceFactory, publisher, subscriber, disposer)
{
    //public Task<bool> Confirm()
    //{
    //    //return Task.FromResult(false);
    //}
}
