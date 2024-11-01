using System.Diagnostics.CodeAnalysis;
using Prism.Mvvm;

namespace Prism.Ioc;

public static class NavigationRegistrationExtensions
{
    public static Pep.Ioc.IContainerRegistry RegisterForNavigation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView>(this Pep.Ioc.IContainerRegistry container, string name = null)
        where TView : Page =>
        container.RegisterForNavigation(typeof(TView), null, name);

    public static Pep.Ioc.IContainerRegistry RegisterForNavigation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TViewModel>(this Pep.Ioc.IContainerRegistry container, string name = null)
        where TView : Page =>
        container.RegisterForNavigation(typeof(TView), typeof(TViewModel), name);

    public static Pep.Ioc.IContainerRegistry RegisterForNavigation(this Pep.Ioc.IContainerRegistry container, Type view, Type viewModel, string name = null)
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
            })
            .Register(view);

        if (viewModel != null)
            container.Register(viewModel);

        return container;
    }
}
