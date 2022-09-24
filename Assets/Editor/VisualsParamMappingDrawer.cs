using SRXDCustomVisuals.Core;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(VisualsParamMapping))]
public class VisualsParamMappingDrawer : VisualsMappingDrawerBase {
    protected override int PropertyCount => 6;

    protected override void DrawProperties(ref Rect rect, float offset, SerializedProperty property, SerializedProperty typeProperty) {
        var nameProperty = property.FindPropertyRelative(nameof(VisualsParamMapping.name));
        var parameterProperty = property.FindPropertyRelative(nameof(VisualsParamMapping.parameter));
        
        EditorGUI.PropertyField(rect, nameProperty);
        rect.y += offset;
        EditorGUI.PropertyField(rect, typeProperty);
        rect.y += offset;
        EditorGUI.PropertyField(rect, parameterProperty);
        rect.y += offset;
    }
}
