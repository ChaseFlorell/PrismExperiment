using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NanoidDotNet;

namespace Pep.Ioc
{
    public class PepDryIocContainerExtension : IContainerExtension<IContainer>
    {
        public PepDryIocContainerExtension() : this(__containerRules)
        {
        }

        public PepDryIocContainerExtension(Rules rules) : this(new Container(rules))
        {
        }

        public PepDryIocContainerExtension(IContainer container) : this(container, null)
        {
        }

        private PepDryIocContainerExtension(IContainer container, IResolverContext? resolverContext)
        {
            _container = container;
            if (resolverContext == null)
            {

            }
            _resolutionContext = resolverContext ?? container.OpenScope("root", true);
            if (!_container.IsRegistered<IContainerProvider>())
            {
                _container.RegisterDelegate<IContainerProvider>(() => this);
            }

            Console.WriteLine("Container Hashcode: {0}", _container.GetHashCode());
        }

        public string InstanceId { get; } = Nanoid.Generate(Nanoid.Alphabets.UppercaseLetters, 6);

        /// <inheritdoc />
        public object Resolve(Type type) => _resolutionContext.Resolve(type);

        /// <inheritdoc />
        public T Resolve<T>() => _resolutionContext.Resolve<T>();

        /// <inheritdoc />
        public object Resolve(Type type, params (Type, object Instance)[] parameters)
        {
            try
            {
                List<object> list = ((IEnumerable<(Type, object)>)parameters).Where<(Type, object)>((Func<(Type, object), bool>)(x => !(x is IContainerProvider))).Select<(Type, object), object>((Func<(Type, object), object>)(x => x)).ToList<object>();
                list.Add((object)this);
                return _resolutionContext.Resolve(type, list.ToArray(), IfUnresolved.Throw, (Type)null, (object)null);
            }
            catch (Exception ex)
            {
                throw new ContainerResolutionException(type, ex, (IContainerProvider)this);
            }
        }

        /// <inheritdoc />
        public IContainerProvider CreateScope(string name)
        {
            Console.WriteLine("Current Resolution {0} creating named scope {1}", _resolutionContext.CurrentScope?.Name, name);
            var newScope = _resolutionContext.OpenScope(name, true);

            Console.WriteLine($"Resolve Context Parent: {newScope?.Parent?.CurrentScope?.Name}");
            Console.WriteLine($"Resolve Context: {newScope?.CurrentScope?.Name}");

            var pepDryIocContainerExtension = new PepDryIocContainerExtension(_container, newScope);
            newScope.Use<IContainerProvider>(pepDryIocContainerExtension);
            var provider = newScope.Resolve<IContainerProvider>();
            if (provider.InstanceId == pepDryIocContainerExtension.InstanceId)
            {

            }

            return pepDryIocContainerExtension;
        }

        /// <inheritdoc />
        public bool IsRegistered<T>() => _container.IsRegistered<T>();

        /// <inheritdoc />
        public IContainerRegistry RegisterInstance<TService>(TService instance)
        {
            _container.RegisterInstance(instance);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterManySingleton<TImplementation>()
        {
            _container.RegisterMany<TImplementation>(Reuse.Singleton);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry TryRegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Singleton);
            return this;
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
            _container.Register<TService, TImplementation>(reuse: Reuse.Transient);
            return this;
        }

        public IContainerRegistry RegisterScoped(Type type)
        {
            _container.Register(type, Reuse.Scoped);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterScoped<TService>(Func<IContainerProvider, TService> factoryMethod)
        {
            _container.RegisterDelegate<IContainerProvider>(typeof(TService), provider => factoryMethod(provider)!, Reuse.Scoped);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Singleton);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterSingleton<TService>(Func<IContainerProvider, TService> factoryMethod)
        {
            _container.RegisterDelegate<IContainerProvider>(typeof(TService), provider => factoryMethod(provider)!, Reuse.Singleton);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry RegisterScoped<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Scoped);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry Register<TService>(Func<TService> func)
        {
            _container.RegisterDelegate(func);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry Register(Type type)
        {
            _container.Register(type);
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry Register<TService>()
        {
            _container.Register<TService>();
            return this;
        }

        /// <inheritdoc />
        public IContainerRegistry TryRegister<TService>()
        {
            _container.Register<TService>();
            return this;
        }

        /// <inheritdoc />
        public IContainer GetContainer() => _container;

        /// <inheritdoc />
        public void Populate(IServiceCollection services) => _container.Populate(services);

        /// <inheritdoc />
        public IServiceProvider CreateServiceProvider() => _container.BuildServiceProvider();

        /// <inheritdoc />
        Type? IContainerInfo.GetRegistrationType(string key)
        {
            ServiceRegistrationInfo registrationInfo = _container.GetServiceRegistrations().Where<ServiceRegistrationInfo>((Func<ServiceRegistrationInfo, bool>)(r => key.Equals(r.OptionalServiceKey?.ToString(), StringComparison.Ordinal))).FirstOrDefault<ServiceRegistrationInfo>();
            if (registrationInfo.OptionalServiceKey == null)
                registrationInfo = _container.GetServiceRegistrations().Where<ServiceRegistrationInfo>((Func<ServiceRegistrationInfo, bool>)(r => key.Equals(r.ImplementationType.Name, StringComparison.Ordinal))).FirstOrDefault<ServiceRegistrationInfo>();
            return registrationInfo.ImplementationType;
        }

        /// <inheritdoc />
        Type? IContainerInfo.GetRegistrationType(Type serviceType)
        {
            ServiceRegistrationInfo registrationInfo = _container.GetServiceRegistrations().Where<ServiceRegistrationInfo>((Func<ServiceRegistrationInfo, bool>)(x => x.ServiceType == serviceType)).FirstOrDefault<ServiceRegistrationInfo>();
            return (object)registrationInfo.ServiceType != null ? registrationInfo.ImplementationType : (Type)null;
        }

        private readonly IResolverContext _resolutionContext;
        private readonly IContainer _container;

        private static readonly Rules __containerRules =
            Rules.Default
                .WithAutoConcreteTypeResolution()
                .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments));
    }
}