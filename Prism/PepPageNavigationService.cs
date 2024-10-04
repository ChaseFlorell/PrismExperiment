using Prism.Common;

namespace PrismExperiment.Prism;

/// <summary>
/// [Scoped] Registered in the container
/// </summary>
public class PepPageNavigationService : PageNavigationService
{
    /// <inheritdoc />
    public PepPageNavigationService(
        IPepContainerProvider container,
        IWindowManager windowManager,
        IEventAggregator eventAggregator,
        IPageAccessor pageAccessor)
        : base(container, windowManager, eventAggregator, pageAccessor)
    {
        _container = container;
    }

    /// <inheritdoc />
    protected override Page CreatePage(string segmentName)
    {
        try
        {
            var scope = PepContainerRegistryExtensions.ScopedDependencies.Contains(segmentName)
                ? _container.CreateNamedScope(segmentName)
                : _hasLaunchedOnce
                    ? _container.CreateFromRecycledScope()
                    : _container.CreateScope();

            var page = (Page)Registry.CreateView(scope, segmentName);

            if (page is null)
                throw new NullReferenceException($"The resolved type for {segmentName} was null. You may be attempting to navigate to a Non-Page type");

            _hasLaunchedOnce = true;

            return page;
        }
        catch (NavigationException)
        {
            throw;
        }
        catch (KeyNotFoundException knfe)
        {
            throw new NavigationException(NavigationException.NoPageIsRegistered, segmentName, knfe);
        }
        catch (ViewModelCreationException vmce)
        {
            throw new NavigationException(NavigationException.ErrorCreatingViewModel, segmentName, _pageAccessor.Page, vmce);
        }
        //catch(ViewCreationException viewCreationException)
        //{
        //    if(!string.IsNullOrEmpty(viewCreationException.InnerException?.Message) && viewCreationException.InnerException.Message.Contains("Maui"))
        //        throw new NavigationException(NavigationException.)
        //}
        catch (Exception ex)
        {
            var inner = ex.InnerException;
            while (inner is not null)
            {
                if (inner.Message.Contains("thread with a dispatcher"))
                    throw new NavigationException(NavigationException.UnsupportedMauiCreation, segmentName, _pageAccessor.Page, ex);
                inner = inner.InnerException;
            }

            throw new NavigationException(NavigationException.ErrorCreatingPage, segmentName, ex);
        }
    }

    private readonly IPepContainerProvider _container;
    private static bool _hasLaunchedOnce;
}