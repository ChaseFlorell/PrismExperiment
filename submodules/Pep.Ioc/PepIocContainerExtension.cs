using DryIoc;

namespace Pep.Ioc;

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
    public IContainerRegistry RegisterInstance<TService>(TService instance)
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
    public IContainerRegistry RegisterInstance(object instance)
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