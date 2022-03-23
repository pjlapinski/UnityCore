#if !UNITY_EDITOR
using System;
#endif
using System;
using System.IO;
using UnityEngine;

namespace PJL.Patterns
{
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : Configurator<T>
    {
        private static T s_instance;

        public static T Instance
        {
            get
            {
                if (s_instance != null) return s_instance;
                s_instance = Resources.Load<T>(typeof(T).Name);
                if (s_instance == null)
                {
#if UNITY_EDITOR
                    if (!UnityEditor.AssetDatabase.IsValidFolder($"Assets{Path.DirectorySeparatorChar}Resources"))
                        UnityEditor.AssetDatabase.CreateFolder("Assets", "Resources");
                    var path = $"Assets{Path.DirectorySeparatorChar}Resources{Path.DirectorySeparatorChar}{typeof(T).Name}.asset";
                    UnityEditor.AssetDatabase.CreateAsset(CreateInstance<T>(), path);
                    s_instance = Resources.Load<T>(typeof(T).Name);
#else
                    throw new NullReferenceException($"No Scriptable Object Singleton of type {typeof(T).Name} in Resources.");
#endif
                }

                return s_instance;
            }
        }

        protected virtual void Awake()
        {
            if (s_instance == null) s_instance = (T)this;
            else if (s_instance != this) throw new MultipleSingletonInstancesException(typeof(T).Name);
        }

        protected virtual void OnDestroy()
        {
            if (s_instance == this) s_instance = null;
        }
    }
}
