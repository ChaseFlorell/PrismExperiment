﻿

using DryIoc;

namespace Prism.Navigation.Regions;

internal static class RegionExtensions
{
    internal static IResolverContext Container(this IRegion region)
    {
        if (region is ITargetAwareRegion car)
            return car.Container;

        throw new NotSupportedException("The Region does not implement IContainerAwareRegion");
    }
}
