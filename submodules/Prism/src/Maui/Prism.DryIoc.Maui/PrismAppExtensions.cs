﻿using DryIoc;
using Pep.Ioc;
using Prism;

namespace Microsoft.Maui;

/// <summary>
/// Application base class using DryIoc
/// </summary>
public static class PrismAppExtensions
{
    public static MauiAppBuilder UsePrism(this MauiAppBuilder builder, Action<PrismAppBuilder> configurePrism)
    {
        return builder.UsePrism(new PepDryIocContainerExtension(), configurePrism);
    }

    public static MauiAppBuilder UsePrism(this MauiAppBuilder builder, Rules rules, Action<PrismAppBuilder> configurePrism)
    {
        rules = rules.WithTrackingDisposableTransients()
            .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments))
            .WithFactorySelector(Rules.SelectLastRegisteredFactory());
        return builder.UsePrism(new PepDryIocContainerExtension(rules), configurePrism);
    }
}
