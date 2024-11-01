namespace PrismExperiment;

internal static class NavigationUrl
{
    public static string NewNavigationPage(string page) => $"NavigationPage/{page}";
    public static string Main => nameof(Main);
    public static string Alpha => nameof(Alpha);
    public static string Bravo => nameof(Bravo);
    public static string AlphaLeaf => nameof(AlphaLeaf);
    public static string BravoLeaf => nameof(BravoLeaf);
}