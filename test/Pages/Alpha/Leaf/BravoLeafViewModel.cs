using DryIoc;
using Prism.Navigation;
using PrismExperiment.Base;
using PrismExperiment.Dependencies;

namespace PrismExperiment.Pages.Alpha.Leaf;

public class BravoLeafViewModel : ViewModelBase
{
    /// <inheritdoc />
    public BravoLeafViewModel(INavigationService navigationService, IResolverContext resolverContext, IDummyDependency dummyDependency) : base(resolverContext, dummyDependency)
    {
    }
}