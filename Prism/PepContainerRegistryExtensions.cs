namespace PrismExperiment.Prism;

public static class PepContainerRegistryExtensions
{
    public static IContainerRegistry RegisterForScopedNavigation<TView, TViewModel>(this IContainerRegistry container, string name = "")
        where TView : Page
    {
        Type? view = typeof(TView);
        Type? viewModel = typeof(TViewModel);

        if (view is null)
            throw new ArgumentNullException(nameof(view));

        if (string.IsNullOrEmpty(name))
            name = view.Name;

        container.RegisterInstance(new ViewRegistration
            {
                Type = ViewType.Page,
                Name = name,
                View = view,
                ViewModel = viewModel
            })
            .Register(view);

        if (viewModel != null)
            container.Register(viewModel);

        return container;
    }
}