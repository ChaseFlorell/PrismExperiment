namespace Pep.Ioc;

public interface IContainerProvider
{
    T Resolve<T>(Type type);
    T Resolve<T>();
    object Resolve(Type type, params (Type, object Instance)[] valueTuple);
    IContainerProvider CreateScope();
    bool IsRegistered<T>();
}