#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UIElements;

namespace PJL.GameplayTags.Editor
{
    public class GameplayTagsSettings : SettingsProvider
    {
        private const string FileName = "GameplayTagsManagerInit.cs";
        private string _newTextField;
        private SerializedObject _settings;

        public GameplayTagsSettings(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path,
            scopes, keywords)
        {
        }

        private string TargetPath([CallerFilePath] string file = null)
        {
            if (file == null) return "";
            var dir = Directory.GetParent(Path.GetDirectoryName(file)).FullName;
            return Path.Combine(dir, FileName);
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = GameplayTagsSource.SerializedObject;
        }

        public override void OnDeactivate()
        {
        }

        public override void OnGUI(string searchContext)
        {
            var source = (GameplayTagsSource)_settings.targetObject;
            string delete = null;

            if (GUILayout.Button("Apply"))
            {
                using (var writer = File.CreateText(TargetPath()))
                {
                    writer.WriteLine("namespace PJL.GameplayTags\n{");
                    writer.WriteLine("    public static partial class GameplayTagsManager\n    {");

                    writer.WriteLine($"        internal const int NumTags = {source._tags.Count + 1};\n");

                    writer.WriteLine("        internal static readonly string[] Names = \n        {");
                    writer.WriteLine("            \"None\",");
                    foreach (var tag in source._tags) writer.WriteLine($"            \"{tag}\",");

                    writer.WriteLine("        };");

                    writer.WriteLine("    }\n}");
                    writer.Close();
                }

                CompilationPipeline.RequestScriptCompilation();
            }

            EditorGUILayout.BeginHorizontal();
            _newTextField = EditorGUILayout.TextField(_newTextField);
            if (GUILayout.Button("New") && !_newTextField.IsNullOrWhiteSpace())
            {
                source.Add(_newTextField);
                _newTextField = "";
            }

            EditorGUILayout.EndHorizontal();

            var indent = EditorGUI.indentLevel;

            for (var i = 0; i < source._tags.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUI.indentLevel = indent + source._tags[i].Count(c => c == '.');
                EditorGUILayout.LabelField(source._tags[i]);
                EditorGUI.indentLevel = indent;
                if (GUILayout.Button("Remove")) delete = source._tags[i];

                EditorGUILayout.EndHorizontal();
            }

            if (!delete.IsNullOrEmpty()) source.Remove(delete);
        }

        [SettingsProvider]
        public static SettingsProvider Create()
        {
            var path = "Project/PJL/Gameplay Tags";
            return new GameplayTagsSettings(path, SettingsScope.Project, GetSearchKeywordsFromPath(path));
        }
    }
}
#endif
