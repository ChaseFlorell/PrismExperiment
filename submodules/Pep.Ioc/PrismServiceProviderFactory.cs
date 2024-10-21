using Microsoft.Extensions.DependencyInjection;

namespace Pep.Ioc;

public class PrismServiceProviderFactory : IServiceProviderFactory<IContainerExtension>
{
    public PrismServiceProviderFactory(Action<IContainerExtension> act)
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