using System;
using UnityEngine;

namespace PJL.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class HideIfAttribute : PropertyAttribute
    {
        public string Condition { get; private set; }
        public HideIfAttribute(string condition)
        {
            Condition = condition;
        }
    }
}
