using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PJL.DependencyInjection {
public class ServiceCollection {
  private Dictionary<Type, ServiceDescriptor> _services;

  private static bool TryGetServiceAttribute(ICustomAttributeProvider type, out ServiceAttribute service) {
    var t = typeof(ServiceAttribute);
    var attr = ((ServiceAttribute[]) type.GetCustomAttributes(t, true)).FirstOrDefault();
    if (attr == null) {
      service = null;
      return false;
    }

    service = attr;
    return true;
  }

  private object[] GetInitializationMethodData(ServiceDescriptor descriptor, out MethodInfo methodInfo) {
    var initMethodName = descriptor.InitializationMethodName;

    ParameterInfo[] initParams;
    if (string.IsNullOrWhiteSpace(initMethodName)) {
      methodInfo = null;
      initParams = descriptor.ImplementationType.GetConstructors().FirstOrDefault()?.GetParameters();
    }
    else {
      methodInfo = descriptor.ImplementationType.GetMethods().FirstOrDefault(m => m.Name == initMethodName);
      initParams = methodInfo?.GetParameters();
    }

    if (initParams == null) {
      Debug.LogError(
        $"Could not find a matching initialization method with name \"{initMethodName}\" for the service of type {descriptor.ServiceType.Name}.");
      methodInfo = null;
      return null;
    }

    var arguments = new object[initParams.Length];
    for (var i = 0; i < initParams.Length; i++) {
      var param = initParams[i];
      var attr = GetService(param.ParameterType);
      if (attr == null) {
        Debug.LogError(
          $"Could not resolve an initialization dependency for the service of type {descriptor.ServiceType.Name}. {param.Name} was null");
        methodInfo = null;
        return null;
      }

      arguments[i] = attr;
    }

    return arguments;
  }

  public ServiceCollection() {
    _services = new Dictionary<Type, ServiceDescriptor>();
  }

  /// <summary>
  /// Removes all registered services from this collection.
  /// </summary>
  public void ClearServices() {
    _services?.Clear();
  }

  private void RegisterService<T, TService>(in object implementation, ServiceCreationPolicy serviceCreationPolicy,
    ServiceLifetime lifetime, string resourcePath = null) where TService : T {
    var serviceType = typeof(T);
    if (_services.ContainsKey(serviceType))
      throw new DuplicateNameException(
        $"The container already has a definition of the {serviceType.Name} service.");
    var implType = typeof(TService);
    if (!TryGetServiceAttribute(implType, out var serviceAttr))
      throw new ArgumentException($"No [Service] attribute on type {implType.Name}.");

    _services[serviceType] = new ServiceDescriptor(
      serviceType,
      implType,
      implementation,
      serviceAttr.InitializationMethodName,
      serviceCreationPolicy,
      lifetime,
      resourcePath
    );
  }

  /// <summary>
  /// Registers a new transient service.
  /// </summary>
  /// <param name="serviceCreationPolicy">The policy for how a new instance of this service should be created.</param>
  /// <typeparam name="T">The type the service will register as.</typeparam>
  /// <typeparam name="TService">The type of the implementation of the service, which implements the service type.</typeparam>
  public void RegisterTransient<T, TService>(ServiceCreationPolicy serviceCreationPolicy) where TService : T
    => RegisterService<T, TService>(null, serviceCreationPolicy, ServiceLifetime.Transient);

  /// <summary>
  /// Registers a new transient service.
  /// </summary>
  /// <param name="serviceCreationPolicy">The policy for how a new instance of this service should be created.</param>
  /// <typeparam name="T">The type the service will register as, and its implementation.</typeparam>
  public void RegisterTransient<T>(ServiceCreationPolicy serviceCreationPolicy)
    => RegisterTransient<T, T>(serviceCreationPolicy);

  /// <summary>
  /// Registers a new Singleton service for a ScriptableObject.
  /// </summary>
  /// <param name="resourcePath">The path that the service's ScriptableObject can be loaded from.</param>
  /// <typeparam name="T">The type the service will register as.</typeparam>
  /// <typeparam name="TService">The type of the implementation of the service, which implements the service type.</typeparam>
  public void RegisterSingleton<T, TService>(string resourcePath) where TService : ScriptableObject, T =>
    RegisterService<T, TService>(null, ServiceCreationPolicy.Resource, ServiceLifetime.Singleton, resourcePath);

  /// <summary>
  /// Registers a new Singleton service for a ScriptableObject.
  /// </summary>
  /// <param name="resourcePath">The path that the service's ScriptableObject can be loaded from.</param>
  /// <typeparam name="T">The type the service will register as, and its implementation.</typeparam>
  public void RegisterSingleton<T>(string resourcePath) where T : ScriptableObject =>
    RegisterSingleton<T, T>(resourcePath);

  /// <summary>
  /// Registers a new Singleton service.
  /// </summary>
  /// <param name="implementation">The implementation of the service that will be returned from GetService</param>
  /// <param name="serviceCreationPolicy">The policy for how a new instance of this service should be created.</param>
  /// <typeparam name="T">The type the service will register as.</typeparam>
  /// <typeparam name="TService">The type of the implementation of the service, which implements the service type.</typeparam>
  public void RegisterSingleton<T, TService>(in object implementation, ServiceCreationPolicy serviceCreationPolicy)
    where TService : T
    => RegisterService<T, TService>(implementation, serviceCreationPolicy, ServiceLifetime.Singleton);

  /// <summary>
  /// Registers a new Singleton service.
  /// </summary>
  /// <param name="implementation">The implementation of the service that will be returned from GetService</param>
  /// <typeparam name="T">The type the service will register as.</typeparam>
  /// <typeparam name="TService">The type of the implementation of the service, which implements the service type.</typeparam>
  public void RegisterSingleton<T, TService>(in object implementation) where TService : T =>
    RegisterSingleton<T, TService>(implementation, ServiceCreationPolicy.Self);

  /// <summary>
  /// Registers a new Singleton service.
  /// </summary>
  /// <param name="implementation">The implementation of the service that will be returned from GetService</param>
  /// <typeparam name="T">The type the service will register as, and its implementation.</typeparam>
  public void RegisterSingleton<T>(in object implementation) =>
    RegisterSingleton<T, T>(implementation);

  /// <summary>
  /// Registers a new Singleton service.
  /// </summary>
  /// <param name="serviceCreationPolicy">The policy for how a new instance of this service should be created.</param>
  /// <typeparam name="T">The type the service will register as, and its implementation.</typeparam>
  public void RegisterSingleton<T>(ServiceCreationPolicy serviceCreationPolicy) =>
    RegisterSingleton<T, T>(serviceCreationPolicy);


  /// <summary>
  /// Registers a new Singleton service.
  /// </summary>
  /// <param name="serviceCreationPolicy">The policy for how a new instance of this service should be created.</param>
  /// <typeparam name="T">The type the service will register as.</typeparam>
  /// <typeparam name="TService">The type of the implementation of the service, which implements the service type.</typeparam>
  public void RegisterSingleton<T, TService>(ServiceCreationPolicy serviceCreationPolicy) where TService : T =>
    RegisterSingleton<T, TService>(null, serviceCreationPolicy);

  /// <summary>
  /// Searches the collection for the given service.
  /// </summary>
  /// <param name="serviceType">The type that the service was registered as.</param>
  /// <returns>The service, null if not found.</returns>
  /// <exception cref="NullReferenceException">Thrown if no service was found.</exception>
  public object GetService(Type serviceType) {
    if (!_services.TryGetValue(serviceType, out var descriptor) || descriptor == null) {
      Debug.LogError($"No service registered for type {serviceType.Name}.");
      return default;
    }

    if (descriptor.Implementation != null && descriptor.Lifetime == ServiceLifetime.Singleton)
      return descriptor.Implementation;
    var isMono = descriptor.ImplementationType.IsSubclassOf(typeof(MonoBehaviour));
    var isScriptable = descriptor.ImplementationType.IsSubclassOf(typeof(ScriptableObject));
    return descriptor.CreationPolicy switch {
      ServiceCreationPolicy.Self when descriptor.Lifetime == ServiceLifetime.Singleton =>
        throw new NullReferenceException(
          $"There is no implementation for the service of type {descriptor.ServiceType.Name} with creation policy 'Self'."),
      ServiceCreationPolicy.Self when descriptor.Lifetime == ServiceLifetime.Transient =>
        throw new ArgumentException(
          $"There cannot be a transient implementation for the service of type {descriptor.ServiceType.Name} with creation policy 'Self'."),
      ServiceCreationPolicy.NewInstance when isMono || isScriptable || descriptor.ImplementationType.IsAbstract ||
                                             descriptor.ImplementationType.IsInterface =>
        throw new ArgumentException(
          $"Trying to make a new instance of service {descriptor.ServiceType.Name}, but it's implementation type does not allow for 'new' instantiation."),
      ServiceCreationPolicy.NewInstance =>
        GetServiceByNewInstance(descriptor),
      ServiceCreationPolicy.NewGameObject when !isMono || isScriptable ||
                                               descriptor.ImplementationType.IsAbstract ||
                                               descriptor.ImplementationType.IsInterface =>
        throw new ArgumentException(
          $"Trying to make a new instance of service {descriptor.ServiceType.Name}, but it's implementation type does not allow for Component instantiation."),
      ServiceCreationPolicy.NewGameObject =>
        GetServiceByNewGameObject(descriptor),
      ServiceCreationPolicy.Find when !isMono || isScriptable || descriptor.ImplementationType.IsAbstract ||
                                      descriptor.ImplementationType.IsInterface =>
        throw new ArgumentException(
          $"Trying to Object.Find an instance of service {descriptor.ServiceType.Name}, but it's implementation type does not allow for Component instantiation."),
      ServiceCreationPolicy.Find when descriptor.Lifetime == ServiceLifetime.Transient =>
        throw new ArgumentException(
          $"Trying to Object.Find an instance of service {descriptor.ServiceType.Name}, but it's lifetime is Transient."),
      ServiceCreationPolicy.Find =>
        GetServiceByFind(descriptor),
      ServiceCreationPolicy.Resource when !isScriptable =>
        throw new ArgumentException(
          $"Trying to Resources.Load an instance of service {descriptor.ServiceType.Name}, but it's implementation is not a ScriptableObject."),
      ServiceCreationPolicy.Resource when string.IsNullOrWhiteSpace(descriptor.ResourcePath) =>
        throw new ArgumentException(
          $"Trying to Resources.Load an instance of service {descriptor.ServiceType.Name}, but no path was given to the resource."),
      ServiceCreationPolicy.Resource =>
        GetServiceByResource(descriptor),
      _ => throw new ArgumentOutOfRangeException(),
    };
  }

  private object GetServiceByFind(ServiceDescriptor descriptor) {
    var arguments = GetInitializationMethodData(descriptor, out var methodInfo);
    if (arguments == null) return null;

    var comp = Object.FindObjectOfType(descriptor.ImplementationType);
    if (comp != null)
      methodInfo.Invoke(comp, arguments);
    else
      throw new ArgumentException($"Could not Object.Find a service of type {descriptor.ServiceType.Name}.");

    descriptor.AddImplementation(comp);
    return comp;
  }

  private object GetServiceByNewGameObject(ServiceDescriptor descriptor) {
    GameObject go;
    object comp;
    if (string.IsNullOrWhiteSpace(descriptor.InitializationMethodName)) {
      go = new GameObject();
      comp = Convert.ChangeType(go.AddComponent(descriptor.ImplementationType), descriptor.ImplementationType);
      descriptor.AddImplementation(comp);
      return comp;
    }

    var arguments = GetInitializationMethodData(descriptor, out var methodInfo);
    if (arguments == null) return null;

    go = new GameObject();
    comp = Convert.ChangeType(go.AddComponent(descriptor.ImplementationType), descriptor.ImplementationType);
    if (comp != null) methodInfo.Invoke(comp, arguments);

    descriptor.AddImplementation(comp);
    return comp;
  }

  private object GetServiceByNewInstance(ServiceDescriptor descriptor) {
    var arguments = GetInitializationMethodData(descriptor, out var methodInfo);
    if (arguments == null) return null;

    if (methodInfo == null) {
      descriptor.AddImplementation(Activator.CreateInstance(descriptor.ImplementationType, arguments));
    }
    else {
      var obj = Activator.CreateInstance(descriptor.ImplementationType);
      methodInfo.Invoke(obj, arguments);
      descriptor.AddImplementation(obj);
    }

    return descriptor.Implementation;
  }

  private object GetServiceByResource(ServiceDescriptor descriptor) {
    var arguments = GetInitializationMethodData(descriptor, out var methodInfo);
    if (arguments == null) return null;

    var so = Resources.Load(descriptor.ResourcePath, descriptor.ImplementationType);
    if (so != null)
      methodInfo.Invoke(so, arguments);
    else
      throw new ArgumentException($"Could not Resources.Load a service of type {descriptor.ServiceType.Name}.");

    descriptor.AddImplementation(so);
    return so;
  }

  /// <summary>
  /// Searches the collection for the given service.
  /// </summary>
  /// <typeparam name="T">The type the service was registered as.</typeparam>
  /// <returns>The service, default if not found.</returns>
  /// <exception cref="NullReferenceException">Thrown if no service was found.</exception>
  public T GetService<T>() => (T) GetService(typeof(T));
}
}
