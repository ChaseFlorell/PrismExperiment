using System.ComponentModel;
using DryIoc;
using Prism.Common;
using Prism.Navigation.Internals;

namespace Prism.Navigation.Xaml;

/// <summary>
/// Provides Attachable properties for Navigation
/// </summary>
public static class Navigation
{
    internal const string PrismContainerProvider = nameof(PrismContainerProvider);

    private static readonly BindableProperty NavigationScopeProperty =
        BindableProperty.CreateAttached(PrismContainerProvider,
            typeof(IResolverContext),
            typeof(Navigation),
            default(IResolverContext),
            propertyChanged: OnNavigationScopeChanged);

    private static readonly BindableProperty ChildMvvmViewsProperty =
        BindableProperty.CreateAttached("ChildRegions",
            typeof(ChildRegionCollection),
            typeof(Navigation),
            null);

    private static void OnNavigationScopeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Page page || oldValue == newValue)
        {
            return;
        }

        if (oldValue != null && newValue is null && oldValue is IResolverContext oldProvider)
        {
            oldProvider.Dispose();
            return;
        }

        if (newValue != null && newValue is IResolverContext scopedProvider)
        {
            var accessor = scopedProvider.Resolve<IPageAccessor>();
            if (accessor.Page is null)
                accessor.Page = page;
            else if (accessor.Page != page)
                throw new InvalidOperationException($"The Scoped Provider has already been assigned to another page. Expected: '{page.GetType().FullName}' - Found: '{accessor.Page.GetType().FullName}'.");

            // scopedProvider.IsAttached = true;
        }
    }

    /// <summary>
    /// Provides bindable CanNavigate Bindable Property
    /// </summary>
    public static readonly BindableProperty CanNavigateProperty =
        BindableProperty.CreateAttached("CanNavigate",
            typeof(bool),
            typeof(Navigation),
            true,
            propertyChanged: OnCanNavigatePropertyChanged);

    internal static readonly BindableProperty RaiseCanExecuteChangedInternalProperty =
        BindableProperty.CreateAttached("RaiseCanExecuteChangedInternal",
            typeof(Action),
            typeof(Navigation),
            default(Action));

    /// <summary>
    /// Gets the Bindable Can Navigate property for an element
    /// </summary>
    /// <param name="view">The bindable element</param>
    public static bool GetCanNavigate(BindableObject view) => (bool)view.GetValue(CanNavigateProperty);

    /// <summary>
    /// Sets the Bindable Can Navigate property for an element
    /// </summary>
    /// <param name="view">The bindable element</param>
    /// <param name="value">The Can Navigate value</param>
    public static void SetCanNavigate(BindableObject view, bool value) => view.SetValue(CanNavigateProperty, value);

    /// <summary>
    /// Gets the Child Regions for a given <see cref="Page"/>
    /// </summary>
    /// <param name="page">The <see cref="Page"/> host.</param>
    /// <param name="setIfNull">Initializes the <see cref="ChildRegionCollection"/> if it has not been set.</param>
    /// <returns>The <see cref="ChildRegionCollection"/>.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static ChildRegionCollection GetChildRegions(this Page page, bool setIfNull = false)
    {
        var value = page.GetValue(ChildMvvmViewsProperty) as ChildRegionCollection;
        if (value is null && setIfNull)
        {
            value = [];
            page.SetValue(ChildMvvmViewsProperty, value);
        }

        return value;
    }

    internal static void ClearChildRegions(this Page page)
    {
        var children = page.GetChildRegions();
        if (children is not null)
        {
            children.Dispose();
            page.SetValue(ChildMvvmViewsProperty, null);
        }
    }

    /// <summary>
    /// Gets the instance of <see cref="INavigationService"/> for the given <see cref="Page"/>
    /// </summary>
    /// <param name="page">The <see cref="Page"/></param>
    /// <returns>The <see cref="INavigationService"/></returns>
    /// <remarks>Do not use... this is an internal use API</remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static INavigationService GetNavigationService(Page page)
    {
        ArgumentNullException.ThrowIfNull(page);

        var container = page.GetResolverContext();
        return container.Resolve<INavigationService>();
    }

    /// <summary>
    /// Sets the <see cref="IResolverContext"/> for the given <see cref="BindableObject"/>
    /// </summary>
    /// <param name="bindable">The <see cref="BindableObject"/>.</param>
    /// <param name="resolverContext">The <see cref="IResolverContext"/>.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void SetResolverContext(this BindableObject bindable, IResolverContext resolverContext)
    {
        bindable.SetValue(NavigationScopeProperty, resolverContext);
    }

    /// <summary>
    /// Gets the Container for the given View
    /// </summary>
    /// <param name="bindable">The View</param>
    /// <returns>The <see cref="IResolverContext"/>.</returns>
    /// <remarks>
    /// Will initialize a new Container Scope if the <see cref="Mvvm.ViewModelLocatorBehavior"/> is Forced.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static IResolverContext GetResolverContext(this BindableObject bindable)
    {
        if (bindable is null)
            return null;

        if (bindable.GetValue(NavigationScopeProperty) is IResolverContext resolverContext)
        {
            return resolverContext;
        }

        switch (bindable)
        {
            case Page { Parent: FlyoutPage flyout } page when flyout.Flyout == page:
            {
                return flyout.GetResolverContext();
            }
            case Page page when Mvvm.ViewModelLocator.GetAutowireViewModel(page) == Mvvm.ViewModelLocatorBehavior.Forced:
            {
                throw new ContainerLocatorMisuseException();
                resolverContext = null;  // ContainerLocator.Container.CreateScope();
                var accessor = resolverContext.Resolve<IPageAccessor>();
                accessor.Page = page;
                SetResolverContext(page, resolverContext);
                return resolverContext;
            }
            case Element { Parent: not null } element:
            {
                return GetResolverContext(element.Parent);
            }
        }

        return null;
    }

    internal static Action GetRaiseCanExecuteChangedInternal(BindableObject view) => (Action)view.GetValue(RaiseCanExecuteChangedInternalProperty);

    internal static void SetRaiseCanExecuteChangedInternal(BindableObject view, Action value) => view.SetValue(RaiseCanExecuteChangedInternalProperty, value);

    private static void OnCanNavigatePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        var action = GetRaiseCanExecuteChangedInternal(bindable);
        action?.Invoke();
    }
}
