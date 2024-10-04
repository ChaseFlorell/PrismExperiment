using DryIoc;
using PrismExperiment.Base;

namespace PrismExperiment.Pages.Bravo;

public class BravoWorkflowViewModel : ViewModelBase
{
    /// <inheritdoc />
    public BravoWorkflowViewModel(INavigationService navigationService, IResolverContext resolverContext) : base(navigationService, resolverContext)
    {
    }
}