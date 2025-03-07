using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    private Dictionary<Type, object> services = new Dictionary<Type, object>(); //Store the services

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    //Register the services
    public void RegisterService<T>(T service)
    {
        var type = typeof(T);
        if (!services.ContainsKey(type))
        {
            services[type] = service;
        }
    }

    //Get the desired service
    public T GetService<T>()
    {
        var type = typeof(T);
        if (services.ContainsKey(type))
        {
            return (T)services[type];
        }
        else
        {
            Debug.LogError($"Service {type} not registered!");
            return default;
        }
    }
}
