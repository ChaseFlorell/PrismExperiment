using System.Windows.Input;
using DryIoc;
using PrismExperiment.Base;

namespace PrismExperiment.Pages.Alpha.Leaf;

public class AlphaLeafViewModel : ViewModelBase
{
    /// <inheritdoc />
    public AlphaLeafViewModel(INavigationService navigationService, IResolverContext resolverContext) : base(navigationService, resolverContext)
    {
        NavigateToLeaf = new AsyncDelegateCommand(() => navigationService.NavigateAsync(NavigationUrl.BravoLeaf));
    }

    public ICommand NavigateToLeaf { get; set; }
}