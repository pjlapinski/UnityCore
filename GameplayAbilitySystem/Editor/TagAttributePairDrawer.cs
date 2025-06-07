#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.GameplayAbilitySystem.Editor
{
    [CustomPropertyDrawer(typeof(TagAttributePair))]
    public class TagAttributePairDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var obj = (TagAttributePair)property.boxedValue;
            EditorGUI.BeginProperty(position, label, property);

            EditorGUILayout.LabelField(obj._tag.ToString());
            ++EditorGUI.indentLevel;
            obj._baseValue = Mathf.Clamp(EditorGUILayout.FloatField("Base Value", obj._baseValue), obj._min, obj._max);
            var min = EditorGUILayout.FloatField("Min", obj._min);
            obj._min = min < obj._min ? min : obj._min;
            var max = EditorGUILayout.FloatField("Max", obj._max);
            obj._max = max > obj._min ? max : obj._max;
            --EditorGUI.indentLevel;

            property.boxedValue = obj;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            base.GetPropertyHeight(property, label) - EditorGUIUtility.singleLineHeight;
    }
}
#endif
