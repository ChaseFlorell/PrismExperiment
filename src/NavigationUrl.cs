using Prism.Navigation;

namespace PrismExperiment;

internal static class NavigationUrl
{
    public static string NewNavigationPage(string page) => $"{nameof(NavigationPage)}/{page}";
    public static string Main => nameof(Main);
    public static string Alpha => nameof(Alpha);
    public static string Bravo => nameof(Bravo);
    public static string AlphaLeaf => nameof(AlphaLeaf);
    public static string BravoLeaf => nameof(BravoLeaf);
}

internal static class NavigationResultExtensions
{
    public static Task<INavigationResult> HandleNavigation(this Task<INavigationResult> navigationResult) =>
        HandleNavigation(navigationResult, task => task.HandleFailedNavigationResult());

    public static Task<INavigationResult> HandleNavigation(this Task<INavigationResult> navigationResult,
        Func<Task<INavigationResult>, INavigationResult> continuationFunction) =>
        navigationResult.ContinueWith<INavigationResult>(task => continuationFunction(task));

    private static INavigationResult HandleFailedNavigationResult(this Task<INavigationResult> navigationResult)
    {
        if (!navigationResult.Result.Success)
        {
            Console.WriteLine(navigationResult.Result.Exception);
        }

        return navigationResult.Result;
    }
}