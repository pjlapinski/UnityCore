using PJL.Logging;
using PJL.Utilities.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using System.Threading.Tasks;
using NaughtyAttributes;
#endif

namespace PJL.Core {
public abstract class ScriptableObjectRepository<T, TObj> : ScriptableObject where TObj : ScriptableObject, IIdentifiable where T : ScriptableObjectRepository<T, TObj> {
#region Singleton

    private static T s_instance;
    public static T Instance {
        get {
            if (s_instance == null) {
                var op = Addressables.LoadAssetAsync<T>(typeof(T).Name);
                s_instance = op.WaitForCompletion();
            }
            return s_instance;
        }
    }

#endregion

#if UNITY_EDITOR
    protected virtual async void Awake() {
        await Task.Yield();
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        var group = settings.DefaultGroup;
        var assetPath = AssetDatabase.GetAssetPath(this);
        var guid = AssetDatabase.AssetPathToGUID(assetPath);
        var address = typeof(T).Name;

        var e = settings.CreateOrMoveEntry(guid, group);
        e.address = address;
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, e, true);
        AssetDatabase.SaveAssets();

        FindAllObjects();
    }

    [Button]
    public void FindAllObjects() {
        var guids = AssetDatabase.FindAssets("t:" + typeof(TObj).Name);
        _objects = new TObj[guids.Length];
        for (var i = 0; i < guids.Length; ++i) {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            _objects[i] = AssetDatabase.LoadAssetAtPath<TObj>(path);
        }
    }
#endif

    [SerializeField] private TObj[] _objects;

    public bool TryGetObjectById(string id, out TObj obj) {
        var idx = _objects.FindIndexOf(o => o.Id == id);
        if (idx >= 0) {
            obj = _objects[idx];
            return true;
        }
        ContextLogger.LogFormat(Severity.Error, "REPOSITORY", "Object with id '{0}' not found in {1}.", id, typeof(T).Name);
        obj = null;
        return false;
    }

    public TObj this[string id] => TryGetObjectById(id, out var obj) ? obj : null;
}
}
