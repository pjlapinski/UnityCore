using System;
using UnityEngine;

namespace PJL.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DisableIfAttribute : PropertyAttribute
    {
        public string Condition { get; private set; }
        public DisableIfAttribute(string condition)
        {
            Condition = condition;
        }
    }
}
