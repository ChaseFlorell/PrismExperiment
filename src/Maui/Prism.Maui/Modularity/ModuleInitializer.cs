using DryIoc;

namespace Prism.Modularity;

public class ModuleInitializer : IModuleInitializer
{
    readonly IResolverContext _container;

    public ModuleInitializer(IResolverContext container)
    {
        _container = container;
    }

    public void Initialize(IModuleInfo moduleInfo)
    {
        var module = CreateModule(Type.GetType(moduleInfo.ModuleType, true));
        if (module != null)
        {
            throw new NotImplementedException();
           // module.RegisterTypes(_container);
            module.OnInitialized(_container);
        }
    }

    protected virtual IModule CreateModule(Type moduleType)
    {
        return (IModule)_container.Resolve(moduleType);
    }
}
