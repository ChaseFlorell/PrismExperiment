namespace Pep.Ioc;

public sealed class ContainerResolutionException : Exception
{
    public ContainerResolutionException(Type type, Exception exception, IContainerProvider containerProvider)
    {
    }
}