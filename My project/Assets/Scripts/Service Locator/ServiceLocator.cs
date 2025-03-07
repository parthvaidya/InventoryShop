using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    // Register a service
    public void Register<T>(T service)
    {
        var type = typeof(T);
        if (services.ContainsKey(type))
        {
            Debug.LogWarning($"ServiceLocator: Service of type {type.Name} is already registered.");
            return;
        }
        services[type] = service;
    }

    // Resolve a service
    public T GetService<T>()
    {
        var type = typeof(T);
        if (services.TryGetValue(type, out var service))
        {
            return (T)service;
        }

        Debug.LogError($"ServiceLocator: Service of type {type.Name} not found.");
        return default;
    }

    // Unregister a service (optional)
    public void Unregister<T>()
    {
        services.Remove(typeof(T));
    }
}
