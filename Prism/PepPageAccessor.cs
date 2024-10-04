using Prism.Common;

namespace PrismExperiment.Prism;

public class PepPageAccessor : IPageAccessor
{
    /// <inheritdoc />
    public Page? Page
    {
        get => _weakPage?.TryGetTarget(out var target) ?? false ? target : null;
        set => _weakPage = value is null ? null : new(value);
    }

    private WeakReference<Page>? _weakPage;
}