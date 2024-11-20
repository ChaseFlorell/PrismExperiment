using System.Windows.Input;
using DryIoc;
using Prism.Commands;
using Prism.Navigation;
using PrismExperiment.Base;
using PrismExperiment.Dependencies;

namespace PrismExperiment.Pages.Alpha.Leaf;

public class AlphaLeafViewModel : ViewModelBase
{
    /// <inheritdoc />
    public AlphaLeafViewModel(INavigationService navigationService, IResolverContext resolverContext, IDummyDependency dummyDependency)
        : base(resolverContext, dummyDependency) =>
        NavigateToLeaf = new AsyncDelegateCommand(() => navigationService.NavigateAsync(NavigationUrl.BravoLeaf));

    public ICommand NavigateToLeaf { get; set; }
}