using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.AppModel;
using Prism.Behaviors;
using Prism.Common;
using Prism.Controls;
using Prism.Dialogs;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Regions;
using Prism.Navigation.Regions.Adapters;
using Prism.Navigation.Xaml;
using Prism.Services;
using IContainer = DryIoc.IContainer;
using TabbedPage = Microsoft.Maui.Controls.TabbedPage;

namespace Prism;

/// <summary>
/// A builder for Prism with .NET MAUI cross-platform applications and services.
/// </summary>
public sealed class PrismAppBuilder
{
    private readonly List<Action<IResolverContext>> _initializations;
    private readonly IContainer _container;
    private Func<IResolverContext, INavigationService, Task> _createWindow;
    private Action<RegionAdapterMappings> _configureAdapters;
    private Action<IRegionBehaviorFactory> _configureBehaviors;

    internal PrismAppBuilder(IContainer container, MauiAppBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(container);
        ArgumentNullException.ThrowIfNull(builder);

        _container = container;
        _initializations = [];

        ViewModelCreationException.SetViewNameDelegate(view =>
        {
            if (view is BindableObject bindable)
                return Mvvm.ViewModelLocator.GetNavigationName(bindable);

            return $"View is not a BindableObject: '{view.GetType().FullName}";
        });

        // Ensure that the DialogStack is cleared when the Application is started.
        // This is primarily to help with Unit Tests
        IDialogContainer.DialogStack.Clear();
        MauiBuilder = builder;
        RegistrationCallback(container);
        MauiBuilder.ConfigureContainer(new PepServiceProviderFactory(container));

        //ContainerLocator.ResetContainer();
        //ContainerLocator.SetContainerExtension(containerExtension);

        container.RegisterInstance(this);
        container.Register<IMauiInitializeService, PrismInitializationService>(Reuse.Singleton);

        ConfigureViewModelLocator();
    }

    /// <summary>
    /// Gets the associated <see cref="MauiAppBuilder"/>.
    /// </summary>
    public MauiAppBuilder MauiBuilder { get; }

    private static void ConfigureViewModelLocator()
    {
        ViewModelLocationProvider.SetDefaultViewToViewModelTypeResolver(view =>
        {
            if (view is not BindableObject bindable)
                return null;

            return bindable.GetValue(ViewModelLocator.ViewModelProperty) as Type;
        });

        ViewModelLocationProvider.SetDefaultViewModelFactory(DefaultViewModelLocator);
    }

    internal static object DefaultViewModelLocator(object view, Type viewModelType)
    {
        try
        {
            if (view is not BindableObject bindable || bindable.BindingContext is not null)
                return null;

            var resolverContext = bindable.GetResolverContext();

            return resolverContext.Resolve(viewModelType/*, (typeof(IDispatcher), bindable.Dispatcher)*/);
        }
        catch (ViewModelCreationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ViewModelCreationException(view, ex);
        }
    }

    /// <summary>
    /// Provides a Delegate to register services with the <see cref="PrismAppBuilder"/>
    /// </summary>
    /// <param name="registerTypes">The delegate to register your services.</param>
    /// <returns>The <see cref="PrismAppBuilder"/>.</returns>
    public PrismAppBuilder RegisterTypes(Action<IContainer> registerTypes)
    {
        registerTypes(_container);
        return this;
    }

    /// <summary>
    /// Provides a Delegate to invoke when the App is initialized.
    /// </summary>
    /// <param name="action">The delegate to invoke.</param>
    /// <returns>The <see cref="PrismAppBuilder"/>.</returns>
    public PrismAppBuilder OnInitialized(System.Action<IResolverContext> action)
    {
        _initializations.Add(action);
        return this;
    }

    private bool _initialized;
    private IResolverContext _rootScope;

    internal void OnInitialized()
    {
        if (_initialized)
            return;

        _initialized = true;
        var logger = _container.Resolve<ILogger<PrismAppBuilder>>();
        var errors = new List<Exception>();

        _initializations.ForEach(action =>
        {
            try
            {
                action(_container);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Initialization Delegate.");
                errors.Add(ex);
            }
        });

        if (errors.Count == 1)
        {
            throw new PrismInitializationException("An error was encountered while invoking the OnInitialized Delegates", errors[0]);
        }
        else if (errors.Count > 1)
        {
            throw new AggregateException("One or more errors were encountered while executing the OnInitialized Delegates", [.. errors]);
        }

        if (_container.IsRegistered<IModuleCatalog>() && _container.Resolve<IModuleCatalog>().Modules.Any())
        {
            try
            {
                logger.LogDebug("Initializing modules.");
                var manager = _container.Resolve<IModuleManager>();
                manager.Run();
                logger.LogDebug("Modules Initialized.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error ocurred while initializing the Modules.");
                throw new PrismInitializationException("An error occurred while initializing the Modules.", ex);
            }
        }
        else
        {
            logger.LogDebug("No Modules found to initialize.");
        }

        var navRegistry = _container.Resolve<INavigationRegistry>();
        if (!navRegistry.IsRegistered(nameof(NavigationPage)))
        {
            var container = _container;
            container
                .RegisterDelegate(() => new PrismNavigationPage());
            container.RegisterInstance(new ViewRegistration
            {
                Name = nameof(NavigationPage),
                View = typeof(PrismNavigationPage),
                Type = ViewType.Page
            });

            var registrations = _container.Resolve<IEnumerable<ViewRegistration>>().ToList();
        }

        if (!navRegistry.IsRegistered(nameof(TabbedPage)))
        {
            var registry = _container;
            registry.RegisterForNavigation<TabbedPage>();
        }
    }

    internal void OnCreateWindow()
    {
        if (_createWindow is null)
            throw new ArgumentException("You must call CreateWindow on the PrismAppBuilder.");

        // Ensure that this is executed before we navigate.
        OnInitialized();
        _rootScope = _container.OpenScope("root");
        var onStart = _createWindow(_rootScope, _rootScope.Resolve<INavigationService>());
        onStart.Wait();
    }

    /// <summary>
    /// When the <see cref="Application"/> is started and the native platform calls <see cref="IApplication.CreateWindow(IActivationState?)"/>
    /// this delegate will be invoked to do your initial Navigation.
    /// </summary>
    /// <param name="createWindow">The Navigation Delegate.</param>
    /// <returns>The <see cref="PrismAppBuilder"/>.</returns>
    public PrismAppBuilder CreateWindow(Func<IResolverContext, INavigationService, Task> createWindow)
    {
        _createWindow = createWindow;
        return this;
    }

    /// <summary>
    /// Configures the <see cref="ViewModelLocator"/> used by Prism.
    /// </summary>
    public PrismAppBuilder ConfigureDefaultViewModelFactory(Func<IResolverContext, object, Type, object> viewModelFactory)
    {
        ViewModelLocationProvider.SetDefaultViewModelFactory((view, type) =>
        {
            if (view is not BindableObject bindable)
                return null;

            var container = bindable.GetResolverContext();
            return viewModelFactory(container, view, type);
        });

        return this;
    }

    private void RegistrationCallback(IContainer container) => RegisterDefaultRequiredTypes(container);

    /// <summary>
    /// Configures <see cref="RegionAdapterMappings"/> for Region Navigation with the <see cref="IRegionManager"/>.
    /// </summary>
    /// <param name="configureMappings">Delegate to configure the <see cref="RegionAdapterMappings"/>.</param>
    /// <returns>The <see cref="PrismAppBuilder"/>.</returns>
    public PrismAppBuilder ConfigureRegionAdapters(Action<RegionAdapterMappings> configureMappings)
    {
        _configureAdapters = configureMappings;
        return this;
    }

    /// <summary>
    /// Configures the <see cref="IRegionBehaviorFactory"/>.
    /// </summary>
    /// <param name="configureBehaviors">Delegate to configure the <see cref="IRegionBehaviorFactory"/>.</param>
    /// <returns>The <see cref="PrismAppBuilder"/>.</returns>
    public PrismAppBuilder ConfigureRegionBehaviors(Action<IRegionBehaviorFactory> configureBehaviors)
    {
        _configureBehaviors = configureBehaviors;
        return this;
    }

    private void RegisterDefaultRequiredTypes(IContainer containerRegistry)
    {
        containerRegistry.Register<IServiceScopeFactory, DryIocServiceScopeFactory>();
        containerRegistry.Register<IEventAggregator, EventAggregator>(Reuse.Singleton);
        containerRegistry.Register<IKeyboardMapper, KeyboardMapper>(Reuse.Singleton);
        containerRegistry.Register<IPageDialogService, PageDialogService>(Reuse.Singleton);
        containerRegistry.Register<IDialogService, DialogService>(Reuse.Scoped);
        containerRegistry.Register<IDialogViewRegistry, DialogViewRegistry>();
        // containerRegistry.RegisterSingleton<IDeviceService, DeviceService>();
        containerRegistry.Register<IPageAccessor, PageAccessor>(Reuse.Scoped);
        containerRegistry.Register<INavigationService, PageNavigationService>(Reuse.Scoped);
        containerRegistry.Register<INavigationRegistry, NavigationRegistry>();
        containerRegistry.RegisterMany<PrismWindowManager>(Reuse.Singleton);
        containerRegistry.RegisterPageBehavior<NavigationPage, NavigationPageSystemGoBackBehavior>();
        containerRegistry.RegisterPageBehavior<NavigationPage, NavigationPageActiveAwareBehavior>();
        containerRegistry.RegisterPageBehavior<NavigationPage, NavigationPageTabbedParentBehavior>();
        containerRegistry.RegisterPageBehavior<TabbedPage, TabbedPageActiveAwareBehavior>();
        containerRegistry.RegisterPageBehavior<PageLifeCycleAwareBehavior>();
        containerRegistry.RegisterPageBehavior<PageScopeBehavior>();
        containerRegistry.RegisterPageBehavior<RegionCleanupBehavior>();
        // containerRegistry.RegisterRegionServices(_configureAdapters, _configureBehaviors);
    }
}

internal class PepServiceProviderFactory(IContainer container)
    : IServiceProviderFactory<IContainer>
{
    /// <inheritdoc />
    public IContainer CreateBuilder(IServiceCollection services)
    {
        container.Populate(services);
        return container;
    }

    /// <inheritdoc />
    public IServiceProvider CreateServiceProvider(IContainer c) => c.BuildServiceProvider();
}
