using DryIoc;
using Microsoft.Maui.LifecycleEvents;
using Pep.Ioc;
using Prism;
using Prism.Controls;
using Prism.Mvvm;
using Prism.Navigation;
using PrismExperiment.Dependencies;
using PrismExperiment.Pages.Alpha;
using PrismExperiment.Pages.Alpha.Leaf;
using PrismExperiment.Pages.Bravo;
using IContainer = DryIoc.IContainer;

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
        .RegisterTypes(RegisterTypes)
        .CreateWindow((_, navigation) => navigation.NavigateAsync(NavigationUrl.NewNavigationPage(NavigationUrl.Main)).HandleNavigation());

    private static void RegisterTypes(IContainer containerRegistry)
    {
        containerRegistry.Register<ILifecycleEventService, LifecycleEventService>();
        // >> Dummy Dependencies
        containerRegistry.Register<IDummyDependency, DummyDependency>(Reuse.Scoped);
        // << Dummy Dependencies
        // >> Navigation
        containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(NavigationUrl.Main);
        containerRegistry.RegisterForNavigation<AlphaWorkflow, AlphaWorkflowViewModel>(NavigationUrl.Alpha);
        containerRegistry.RegisterForNavigation<BravoWorkflow, BravoWorkflowViewModel>(NavigationUrl.Bravo);
        containerRegistry.RegisterForNavigation<AlphaLeaf, AlphaLeafViewModel>(NavigationUrl.AlphaLeaf);
        containerRegistry.RegisterForNavigation<BravoLeaf, BravoLeafViewModel>(NavigationUrl.BravoLeaf);
        containerRegistry.RegisterDelegate(() => new PrismNavigationPage());
        containerRegistry.RegisterInstance(new ViewRegistration
        {
            Name = nameof(NavigationPage),
            View = typeof(PrismNavigationPage),
            Type = ViewType.Page
        });
        // << Navigation
        containerRegistry
            .Register<object>(
                made: Made.Of(req => typeof(LoggingDecorator)
                    .SingleMethod(nameof(LoggingDecorator.Decorate))
                    .MakeGenericMethod(req.ServiceType, typeof(IResolverContext))),
                setup: Setup.DecoratorWith(r => !r.ServiceType.Namespace!.StartsWith("DryIoc"), allowDisposableTransient: true));
    }
}

public static class LoggingDecorator
{
    public static T1 Decorate<T1, T2>(T1 service, T2 resolverContext)
    {
        var serviceType = typeof(T1).Name;
        var implementationType = service?.GetType() ?? throw new ArgumentException("Service cannot be null", nameof(service));
        Console.WriteLine($"Created: [ServiceType: {serviceType} Concretion: {implementationType.Name}, Hash: {service.GetHashCode()}, Scope: {resolverContext}]");
        return service;
    }
}