using Prism.Mvvm;

namespace PrismExperiment.Prism;

public abstract class PepViewRegistryBase : ViewRegistryBase<BindableObject>
{
    /// <inheritdoc />
    public PepViewRegistryBase(ViewType registryType, IEnumerable<ViewRegistration> registrations)
        : base(registryType, registrations)
    {
    }

    protected override void Autowire(BindableObject view)
    {
        if (view.BindingContext is not null)
            return;

        PepViewModelLocator.Autowire(view);
    }

    protected override void SetContainerProvider(BindableObject view, IContainerProvider container)
    {
        view.SetContainerProvider(container);
    }

    protected override void SetNavigationNameProperty(BindableObject view, string name)
    {
        view.SetValue(PepViewModelLocator.NavigationNameProperty, name);
    }

    protected override void SetViewModelProperty(BindableObject view, Type viewModelType)
    {
        view.SetValue(PepViewModelLocator.ViewModelProperty, viewModelType);
    }
}