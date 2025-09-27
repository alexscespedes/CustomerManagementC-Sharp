using System;

namespace CustomerManagement.DependencyInjection;

public interface IServiceContainer
{
    void RegisterSingletonType<TInterface, TImplementation>()
        where TImplementation : class, TInterface
        where TInterface : class;

    void RegisterSingletonInstance<T>(T instance) where T : class;

    void RegisterTransientType<TInterface, TImplementation>()
        where TImplementation : class, TInterface
        where TInterface : class;

    T Resolve<T>() where T : class;
    object Resolve(Type type);
}
