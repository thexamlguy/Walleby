using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class LockerActivatedHandler(ILockerHostCollection lockers,
    IPublisher publisher) :
    INotificationHandler<ActivatedEventArgs<IComponentHost>>
{
    public Task Handle(ActivatedEventArgs<IComponentHost> args)
    {
        if (args.Value is IComponentHost locker)
        {
            List<IComponentHost> sortedLockers = [.. lockers, locker];
            sortedLockers = [.. sortedLockers.OrderBy(x => x.GetConfiguration<LockerConfiguration>() is LockerConfiguration configuration ? configuration.Name : null)];

            int index = sortedLockers.IndexOf(locker);

            if (locker.Services.GetRequiredService<LockerConfiguration>() is LockerConfiguration configuration)
            {
                if (locker.Services.GetRequiredService<IServiceFactory>() is IServiceFactory serviceFactory)
                {
                    if (serviceFactory.Create<LockerNavigationViewModel>(configuration.Name) is LockerNavigationViewModel viewModel)
                    {
                        publisher.Publish(new InsertEventArgs<IMainNavigationViewModel>(index, viewModel),
                            nameof(MainViewModel));
                    }
                }
            }
        }

        return Task.CompletedTask;
    }
}