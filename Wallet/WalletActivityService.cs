using Toolkit.Foundation;
using Timer = System.Threading.Timer;

namespace Wallet;

public class WalletActivityService(ISubscriber subscriber,
    IPublisher publisher) :
    IWalletActivityService,
    INotificationHandler<ActivatedEventArgs<Wallet>>,
    INotificationHandler<DeactivatedEventArgs<Wallet>>,
    INotificationHandler<OpenedEventArgs<Wallet>>,
    INotificationHandler<ClosedEventArgs<Wallet>>
{
    private bool isOpen;
    private readonly int timeout = 10000;
    private Timer? timer;

    public void Dispose()
    {
        timer?.Dispose();
    }

    public Task Handle(ActivatedEventArgs<Wallet> args)
    {
        if (isOpen)
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
        }

        return Task.CompletedTask;
    }

    public Task Handle(DeactivatedEventArgs<Wallet> args)
    {
        if (isOpen)
        {
            timer?.Change(timeout, Timeout.Infinite);
        }

        return Task.CompletedTask;
    }

    public Task Handle(OpenedEventArgs<Wallet> args)
    {
        isOpen = true;
        return Task.CompletedTask;
    }

    public Task Handle(ClosedEventArgs<Wallet> args)
    {
        isOpen = false;
        timer?.Change(Timeout.Infinite, Timeout.Infinite);

        return Task.CompletedTask;
    }

    public void Initialize()
    {
        subscriber.Subscribe(this);
        timer = new Timer(OnTimedEvent, null, Timeout.Infinite, Timeout.Infinite);
    }

    private void OnTimedEvent(object? state)
    {
        if (isOpen)
        {
            publisher.PublishUI(new ClosedEventArgs<Wallet>());
        }
    }
}
