using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading;
using System.Threading.Tasks;
using Toolkit.Foundation;

namespace Bitvault.Avalonia;

public class AppHandler(IPublisher publisher) :
    INotificationHandler<StartedEventArgs>
{
    public Task Handle(StartedEventArgs args)
    {
        if (Application.Current is Application application)
        {
            if (application.ApplicationLifetime is IApplicationLifetime lifetime)
            {
                publisher.Publish(new NavigateEventArgs(lifetime is IClassicDesktopStyleApplicationLifetime ? "MainWindow" : "Main",
                    lifetime is IClassicDesktopStyleApplicationLifetime ? typeof(IClassicDesktopStyleApplicationLifetime) :
                    typeof(ISingleViewApplicationLifetime)));
            }
        }

        return Task.CompletedTask;
    }
}