using System;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;

namespace CustomerManagement.Services;

public class SimpleServiceContainer : IServiceContainer
{
    private readonly Dictionary<Type, ServiceDescriptor> _services = new();

    public void RegisterSingleton<T>(T instance) where T : class
    {
        _services[typeof(T)] = new ServiceDescriptor
        {
            ServiceType = typeof(T),
            Instance = instance,
            Lifetime = ServiceLifetime.Singleton
        };
    }

    public T Resolve<T>() where T : class
    {
        return (T)Resolve(typeof(T));
    }

    public object Resolve(Type type)
    {
        if (!_services.TryGetValue(type, out var serviceDescriptor))
        {
            throw new InvalidOperationException($"Service of type {type.Name} is not registered.");
        }

        if (serviceDescriptor.Lifetime == ServiceLifetime.Singleton && serviceDescriptor.Instance != null)
        {
            return serviceDescriptor.Instance;
        }

        object instance;
        if (serviceDescriptor.ImplementationType != null)
        {
            instance = CreateInstance(serviceDescriptor.ImplementationType);
        }
        else if (serviceDescriptor.Instance != null)
        {
            return serviceDescriptor.Instance;
        }
        else
        {
            throw new InvalidOperationException($"Unable to create instance of {type.Name}");
        }

        if (serviceDescriptor.Lifetime == ServiceLifetime.Singleton)
        {
            serviceDescriptor.Instance = instance;
        }

        return instance;
    }

    private object CreateInstance(Type type)
    {
        var constructors = type.GetConstructors();
        var constructor = constructors.OrderByDescending(c => c.GetParameters().Length).First();

        var parameters = constructor.GetParameters();
        var parameterInstances = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            parameterInstances[i] = Resolve(parameters[i].ParameterType);
        }

        return Activator.CreateInstance(type, parameterInstances)!;
    }

    void IServiceContainer.RegisterSingleton<TInterface, TImplementation>()
    {
        _services[typeof(TInterface)] = new ServiceDescriptor
        {
            ServiceType = typeof(TInterface),
            ImplementationType = typeof(TImplementation),
            Lifetime = ServiceLifetime.Singleton
        };
    }

    void IServiceContainer.RegisterTransient<TInterface, TImplementation>()
    {
        _services[typeof(TInterface)] = new ServiceDescriptor
        {
            ServiceType = typeof(TInterface),
            ImplementationType = typeof(TImplementation),
            Lifetime = ServiceLifetime.Transiet
        };
    }
}

internal class ServiceDescriptor
{
    public Type ServiceType { get; set; } = null!;
    public Type? ImplementationType { get; set; }
    public object? Instance { get; set; }
    public ServiceLifetime Lifetime { get; set; }
}

internal enum ServiceLifetime
{
    Transiet,
    Singleton
}