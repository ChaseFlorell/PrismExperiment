using DryIoc;

namespace PrismExperiment.Prism;

public class PepContainerProvider(IResolverContext scope, bool isRecycled = false)
    : IPepContainerProvider, IScopedProvider
{
    internal Guid InstanceId { get; } = Guid.NewGuid();
    internal bool IsRecycled { get; } = isRecycled;

    /// <inheritdoc />
    public object Resolve(Type type)
    {
        Console.WriteLine("Resolving {0}", type.Name);
        return scope.Resolve(type);
    }

    /// <inheritdoc />
    public object Resolve(Type type, params (Type Type, object Instance)[] parameters)
    {
        Console.WriteLine("Resolving {0}", type.Name);
        return scope.Resolve(type);
    }

    /// <inheritdoc />
    public object Resolve(Type type, string name)
    {
        Console.WriteLine("Resolving {0}", type.Name);
        return scope.Resolve(type, name);
    }

    /// <inheritdoc />
    public object Resolve(Type type, string name, params (Type Type, object Instance)[] parameters)
    {
        Console.WriteLine("Resolving {0}", type.Name);
        return scope.Resolve(type, name);
    }

    /// <inheritdoc />
    public IScopedProvider CreateScope() => new PepContainerProvider(scope.OpenScope());

    /// <inheritdoc />
    public IScopedProvider CreateNamedScope(string name) => new PepContainerProvider(scope.OpenScope(name));

    /// <inheritdoc />
    public IScopedProvider CreateFromRecycledScope() => new PepContainerProvider(scope.OpenScope(), true);

    /// <inheritdoc />
    public IScopedProvider CreateChildScope() => throw new NotImplementedException();

    /// <inheritdoc />
    public IScopedProvider CurrentScope => this;

    /// <inheritdoc />
    public void Dispose()
    {
        // todo: investigate how to not dispose of the scope
        // _scope.Dispose();
    }

    /// <inheritdoc />
    public bool IsAttached { get; set; }
}