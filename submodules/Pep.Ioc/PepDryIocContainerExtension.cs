using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Pep.Ioc
{
    public class PepDryIocContainerExtension
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
            _resolutionContext = resolverContext ?? container.OpenScope("root", true);

            Console.WriteLine("Container Hashcode: {0}", _container.GetHashCode());
        }

        /// <inheritdoc />
        public object Resolve(Type type) => _resolutionContext.Resolve(type);

        /// <inheritdoc />
        public T Resolve<T>() => _resolutionContext.Resolve<T>();

        /// <inheritdoc />
        public object Resolve(Type type, params (Type, object Instance)[] parameters)
        {
            try
            {
                List<object> list = ((IEnumerable<(Type, object)>)parameters).Where<(Type, object)>((Func<(Type, object), bool>)(x => !(x is IResolverContext))).Select<(Type, object), object>((Func<(Type, object), object>)(x => x)).ToList<object>();
                list.Add((object)this);
                return _resolutionContext.Resolve(type, list.ToArray(), IfUnresolved.Throw, (Type)null, (object)null);
            }
            catch (Exception ex)
            {
                throw new ContainerResolutionException(type, ex, (IResolverContext)this);
            }
        }

        /// <inheritdoc />
        public IResolverContext CreateScope(string name) => _resolutionContext.OpenScope(name, true);

        /// <inheritdoc />
        public bool IsRegistered<T>() => _container.IsRegistered<T>();

        /// <inheritdoc />
        public DryIoc.IContainer RegisterInstance<TService>(TService instance)
        {
            _container.RegisterInstance(instance);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer RegisterManySingleton<TImplementation>()
        {
            _container.RegisterMany<TImplementation>(Reuse.Singleton);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer TryRegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Singleton);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer TryRegisterScoped<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Scoped);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer Register<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>();
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer TryRegister<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(reuse: Reuse.Transient);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer RegisterScoped<TService>(Func<IResolverContext, TService> factoryMethod)
        {
            _container.RegisterDelegate<IResolverContext>(typeof(TService), provider => factoryMethod(provider)!, Reuse.Scoped);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Singleton);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer RegisterSingleton<TService>(Func<IResolverContext, TService> factoryMethod)
        {
            _container.RegisterDelegate<IResolverContext>(typeof(TService), provider => factoryMethod(provider)!, Reuse.Singleton);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer RegisterScoped<TService, TImplementation>() where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Scoped);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer Register<TService>(Func<TService> func)
        {
            _container.RegisterDelegate(func);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer Register(Type type)
        {
            _container.Register(type);
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer Register<TService>()
        {
            _container.Register<TService>();
            return default;
        }

        /// <inheritdoc />
        public DryIoc.IContainer TryRegister<TService>()
        {
            _container.Register<TService>();
            return default;
        }

        /// <inheritdoc />
        public IContainer GetContainer() => _container;

        /// <inheritdoc />
        public void Populate(IServiceCollection services) => _container.Populate(services);

        /// <inheritdoc />
        public IServiceProvider CreateServiceProvider() => _container.BuildServiceProvider();

        /// <inheritdoc />
        Type? GetRegistrationType(string key)
        {
            ServiceRegistrationInfo registrationInfo = _container.GetServiceRegistrations().Where<ServiceRegistrationInfo>((Func<ServiceRegistrationInfo, bool>)(r => key.Equals(r.OptionalServiceKey?.ToString(), StringComparison.Ordinal))).FirstOrDefault<ServiceRegistrationInfo>();
            if (registrationInfo.OptionalServiceKey == null)
                registrationInfo = _container.GetServiceRegistrations().Where<ServiceRegistrationInfo>((Func<ServiceRegistrationInfo, bool>)(r => key.Equals(r.ImplementationType.Name, StringComparison.Ordinal))).FirstOrDefault<ServiceRegistrationInfo>();
            return registrationInfo.ImplementationType;
        }

        /// <inheritdoc />
        Type? GetRegistrationType(Type serviceType)
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