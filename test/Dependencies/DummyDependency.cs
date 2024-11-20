using DryIoc;
using NanoidDotNet;

namespace PrismExperiment.Dependencies;

public class DummyDependency : IDummyDependency
{
    public DummyDependency(IResolverContext resolverContext)
    {
        InstanceId = Nanoid.Generate(Nanoid.Alphabets.UppercaseLetters, 4);
        ScopeName = resolverContext.CurrentScope?.Name?.ToString();
    }

    public string InstanceId { get; }
    public string? ScopeName { get; }
}