using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UIScripts.CommonComponents.Popup.ToolTip.Editor
{
    [CustomEditor(typeof(ToolTipsContainer))]
    public class ToolTipSystemEditor : UnityEditor.Editor
    {
        private bool _addButton;
        private SerializedProperty _tooltips;
        private ToolTipsScreen _screen;

        private void OnEnable()
        {
            _tooltips = serializedObject.FindProperty("_components");
        }

        public override void OnInspectorGUI()
        {
            var screenType = serializedObject.FindProperty("screen").enumValueIndex;
            EditorGUILayout.Space(5f);
            _screen = (ToolTipsScreen) screenType;
            _screen = (ToolTipsScreen) EditorGUILayout.EnumPopup("Screen", _screen);

            serializedObject.FindProperty("screen").enumValueIndex = (int) _screen;
            serializedObject.FindProperty("screen").serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space(5f);
            EditorGUILayout.PropertyField(_tooltips);
            GuiLine();
            GetArray().ForEach(DrawToolTipWrapper);
        }

        private void DrawToolTipWrapper(SerializedProperty property)
        {
            if (property.objectReferenceValue == null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Empty");
                EditorGUILayout.EndHorizontal();
                GuiLine();
                return;
            }
            
            var obj = new SerializedObject(property.objectReferenceValue);
            var names = ToolTipsNames.GetScreenTooltipNames(_screen);
            var key = obj.FindProperty("key").stringValue;
            var stringValues = names.Keys.Select(enumKey => enumKey.ToString()).ToList();
            var index = stringValues.Contains(key) ? stringValues.IndexOf(key) : 0;

            var fieldObj = 
                EditorGUILayout.ObjectField("Component", 
                    property.objectReferenceValue, typeof(ToolTipComponent),true);
            index = EditorGUILayout.Popup("Key", index, stringValues.ToArray());

            var description = 
                EditorGUILayout.TextField("Description", obj.FindProperty("description").stringValue);

            obj.FindProperty("description").stringValue = description;
            obj.FindProperty("key").stringValue = stringValues[index];
            property.objectReferenceValue = fieldObj;
            
            property.serializedObject.ApplyModifiedProperties();
            obj.ApplyModifiedProperties();
            

            GuiLine();
        }

        private List<SerializedProperty> GetArray()
        {
            var list = new List<SerializedProperty>();
            for (var i = 0; i < _tooltips.arraySize; i++)
            {
                list.Add(_tooltips.GetArrayElementAtIndex(i));
            }

            return list;
        }

        void GuiLine(int i_height = 1)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height );

            rect.height = i_height;
            
            EditorGUI.DrawRect(rect, new Color ( 0.5f,0.5f,0.5f, 1 ) );
            EditorGUILayout.Space(5f);
        }
    }
}