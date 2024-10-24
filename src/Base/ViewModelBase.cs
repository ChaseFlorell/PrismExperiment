using DryIoc;
using PrismExperiment.Dependencies;

namespace PrismExperiment.Base;

public abstract class ViewModelBase : IInitializeAsync
{
    protected ViewModelBase(INavigationService navigationService, IResolverContext resolverContext, IDummyDependency dummyDependency)
    {
        _navigationService = navigationService;
        _resolverContext = resolverContext;
        _dummyDependency = dummyDependency;
    }


    public string ScopeName => _resolverContext.CurrentScope?.Name?.ToString() ?? "<No Scope Name>";
    public string InstanceId => _dummyDependency.InstanceId;

    protected virtual Task InitializeAsync(INavigationParameters parameters) => Task.CompletedTask;

    /// <inheritdoc />
    Task IInitializeAsync.InitializeAsync(INavigationParameters parameters) => InitializeAsync(parameters);

    private readonly INavigationService _navigationService;
    private readonly IResolverContext _resolverContext;
    private readonly IDummyDependency _dummyDependency;
}