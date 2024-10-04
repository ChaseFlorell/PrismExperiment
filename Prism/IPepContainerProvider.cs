namespace PrismExperiment.Prism;

public interface IPepContainerProvider : IContainerProvider
{
    IScopedProvider CreateScope(string name);
}