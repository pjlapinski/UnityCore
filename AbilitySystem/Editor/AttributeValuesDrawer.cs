#if UNITY_EDITOR
using System.Linq;
using System.Reflection;
using PJL.GameplayTags;
using UnityEditor;

namespace PJL.AbilitySystem.Editor
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
                if (typeof(AttributeSet)
                        .GetField("_attributes", BindingFlags.NonPublic | BindingFlags.Instance)
                        !.GetValue(set) is GameplayTag[] tags)
                {
                    for (var i = 0; i < tags.Length; ++i)
                        AddTagToDataIfMissing(tags, i, dataField);

                    RemoveOrphanedData(tags, dataField);

                    for (var i = 0; i < dataField.arraySize; ++i)
                        EditorGUILayout.PropertyField(dataField.GetArrayElementAtIndex(i));
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void AddTagToDataIfMissing(GameplayTag[] tags, int idx, SerializedProperty data)
        {
            for (var i = data.arraySize; i < tags.Length; i++)
                data.InsertArrayElementAtIndex(i);

            var kvp = (TagAttributeData)data.GetArrayElementAtIndex(idx).boxedValue;
            if (kvp._tag == tags[idx]) return;

            var value = new TagAttributeData
            {
                _tag = tags[idx],
                _attribute = 0,
                _min = 0,
                _max = 100
            };
            data.GetArrayElementAtIndex(idx).boxedValue = value;
        }

        private void RemoveOrphanedData(GameplayTag[] tags, SerializedProperty data)
        {
            for (var i = data.arraySize - 1; i >= 0; i--)
            {
                var k = data.GetArrayElementAtIndex(i);
                var kvp = (TagAttributeData)data.GetArrayElementAtIndex(i).boxedValue;
                if (tags.Length <= i || !tags.Any(t => t.MatchesTagExact(kvp._tag)))
                    data.DeleteArrayElementAtIndex(i);
            }
        }
    }
}
#endif
