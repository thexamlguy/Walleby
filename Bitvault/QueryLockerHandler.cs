using Bitvault.Data;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class QueryLockerHandler(IDbContextFactory<LockerContext> dbContextFactory) :
    IHandler<QueryEventArgs<Locker<(string, string)>>, IReadOnlyCollection<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)>>
{
    public async Task<IReadOnlyCollection<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)>> 
        Handle(QueryEventArgs<Locker<(string, string)>> args,CancellationToken cancellationToken)
    {
        List<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)> items = [];
        if (args.Value is Locker<(string, string)> locker)
        {
            (string filter, string text) = locker.Value;

            ExpressionStarter<ItemEntry> predicate =
                PredicateBuilder.New<ItemEntry>(true);

            if (filter == "All")
            {
                predicate = predicate.And(x => x.State != 2);
            }

            if (filter == "Starred")
            {
                predicate = predicate.And(x => x.State != 2 && x.State == 1);
            }

            if (filter == "Archive")
            {
                predicate = predicate.And(x => x.State == 2);
            }

            if (text is { Length: > 0 })
            {
                predicate = predicate.And(x => EF.Functions.Like(x.Name, $"%{text}%"));
            }

            using LockerContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            var results = await context.Set<ItemEntry>()
                .Where(predicate)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Category,
                    Favourite = x.State == 1,
                    Archived = x.State == 2
                }).ToListAsync(cancellationToken: cancellationToken);

            foreach (var result in results.OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase))
            {
                items.Add(new(result.Id, result.Name, result.Category, result.Favourite, result.Archived));
            }
        }

        return items;
    }
}