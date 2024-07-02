using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Wallet;

public class ItemCollectionViewModelActivatedHandler(IMediator mediator,
    IServiceProvider serviceProvider,
    ICache<Item<(Guid, string)>> cache,
    IPublisher publisher) :
    INotificationHandler<ActivationEventArgs<ItemNavigationViewModel, ItemCollectionConfiguration>>
{
    public async Task Handle(ActivationEventArgs<ItemNavigationViewModel,
        ItemCollectionConfiguration> args)
    {
        if (args.Value is ItemCollectionConfiguration configuration)
        {
            cache.Clear();
            bool selected = true;

            IReadOnlyCollection<(Guid Id, string Name, string Category, bool Favourite, bool Archived)>? results = 
                await mediator.Handle<QueryEventArgs<Wallet<(string?, string?)>>,
                    IReadOnlyCollection<(Guid Id, string Name, string Category, bool Favourite,
                    bool Archived)>>(Query.As(new Wallet<(string?, string?)>((configuration.Filter, configuration.Query))));

            if (results is not null)
            {
                foreach ((Guid Id, string Name, string Category, bool Favourite, bool Archived) in results)
                {
                    IServiceScope serviceScope = serviceProvider.CreateScope();
                    IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();
                    IDecoratorService<Item<(Guid, string)>> decoratorService = serviceScope.ServiceProvider
                        .GetRequiredService<IDecoratorService<Item<(Guid, string)>>>();

                    if (serviceFactory.Create<ItemNavigationViewModel>(args => args.Initialize(), 
                        Id, Name, "Description", Category, selected, Favourite, Archived) 
                        is ItemNavigationViewModel viewModel)
                    {
                        Item<(Guid, string)> item = new((Id, Name));

                        decoratorService.Set(item);
                        cache.Add(item);

                        publisher.Publish(Create.As(viewModel), nameof(ItemCollectionViewModel));
                    }

                    selected = false;
                }
            }
        }
    }
}