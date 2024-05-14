using Bitvault.Data;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class ContainerViewModelHandler(IDbContextFactory<ContainerDbContext> dbContextFactory,
    IServiceProvider serviceProvider,
    ICache<Item> cache,
    IPublisher publisher) :
    INotificationHandler<Enumerate<ItemNavigationViewModel, ContainerViewModelConfiguration>>
{
    public async Task Handle(Enumerate<ItemNavigationViewModel, ContainerViewModelConfiguration> args)
    {
        if (args.Options is ContainerViewModelConfiguration configuration)
        {
            cache.Clear();

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

            var results = await Task.Run(async () =>
            {
                using ContainerDbContext context = dbContextFactory.CreateDbContext();
                return await context.Set<ItemEntry>().Where(predicate).Select(x => new
                {
                    x.Id,
                    x.Name
                }).OrderBy(x => x.Name).ToListAsync();
            });

            bool selected = true;
            foreach (var result in results)
            {
                IServiceScope serviceScope = serviceProvider.CreateScope();
                IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();
                IValueStore<Item> valueStore = serviceScope.ServiceProvider.GetRequiredService<IValueStore<Item>>();

                if (serviceFactory.Create<ItemNavigationViewModel>(result.Id, result.Name, "Description " + 1, selected) is ItemNavigationViewModel viewModel)
                {
                    Item item = new() { Id = result.Id, Name = result.Name };
                    valueStore.Set(item);

                    publisher.Publish(Create.As(viewModel), nameof(ContainerViewModel));
                }

                selected = false;
            }
        }
    }
}