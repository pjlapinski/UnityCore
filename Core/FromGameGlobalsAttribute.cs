using System;

namespace PJL.Core {
/// <summary>
///     On Start, inject using GameGlobals.Get
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class FromGameGlobalsAttribute : Attribute { }
}
