using Microsoft.Extensions.DependencyInjection;

namespace Pep.Ioc;

public class PepServiceProviderFactory : IServiceProviderFactory<IContainerExtension>
{
    public PepServiceProviderFactory(Action<IContainerExtension> registerTypes, IContainerExtension container)
    {
        _container = container;
        this._registerTypes = registerTypes;
        this._currentContainer = new Lazy<IContainerExtension>((Func<IContainerExtension>)(() => container));
    }

    public IContainerExtension CreateBuilder(IServiceCollection services)
    {
        IContainerExtension container = this._currentContainer.Value;
        Populate(container, services);
        this._registerTypes(container);
        return container;
    }

    private void Populate(IContainerExtension container, IServiceCollection services)
    {
        if (container is not IServiceCollectionAware serviceCollectionAware)
        {
            throw new InvalidOperationException("The instance of IContainerExtension does not implement IServiceCollectionAware");
        }

        serviceCollectionAware.Populate(services);
    }

    public IServiceProvider CreateServiceProvider(IContainerExtension container) =>
        container is IServiceCollectionAware serviceCollectionAware
            ? serviceCollectionAware.CreateServiceProvider()
            : throw new InvalidOperationException("The instance of IContainerExtension does not implement IServiceCollectionAware");

    private readonly IContainerExtension _container;

    private readonly Action<IContainerExtension> _registerTypes;

    private readonly Lazy<IContainerExtension> _currentContainer;
}