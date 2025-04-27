using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PJL.GameplayTags
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
            _tags.Add(tag);
            var dot = tag.LastIndexOf('.');
            if (dot > -1) Add(tag[..dot]);

            _tags.Sort();
        }

        internal void Remove(string tag)
        {
            _tags.RemoveAll(t => t.StartsWith(tag));
            _tags.Sort();
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
