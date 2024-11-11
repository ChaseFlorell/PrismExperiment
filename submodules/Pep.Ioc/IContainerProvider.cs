namespace Pep.Ioc;

public interface IContainerProvider
{
    public string InstanceId { get; }
    T Resolve<T>();
    object Resolve(Type type);
    object Resolve(Type type, params (Type, object Instance)[] valueTuple);
    IContainerProvider CreateScope(string name);
    bool IsRegistered<T>();
}