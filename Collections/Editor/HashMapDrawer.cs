using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace PJL.Collections.Editor {
[CustomPropertyDrawer(typeof(HashMap<,>), true)]
public class HashMapDrawer : PropertyDrawer {
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUI.BeginProperty(position, label, property);

    var valuesField = property.FindPropertyRelative("_initialValues");
    EditorGUI.PropertyField(position, valuesField, new GUIContent(label.text));

    EditorGUI.EndProperty();
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
    var valuesField = property.FindPropertyRelative("_initialValues");
    var valuesFieldHeight = EditorGUI.GetPropertyHeight(valuesField);
    return valuesFieldHeight;
  }
}
}
#endif