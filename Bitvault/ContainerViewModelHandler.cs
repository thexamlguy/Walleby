using Toolkit.Foundation;

namespace Bitvault;

public class ContainerViewModelHandler(IServiceFactory factory,
    IPublisher publisher) :
    INotificationHandler<Enumerate<ItemNavigationViewModel, ContainerViewModelConfiguration>>
{
    public async Task Handle(Enumerate<ItemNavigationViewModel, ContainerViewModelConfiguration> args,
        CancellationToken cancellationToken = default)
    {
        if (args.Options?.Filter is "All")
        {
            for (int i = 0; i < 100; i++)
            {
                if (factory.Create<ItemNavigationViewModel>("Name " + i, "Description " + 1) is ItemNavigationViewModel viewModel)
                {
                    await publisher.Publish(new Create<ItemNavigationViewModel>(viewModel),
                        nameof(ContainerViewModel), cancellationToken);
                }
            }
        }

        if (args.Options?.Filter is "Starred")
        {
            for (int i = 0; i < 10; i++)
            {
                if (factory.Create<ItemNavigationViewModel>("Name " + i, "Description " + 1) is ItemNavigationViewModel viewModel)
                {
                    await publisher.Publish(new Create<ItemNavigationViewModel>(viewModel),
                        nameof(ContainerViewModel), cancellationToken);
                }
            }
        }

        if (args.Options?.Filter is "Archive")
        {
            for (int i = 0; i < 1000; i++)
            {
                if (factory.Create<ItemNavigationViewModel>("Name " + i, "Description " + 1) is ItemNavigationViewModel viewModel)
                {
                    await publisher.Publish(new Create<ItemNavigationViewModel>(viewModel),
                        nameof(ContainerViewModel), cancellationToken);
                }
            }
        }
    }
}