using DryIoc;

namespace Pep.Ioc;

public interface IContainerRegistry
{
    IContainer GetContainer();
    IContainerRegistry Register(Type type);
    IContainerRegistry Register<T>();
    IContainerRegistry Register<T>(Func<T> func);
    IContainerRegistry Register<TService, TImplementation>();
    IContainerRegistry RegisterForNavigation<TPage, TViewModel>(string scopeName);
    IContainerRegistry RegisterInstance(object instance);
    IContainerRegistry RegisterInstance<TService>(TService instance);
    IContainerRegistry RegisterManySingleton<TImplementation>();
    IContainerRegistry RegisterScoped<TService, TImplementation>();
    IContainerRegistry RegisterScoped<TService>(Func<IContainerProvider, TService> func);
    IContainerRegistry RegisterSingleton<TService, TImplementation>();
    IContainerRegistry RegisterSingleton<TService>(Func<IContainerProvider, TService> func);
    IContainerRegistry TryRegister<T>();
    IContainerRegistry TryRegister<TService, TImplementation>();
    IContainerRegistry TryRegisterScoped<TService, TImplementation>();
    IContainerRegistry TryRegisterSingleton<TService, TImplementation>();
}