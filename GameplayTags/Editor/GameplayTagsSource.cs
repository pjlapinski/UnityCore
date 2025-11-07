#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace PJL.GameplayTags.Editor
{
    internal class GameplayTagsSource : ScriptableObject
    {
        private const string DirectoryPath = "PJLData/Editor/";
        private const string RelativePath = DirectoryPath + "GameplayTags.asset";
        private const string AssetPath = "Assets/" + RelativePath;
        [SerializeField] public List<string> _tags;

        internal static GameplayTagsSource Instance
        {
            get
            {
                var settings = AssetDatabase.LoadAssetAtPath<GameplayTagsSource>(AssetPath);
                if (settings == null)
                {
                    settings = CreateInstance<GameplayTagsSource>();
                    settings._tags = new List<string>();
                    if (!Directory.Exists(Path.Join(Application.dataPath, DirectoryPath)))
                        Directory.CreateDirectory(Path.Join(Application.dataPath, DirectoryPath));
                    AssetDatabase.CreateAsset(settings, AssetPath);
                    AssetDatabase.SaveAssets();
                }

                return settings;
            }
        }

        internal static SerializedObject SerializedObject => new(Instance);

        internal void Add(string tag)
        {
            if (_tags.Contains(tag)) return;
            Insert(tag);
            var dot = tag.LastIndexOf('.');
            if (dot > -1) Add(tag[..dot]);
        }

        internal void Insert(string tag)
        {
            var emptyIdx = _tags.IndexOf(string.Empty);
            if (emptyIdx == -1) _tags.Add(tag);
            else _tags[emptyIdx] = tag;
        }

        internal void Remove(string tag)
        {
            var indices = _tags
                .ZipWithIndex()
                .Where(pair => pair.Value.StartsWith(tag))
                .Select(pair => pair.Index)
                .ToArray();

            foreach (var index in indices)
                _tags[index] = string.Empty;

            while (_tags.Count > 0 && _tags[^1].IsNullOrEmpty())
                _tags.RemoveAt(_tags.Count - 1);
        }

        internal void RefreshData(string sourceFilePath)
        {
            if (!File.Exists(sourceFilePath)) return;
            _tags.Clear();
            using var reader = File.OpenText(sourceFilePath);
            reader.ReadLine(); // namespace
            reader.ReadLine(); // {
            reader.ReadLine(); // class
            reader.ReadLine(); // {
            reader.ReadLine(); // NumTags
            reader.ReadLine(); //
            reader.ReadLine(); // Names =
            reader.ReadLine(); // {
            reader.ReadLine(); // None
            var currentLine = reader.ReadLine();
            while (!currentLine?.EndsWith("};") ?? false)
            {
                var firstIdx = currentLine.IndexOf('"') + 1;
                _tags.Add(currentLine[firstIdx..^2]);
                currentLine = reader.ReadLine();
            }
        }
    }
}
#endif
