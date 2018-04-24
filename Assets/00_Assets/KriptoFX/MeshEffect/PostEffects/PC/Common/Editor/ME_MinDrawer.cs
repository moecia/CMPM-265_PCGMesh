using UnityEditor;
using UnityEngine;

namespace UnityStandardAssets.CinematicEffects
{
    [CustomPropertyDrawer(typeof(ME_MinAttribute_ME))]
    internal sealed class ME_MinDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ME_MinAttribute_ME attribute = (ME_MinAttribute_ME) base.attribute;

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                int v = EditorGUI.IntField(position, label, property.intValue);
                property.intValue = (int)Mathf.Max(v, attribute.min);
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                float v = EditorGUI.FloatField(position, label, property.floatValue);
                property.floatValue = Mathf.Max(v, attribute.min);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use Min with float or int.");
            }
        }
    }
}
