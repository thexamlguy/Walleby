using Wallet.Data;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Wallet;

public class QueryWalletHandler(IDbContextFactory<WalletContext> dbContextFactory) :
    IHandler<QueryEventArgs<Wallet<(string, string)>>, IReadOnlyCollection<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)>>
{
    public async Task<IReadOnlyCollection<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)>> 
        Handle(QueryEventArgs<Wallet<(string, string)>> args,CancellationToken cancellationToken)
    {
        List<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)> items = [];
        if (args.Value is Wallet<(string, string)> Wallet)
        {
            (string filter, string text) = Wallet.Sender;

            ExpressionStarter<ItemEntry> predicate =
                PredicateBuilder.New<ItemEntry>(true);

            if (filter is { Length: <= 0 })
            {
                return items;
            }

            if (filter == "All")
            {
                predicate = predicate.And(x => x.State != 2);
            }
            else if (filter == "Starred")
            {
                predicate = predicate.And(x => x.State != 2 && x.State == 1);
            }
            else if (filter == "Archive")
            {
                predicate = predicate.And(x => x.State == 2);
            }
            else
            {
                predicate = predicate.And(x => x.State != 2)
                    .And(x => EF.Functions.Like(x.Category, $"%{filter}%"));
            }


            if (text is { Length: > 0 })
            {
                predicate = predicate.And(x => EF.Functions.Like(x.Name, $"%{text}%"));
            }

            using WalletContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
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