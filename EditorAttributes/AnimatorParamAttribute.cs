using System;
using UnityEngine;

namespace PJL.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AnimatorParamAttribute : PropertyAttribute
    {
        public string AnimatorName { get; private set; }
        public AnimatorParamAttribute(string animatorName)
        {
            AnimatorName = animatorName;
        }
    }
}
