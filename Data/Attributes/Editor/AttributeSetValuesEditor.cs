#if UNITY_EDITOR
using System.Linq;
using PJL.GameplayTags;
using UnityEditor;

namespace PJL.Data.Attributes.Editor
{
    [CustomEditor(typeof(AttributeSetValues)), CanEditMultipleObjects]
    public class AttributeSetValuesEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var attrSetProp = serializedObject.FindProperty("_attributeSet");
            var valuesProp = serializedObject.FindProperty("<Values>k__BackingField");

            EditorGUILayout.PropertyField(attrSetProp);
            EditorGUILayout.Space();

            var set = attrSetProp.objectReferenceValue as AttributeSet;
            if (set != null)
            {
                for (var i = 0; i < set.Attributes.Length; ++i)
                    AddTagToDataIfMissing(set.Attributes, i, valuesProp);

                RemoveOrphanedData(set.Attributes, valuesProp);

                for (var i = 0; i < valuesProp.arraySize; ++i)
                {
                    EditorGUILayout.BeginHorizontal();
                    var tagProp = valuesProp.GetArrayElementAtIndex(i).FindPropertyRelative("<Attribute>k__BackingField");
                    EditorGUILayout.LabelField(((GameplayTag)tagProp.boxedValue).ToString());
                    var valueProp = valuesProp.GetArrayElementAtIndex(i).FindPropertyRelative("<Value>k__BackingField");
                    valueProp.floatValue = EditorGUILayout.FloatField(valueProp.floatValue);
                    EditorGUILayout.EndHorizontal();
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
     
        private void AddTagToDataIfMissing(AttributeDefinition[] attrs, int idx, SerializedProperty data)
        {
            for (var i = data.arraySize; i < attrs.Length; i++)
                data.InsertArrayElementAtIndex(i);

            var kvp = (TagAttributeData)data.GetArrayElementAtIndex(idx).boxedValue;
            if (kvp.Attribute == attrs[idx].IdentifierTag) return;

            var value = new TagAttributeData
            {
                Attribute = attrs[idx].IdentifierTag,
                Value = 0f
            };
            data.GetArrayElementAtIndex(idx).boxedValue = value;
        }

        private void RemoveOrphanedData(AttributeDefinition[] attrs, SerializedProperty data)
        {
            for (var i = data.arraySize - 1; i >= 0; i--)
            {
                var kvp = (TagAttributeData)data.GetArrayElementAtIndex(i).boxedValue;
                if (attrs.Length <= i || !attrs.Any(t => t.IdentifierTag.MatchesTagExact(kvp.Attribute)))
                    data.DeleteArrayElementAtIndex(i);
            }
        }
    }
}
#endif
