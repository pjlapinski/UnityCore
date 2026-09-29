using System;
using System.IO;
using System.Linq;
using System.Text;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace PJL.Data.Editor
{
    [CanEditMultipleObjects, CustomEditor(typeof(ConstStringList))]
    public class ConstStringListInspector : UnityEditor.Editor
    {
        private const string DirectoryPath = "PJLData/ConstStrings";

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Write"))
                UpdateStaticClass();
        }

        private static bool ValidateName(string name) =>
            !name.IsNullOrWhiteSpace() &&
            !char.IsDigit(name[0]) &&
            name.All(ch => char.IsLetterOrDigit(ch) || ch == '_');

        private void UpdateStaticClass()
        {
            var obj = (ConstStringList)target;
            if (!ValidateName(obj.GroupName)) return;

            var path = Path.Join(Application.dataPath, DirectoryPath);

            if (!Directory.Exists(path)) 
                Directory.CreateDirectory(path);

            if (ValidateName(obj.Namespace))
                path = Path.Join(path, obj.Namespace);

            if (!Directory.Exists(path)) 
                Directory.CreateDirectory(path);

            path = Path.Join(path, obj.GroupName + ".cs");
            var classString = GetClassString();
            File.WriteAllText(path, classString);
            AssetDatabase.Refresh();
        }

        private string Indent(int i) => string.Join("", Enumerable.Repeat("    ", Math.Max(0, i)));

        private string GetClassString()
        {
            var obj = (ConstStringList)target;
            var sb = new StringBuilder();
            var indent = 0;

            if (ValidateName(obj.Namespace))
            {
                sb.Append("namespace ").AppendLine(obj.Namespace).AppendLine("{");
                ++indent;
            }

            sb
                .Append(Indent(indent)).Append("public static class ").AppendLine(obj.GroupName)
                .Append(Indent(indent)).AppendLine("{");
            ++indent;
            foreach (var value in obj.Values)
            {
                if (!ValidateName(value)) continue;
                sb.Append(Indent(indent)).Append("public const string ").Append(value).Append(" = nameof(").Append(value).AppendLine(");");
            }

            sb.Append(Indent(--indent)).AppendLine("}");
            if (ValidateName(obj.Namespace)) sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
