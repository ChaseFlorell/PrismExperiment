using System.ComponentModel;
using DryIoc;
using Prism.Navigation.Xaml;

namespace Prism.Navigation.Regions;

[EditorBrowsable(EditorBrowsableState.Never)]
public interface ITargetAwareRegion : IRegion
{
    VisualElement TargetElement { get; set; }

    IResolverContext Container => TargetElement.GetResolverContext();
}
