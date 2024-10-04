using System.Windows.Input;
using DryIoc;
using PrismExperiment.Base;

namespace PrismExperiment;

public class MainPageViewModel : ViewModelBase
{
    /// <inheritdoc />
    public MainPageViewModel(INavigationService navigationService, IResolverContext resolverContext) : base(navigationService, resolverContext)
    {
        NavigationParameters WorkflowParameters() => new() { { KnownNavigationParameters.UseModalNavigation, true } };
        NavigateToAlpha = new AsyncDelegateCommand(() => navigationService.NavigateAsync(NavigationUrl.NewNavigationPage(NavigationUrl.Alpha), WorkflowParameters()));
        NavigateToBravo = new AsyncDelegateCommand(() => navigationService.NavigateAsync(NavigationUrl.NewNavigationPage(NavigationUrl.Bravo), WorkflowParameters()));
    }

    public ICommand NavigateToBravo { get; }
    public ICommand NavigateToAlpha { get; }
}