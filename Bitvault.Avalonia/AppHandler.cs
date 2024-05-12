using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading;
using System.Threading.Tasks;
using Toolkit.Foundation;

namespace Bitvault.Avalonia;

public class AppHandler(IPublisher publisher) :
    INotificationHandler<StartedEventArgs>
{
    public async Task Handle(StartedEventArgs args, CancellationToken cancellationToken = default)
    {
        if (Application.Current is Application application)
        {
            if (application.ApplicationLifetime is IApplicationLifetime lifetime)
            {
                await publisher.Publish(new NavigateEventArgs(lifetime is IClassicDesktopStyleApplicationLifetime ? "MainWindow" : "Main",
                    lifetime is IClassicDesktopStyleApplicationLifetime ? typeof(IClassicDesktopStyleApplicationLifetime) :
                    typeof(ISingleViewApplicationLifetime)), cancellationToken);
            }
        }
    }
}