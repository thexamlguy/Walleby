using System.Diagnostics;
using Toolkit.Foundation;

namespace Wallet;

public class HyperlinkHandler : 
    INotificationHandler<CreateEventArgs<Hyperlink>>
{
    public Task Handle(CreateEventArgs<Hyperlink> args)
    {
        if (args.Sender is Hyperlink hyperlink && hyperlink.Value is { Length: > 0 } value)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = value, UseShellExecute = true });
            }
            catch
            {

            }
        }

        return Task.CompletedTask;
    }
}
