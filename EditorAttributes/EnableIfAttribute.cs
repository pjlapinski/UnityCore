using System;
using UnityEngine;

namespace PJL.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnableIfAttribute : PropertyAttribute
    {
        public string Condition { get; private set; }
        public EnableIfAttribute(string condition)
        {
            Condition = condition;
        }
    }
}
