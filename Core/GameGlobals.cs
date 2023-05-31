using System;
using System.Collections.Generic;
using UnityEngine;

namespace PJL.Core
{
    /// <summary>
    /// Statically registered objects, accessible by everyone and available to be injected
    /// </summary>
    public class GameGlobals : MonoBehaviour
    {
        private Dictionary<Type, object> _globals;
        private static GameGlobals s_instance;

        private static GameGlobals Instance
        {
            get
            {
                if (s_instance == null)
                {
                    var found = FindObjectOfType<GameGlobals>();
                    if (found == null)
                    {
                        var go = new GameObject { name = nameof(GameGlobals) };
                        go.transform.SetSiblingIndex(0);
                        found = go.AddComponent<GameGlobals>();
                    }
                    s_instance = found;
                    s_instance._globals = new();
                }
                return s_instance;
            }
        }

        public static void Register<T>(T instance) => Instance._globals[typeof(T)] = instance;

        public static bool TryGet(Type type, out object instance) => Instance._globals.TryGetValue(type, out instance);
        public static bool TryGet<T>(out T instance)
        {
            if (TryGet(typeof(T), out var inst))
            {
                instance = (T)inst;
                return true;
            }

            instance = default;
            return false;
        }

        public static object Get(Type type) => TryGet(type, out var inst) ? inst : default;
        public static T Get<T>() => (T) Get(typeof(T));

        public static void Remove(Type type) => Instance._globals.Remove(type);
        public static void Remove<T>() => Remove(typeof(T));
    }
}
