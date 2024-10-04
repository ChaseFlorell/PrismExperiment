using DryIoc;

namespace PrismExperiment.Prism;

public class PepContainerProvider : IPepContainerProvider, IScopedProvider
{
    public PepContainerProvider(IResolverContext scope) => _scope = scope;

    /// <inheritdoc />
    public object Resolve(Type type) => _scope.Resolve(type);

    /// <inheritdoc />
    public object Resolve(Type type, params (Type Type, object Instance)[] parameters) => _scope.Resolve(type);

    /// <inheritdoc />
    public object Resolve(Type type, string name) => _scope.Resolve(type, name);

    /// <inheritdoc />
    public object Resolve(Type type, string name, params (Type Type, object Instance)[] parameters) => _scope.Resolve(type, name);

    /// <inheritdoc />
    public IScopedProvider CreateScope() => new PepContainerProvider(_scope.OpenScope());

    public IScopedProvider CreateScope(string name) => new PepContainerProvider(_scope.OpenScope(name));

    /// <inheritdoc />
    public IScopedProvider CurrentScope => this;

    private IResolverContext? _scope;

    /// <inheritdoc />
    public void Dispose()
    {
        _scope?.Dispose();
        _scope = null;
    }

    /// <inheritdoc />
    public IScopedProvider CreateChildScope() => new PepContainerProvider(_scope.OpenScope());

    /// <inheritdoc />
    public bool IsAttached { get; set; }
}