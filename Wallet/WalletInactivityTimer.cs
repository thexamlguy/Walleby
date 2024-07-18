using Toolkit.Foundation;
using Timer = System.Threading.Timer;

namespace Wallet;

public class WalletInactivityTimer(WalletConfiguration configuration,
    IUserInteraction userInteraction, 
    ISubscriber subscriber,
    IPublisher publisher) :
    IWalletInactivityTimer,
    INotificationHandler<ActivatedEventArgs<Wallet>>,
    INotificationHandler<DeactivatedEventArgs<Wallet>>,
    INotificationHandler<OpenedEventArgs<Wallet>>,
    INotificationHandler<ClosedEventArgs<Wallet>>
{
    private bool isOpen;
    private Timer? timer;
    private readonly object timerLock = new();

    public void Initialize()
    {
        subscriber.Subscribe(this);
        timer = new Timer(OnTimedEvent, null, Timeout.Infinite, Timeout.Infinite);
        userInteraction.UserInteracted += OnUserInteracted;
    }

    public Task Handle(ActivatedEventArgs<Wallet> args)
    {
        if (isOpen)
        {
            ResetTimer();
            userInteraction.Start();
        }

        return Task.CompletedTask;
    }

    public Task Handle(DeactivatedEventArgs<Wallet> args)
    {
        if (isOpen)
        {
            ResetTimer();
            userInteraction.Stop();
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

        StopTimer();
        userInteraction.Stop();

        return Task.CompletedTask;
    }

    private void ResetTimer()
    {
        lock (timerLock)
        {
            timer?.Change(configuration.LockTimeout ?? 300000, Timeout.Infinite);
        }
    }

    private void StopTimer()
    {
        lock (timerLock)
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }

    private void OnTimedEvent(object? state)
    {
        if (isOpen)
        {
            publisher.PublishUI(new ClosedEventArgs<Wallet>());
        }
    }

    private void OnUserInteracted(object? sender, UserInteractedEventArgs args)
    {
        if (isOpen)
        {
            ResetTimer();
        }
    }

    public void Dispose()
    {
        userInteraction.UserInteracted -= OnUserInteracted;
        timer?.Dispose();
    }
}
