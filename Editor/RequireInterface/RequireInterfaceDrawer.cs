using UnityEditor;
using UnityEngine;

namespace UIScripts.Editor.RequireInterface
{
    [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public class RequireInterfaceDrawer : PropertyDrawer
    {
        private const string NOT_INTERFACE = "{0} is not interface";
        private const string NOT_REFERENCE = "{0} is not reference";
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var cls = attribute as RequireInterfaceAttribute;
            
            if (!cls.Type.IsInterface)
            {
                DrawEmptyField(position, property, label, string.Format(NOT_INTERFACE, cls.Type.Name));
                return;
            }

            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                DrawEmptyField(position, property, label, string.Format(NOT_REFERENCE, property.displayName));
                return;
            }
            
            var title = $"[{cls.Type.Name}]";

            var prevObj = property.objectReferenceValue;
            
            property.objectReferenceValue = 
                EditorGUI.ObjectField(position, title, property.objectReferenceValue, typeof(GameObject), true);

            var gameObject = (GameObject) property.objectReferenceValue;
            
            if (gameObject == null) return;

            if (!gameObject.GetComponent(cls.Type))
            {
                property.objectReferenceValue = prevObj;
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        private void DrawEmptyField(Rect position, SerializedProperty property, GUIContent label, string description)
        {
            EditorGUI.LabelField(position, label);
            var rect = new Rect(position.x + EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing,
                position.y, position.width - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(rect, description);
        }
    }
    
}