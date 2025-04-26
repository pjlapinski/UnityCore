#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PJL.GameplayTags.Editor {
[CustomPropertyDrawer(typeof(GameplayTag))]
public class GameplayTagDrawer : PropertyDrawer {
    private GUIContent[] _contents;
    private int _selection = -1;

    private void Init(SerializedProperty property) {
        if (_contents == null) {
            _contents = new GUIContent[GameplayTagsManager.NumTags];
            for (var i = 0; i < GameplayTagsManager.NumTags; ++i) {
                _contents[i] = new GUIContent(GameplayTagsManager.Names[i]);
            }
        }
        if (_selection == -1) {
            _selection = property.FindPropertyRelative("_runtimeIndex").intValue;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        Init(property);

        EditorGUI.BeginProperty(position, label, property);

        var previous = _selection;
        _selection = EditorGUI.Popup(position, label, _selection, _contents);
        if (previous != _selection) {
            var idx = property.FindPropertyRelative("_runtimeIndex");
            var parent = property.FindPropertyRelative("_directParentIndex");
            var depth = property.FindPropertyRelative("_depth");
            var tag = GameplayTagsManager.Tags[_selection];
            idx.intValue = tag._runtimeIndex;
            parent.intValue = tag._directParentIndex;
            depth.intValue = tag._depth;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUIUtility.singleLineHeight;
    }
}
}
#endif
