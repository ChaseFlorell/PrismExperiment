using DryIoc;

namespace PrismExperiment.Base;

public class WorkflowViewModelBase : ViewModelBase
{
    /// <inheritdoc />
    public WorkflowViewModelBase(INavigationService navigationService, IResolverContext resolverContext) : base(navigationService, resolverContext)
    {
    }
}