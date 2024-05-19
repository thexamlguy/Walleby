using Bitvault.Data;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Toolkit.Foundation;

namespace Bitvault;

public class QueryItemHandler(IDbContextFactory<ContainerDbContext> dbContextFactory) :
    IHandler<RequestEventArgs<QueryItemConfiguration>, (int Id, string? Name)>
{
    public Task<(int Id, string? Name)> Handle(RequestEventArgs<QueryItemConfiguration> args, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record QueryItemConfiguration
{
    public int Id { get; set; }
}

public class QueryContainerHandler(IDbContextFactory<ContainerDbContext> dbContextFactory) :
    IHandler<RequestEventArgs<QueryContainerConfiguration>, IReadOnlyCollection<(int Id, string? Name, bool Favourite, bool Archived)>>
{
    public async Task<IReadOnlyCollection<(int Id, string? Name, bool Favourite, bool Archived)>> Handle(RequestEventArgs<QueryContainerConfiguration> args, 
        CancellationToken cancellationToken)
    {
        List<(int Id, string? Name, bool Favourite, bool Archived)> items = [];

        if (args.Value is  QueryContainerConfiguration queryConfiguration)
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
                    x.Name,
                    Favourite = x.State == 1,
                    Archived = x.State == 2
                }).OrderBy(x => x.Name).ToListAsync();
            });

            foreach (var result in results)
            {
                items.Add(new(result.Id, result.Name, result.Favourite, result.Archived));
            }
        }

        return items;
    }
}
