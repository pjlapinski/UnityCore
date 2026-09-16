using System;
using UnityEngine;

namespace PJL.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DropdownAttribute : PropertyAttribute
    {
        public string Source { get; private set; }
        public DropdownAttribute(string source)
        {
            Source = source;
        }
    }
}
