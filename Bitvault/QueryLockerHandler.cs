using Bitvault.Data;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class QueryLockerHandler(IDbContextFactory<LockerContext> dbContextFactory) :
    IHandler<RequestEventArgs<QueryLockerConfiguration>, IReadOnlyCollection<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)>>
{
    public async Task<IReadOnlyCollection<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)>> Handle(RequestEventArgs<QueryLockerConfiguration> args,
        CancellationToken cancellationToken)
    {
        List<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)> items = [];

        if (args.Value is QueryLockerConfiguration queryConfiguration)
        {
            ExpressionStarter<ItemEntry> predicate =
                PredicateBuilder.New<ItemEntry>(true);

            if (queryConfiguration.Filter == "All")
            {
                predicate = predicate.And(x => x.State != 2);
            }

            if (queryConfiguration.Filter == "Starred")
            {
                predicate = predicate.And(x => x.State != 2 && x.State == 1);
            }

            if (queryConfiguration.Filter == "Archive")
            {
                predicate = predicate.And(x => x.State == 2);
            }

            if (queryConfiguration.Query is { Length: > 0 } query)
            {
                predicate = predicate.And(x => EF.Functions.Like(x.Name, $"%{query}%"));
            }

            var results = await Task.Run(async () =>
            {
                using LockerContext context = dbContextFactory.CreateDbContext();
                return await context.Set<ItemEntry>()
                    .Where(predicate)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Category,
                        Favourite = x.State == 1,
                        Archived = x.State == 2
                    }).ToListAsync();
            });

            foreach (var result in results.OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase))
            {
                items.Add(new(result.Id, result.Name, result.Category, result.Favourite, result.Archived));
            }
        }

        return items;
    }
}