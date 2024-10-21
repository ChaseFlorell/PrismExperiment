using System;
using DryIoc;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Prism;
using Prism.Navigation;
using PrismExperiment.Dependencies;
using PrismExperiment.Pages.Alpha;
using PrismExperiment.Pages.Alpha.Leaf;
using PrismExperiment.Pages.Bravo;
using IContainerRegistry = Pep.Ioc.IContainerRegistry;

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

    private static void RegisterTypes(IContainerRegistry containerRegistry) =>
        containerRegistry
            // >> Dummy Dependencies
            .RegisterScoped<IDummyDependency, DummyDependency>()
            // << Dummy Dependencies
            // >> Navigation
            .RegisterForNavigation<MainPage, MainPageViewModel>(NavigationUrl.Main)
            .RegisterForNavigation<AlphaWorkflow, AlphaWorkflowViewModel>(NavigationUrl.Alpha)
            .RegisterForNavigation<BravoWorkflow, BravoWorkflowViewModel>(NavigationUrl.Bravo)
            .RegisterForNavigation<AlphaLeaf, AlphaLeafViewModel>(NavigationUrl.AlphaLeaf)
            .RegisterForNavigation<BravoLeaf, BravoLeafViewModel>(NavigationUrl.BravoLeaf)
            // << Navigation
            .GetContainer()
            .Register<object>(
                made: Made.Of(req => typeof(LoggingDecorator)
                    .SingleMethod(nameof(LoggingDecorator.Decorate))
                    .MakeGenericMethod(req.ServiceType, typeof(IResolverContext))),
                setup: Setup.DecoratorWith(r => !r.ServiceType.Namespace!.StartsWith("DryIoc"), allowDisposableTransient: true));
}

public static class LoggingDecorator
{
    public static T1 Decorate<T1, T2>(T1 service, T2 resolverContext)
    {
        var serviceType = typeof(T1).Name;
        var implementationType = service?.GetType() ?? throw new ArgumentException(nameof(service));
        Console.WriteLine($"Created: [ServiceType: {serviceType} Concretion: {implementationType.Name}, Hash: {service.GetHashCode()}, Scope: {resolverContext}]");
        return service;
    }
}