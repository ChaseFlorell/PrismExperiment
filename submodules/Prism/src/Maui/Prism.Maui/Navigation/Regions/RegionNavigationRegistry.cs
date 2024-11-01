using Prism.Mvvm;
using IContainerProvider = Pep.Ioc.IContainerProvider;

namespace Prism.Navigation.Regions;

internal class RegionNavigationRegistry : ViewRegistryBase, IRegionNavigationRegistry
{
    public RegionNavigationRegistry(IEnumerable<ViewRegistration> registrations)
        : base(ViewType.Region, registrations)
    {
    }

    protected override void ConfigureView(BindableObject bindable, IContainerProvider container)
    {
    }
}
