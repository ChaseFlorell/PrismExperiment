namespace Pep.Ioc;

public interface IScopedProvider : IContainerProvider, IDisposable
{
    bool IsAttached { get; set; }
}