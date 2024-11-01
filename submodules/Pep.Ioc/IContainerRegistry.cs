namespace Pep.Ioc;

public interface IContainerRegistry
{
    IContainerRegistry Register(Type type);
    IContainerRegistry Register<TService>();
    IContainerRegistry Register<TService>(Func<TService> func);
    IContainerRegistry Register<TService, TImplementation>() where TImplementation : TService;
    IContainerRegistry RegisterInstance<TService>(TService instance);
    IContainerRegistry RegisterManySingleton<TImplementation>();
    IContainerRegistry RegisterScoped<TService, TImplementation>() where TImplementation : TService;
    IContainerRegistry RegisterScoped<TService>(Func<IContainerProvider, TService> func);
    IContainerRegistry RegisterSingleton<TService, TImplementation>() where TImplementation : TService;
    IContainerRegistry RegisterSingleton<TService>(Func<IContainerProvider, TService> func);
    IContainerRegistry TryRegister<TService>();
    IContainerRegistry TryRegister<TService, TImplementation>() where TImplementation : TService;
    IContainerRegistry TryRegisterScoped<TService, TImplementation>() where TImplementation : TService;
    IContainerRegistry TryRegisterSingleton<TService, TImplementation>() where TImplementation : TService;
}