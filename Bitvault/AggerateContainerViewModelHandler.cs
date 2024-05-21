using Microsoft.Extensions.DependencyInjection;
using Toolkit.Foundation;

namespace Bitvault;

public class AggerateContainerViewModelHandler(IMediator mediator,
    IServiceProvider serviceProvider,
    ICache<Item> cache,
    IPublisher publisher) :
    INotificationHandler<AggerateEventArgs<ItemNavigationViewModel, ContainerViewModelConfiguration>>
{
    public async Task Handle(AggerateEventArgs<ItemNavigationViewModel, 
        ContainerViewModelConfiguration> args)
    {
        if (args.Options is ContainerViewModelConfiguration configuration)
        {
            cache.Clear();
            bool selected = true;

            if (await mediator.Handle<RequestEventArgs<QueryContainerConfiguration>,
                IReadOnlyCollection<(int Id, string Name, bool Favourite, bool Archived)>>(Request.As(new QueryContainerConfiguration 
                { 
                    Filter = configuration.Filter,
                    Query = configuration.Query 
                })) is IReadOnlyCollection<(int Id, string Name, bool Favourite, bool Archived)> results)
            {
                foreach ((int Id, string Name, bool Favourite, bool Archived) in results)
                {
                    IServiceScope serviceScope = serviceProvider.CreateScope();
                    IServiceFactory serviceFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceFactory>();
                    IValueStore<Item> valueStore = serviceScope.ServiceProvider.GetRequiredService<IValueStore<Item>>();

                    if (serviceFactory.Create<ItemNavigationViewModel>(Id, Name, "Description", selected, Favourite, Archived) is ItemNavigationViewModel viewModel)
                    {
                        Item item = new() { Id = Id, Name = Name };
                        valueStore.Set(item);

                        cache.Add(item);
                        publisher.Publish(Create.As(viewModel), nameof(ContainerViewModel));
                    }

                    selected = false;
                }
            }
        }
    }
}
