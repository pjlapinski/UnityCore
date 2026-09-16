using System;
using UnityEngine;

namespace PJL.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string Text { get; private set; }

        public ButtonAttribute() : this(null) { }
        public ButtonAttribute(string text)
        {
            Text = text;
        }
    } 
}
