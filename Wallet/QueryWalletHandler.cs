﻿using LinqKit;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;
using Wallet.Data;

namespace Wallet;

public class QueryWalletHandler(IDbContextFactory<WalletContext> dbContextFactory) :
    IHandler<QueryEventArgs<Wallet<(string, string)>>, IReadOnlyCollection<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)>>
{
    public async Task<IReadOnlyCollection<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)>>
        Handle(QueryEventArgs<Wallet<(string, string)>> args, CancellationToken cancellationToken)
    {
        List<(Guid Id, string? Name, string Category, bool Favourite, bool Archived)> items = [];
        if (args.Sender is Wallet<(string, string)> Wallet)
        {
            (string filter, string text) = Wallet.Value;

            ExpressionStarter<ItemEntity> predicate =
                PredicateBuilder.New<ItemEntity>(true);

            if (filter is { Length: <= 0 })
            {
                return items;
            }

            if (filter == "All")
            {
                predicate = predicate.And(x => x.State != 2);
            }
            else if (filter == "Favourites")
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
            var results = await context.Set<ItemEntity>()
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