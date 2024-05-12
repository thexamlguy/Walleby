using Bitvault.Data;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class ContainerViewModelHandler(IDbContextFactory<ContainerDbContext> dbContextFactory,
    IServiceFactory factory,
    IPublisher publisher) :
    INotificationHandler<Enumerate<ItemNavigationViewModel, ContainerViewModelConfiguration>>
{
    public async Task Handle(Enumerate<ItemNavigationViewModel, ContainerViewModelConfiguration> args,
        CancellationToken cancellationToken = default)
    {
        var items = await Task.Run(async () =>
        {
            using ContainerDbContext context = dbContextFactory.CreateDbContext();
            return await context.Set<Data.Item>().Select(x => new 
            { 
                x.Name,
                x.State 
            }).Where(x => x.State != 3).ToListAsync();

        }, cancellationToken);

        foreach (var item in items)
        {
            if (factory.Create<ItemNavigationViewModel>(item.Name, "Description " + 1) is ItemNavigationViewModel viewModel)
            {
                await publisher.Publish(new Create<ItemNavigationViewModel>(viewModel),
                    nameof(ContainerViewModel), cancellationToken);
            }
        }
    }
}