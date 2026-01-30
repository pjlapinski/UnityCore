using UnityEngine;
using UnityEngine.AddressableAssets;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using System.Threading.Tasks;
#endif

namespace PJL.Data
{
    public class AddressableScriptableSingleton<T> : ScriptableObject where T : AddressableScriptableSingleton<T>
    {
        private static T s_instance;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                {
                    var op = Addressables.LoadAssetAsync<T>(typeof(T).Name);
                    s_instance = op.WaitForCompletion();
                }

                return s_instance;
            }
        }

#if UNITY_EDITOR
        protected virtual async void Awake()
        {
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
        }
#endif
    }
}
