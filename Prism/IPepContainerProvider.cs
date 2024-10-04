namespace PrismExperiment.Prism;

public interface IPepContainerProvider : IContainerProvider
{
    IScopedProvider CreateNamedScope(string name);
    IScopedProvider CreateFromRecycledScope();
}