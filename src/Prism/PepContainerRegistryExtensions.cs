namespace PrismExperiment.Prism;

public static class PepContainerRegistryExtensions
{
    public static IContainerRegistry RegisterForScopedNavigation<TView, TViewModel>(this IContainerRegistry container, string name = "")
        where TView : Page
    {
        container.RegisterForNavigation<TView, TViewModel>(name);

        ScopedDependencies.Add(name);
        return container;
    }

    internal static HashSet<string> ScopedDependencies { get; } = [];
}