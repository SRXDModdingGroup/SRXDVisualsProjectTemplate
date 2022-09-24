using SRXDCustomVisuals.Core;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(VisualsParamMapping))]
public class VisualsParamMappingDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        var rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        float offset = EditorGUIUtility.singleLineHeight + 2f;

        property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label);
        rect.y += offset;

        if (!property.isExpanded) {
            EditorGUI.EndProperty();
            
            return;
        }
        
        var nameProperty = property.FindPropertyRelative(nameof(VisualsParamMapping.name));
        var typeProperty = property.FindPropertyRelative(nameof(VisualsParamMapping.type));
        var parameterProperty = property.FindPropertyRelative(nameof(VisualsParamMapping.parameter));
        var scaleProperty = property.FindPropertyRelative(nameof(VisualsParamMapping.scale));
        var biasProperty = property.FindPropertyRelative(nameof(VisualsParamMapping.bias));

        EditorGUI.PropertyField(rect, nameProperty);
        rect.y += offset;
        EditorGUI.PropertyField(rect, typeProperty);
        rect.y += offset;
        EditorGUI.PropertyField(rect, parameterProperty);
        rect.y += offset;
        scaleProperty.vector4Value = (VisualsParamType) typeProperty.intValue switch {
            VisualsParamType.Bool => Util.FloatToVector4(EditorGUI.Toggle(rect, "Invert", scaleProperty.vector4Value.x <= 0f) ? -1f : 1f, 1f),
            VisualsParamType.Int => Util.FloatToVector4(EditorGUI.IntField(rect, "Scale", Mathf.RoundToInt(scaleProperty.vector4Value.x)), 1f),
            VisualsParamType.Float => Util.FloatToVector4(EditorGUI.FloatField(rect, "Scale", scaleProperty.vector4Value.x), 1f),
            VisualsParamType.Vector => Util.Vector3ToVector4(EditorGUI.Vector3Field(rect, "Scale", scaleProperty.vector4Value), 1f),
            VisualsParamType.Color => EditorGUI.ColorField(rect, "Scale", scaleProperty.vector4Value),
            _ => scaleProperty.vector4Value
        };
        rect.y += offset;
        biasProperty.vector4Value = (VisualsParamType) typeProperty.intValue switch {
            VisualsParamType.Bool => Vector4.zero,
            VisualsParamType.Int => Util.FloatToVector4(EditorGUI.IntField(rect, "Bias", Mathf.RoundToInt(biasProperty.vector4Value.x)), 0f),
            VisualsParamType.Float => Util.FloatToVector4(EditorGUI.FloatField(rect, "Bias", biasProperty.vector4Value.x), 0f),
            VisualsParamType.Vector => Util.Vector3ToVector4(EditorGUI.Vector3Field(rect, "Bias", biasProperty.vector4Value), 0f),
            VisualsParamType.Color => EditorGUI.ColorField(rect, "Bias", biasProperty.vector4Value),
            _ => biasProperty.vector4Value
        };

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        float offset = EditorGUIUtility.singleLineHeight + 2f;
        
        return property.isExpanded ? 6f * offset : offset;
    }
}
