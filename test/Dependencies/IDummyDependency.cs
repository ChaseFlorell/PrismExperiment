namespace PrismExperiment.Dependencies;

public interface IDummyDependency
{
    string InstanceId { get; }
    string? ScopeName { get; }
}