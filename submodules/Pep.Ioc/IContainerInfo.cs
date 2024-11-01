using System.ComponentModel;

namespace Pep.Ioc;

[EditorBrowsable(EditorBrowsableState.Never)]
public interface IContainerInfo
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    Type? GetRegistrationType(string key);

    [EditorBrowsable(EditorBrowsableState.Never)]
    Type? GetRegistrationType(Type serviceType);
}

[EditorBrowsable(EditorBrowsableState.Never)]
public static class IContainerInfoExtensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Type? GetRegistrationType(this IContainerExtension container, string key)
    {
        return container is IContainerInfo containerInfo ? containerInfo.GetRegistrationType(key) : default;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Type? GetRegistrationType(this IContainerExtension container, Type type)
    {
        return container is IContainerInfo containerInfo ? containerInfo.GetRegistrationType(type) : default;
    }
}