using Toolkit.Foundation;

namespace Wallet;

public class CreateItemViewModelHandler(IServiceFactory serviceFactory,
    IDecoratorService<ItemHeaderConfiguration> itemHeaderConfigurationDecorator) : 
    IHandler<CreateEventArgs<ItemViewModel>, ItemViewModel?>
{
    public Task<ItemViewModel?> Handle(CreateEventArgs<ItemViewModel> args,
        CancellationToken cancellationToken)
    {
        string? name = "";
        ItemState? state = null;

        if (args.Parameters is { Length: 5 })
        {
            (name, bool _, bool _, bool _, state) = args.Parameters.CreateValueTuple<string, bool, bool, bool, ItemState>();
        }

        if (args.Parameters is { Length: 2 })
        {
            (bool _, state) = args.Parameters.CreateValueTuple<bool, ItemState>();
        }

        ItemHeaderConfiguration configuration = new()
        {
            Name = name
        };

        itemHeaderConfigurationDecorator.Set(configuration);

        if (serviceFactory.Create<ItemViewModel>(args => args.Initialize(), args.Parameters) is ItemViewModel itemViewModel)
        {
            itemViewModel.Add<ItemHeaderViewModel>(configuration, state, "", name);
            itemViewModel.Add<ItemContentViewModel>();

            return Task.FromResult<ItemViewModel?>(itemViewModel);
        }    

        return Task.FromResult(default(ItemViewModel));
    }
}
