using System.Windows.Input;
using DryIoc;
using Prism.Commands;
using Prism.Navigation;
using PrismExperiment.Base;
using PrismExperiment.Dependencies;

namespace PrismExperiment.Pages.Alpha;

public class AlphaWorkflowViewModel : ViewModelBase
{
    /// <inheritdoc />
    public AlphaWorkflowViewModel(INavigationService navigationService, IResolverContext resolverContext, IDummyDependency dummyDependency) : base(navigationService, resolverContext, dummyDependency)
    {
        NavigateToLeaf = new AsyncDelegateCommand(() => navigationService.NavigateAsync(NavigationUrl.AlphaLeaf));
    }

    public ICommand NavigateToLeaf { get; set; }
}