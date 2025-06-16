#if UNITY_EDITOR
using System.Linq;
using PJL.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace PJL.GameplayTags.Editor
{
    [CustomPropertyDrawer(typeof(GameplayTag))]
    public class GameplayTagDrawer : PropertyDrawer
    {
        private GUIContent[] _contents;

        private void Init(SerializedProperty property)
        {
            _contents ??= GameplayTagsManager
                .Names
                .Skip(1)
                .Where(n => !n.IsNullOrEmpty())
                .OrderBy(n => n)
                .Prepend(GameplayTagsManager.Names[0])
                .Select(n => new GUIContent(n))
                .ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);

            EditorGUI.BeginProperty(position, label, property);

            var idx = property.FindPropertyRelative("_runtimeIndex");
            if (GameplayTagsManager.Names.Length <= idx.intValue || GameplayTagsManager.Names[idx.intValue].IsNullOrEmpty())
            {
                idx.intValue = 0;
            }
            else
            {
                var previous = _contents.FindIndexOf(n => n.text == GameplayTagsManager.Names[idx.intValue]);
                var i = EditorGUI.Popup(position, label, previous, _contents);
                var name = _contents[i].text;
                idx.intValue = GameplayTagsManager.Names.IndexOf(name);
                if (previous != idx.intValue)
                {
                    var parent = property.FindPropertyRelative("_directParentIndex");
                    var depth = property.FindPropertyRelative("_depth");
                    var tag = GameplayTagsManager.Tags[idx.intValue];
                    parent.intValue = tag._directParentIndex;
                    depth.intValue = tag._depth;
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUIUtility.singleLineHeight;
    }
}
#endif
