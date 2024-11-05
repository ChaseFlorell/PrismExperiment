using System.Windows.Input;
using DryIoc;
using Prism.Commands;
using Prism.Navigation;
using PrismExperiment.Base;
using PrismExperiment.Dependencies;

namespace PrismExperiment;

public class MainPageViewModel : ViewModelBase
{
    /// <inheritdoc />
    public MainPageViewModel(INavigationService navigationService, IResolverContext resolverContext, IDummyDependency dummyDependency) : base(resolverContext, dummyDependency)
    {
        NavigationParameters workflowParameters = new() { { KnownNavigationParameters.UseModalNavigation, true } };
        NavigateToAlpha = new AsyncDelegateCommand(() => navigationService.NavigateAsync(NavigationUrl.NewNavigationPage(NavigationUrl.Alpha), workflowParameters));
        NavigateToBravo = new AsyncDelegateCommand(() => navigationService.NavigateAsync(NavigationUrl.NewNavigationPage(NavigationUrl.Bravo), workflowParameters));
    }

    public ICommand NavigateToBravo { get; }
    public ICommand NavigateToAlpha { get; }
}