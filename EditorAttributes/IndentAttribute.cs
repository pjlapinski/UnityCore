using System;
using UnityEngine;

namespace PJL.EditorAttributes
{

    [AttributeUsage(AttributeTargets.Field)]
    public class IndentAttribute : PropertyAttribute
    {
        public int AddLevel { get; set; }

        public IndentAttribute(int addLevel = 1)
        {
            AddLevel = addLevel;
        }
    }
}
