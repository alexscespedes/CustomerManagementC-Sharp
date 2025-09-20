using System;

namespace CustomerManagement.Services;

public interface IServiceContainer
{
    void RegisterSingleton<TInterface, TImplementation>()
        where TImplementation : class, TInterface
        where TInterface : class;

    void RegisterSingleton<T>(T instance) where T : class;

    void RegisterTransient<TInterface, TImplementation>()
        where TImplementation : class, TInterface
        where TInterface : class;

    T Resolve<T>() where T : class;
    object Resolve(Type type);
}
