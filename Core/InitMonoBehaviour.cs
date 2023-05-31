using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace PJL.Core
{
    /// <summary>
    /// MonoBehaviour with dependency injection capabilities
    /// </summary>
    public abstract class InitMonoBehaviour<T> : MonoBehaviour where T : InitMonoBehaviour<T>
    {
        protected virtual void Start()
        {
            var type = typeof(T);

            InjectGlobalsAndGameObject(type);
            RunInit(type);
        }

        private void InjectGlobalsAndGameObject(Type thisType)
        {
            foreach (var field in thisType.GetRuntimeFields())
            {
                if (field.GetCustomAttributes(typeof(FromGameGlobalsAttribute)).Any())
                {
                    var value = GameGlobals.Get(field.FieldType);
                    if (value == null)
                        throw new NullReferenceException($"Object of type {thisType} tried to extract <{field.FieldType}> from GameGlobals, but it was null.");
                    field.SetValue(this, value);
                }
                if (field.GetCustomAttributes(typeof(FromGameObjectAttribute)).Any())
                    field.SetValue(this, GetComponent(field.FieldType));
            }
        }

        private void RunInit(Type thisType)
        { 
            var initMethods = thisType.GetRuntimeMethods().Where(m => m.GetCustomAttributes(typeof(InitMethodAttribute)).Any());

            foreach (var method in initMethods)
            {
                var initParams = method.GetParameters();
                
                var args = new object[initParams.Length];
                for (var i = 0; i < args.Length; i++)
                {
                    args[i] = GameGlobals.Get(initParams[i].ParameterType);
                    if (args[i] == null)
                        throw new NullReferenceException($"Object of type {thisType} tried to extract <{initParams[i].ParameterType}> from GameGlobals in its init method, but it was null.");
                }
                
                method.Invoke(this, args);
            }
        }
    }
}
