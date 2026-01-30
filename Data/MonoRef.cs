using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PJL.Data
{
    [Serializable]
    public class MonoRef<TInterface, TObject> where TInterface : class where TObject : Object
    {
        [SerializeField, HideInInspector] private TObject _object;

        public TInterface Object
        {
            get => _object switch
            {
                null => null,
                TInterface i => i,
                _ => throw new InvalidCastException("unreachable")
            };
            set => _object = value switch
            {
                null => null,
                TObject o => o,
                _ => throw new InvalidCastException($"{value} needs to be of type {typeof(TObject)}")
            };
        }

        public MonoRef() { }
        public MonoRef(TInterface i) => Object = i;

        public static implicit operator TInterface(MonoRef<TInterface, TObject> obj) => obj.Object;
    }

    [Serializable]
    public class MonoRef<TInterface> : MonoRef<TInterface, Object> where TInterface : class {}
}
