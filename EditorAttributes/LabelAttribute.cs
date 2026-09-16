using System;
using UnityEngine;

namespace PJL.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LabelAttribute : PropertyAttribute
    {
        public string Text { get; private set; }
        public LabelAttribute(string text)
        {
            Text = text;
        }
    }
}
