#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CustomRange))]
public class CustomRangeDrawerprivate : PropertyDrawer
{
    const float SegmentSpacing = 4f;

    private struct FieldInfo
    {
        public GUIContent Label;
        public string PropertyName;
    }

    private static readonly FieldInfo[] _fields = new FieldInfo[]
    {
        new() { Label = new("Min:"), PropertyName = "_min" },
        new() { Label = new("Max:"), PropertyName = "_max" }
    };

    private static float _dpiScale;
    private static float[] _cachedLabelWidths;
    private static Rect[] _labelRects;
    private static Rect[] _fieldRects;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EnsureCache();

        position = EditorGUI.PrefixLabel(position, label);

        int count = _fields.Length;
        float spacing = SegmentSpacing * _dpiScale;

        float totalLabelsWidth = 0f;

        for (int i = 0; i < count; i++)
            totalLabelsWidth += _cachedLabelWidths[i];

        float totalSpacing = SegmentSpacing * count;
        float fieldWidth = (position.width - totalLabelsWidth - totalSpacing) / count;

        PartitionRects(position, count, _cachedLabelWidths, fieldWidth, spacing);

        for (int i = 0; i < count; i++)
        {
            SerializedProperty prop = property.FindPropertyRelative(_fields[i].PropertyName);
            EditorGUI.LabelField(_labelRects[i], _fields[i].Label);
            EditorGUI.PropertyField(_fieldRects[i], prop, GUIContent.none);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
        EditorGUIUtility.singleLineHeight;

    private void EnsureCache()
    {
        float currentScale = EditorGUIUtility.pixelsPerPoint;

        if (_cachedLabelWidths != null && Mathf.Approximately(currentScale, _dpiScale))
            return;

        _dpiScale = currentScale;
        int count = _fields.Length;
        _cachedLabelWidths = new float[count];
        _labelRects = new Rect[count];
        _fieldRects = new Rect[count];

        for (int i = 0; i < count; i++)
        {
            float rawWidth = EditorStyles.label.CalcSize(_fields[i].Label).x;
            _cachedLabelWidths[i] = rawWidth + SegmentSpacing * _dpiScale;
        }
    }

    private static void PartitionRects(Rect position, int count, float[] labelWidths, float fieldWidth, float spacing)
    {
        float x = position.x;

        for (int i = 0; i < count; i++)
        {
            float width = labelWidths[i];

            _labelRects[i] = new(x, position.y, width, position.height);
            x += width;

            _fieldRects[i] = new(x, position.y, fieldWidth, position.height);
            x += fieldWidth + spacing;
        }
    }
}
#endif