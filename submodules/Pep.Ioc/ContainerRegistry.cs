using DryIoc;
using Microsoft.Extensions.DependencyInjection;

namespace Pep.Ioc;

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

    /// <inheritdoc />
    public T Resolve<T>(Type type)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public T Resolve<T>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public object Resolve(Type type, params (Type, object Instance)[] valueTuple)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerProvider CreateScope()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool IsRegistered<T>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry RegisterInstance<TService>(TService implementation)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry TryRegisterSingleton<TService, TImplementation>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry TryRegisterScoped<TService, TImplementation>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry Register<TService, TImplementation>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry TryRegister<TService, TImplementation>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry RegisterSingleton<TService, TImplementation>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry RegisterScoped<TService, TImplementation>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry RegisterInstance(object prismAppBuilder)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry Register<T>(Func<T> func)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry Register(Type type)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry Register<T>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry TryRegister<T>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry RegisterSingleton<TService>(Func<IContainerProvider, TService> func)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry RegisterScoped<TService>(Func<IContainerProvider, TService> func)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry RegisterManySingleton<TImplementation>()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainerRegistry RegisterForNavigation<TPage, TViewModel>(string scopeName)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IContainer GetContainer()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IServiceProvider CreateServiceProvider(IContainerExtension containerBuilder)
    {
        throw new NotImplementedException();
    }
}

public static class ExceptionExtensions
{
    public static Exception GetRootException(this Exception exception)
    {
        throw new NotImplementedException();
    }
}