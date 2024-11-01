using System.ComponentModel;
using Prism.Navigation.Xaml;
using IContainerProvider = Pep.Ioc.IContainerProvider;

namespace Prism.Navigation.Regions;

[EditorBrowsable(EditorBrowsableState.Never)]
public interface ITargetAwareRegion : IRegion
{
    VisualElement TargetElement { get; set; }

    IContainerProvider Container => TargetElement.GetContainerProvider();
}
