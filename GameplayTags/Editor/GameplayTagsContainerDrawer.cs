#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PJL.GameplayTags.Editor
{
    [CustomPropertyDrawer(typeof(GameplayTagsContainer))]
    public class GameplayTagsContainerDrawer : PropertyDrawer
    {
        private bool[] _expanded, _displayed;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var tags = GetTags();
            if (_expanded is not { Length: GameplayTagsManager.NumTags })
            {
                _expanded = new bool[GameplayTagsManager.NumTags];
                _displayed = new bool[GameplayTagsManager.NumTags];
            }

            using var ps = new EditorGUI.PropertyScope(position, label, property);
            var foldoutRect = position;
            foldoutRect.height = EditorGUIUtility.singleLineHeight;
            var toggleX = position.width * 4 / 5;
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true);
            if (property.isExpanded)
            {
                var asContainer = (GameplayTagsContainer)property.boxedValue;
                if (asContainer._tags.Length < GameplayTagsManager.NumTags)
                    asContainer = new GameplayTagsContainer();
                using (new EditorGUI.IndentLevelScope())
                {
                    var y = position.y + EditorGUIUtility.singleLineHeight;
                    for (var i = 0; i < tags.Length; ++i)
                    {
                        var tag = tags[i];
                        var idx = tag._runtimeIndex;
                        var name = GameplayTagsManager.Names[idx];
                        name = asContainer._parents[idx] ? name + " *" : name;
                        
                        _displayed[idx] = false;
                        if (tag._directParentIndex > -1 && !_expanded[tag._directParentIndex])
                            continue;
                        _displayed[idx] = true;

                        using (new EditorGUI.IndentLevelScope(tag.Depth))
                        {
                            var rect = position;
                            rect.y = y;
                            rect.height = EditorGUIUtility.singleLineHeight;
                            if (!IsLeaf(tags, i))
                                _expanded[idx] = EditorGUI.Foldout(rect, _expanded[idx], name);
                            else
                            {
                                _expanded[idx] = true;
                                EditorGUI.LabelField(rect, name);
                            }
                            using (new EditorGUI.IndentLevelScope(-tag.Depth))
                            {
                                var toggleRect = rect;
                                toggleRect.x = toggleX;
                                asContainer._tags[idx] = EditorGUI.Toggle(toggleRect, "", asContainer._tags[idx]);
                            }
                        }
                        y += EditorGUIUtility.singleLineHeight;
                    }
                }

                asContainer.UpdateParents();
                property.boxedValue = asContainer;
            }
        }

        private static GameplayTag[] GetTags() => GameplayTagsManager
                .Tags
                .Where(t => !t.IsNone)
                .OrderBy(t => t.Name.ToString())
                .ToArray();

        private bool IsLeaf(GameplayTag[] tags, int idx)
        {
            for (var i = idx + 1; i < tags.Length; ++i)
            {
                if (tags[i].Depth < tags[idx].Depth) return true;
                if (tags[i].Depth > tags[idx].Depth) return false;
            }

            return true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded) return EditorGUIUtility.singleLineHeight;
            return EditorGUIUtility.singleLineHeight * ((_displayed?.Count(a => a) ?? 0) + 1);
        }
    }
}
#endif
