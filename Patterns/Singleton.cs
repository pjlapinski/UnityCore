using System;
using UnityEngine;

namespace PJL.Patterns
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T s_instance;

        public static T Instance
        {
            get
            {
                if (s_instance != null) return s_instance;
                var instances = FindObjectsOfType<T>();
                switch (instances.Length)
                {
                    case 0:
                        return null;
                    case > 1:
                        throw new MultipleSingletonInstancesException(typeof(T).Name);
                    default:
                        s_instance = instances[0];
                        return s_instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (s_instance == null) s_instance = (T) this;
            else if (s_instance != this) throw new MultipleSingletonInstancesException(typeof(T).Name);
        }

        protected virtual void OnDestroy()
        {
            if (s_instance == this) s_instance = null;
        }
    }

    public class MultipleSingletonInstancesException : Exception
    {
        public MultipleSingletonInstancesException(string typeName) 
            : base($"Multiple instances of singleton of type {typeName}.")
        { }
    }
}
