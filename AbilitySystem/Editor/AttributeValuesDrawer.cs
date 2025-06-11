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

            var kvp = (TagAttributePair)data.GetArrayElementAtIndex(idx).boxedValue;
            if (kvp.Tag == tags[idx]) return;

            var value = new TagAttributePair
            {
                Tag = tags[idx],
                Attribute = new Attribute(){BaseValue = 0, Max = 100, Min = 0}
            };
            data.GetArrayElementAtIndex(idx).boxedValue = value;
        }

        private void RemoveOrphanedData(GameplayTag[] tags, SerializedProperty data)
        {
            for (var i = data.arraySize - 1; i >= 0; i--)
            {
                var k = data.GetArrayElementAtIndex(i);
                var kvp = (TagAttributePair)data.GetArrayElementAtIndex(i).boxedValue;
                if (tags.Length <= i || !tags.Any(t => t.MatchesTagExact(kvp.Tag)))
                    data.DeleteArrayElementAtIndex(i);
            }
        }
    }
}
#endif
