#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PJL.GameplayTags;
using UnityEditor;

namespace PJL.GameplayAbilitySystem.Editor
{
    [CustomEditor(typeof(AttributeValues)), CanEditMultipleObjects]
    public class AttributeValuesDrawer : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var setField = serializedObject.FindProperty("_attributeSet");
            var dataField = serializedObject.FindProperty("_data");

            EditorGUILayout.PropertyField(setField);

            var set = setField.objectReferenceValue as AttributeSet;
            if (set != null)
            {
                var tags = typeof(AttributeSet)
                    .GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance)
                    !.GetValue(set) as GameplayTag[];
                if (tags != null)
                {
                    foreach (var tag in tags)
                        AddTagToDataIfMissing(tag, dataField);
                    RemoveOrphanedData(tags, dataField);

                    for (var i = 0; i < dataField.arraySize; ++i)
                        EditorGUILayout.PropertyField(dataField.GetArrayElementAtIndex(i));
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void AddTagToDataIfMissing(GameplayTag tag, SerializedProperty data)
        {
            for (var i = 0; i < data.arraySize; ++i)
            {
                var k = data.GetArrayElementAtIndex(i);
                var kvp = (TagAttributePair)data.GetArrayElementAtIndex(i).boxedValue;
                if (kvp._tag == tag) return;
            }

            var value = new TagAttributePair
            {
                _tag = tag,
                _baseValue = 0,
                _max = 100,
                _min = 0
            };
            data.InsertArrayElementAtIndex(data.arraySize);
            data.GetArrayElementAtIndex(data.arraySize - 1).boxedValue = value;
        }

        private void RemoveOrphanedData(GameplayTag[] tags, SerializedProperty data)
        {
            for (var i = data.arraySize - 1; i >= 0; i--)
            {
                var k = data.GetArrayElementAtIndex(i);
                var kvp = (TagAttributePair)data.GetArrayElementAtIndex(i).boxedValue;
                if (!tags.Any(t => t.MatchesTagExact(kvp._tag)))
                    data.DeleteArrayElementAtIndex(i);
            }
        }
    }
}
#endif
