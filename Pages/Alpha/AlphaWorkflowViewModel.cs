using System.Windows.Input;
using DryIoc;
using PrismExperiment.Base;

namespace PrismExperiment.Pages.Alpha;

public class AlphaWorkflowViewModel : ViewModelBase
{
    /// <inheritdoc />
    public AlphaWorkflowViewModel(INavigationService navigationService, IResolverContext resolverContext) : base(navigationService, resolverContext)
    {
        NavigateToLeaf = new AsyncDelegateCommand(() => navigationService.NavigateAsync(NavigationUrl.AlphaLeaf));
    }

    public ICommand NavigateToLeaf { get; set; }
}