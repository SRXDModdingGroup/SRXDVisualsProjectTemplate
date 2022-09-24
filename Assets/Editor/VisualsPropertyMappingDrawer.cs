using SRXDCustomVisuals.Core;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(VisualsPropertyMapping))]
public class VisualsPropertyMappingDrawer : VisualsMappingDrawerBase {
    protected override int PropertyCount => 6;

    protected override void DrawProperties(ref Rect rect, float offset, SerializedProperty property, SerializedProperty typeProperty) {
        var targetProperty = property.FindPropertyRelative(nameof(VisualsPropertyMapping.target));
        var nameProperty = property.FindPropertyRelative(nameof(VisualsPropertyMapping.name));
        
        EditorGUI.PropertyField(rect, targetProperty);
        rect.y += offset;
        EditorGUI.PropertyField(rect, nameProperty);
        rect.y += offset;
        EditorGUI.PropertyField(rect, typeProperty);
        rect.y += offset;
    }
}
