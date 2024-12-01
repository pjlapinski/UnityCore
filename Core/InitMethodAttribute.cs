using System;

namespace PJL.Core {
/// <summary>
///     Run on start, with parameters injected from GameGlobals
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class InitMethodAttribute : Attribute { }
}
