using System.Diagnostics.CodeAnalysis;
using DryIoc;
using Prism.Mvvm;
using IContainer = DryIoc.IContainer;


namespace Prism.Ioc;

public static class NavigationRegistrationExtensions
{
    public static void RegisterForNavigation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView>(this IContainer container, string name = null)
        where TView : Page =>
        container.RegisterForNavigation(typeof(TView), null, name);

    public static void RegisterForNavigation<TView>(
        this IContainer container, Func<TView> factory, string name = null)
    {
        ArgumentNullException.ThrowIfNull(factory);
        var view = typeof(TView);

        if (string.IsNullOrEmpty(name))
        {
            name = view.Name;
        }

        container.RegisterDelegate(factory);
        container.RegisterInstance(new ViewRegistration
        {
            Type = ViewType.Page,
            Name = name,
            View = view
        });
    }

    public static void RegisterForNavigation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
        TViewModel>(this IContainer container, string name = null)
        where TView : Page =>
        container.RegisterForNavigation(typeof(TView), typeof(TViewModel), name);

    public static void RegisterForNavigation(this IContainer container, Type view, Type viewModel, string name = null)
    {
        if (view is null)
        {
            throw new ArgumentNullException(nameof(view));
        }

        if (string.IsNullOrEmpty(name))
        {
            name = view.Name;
        }

        container.RegisterInstance(new ViewRegistration
        {
            Type = ViewType.Page,
            Name = name,
            View = view,
            ViewModel = viewModel
        });
        container.Register(view);

        if (viewModel != null)
        {
            container.Register(viewModel);
            ViewModelLocationProvider.Register(view.Name, viewModel);
        }
    }
}
