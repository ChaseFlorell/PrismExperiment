using DryIoc;
using Prism.Navigation;
using PrismExperiment.Base;
using PrismExperiment.Dependencies;

namespace PrismExperiment.Pages.Bravo;

public class BravoWorkflowViewModel : ViewModelBase
{
    /// <inheritdoc />
    public BravoWorkflowViewModel(INavigationService navigationService, IResolverContext resolverContext, IDummyDependency dummyDependency) : base(navigationService, resolverContext, dummyDependency)
    {
    }
}