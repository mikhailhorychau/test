using System;
using System.Collections.Generic;
using UIScripts.Screens.Panels.HeaderMenu.Bonuses;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace UIScripts.Editor
{
    [CustomPropertyDrawer(typeof(UniqueBonusPresenterContainer))]
    public class UniquePairContainerDrawer : PropertyDrawer
    {
        private const string KEYS = "keys";
        private const string VALUES = "values";
        private const string ADD_ITEM = "Add Type-Presenter Pair";
        private const string WARNING_LOG_FORMAT = "{0}  already used in container";
        private const string REMOVE_ITEM = "x";

        private const float ADD_BTN_WIDTH = 230f;
        private const float ADD_BTN_HEIGHT = 23f;

        private float LineHeight => EditorGUIUtility.singleLineHeight;
        private float ButtonWidth => EditorGUIUtility.singleLineHeight;
        private float Spacing => EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var keys = GetKeysProperty(property);
            var values = GetValuesProperty(property);

            for (var i = 0; i < keys.arraySize; i++)
            {
                DrawItem(position, property, i);
            }
            
            var btnX =  (position.x + position.width - ADD_BTN_WIDTH) / 2 + 2;
            var btnRect = new Rect(btnX , position.y + keys.arraySize * (LineHeight + Spacing) + Spacing, ADD_BTN_WIDTH, ADD_BTN_HEIGHT);
            var addBtn = GUI.Button(btnRect, ADD_ITEM);
            
            if (addBtn)
                InsertNewPair(property);
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var keys = GetKeysProperty(property);
            return keys.arraySize * LineHeight + Spacing * keys.arraySize + ADD_BTN_HEIGHT;
        }

        private void DrawItem(Rect position, SerializedProperty property, int index)
        {
            var keys = GetKeysProperty(property);
            var values = GetValuesProperty(property);
            
            var key = keys.GetArrayElementAtIndex(index);
            var value = values.GetArrayElementAtIndex(index);

            var y = position.y + (LineHeight + Spacing) * index;
            var keyWidth = EditorGUIUtility.labelWidth;
            var valueWidth = position.width - keyWidth - ButtonWidth - Spacing * 2;
            
            var keyPos = new Rect(position.x, y, keyWidth, LineHeight);
            var valuePos = new Rect(position.x + keyPos.width + Spacing, y, valueWidth, LineHeight);
            var btnPos = new Rect(valuePos.x + valuePos.width + Spacing, y, ButtonWidth, LineHeight);

            key.enumValueIndex = EditorGUI.Popup(keyPos, key.enumValueIndex, key.enumDisplayNames);
            key.serializedObject.ApplyModifiedProperties();

            var newValue = EditorGUI.ObjectField(valuePos, value.objectReferenceValue, typeof(BonusPresenter), true);
            if (!GetValues(property).Contains(newValue))
            {
                value.objectReferenceValue = newValue;
                value.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                if (newValue != null && newValue != value.objectReferenceValue)
                    Debug.LogWarning(String.Format(WARNING_LOG_FORMAT, newValue));
            }

            var removeBtn = GUI.Button(btnPos, REMOVE_ITEM);
            if (removeBtn)
            {
                keys.DeleteArrayElementAtIndex(index);
                values.DeleteArrayElementAtIndex(index);
                property.serializedObject.ApplyModifiedProperties();
            }
        }
        
        private void InsertNewPair(SerializedProperty property)
        {
            var keys = GetKeysProperty(property);
            var values = GetValuesProperty(property);
            
            keys.InsertArrayElementAtIndex(keys.arraySize);
            keys.GetArrayElementAtIndex(keys.arraySize - 1).enumValueIndex = default;
            keys.serializedObject.ApplyModifiedProperties();
                
            values.InsertArrayElementAtIndex(values.arraySize);
            values.GetArrayElementAtIndex(values.arraySize - 1).objectReferenceValue = default;
            values.serializedObject.ApplyModifiedProperties();
            
            
        }

        private SerializedProperty GetKeysProperty(SerializedProperty property) => property.FindPropertyRelative(KEYS);
        private SerializedProperty GetValuesProperty(SerializedProperty property) => property.FindPropertyRelative(VALUES);

        private List<Object> GetValues(SerializedProperty property) => GetList(property, VALUES);

        private List<Object> GetList(SerializedProperty property, string name)
        {
            var list = new List<Object>();
            var keys = property.FindPropertyRelative(name);
            for (var i = 0; i < keys.arraySize; i++)
            {
                list.Add(keys.GetArrayElementAtIndex(i).objectReferenceValue);
            }

            return list;
        }
        
    }
}