using System;
using UnityEngine;

namespace PJL.Data
{
    public class ResourceScriptableSingleton<T> : ScriptableObject where T : ResourceScriptableSingleton<T>
    {
        private static T s_instance;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                {
                    var s = Resources.LoadAll<T>("");
                    var idx = -1;
                    if (s.Length == 1) idx = 0;
                    else
                    {
                        for (var i = 0; i < s.Length; i++)
                        {
                            var service = s[i];
                            if (service.GetType() == typeof(T)) // allow subtypes to exist, but only get the instance of the base type
                            {
                                if (idx != -1) throw new Exception(); // encountered more than one instance
                                idx = i;
                            }
                        }
                    }

                    if (idx == -1 || s[idx] == null) throw new Exception();
                    s_instance = s[idx];
                }
                return s_instance;
            }
        }
    }
}
