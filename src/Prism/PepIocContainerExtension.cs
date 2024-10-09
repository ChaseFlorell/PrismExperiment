namespace PrismExperiment.Prism;

/// <summary>
/// [Singleton] This this in newed up once at startup and is not part of IoC.
/// </summary>
public class PepIocContainerExtension : DryIocContainerExtension
{
    /// <inheritdoc />
    public override IScopedProvider CreateScope()
    {
        return base.CreateScope();
    }
}