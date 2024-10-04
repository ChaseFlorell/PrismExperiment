using NanoidDotNet;

namespace PrismExperiment.Dependencies;

public class DummyDependency : IDummyDependency
{
    public string InstanceId { get; } = Nanoid.Generate(Nanoid.Alphabets.UppercaseLetters, 4);
}