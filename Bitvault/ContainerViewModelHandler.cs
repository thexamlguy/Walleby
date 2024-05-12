using Bitvault.Data;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class ContainerViewModelHandler(IDbContextFactory<ContainerDbContext> dbContextFactory,
    IServiceProvider serviceProvider,
    IPublisher publisher) :
    INotificationHandler<Enumerate<ItemNavigationViewModel, ContainerViewModelConfiguration>>
{
    public async Task Handle(Enumerate<ItemNavigationViewModel, ContainerViewModelConfiguration> args,
        CancellationToken cancellationToken = default)
    {
        if (args.Options is ContainerViewModelConfiguration configuration)
        {
            ExpressionStarter<ItemEntry> predicate = PredicateBuilder.New<ItemEntry>(true);

            if (configuration.Filter == "All")
            {
                predicate = predicate.And(x => x.State != 3);
            }

            if (configuration.Filter == "Starred")
            {
                predicate = predicate.And(x => x.State != 3 && x.State == 2);
            }

            if (configuration.Filter == "Archive")
            {
                predicate = predicate.And(x => x.State == 3);
            }

            var items = await Task.Run(async () =>
            {
                using ContainerDbContext context = dbContextFactory.CreateDbContext();
                return await context.Set<ItemEntry>().Where(predicate).Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            }, cancellationToken);

            foreach (var item in items)
            {
                IServiceScope serviceScope = serviceProvider.CreateScope();
                IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();

                if (serviceFactory.Create<ItemNavigationViewModel>(item.Id, item.Name, "Description " + 1) is ItemNavigationViewModel viewModel)
                {
                    await publisher.Publish(new CreateEventArgs<ItemNavigationViewModel>(viewModel),
                        nameof(ContainerViewModel), cancellationToken);
                }
            }
        }
    }
}