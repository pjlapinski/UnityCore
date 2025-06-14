#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UIElements;

namespace PJL.GameplayTags.Editor
{
    public class GameplayTagsSettings : SettingsProvider
    {
        private string _newTextField;
        private SerializedObject _settings;
        private bool _waiting;

        public GameplayTagsSettings(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path,
            scopes, keywords)
        {
        }

        private string TargetDir => Path.Join(Application.dataPath, "PJLData");
        private string TargetRefPath => Path.Join(TargetDir, "PJL.asmref");
        private string TargetPath => Path.Join(TargetDir, "GameplayTagsManagerInit.cs");

        private static List<NamedBuildTarget> AllBuildTargets()
        {
            var staticFields = typeof(NamedBuildTarget).GetFields(BindingFlags.Public | BindingFlags.Static);
            var buildTargets = new List<NamedBuildTarget>();

            foreach (var staticField in staticFields)
            {
                // We exclude 'Unknown' because this can throw errors when used with certain methods.
                if (staticField.Name == "Unknown")
                    continue;

                var isObsolete = staticField.GetCustomAttribute<ObsoleteAttribute>() != null;
                if (staticField.FieldType == typeof(NamedBuildTarget) && !isObsolete)
                    buildTargets.Add((NamedBuildTarget)staticField.GetValue(null));
            }

            return buildTargets;
        }

        private async Task WriteChanges()
        {
            var source = (GameplayTagsSource)_settings.targetObject;
            if (!Directory.Exists(TargetDir)) Directory.CreateDirectory(TargetDir);
            if (!File.Exists(TargetRefPath))
            {
                await using var writer = File.CreateText(TargetRefPath);
                await writer.WriteLineAsync("{");
                await writer.WriteLineAsync("    \"reference\": \"PJL\"");
                await writer.WriteLineAsync("}");
                writer.Close();
            }
            await using (var writer = File.CreateText(TargetPath))
            {
                await writer.WriteLineAsync("namespace PJL.GameplayTags\n{");
                await writer.WriteLineAsync("    public static partial class GameplayTagsManager\n    {");

                await writer.WriteLineAsync($"        internal const int NumTags = {source._tags.Count + 1};\n");

                await writer.WriteLineAsync("        internal static string[] NamesInit() => new []\n        {");
                await writer.WriteLineAsync("            \"None\",");
                foreach (var tag in source._tags) await writer.WriteLineAsync($"            \"{tag}\",");
                await writer.WriteLineAsync("        };\n");

                await writer.WriteLineAsync("        internal static string[] Names = NamesInit();");


                await writer.WriteLineAsync("    }\n}");
                writer.Close();
            }

            await Task.Yield();
            AssetDatabase.Refresh();
            await Task.Yield();

            foreach (var target in AllBuildTargets())
            {
                PlayerSettings.GetScriptingDefineSymbols(target, out var symbols);
                if (symbols.Contains("PJL_GAMEPLAY_TAGS_GENERATED")) continue;
                var newSymbols = new string[symbols.Length + 1];
                Array.Copy(symbols, newSymbols, symbols.Length);
                newSymbols[symbols.Length] = "PJL_GAMEPLAY_TAGS_GENERATED";
                PlayerSettings.SetScriptingDefineSymbols(target, newSymbols);
            }
        }

        private void RefreshData() =>
            ((GameplayTagsSource)_settings.targetObject).RefreshData(TargetPath);

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = GameplayTagsSource.SerializedObject;
        }

        public override void OnDeactivate()
        {
        }

        public override async void OnGUI(string searchContext)
        {
            var source = (GameplayTagsSource)_settings.targetObject;
            string delete = null;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Apply"))
            {
                await WriteChanges();
                GameplayTagsManager.Names = GameplayTagsManager.NamesInit();
                return;
            }

            if (GUILayout.Button("Refresh"))
            {
                RefreshData();
                return;
            }
            EditorGUILayout.EndHorizontal();

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
