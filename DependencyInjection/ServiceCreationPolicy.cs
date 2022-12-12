namespace PJL.DependencyInjection {
/// <summary>
/// Determines how new instance of a service should be created.
/// </summary>
public enum ServiceCreationPolicy {
  /// <summary>
  /// The instance will be provided during registration. Only valid for singleton services.
  /// </summary>
  Self,

  /// <summary>
  /// The instance will be created using Activator.CreateInstance.
  /// </summary>
  NewInstance,

  /// <summary>
  /// The instance will be created using new GameObject and adding the service as a Component to it
  /// </summary>
  NewGameObject,

  /// <summary>
  /// The instance will be found on the scene using Object.Find.
  /// </summary>
  Find,

  /// <summary>
  /// The instance will be found in the Resources directory.
  /// </summary>
  Resource,
}
}