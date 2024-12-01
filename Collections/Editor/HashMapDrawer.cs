using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace PJL.Collections.Editor {
public static class KvpExtension {
    public static KeyValue<TKey, TValue> ToKeyValue<TKey, TValue>(KeyValuePair<TKey, TValue> kvp) =>
        new(kvp.Key, kvp.Value);
}

[CustomPropertyDrawer(typeof(HashMap<,>), true)]
public class HashMapDrawer : PropertyDrawer {
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
        foreach (var val in obj) valuesField.GetArrayElementAtIndex(i++).boxedValue = KvpExtension.ToKeyValue(val);
        return valuesField;
    }
}
}
#endif
