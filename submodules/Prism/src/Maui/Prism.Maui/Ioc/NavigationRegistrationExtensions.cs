using System.Diagnostics.CodeAnalysis;
using DryIoc;
using Prism.Mvvm;


namespace Prism.Ioc;

public static class NavigationRegistrationExtensions
{
    public static void RegisterForNavigation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView>(this DryIoc.IContainer container, string name = null)
        where TView : Page =>
        container.RegisterForNavigation(typeof(TView), null, name);

    public static void RegisterForNavigation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
        TViewModel>(this DryIoc.IContainer container, string name = null)
        where TView : Page =>
        container.RegisterForNavigation(typeof(TView), typeof(TViewModel), name);

    public static void RegisterForNavigation(this DryIoc.IContainer container, Type view, Type viewModel, string name = null)
    {
        if (view is null)
            throw new ArgumentNullException(nameof(view));

        if (string.IsNullOrEmpty(name))
            name = view.Name;

        container.RegisterInstance(new ViewRegistration
        {
            Type = ViewType.Page,
            Name = name,
            View = view,
            ViewModel = viewModel
        });
        container.Register(view);

        if (viewModel != null)
            container.Register(viewModel);
    }
}
