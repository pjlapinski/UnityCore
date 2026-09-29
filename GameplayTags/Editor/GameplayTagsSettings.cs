#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

namespace PJL.GameplayTags.Editor
{
    public class GameplayTagsSettings : SettingsProvider
    {
        private string _newTextField;
        private SerializedObject _settings;
        private bool _waiting;

        public GameplayTagsSettings(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords)
        { }

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

        private string Indent(int i) => string.Join("", Enumerable.Repeat("    ", Math.Max(0, i)));

        private void WriteChanges()
        {
            var source = (GameplayTagsSource)_settings.targetObject;
            if (!Directory.Exists(TargetDir)) Directory.CreateDirectory(TargetDir);
            if (!File.Exists(TargetRefPath))
            {
                var sbd = new StringBuilder();
                sbd.AppendLine("{")
                    .Append(Indent(1)).AppendLine("\"reference\": \"PJL\"")
                    .AppendLine("}");
                File.WriteAllText(TargetRefPath, sbd.ToString());
            }

            var sb = new StringBuilder();
            sb
                .AppendLine("namespace PJL.GameplayTags")
                .AppendLine("{")
                .Append(Indent(1)).AppendLine("public static partial class GameplayTagsManager")
                .Append(Indent(1)).AppendLine("{")
                .Append(Indent(2)).AppendLine($"internal const int NumTags = {source._tags.Count + 1};")
                .Append(Indent(2)).AppendLine("internal static string[] NamesInit() => new []")
                .Append(Indent(2)).AppendLine("{")
                .Append(Indent(3)).AppendLine("\"None\",");
            foreach (var tag in source._tags)
                sb.Append(Indent(3)).Append('"').Append(tag).Append('"').AppendLine(",");
            sb
                .Append(Indent(2)).AppendLine("};")
                .AppendLine("")
                .Append(Indent(2)).AppendLine("internal static string[] Names = NamesInit();")
                .Append(Indent(1)).AppendLine("}")
                .AppendLine("}");

            File.WriteAllText(TargetPath, sb.ToString());
            AssetDatabase.Refresh();

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
            RefreshData();
        }

        public override void OnDeactivate()
        {
        }

        public override void OnGUI(string searchContext)
        {
            var source = (GameplayTagsSource)_settings.targetObject;
            string delete = null;

            // if (GUILayout.Button("Refresh"))
            // {
            //     RefreshData();
            //     return;
            // }

            EditorGUILayout.BeginHorizontal();
            _newTextField = EditorGUILayout.TextField(_newTextField);
            if (GUILayout.Button("Add") && !_newTextField.IsNullOrWhiteSpace())
            {
                source.Add(_newTextField);
                _newTextField = "";
            }
            if (GUILayout.Button("Apply"))
            {
                WriteChanges();
                GameplayTagsManager.Names = GameplayTagsManager.NamesInit();
                _newTextField = string.Empty;
                return;
            }

            EditorGUILayout.EndHorizontal();

            var indent = EditorGUI.indentLevel;

            var tags = source._tags.Where(t => !t.IsNullOrEmpty()).OrderBy(t => t).ToArray();
            foreach (var tag in tags)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUI.indentLevel = indent + tag.Count(c => c == '.');
                EditorGUILayout.LabelField(tag);
                EditorGUI.indentLevel = indent;
                if (GUILayout.Button("Remove")) delete = tag;

                EditorGUILayout.EndHorizontal();
            }

            if (!delete.IsNullOrEmpty()) source.Remove(delete);
        }

        [SettingsProvider]
        public static SettingsProvider Create()
        {
            const string path = "Project/PJL/Gameplay Tags";
            return new GameplayTagsSettings(path, SettingsScope.Project, GetSearchKeywordsFromPath(path));
        }
    }
}
#endif
