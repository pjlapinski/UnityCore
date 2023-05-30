using System.Linq;
using System.Reflection;
using UnityEngine;

namespace PJL.Globals
{
    public abstract class InitMonoBehaviour<T> : MonoBehaviour where T : InitMonoBehaviour<T>
    {
        protected virtual void Start()
        {
            var type = typeof(T);
            var initMethods = type.GetRuntimeMethods().Where(m => m.GetCustomAttributes(typeof(InitMethodAttribute)).Any());

            foreach (var method in initMethods)
            {
                var initParams = method.GetParameters();
                
                var args = new object[initParams.Length];
                for (var i = 0; i < args.Length; i++)
                    args[i] = GameGlobals.Get(initParams[i].ParameterType);
                
                method.Invoke(this, args);
            }
        }
    }
}
