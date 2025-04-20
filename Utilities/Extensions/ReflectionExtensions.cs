using System;
using System.Linq;
using System.Reflection;

namespace PJL.Utilities.Extensions {
public static class ReflectionExtensions {
    public static (MethodInfo method, Attribute attribute)[] GetMethodsWithAttribute(this Type type, Type attributeType) =>
        type
            .GetMethods()
            .Select(method => (method, attribute: method.GetCustomAttribute(attributeType)))
            .Where(pair => pair.attribute != null)
            .ToArray();

    public static (MethodInfo method, Attribute attribute)[] GetMethodsWithAttribute(this Type type, Type attributeType, BindingFlags flags) =>
        type
            .GetMethods(flags)
            .Select(method => (method, attribute: method.GetCustomAttribute(attributeType)))
            .Where(pair => pair.attribute != null)
            .ToArray();

    public static (MethodInfo method, T attribute)[] GetMethodsWithAttribute<T>(this Type type) where T : Attribute =>
        type
            .GetMethods()
            .Select(method => (method, attribute: method.GetCustomAttribute<T>()))
            .Where(pair => pair.attribute != null)
            .ToArray();

    public static (MethodInfo method, T attribute)[] GetMethodsWithAttribute<T>(this Type type, BindingFlags flags) where T : Attribute =>
        type
            .GetMethods(flags)
            .Select(method => (method, attribute: method.GetCustomAttribute<T>()))
            .Where(pair => pair.attribute != null)
            .ToArray();
}
}
