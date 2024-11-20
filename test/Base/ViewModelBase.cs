using System.Threading.Tasks;
using DryIoc;
using Prism.Navigation;
using PrismExperiment.Dependencies;

namespace PrismExperiment.Base;

public abstract class ViewModelBase : IInitializeAsync
{
    protected ViewModelBase(IResolverContext resolverContext, IDummyDependency dummyDependency)
    {
        _resolverContext = resolverContext;
        _dummyDependency = dummyDependency;
    }

    public string ScopeName => _resolverContext.CurrentScope?.Name?.ToString() ?? "<No Scope Name>";
    public string ParentScopeName => _resolverContext.Parent?.CurrentScope?.Name?.ToString() ?? "<No Parent Scope Name>";
    public string InstanceId => _dummyDependency.InstanceId;
    public string DependencyScope => _dummyDependency.ScopeName;

    protected virtual Task InitializeAsync(INavigationParameters parameters) => Task.CompletedTask;

    /// <inheritdoc />
    Task IInitializeAsync.InitializeAsync(INavigationParameters parameters) => InitializeAsync(parameters);

    private readonly IResolverContext _resolverContext;
    private readonly IDummyDependency _dummyDependency;
}