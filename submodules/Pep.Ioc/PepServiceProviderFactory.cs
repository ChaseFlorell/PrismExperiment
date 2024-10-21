using Microsoft.Extensions.DependencyInjection;

namespace Pep.Ioc;

public class PepServiceProviderFactory : IServiceProviderFactory<IContainerExtension>
{
    public PepServiceProviderFactory(Action<IContainerExtension> act)
    {
    }

    /// <inheritdoc />
    public IContainerExtension CreateBuilder(IServiceCollection services)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IServiceProvider CreateServiceProvider(IContainerExtension containerBuilder)
    {
        throw new NotImplementedException();
    }
}