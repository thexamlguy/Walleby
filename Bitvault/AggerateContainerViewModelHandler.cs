using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class AggerateContainerViewModelHandler(IMediator mediator,
    IServiceProvider serviceProvider,
    ICache<Item> cache,
    IPublisher publisher) :
    INotificationHandler<Aggerate<ItemNavigationViewModel, ContainerViewModelConfiguration>>
{
    public async Task Handle(Aggerate<ItemNavigationViewModel, 
        ContainerViewModelConfiguration> args)
    {
        if (args.Options is ContainerViewModelConfiguration configuration)
        {
            cache.Clear();
            bool selected = true;

            if (await mediator.Handle<RequestEventArgs<QueryContainerConfiguration>,
                IReadOnlyCollection<(int Id, string? Name)>>(Request.As(new QueryContainerConfiguration 
                { 
                    Filter = configuration.Filter,
                    Query = configuration.Query 
                })) is IReadOnlyCollection<(int Id, string? Name)> results)
            {
                foreach ((int Id, string? Name) in results)
                {
                    IServiceScope serviceScope = serviceProvider.CreateScope();
                    IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();
                    IValueStore<Item> valueStore = serviceScope.ServiceProvider.GetRequiredService<IValueStore<Item>>();

                    if (serviceFactory.Create<ItemNavigationViewModel>(Id, Name, "Description " + 1, selected) is ItemNavigationViewModel viewModel)
                    {
                        Item item = new() { Id = Id, Name = Name };
                        valueStore.Set(item);

                        publisher.Publish(Create.As(viewModel), nameof(ContainerViewModel));
                    }

                    selected = false;
                }
            }
        }
    }
}
