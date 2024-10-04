using DryIoc;
using PrismExperiment.Base;

namespace PrismExperiment.Pages.Alpha.Leaf;

public class BravoLeafViewModel : ViewModelBase
{
    /// <inheritdoc />
    public BravoLeafViewModel(INavigationService navigationService, IResolverContext resolverContext) : base(navigationService, resolverContext)
    {
    }
}