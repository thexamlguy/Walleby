using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class AggerateMainViewModelHandler(IPublisher publisher,
    ILockerHostCollection lockers) :
    INotificationHandler<AggerateEventArgs<IMainNavigationViewModel>>
{
    public Task Handle(AggerateEventArgs<IMainNavigationViewModel> args)
    {
        bool selected = true;
        foreach (IComponentHost locker in lockers.OrderBy(x => x.Services.GetRequiredService<IConfigurationDescriptor<LockerConfiguration>>()
            is IConfigurationDescriptor<LockerConfiguration> descriptor ? descriptor.Name : null))
        {
            if (locker.Services.GetRequiredService<IConfigurationDescriptor<LockerConfiguration>>() is IConfigurationDescriptor<LockerConfiguration> descriptor)
            {
                if (locker.Services.GetRequiredService<IServiceFactory>() is IServiceFactory factory)
                {
                    if (factory.Create<LockerNavigationViewModel>(descriptor.Name, selected) is LockerNavigationViewModel viewModel)
                    {
                        publisher.Publish(Create.As<IMainNavigationViewModel>(viewModel),
                            nameof(MainViewModel));

                        selected = false;
                    }
                }
            }
        }

        return Task.CompletedTask;
    }
}