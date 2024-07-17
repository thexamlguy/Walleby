using Avalonia.Media;
using FluentAvalonia.UI.Windowing;
using System.Threading;
using System.Threading.Tasks;

namespace Wallet.Avalonia;

public class ApplicationSplashScreen :
    IApplicationSplashScreen
{
    public string? AppName { get; }

    public IImage? AppIcon { get; }

    public object SplashScreenContent => new SplashView();

    public int MinimumShowTime => 2000;

    public Task RunTasks(CancellationToken cancellationToken) =>
        Task.CompletedTask;
}