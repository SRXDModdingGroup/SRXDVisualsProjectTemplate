using SRXDCustomVisuals.Core;
using UnityEditor;
using UnityEngine;

public abstract class VisualsMappingDrawerBase : PropertyDrawer {
    protected abstract int PropertyCount { get; }
    
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
        
        var typeProperty = property.FindPropertyRelative("type");
        var scaleProperty = property.FindPropertyRelative("scale");
        var biasProperty = property.FindPropertyRelative("bias");
        
        DrawProperties(ref rect, offset, property, typeProperty);
        scaleProperty.vector4Value = (VisualsParamType) typeProperty.intValue switch {
            VisualsParamType.Bool => Util.FloatToVector4(EditorGUI.Toggle(rect, "Invert", scaleProperty.vector4Value.x < 0f) ? -1f : 1f, 1f),
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
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        float offset = EditorGUIUtility.singleLineHeight + 2f;
        
        return property.isExpanded ? PropertyCount * offset : offset;
    }

    protected abstract void DrawProperties(ref Rect rect, float offset, SerializedProperty property, SerializedProperty typeProperty);
}
