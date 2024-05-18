using Bitvault.Data;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class QueryContainerHandler(IDbContextFactory<ContainerDbContext> dbContextFactory) :
    IHandler<RequestEventArgs<QueryContainerConfiguration>, IReadOnlyCollection<(int Id, string? Name)>>
{
    public async Task<IReadOnlyCollection<(int Id, string? Name)>> Handle(RequestEventArgs<QueryContainerConfiguration> args, 
        CancellationToken cancellationToken)
    {
        List<(int Id, string? Name)> items = [];

        if (args.Value is  QueryContainerConfiguration queryConfiguration)
        {
            ExpressionStarter<ItemEntry> predicate = 
                PredicateBuilder.New<ItemEntry>(true);

            if (queryConfiguration.Filter == "All")
            {
                predicate = predicate.And(x => x.State != 3);
            }

            if (queryConfiguration.Filter == "Starred")
            {
                predicate = predicate.And(x => x.State != 3 && x.State == 2);
            }

            if (queryConfiguration.Filter == "Archive")
            {
                predicate = predicate.And(x => x.State == 3);
            }

            if (queryConfiguration.Query is { Length: > 0} query)
            {
                predicate = predicate.And(x => EF.Functions.Like(x.Name, $"%{query}%"));
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

            foreach (var result in results)
            {
                items.Add(new(result.Id, result.Name));
            }
        }

        return items;
    }
}
