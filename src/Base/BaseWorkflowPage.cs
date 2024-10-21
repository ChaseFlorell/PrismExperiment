using Microsoft.Maui.Controls;
using Prism.Navigation;

namespace PrismExperiment.Base;

public class BaseWorkflowPage : PageBase
{
    public BaseWorkflowPage(INavigationService navigationService)
    {
        ToolbarItems.Add(new ToolbarItem("Cancel", "", () => { navigationService.GoBackAsync((KnownNavigationParameters.UseModalNavigation, true)); }));
    }
}