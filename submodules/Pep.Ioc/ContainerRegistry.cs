using DryIoc;
using Microsoft.Extensions.DependencyInjection;

namespace Pep.Ioc;

public class ContainerRegistry : IContainerRegistry
{
}

public interface IContainerRegistry
{
    IContainerRegistry RegisterInstance<TService>(TService implementation);
    IContainerRegistry TryRegisterSingleton<TService, TImplementation>();
    IContainerRegistry TryRegisterScoped<TService, TImplementation>();
    IContainerRegistry Register<TService, TImplementation>();
    IContainerRegistry TryRegister<TService, TImplementation>();
    IContainerRegistry RegisterSingleton<TService, TImplementation>();
    IContainerRegistry RegisterScoped<TService, TImplementation>();
    IContainerRegistry RegisterInstance(object prismAppBuilder);
    IContainerRegistry Register<T>(Func<T> func);
    IContainerRegistry Register(Type type);
    IContainerRegistry Register<T>();
    IContainerRegistry TryRegister<T>();
    IContainerRegistry RegisterSingleton<TService>(Func<IContainerProvider, TService> func);
    IContainerRegistry RegisterScoped<TService>(Func<IContainerProvider, TService> func);
    IContainerRegistry RegisterManySingleton<TImplementation>();
    IContainerRegistry RegisterForNavigation<TPage, TViewModel>(string scopeName);
    IContainer GetContainer();
}

public interface IContainerProvider
{
    T Resolve<T>(Type type);
    T Resolve<T>();
    object Resolve(Type type, params (Type, object Instance)[] valueTuple);
    IContainerProvider CreateScope();
    bool IsRegistered<T>();
}

public interface IContainerExtension : IContainerProvider, IContainerRegistry
{
}

public interface IScopedProvider : IContainerProvider, IDisposable
{
    bool IsAttached { get; set; }
}

public class PepIocContainerExtension : IContainerExtension
{
    public PepIocContainerExtension()
    {
    }

    public PepIocContainerExtension(Rules rules)
    {
    }
}

public class ContainerResolutionException : Exception
{
}

public class PrismServiceProviderFactory : IServiceProviderFactory<IContainerExtension>
{
    public PrismServiceProviderFactory(Action<IContainerExtension> registrationCallback)
    {
    }

    /// <inheritdoc />
    public IContainerExtension CreateBuilder(IServiceCollection services)
    {
        return TODO_IMPLEMENT_ME;
    }

    /// <inheritdoc />
    public IServiceProvider CreateServiceProvider(IContainerExtension containerBuilder)
    {
        return TODO_IMPLEMENT_ME;
    }
}

public static class ExceptionExtensions
{
    public static Exception GetRootException(this Exception exception)
    {
    }
}