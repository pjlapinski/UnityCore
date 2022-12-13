#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.Collections.Editor {
[CustomPropertyDrawer(typeof(Set<>), true)]
public class SetDrawer : PropertyDrawer {
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    var valuesField = GetModifiedElements(property);
    if (valuesField == null)
      return;
    EditorGUI.BeginProperty(position, label, property);

    EditorGUI.PropertyField(position, valuesField, new GUIContent(label.text));

    EditorGUI.EndProperty();
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
    var valuesField = GetModifiedElements(property);
    if (valuesField == null)
      return 0f;
    var valuesFieldHeight = EditorGUI.GetPropertyHeight(valuesField);
    return valuesFieldHeight;
  }

  private SerializedProperty GetModifiedElements(SerializedProperty property) {
    var valuesField = property.FindPropertyRelative("_initialValues");
    if (valuesField == null || !Application.isPlaying)
      return valuesField;
    dynamic obj = fieldInfo.GetValue(property.serializedObject.targetObject);
    valuesField.arraySize = obj.Count;
    var i = 0;
    foreach (var val in obj) {
      valuesField.GetArrayElementAtIndex(i++).boxedValue = val;
    }
    return valuesField;
  }
}
}
#endif
