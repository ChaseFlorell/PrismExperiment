using Prism;
using Prism.Common;
using Prism.Navigation;
using PrismExperiment.Dependencies;
using PrismExperiment.Pages.Alpha;
using PrismExperiment.Pages.Alpha.Leaf;
using PrismExperiment.Pages.Bravo;
using PrismExperiment.Prism;

namespace PrismExperiment;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp() => MauiApp
        .CreateBuilder()
        .UseMauiApp<App>()
        .UsePrism(ConfigurePrism)
        .ConfigureFonts(ConfigureFonts)
        .Build();

    private static void ConfigureFonts(IFontCollection fonts) => fonts
        .AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
        .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

    private static void ConfigurePrism(PrismAppBuilder builder) => builder
        .CreateWindow((_, navigation) => navigation.NavigateAsync(NavigationUrl.NewNavigationPage(NavigationUrl.Main)))
        .RegisterTypes(RegisterTypes);

    private static void RegisterTypes(IContainerRegistry containerRegistry) => containerRegistry
        // custom magic
        .Register<INavigationService, PepPageNavigationService>()
        .RegisterScoped<IPepContainerProvider, PepContainerProvider>()
        .RegisterScoped<IContainerProvider, PepContainerProvider>()
        .RegisterScoped<IScopedProvider, PepContainerProvider>()
        .RegisterScoped<IPageAccessor, PepPageAccessor>()
        .Register<INavigationRegistry, PepNavigationRegistry>()
        // end custom magic
        // junk
        .RegisterScoped<IDummyDependency, DummyDependency>()
        // end junk
        .RegisterForNavigation<MainPage, MainPageViewModel>(NavigationUrl.Main)
        .RegisterForScopedNavigation<AlphaWorkflow, AlphaWorkflowViewModel>(NavigationUrl.Alpha)
        .RegisterForScopedNavigation<BravoWorkflow, BravoWorkflowViewModel>(NavigationUrl.Bravo)
        .RegisterForNavigation<AlphaLeaf, AlphaLeafViewModel>(NavigationUrl.AlphaLeaf)
        .RegisterForNavigation<BravoLeaf, BravoLeafViewModel>(NavigationUrl.BravoLeaf);
}