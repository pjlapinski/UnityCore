using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PJL.GameplayTags {
internal class GameplayTagsSource : ScriptableObject {
    [SerializeField] public List<string> _tags;

    private const string RelativePath = "Editor/GameplayTags.asset";
    private const string AssetPath = "Assets/" + RelativePath;

    internal static GameplayTagsSource Instance {
        get {
            var settings = AssetDatabase.LoadAssetAtPath<GameplayTagsSource>(AssetPath);
            if (settings == null) {
                settings = CreateInstance<GameplayTagsSource>();
                settings._tags = new List<string>();
                if (!Directory.Exists(Path.Join(Application.dataPath, RelativePath))) {
                    Directory.CreateDirectory(Path.Join(Application.dataPath, Directory.GetParent(RelativePath).Name));
                }
                AssetDatabase.CreateAsset(settings, AssetPath);
                AssetDatabase.SaveAssets();
            }

            return settings;
        }
    }

    internal static SerializedObject SerializedObject => new(Instance);

    internal void Add(string tag) {
        if (_tags.Contains(tag)) {
            return;
        }
        _tags.Add(tag);
        var dot = tag.LastIndexOf('.');
        if (dot > -1) {
            Add(tag[..dot]);
        }

        _tags.Sort();
    }

    internal void Remove(string tag) {
        _tags.RemoveAll(t => t.StartsWith(tag));
        _tags.Sort();
    }
}
}
