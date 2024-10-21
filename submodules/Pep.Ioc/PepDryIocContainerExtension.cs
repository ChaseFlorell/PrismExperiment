using DryIoc;

namespace Pep.Ioc
{
    public class PepDryIocContainerExtension : IContainerExtension
    {
        private readonly IResolverContext _resolverContext;
        private readonly IContainer _container;

        public PepDryIocContainerExtension() : this(new Container())
        {
        }

        public PepDryIocContainerExtension(Rules rules) : this(new Container(rules))
        {
        }

        private PepDryIocContainerExtension(IContainer container) : this(container, container)
        {
        }

        private PepDryIocContainerExtension(IContainer container, IResolverContext resolverContext)
        {
            _container = container;
            _resolverContext = resolverContext;
        }

        /// <inheritdoc />
        public T Resolve<T>() => _resolverContext.Resolve<T>();

        /// <inheritdoc />
        public object Resolve(Type type, params (Type, object Instance)[] valueTuple)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IContainerProvider CreateScope(string name)
        {
            return new PepDryIocContainerExtension(_container, _resolverContext.OpenScope(name)); // note: there is also a "CreateScope()" method that we should look at
        }

        /// <inheritdoc />
        public bool IsRegistered<T>()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterForNavigation<TPage>()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterInstance<TService>(TService instance)
        {
            _container.RegisterInstance(instance);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry TryRegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Singleton);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterDialogContainer<TDialogContainer>()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterPageBehavior<TPage, TBehavior>()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterPageBehavior<TBehavior>()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IContainerRegistry TryRegisterScoped<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Scoped);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry Register<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>();
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry TryRegister<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>();
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Singleton);
            return this;
        }

        public IContainerRegistry RegisterScoped<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Scoped);
            return this;
        }

        public IContainerRegistry Register<TService>(Func<TService> func)
        {
            _container.RegisterDelegate(func);
            return this;
        }

        public IContainerRegistry Register(Type type)
        {
            _container.Register(type);
            return this;
        }

        public IContainerRegistry Register<TService>()
        {
            _container.Register<TService>();
            return this;
        }

        public IContainerRegistry TryRegister<TService>()
        {
            _container.Register<TService>();
            return this;
        }

        public IContainerRegistry RegisterSingleton<TService>(Func<IContainerProvider, TService> func)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterScoped<TService>(Func<IContainerProvider, TService> func)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterManySingleton<TImplementation>()
        {
            _container.RegisterMany<TImplementation>(Reuse.Singleton);
            return this;
        }

        public IContainerRegistry RegisterForNavigation<TPage, TViewModel>(string scopeName)
        {
            throw new NotImplementedException();
        }

        public IContainer GetContainer() => _container;
    }
}