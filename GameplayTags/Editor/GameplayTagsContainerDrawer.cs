#if UNITY_EDITOR
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

            // ReSharper disable once AssignmentInConditionalExpression
            if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true))
            {
                var asContainer = (GameplayTagsContainer)property.boxedValue;
                ++EditorGUI.indentLevel;

                for (var i = 1; i < GameplayTagsManager.NumTags; ++i)
                {
                    var tag = GameplayTagsManager.Tags[i];
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.indentLevel += tag.Depth;
                    var name = GameplayTagsManager.Names[i];
                    EditorGUILayout.LabelField(asContainer._parents[i] ? name + " *" : name);
                    EditorGUI.indentLevel -= tag.Depth;
                    asContainer._tags[i] = EditorGUILayout.Toggle("", asContainer._tags[i]);
                    EditorGUILayout.EndHorizontal();
                }

                asContainer.UpdateParents();
                property.boxedValue = asContainer;

                --EditorGUI.indentLevel;
            }

            EditorGUI.EndProperty();
        }
    }
}
#endif