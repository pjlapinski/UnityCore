#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PJL.GameplayTags.Editor
{
    [CustomPropertyDrawer(typeof(GameplayTagsContainer))]
    public class GameplayTagsContainerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var foldoutRect = position;
            foldoutRect.height = EditorGUIUtility.singleLineHeight;
            var toggleX = position.width * 3 / 4;
            // ReSharper disable once AssignmentInConditionalExpression
            if (property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true))
            {
                var asContainer = (GameplayTagsContainer)property.boxedValue;
                if (asContainer._tags.Length < GameplayTagsManager.NumTags)
                    asContainer = new GameplayTagsContainer();
                ++EditorGUI.indentLevel;

                var tags = GetTags();
                var y = position.y + EditorGUIUtility.singleLineHeight;
                for (var i = 0; i < tags.Length; ++i)
                {
                    var rect = position;
                    rect.y = y;
                    rect.height = EditorGUIUtility.singleLineHeight;
                    var toggleRect = rect;
                    toggleRect.x = toggleX;
                    var tag = tags[i];
                    var idx = tags[i]._runtimeIndex;
                    EditorGUI.indentLevel += tag.Depth;
                    var name = GameplayTagsManager.Names[idx];
                    EditorGUI.LabelField(rect, asContainer._parents[idx] ? name + "*" : name);
                    EditorGUI.indentLevel -= tag.Depth;
                    asContainer._tags[idx] = EditorGUI.Toggle(toggleRect, "", asContainer._tags[idx]);
                    y += EditorGUIUtility.singleLineHeight;
                }

                asContainer.UpdateParents();
                property.boxedValue = asContainer;

                --EditorGUI.indentLevel;
            }

            EditorGUI.EndProperty();
        }

        private static GameplayTag[] GetTags() => GameplayTagsManager
                .Tags
                .Where(t => !t.IsNone)
                .OrderBy(t => t.Name.ToString())
                .ToArray();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded) return EditorGUIUtility.singleLineHeight;
            return EditorGUIUtility.singleLineHeight * (GetTags().Length + 1);
        }
    }
}
#endif
