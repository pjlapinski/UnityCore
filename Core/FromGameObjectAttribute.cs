using System;

namespace PJL.Core
{
    /// <summary>
    /// On Start, inject using GetComponent
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class FromGameObjectAttribute : Attribute { }
}
