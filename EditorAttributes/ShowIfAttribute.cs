using System;
using UnityEngine;

namespace PJL.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public string Condition { get; private set; }
        public ShowIfAttribute(string condition)
        {
            Condition = condition;
        }
    }
}
