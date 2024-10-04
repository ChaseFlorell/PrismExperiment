using DryIoc;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace PrismExperiment.Base;

public abstract class ViewModelBase : IInitializeAsync
{
    protected ViewModelBase(INavigationService navigationService, IResolverContext resolverContext)
    {
        _navigationService = navigationService;
        _resolverContext = resolverContext;
    }


    public string ScopeName => _resolverContext.CurrentScope?.Name?.ToString() ?? "<No Scope Name>";

    protected virtual Task InitializeAsync(INavigationParameters parameters) => Task.CompletedTask;

    /// <inheritdoc />
    Task IInitializeAsync.InitializeAsync(INavigationParameters parameters) => InitializeAsync(parameters);

    private readonly INavigationService _navigationService;
    private readonly IResolverContext _resolverContext;
}