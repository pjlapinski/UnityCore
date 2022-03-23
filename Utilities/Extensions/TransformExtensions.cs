using UnityEngine;

namespace PJL.Utilities.Extensions
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; Object.Destroy(transform.GetChild(i--).gameObject)) ;
        }
    }
}
