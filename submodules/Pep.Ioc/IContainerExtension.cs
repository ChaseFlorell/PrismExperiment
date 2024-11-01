using Microsoft.Extensions.DependencyInjection;

namespace Pep.Ioc;

public interface IContainerExtension : IContainerProvider,
    IContainerRegistry,
    IContainerInfo,
    IServiceCollectionAware;

public interface IContainerExtension<TContainer> : IContainerExtension
{
    TContainer GetContainer();
}

public interface IServiceCollectionAware
{
    void Populate(IServiceCollection services);

    IServiceProvider CreateServiceProvider();
}