
using DryIoc;

namespace Prism.Behaviors;

internal class DelegatePageBehaviorFactory : IPageBehaviorFactory
{
    private readonly Action<Page> _applyBehaviors;

    public DelegatePageBehaviorFactory(Action<Page> applyBehaviors)
    {
        _applyBehaviors = applyBehaviors;
    }

    public void ApplyPageBehaviors(Page page)
    {
        _applyBehaviors(page);
    }
}

internal class DelegateContainerPageBehaviorFactory : IPageBehaviorFactory
{
    private readonly Action<IResolverContext, Page> _applyBehaviors;
    private readonly IResolverContext _container;

    public DelegateContainerPageBehaviorFactory(Action<IResolverContext, Page> applyBehaviors, IResolverContext container)
    {
        _applyBehaviors = applyBehaviors;
        _container = container;
    }

    public void ApplyPageBehaviors(Page page)
    {
        _applyBehaviors(_container, page);
    }
}
