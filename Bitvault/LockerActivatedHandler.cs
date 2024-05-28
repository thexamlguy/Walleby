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
            sortedLockers = [.. sortedLockers.OrderBy(x => x.Services.GetRequiredService<IConfigurationDescriptor<LockerConfiguration>>() is 
                IConfigurationDescriptor<LockerConfiguration> descriptor ? descriptor.Name : null)];

            int index = sortedLockers.IndexOf(locker);

            if (locker.Services.GetRequiredService<ConfigurationDescriptor<LockerConfiguration>>() is ConfigurationDescriptor<LockerConfiguration> descriptor)
            {
                if (locker.Services.GetRequiredService<IServiceFactory>() is IServiceFactory serviceFactory)
                {
                    if (serviceFactory.Create<LockerNavigationViewModel>(descriptor.Name) is LockerNavigationViewModel viewModel)
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