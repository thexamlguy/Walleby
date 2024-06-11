using Microsoft.EntityFrameworkCore;
using System.Linq;
using Toolkit.Foundation;
using Wallet.Data;

namespace Wallet;

public class CountCategoriesHandler(IDbContextFactory<WalletContext> dbContextFactory) : 
    IHandler<CountEventArgs<ItemCategory>, IReadOnlyCollection<(string, int)>>
{
    public async Task<IReadOnlyCollection<(string, int)>> Handle(CountEventArgs<ItemCategory> args, 
        CancellationToken cancellationToken)
    {
        using WalletContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var stateCounts = await context.Items
            .GroupBy(i => i.State)
            .Select(g => new
            {
                g.Key,
                Count = g.Count()
            })
            .ToListAsync(cancellationToken: cancellationToken);

        var categoryCounts = await context.Items.Where(x => !string.IsNullOrEmpty(x.Category))
            .GroupBy(i => i.Category)
            .Select(g => new
            {
                g.Key,
                Count = g.Count()
            })
            .ToListAsync(cancellationToken: cancellationToken);

        int allCount = stateCounts.Where(x => x.Key != 2).Sum(x => x.Count);
        int favouritesCount = stateCounts.Where(x => x.Key == 1).Sum(x => x.Count);
        int archiveCount = stateCounts.Where(x => x.Key == 2).Sum(x => x.Count);

        List<(string, int)> combinedCounts =
        [
            new("Favourites", favouritesCount),
            new("Archive", archiveCount),
            new("All", allCount)
        ];

        foreach ((string key, int count) in categoryCounts.Select(x => (x.Key, x.Count)))
        {
            combinedCounts.Add((key, count));
        }

        return combinedCounts;
    }
}
