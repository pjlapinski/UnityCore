using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace PJL.Data.Editor
{
    [CustomPropertyDrawer(typeof(MinMaxRange))]
    public class MinMaxRangeDrawer : PropertyDrawer
    {
        private SerializedProperty _min, _max;
        private const float FieldWidth = .25f;
        private const float FieldLabelWidth = 30f;
        private const float FieldLabelMargin = 4f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _min ??= property.FindPropertyRelative("<Min>k__BackingField");
            _max ??= property.FindPropertyRelative("<Max>k__BackingField");

            EditorGUI.BeginProperty(position, label, property);
            var width = EditorGUIUtility.labelWidth;
            var x = position.x;

            EditorGUI.LabelField(new(x, position.y, width, position.height), label);
            x += width;

            var totalW = position.width - EditorGUIUtility.labelWidth;
            width = FieldLabelWidth;
            EditorGUI.LabelField(new(x, position.y, width, position.height), "Min");
            x += width;
            width = totalW * FieldWidth;
            _min.floatValue = EditorGUI.FloatField(new(x, position.y, width, position.height), _min.floatValue);
            x += width + FieldLabelMargin;
            width = FieldLabelWidth;
            EditorGUI.LabelField(new(x, position.y, width, position.height), "Max");
            x += width;
            width = totalW * FieldWidth;
            _max.floatValue = EditorGUI.FloatField(new(x, position.y, width, position.height), _max.floatValue);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUIUtility.singleLineHeight;
    }

    [CustomPropertyDrawer(typeof(MinMaxRangeInt))]
    public class MinMaxRangeIntDrawer : PropertyDrawer
    {
        private SerializedProperty _min, _max;
        private const float FieldWidth = .25f;
        private const float FieldLabelWidth = 30f;
        private const float FieldLabelMargin = 4f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _min ??= property.FindPropertyRelative("<Min>k__BackingField");
            _max ??= property.FindPropertyRelative("<Max>k__BackingField");

            EditorGUI.BeginProperty(position, label, property);
            var width = EditorGUIUtility.labelWidth;
            var x = position.x;

            EditorGUI.LabelField(new(x, position.y, width, position.height), label);
            x += width;

            var totalW = position.width - EditorGUIUtility.labelWidth;
            width = FieldLabelWidth;
            EditorGUI.LabelField(new(x, position.y, width, position.height), "Min");
            x += width;
            width = totalW * FieldWidth;
            _min.intValue = EditorGUI.IntField(new(x, position.y, width, position.height), _min.intValue);
            x += width + FieldLabelMargin;
            width = FieldLabelWidth;
            EditorGUI.LabelField(new(x, position.y, width, position.height), "Max");
            x += width;
            width = totalW * FieldWidth;
            _max.intValue = EditorGUI.IntField(new(x, position.y, width, position.height), _max.intValue);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUIUtility.singleLineHeight;
    }
}
#endif
