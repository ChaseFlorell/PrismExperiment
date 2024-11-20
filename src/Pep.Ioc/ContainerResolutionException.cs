using DryIoc;

namespace Pep.Ioc;

public sealed class ContainerResolutionException : Exception
{
    public ContainerResolutionException(Type type, Exception exception, IResolverContext resolverContext)
    {
    }
}