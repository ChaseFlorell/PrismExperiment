using DryIoc;
using Prism.Navigation;
using PrismExperiment.Dependencies;

namespace PrismExperiment.Base;

public class WorkflowViewModelBase : ViewModelBase
{
    /// <inheritdoc />
    public WorkflowViewModelBase(INavigationService navigationService, IResolverContext resolverContext, IDummyDependency dummyDependency) : base(navigationService, resolverContext, dummyDependency)
    {
    }
}