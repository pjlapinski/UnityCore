using System;

namespace PJL.DependencyInjection {
internal class ServiceDescriptor {
  /// <summary>
  /// The type that the service will be registered as (usually an interface).
  /// </summary>
  public Type ServiceType { get; }

  /// <summary>
  /// The type of the service's implementation, which has to derive from ServiceType.
  /// </summary>
  public Type ImplementationType { get; }

  /// <summary>
  /// The implementation used in singleton services
  /// </summary>
  public object Implementation { get; private set; }

  /// <summary>
  /// The name of the method to be called on a new object after instantiation.
  /// If string.IsNullOrWhiteSpace is true for this field, the constructor is called, if the object is not derived from MonoBehaviour.
  /// </summary>
  public string InitializationMethodName { get; }

  /// <summary>
  /// Determines how an instance of this service should be created.
  /// </summary>
  public ServiceCreationPolicy CreationPolicy { get; }

  /// <summary>
  /// Determines whether this service is a singleton or transient.
  /// </summary>
  public ServiceLifetime Lifetime { get; }

  /// <summary>
  /// If the service is a scriptable object, it can be found at this path.
  /// </summary>
  public string ResourcePath { get; }

  public ServiceDescriptor(Type serviceType,
    Type implementationType,
    object implementation,
    string initializationMethodName,
    ServiceCreationPolicy creationPolicy,
    ServiceLifetime lifetime,
    string resourcePath) {
    ServiceType = serviceType;
    ImplementationType = implementationType;
    Implementation = implementation;
    InitializationMethodName = initializationMethodName;
    CreationPolicy = creationPolicy;
    Lifetime = lifetime;
    ResourcePath = resourcePath;
  }

  internal void AddImplementation(object implementation) {
    if (Lifetime == ServiceLifetime.Singleton)
      Implementation ??= implementation;
    else
      Implementation = implementation;
  }
}
}