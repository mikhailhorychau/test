using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UIScripts.CommonComponents.Popup.ToolTip.Editor
{
    [CustomEditor(typeof(ToolTipsSystemSettings))]
    public class ToolTipSettingsEditor : UnityEditor.Editor
    {
        private bool _expanded;
        private string _status = "Header";
        private string _addCategoryName = "Category Name";
        private bool _addCategoryButton;
        
        private Dictionary<string, bool> _categoriesFoldout = new Dictionary<string, bool>();
        
        private SerializedProperty data;
        private SerializedProperty categories;

        public void OnEnable()
        {
            data = serializedObject.FindProperty("data");
            categories = data.FindPropertyRelative("categories");
        }

        public override void OnInspectorGUI()
        {
            _expanded = EditorGUILayout.BeginFoldoutHeaderGroup(_expanded, _status);
            var toolTipList = new List<string>();

            for (var i = 0; i < categories.arraySize; i++)
            {
                var categoryName = categories.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue;
                var categoryItems = categories.GetArrayElementAtIndex(i).FindPropertyRelative("items");
                toolTipList.Add(categoryName);

                var items = new List<string>();
                for (var j = 0; j < categoryItems.arraySize; j++)
                {
                    items.Add(categoryItems.GetArrayElementAtIndex(j).stringValue);
                }
                
                DrawCategory(categoryName, items);
                if (!_categoriesFoldout.ContainsKey(categoryName))
                {
                    _categoriesFoldout.Add(categoryName, false);
                }
            }

            if (_expanded)
            {
                EditorGUI.indentLevel++;
                DrawItemHeader(ref _addCategoryButton, ref _addCategoryName);
                if (_addCategoryButton)
                {
                    if (!CategoriesNames().Contains(_addCategoryName))
                    {
                        categories.InsertArrayElementAtIndex(0);
                        var newProperty = categories.GetArrayElementAtIndex(0).FindPropertyRelative("name");
                        newProperty.stringValue = _addCategoryName;
                        newProperty.serializedObject.ApplyModifiedProperties();
                    }
                }
                // toolTipList.ForEach(DrawCategory);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private List<string> CategoriesNames()
        {
            var list = new List<string>();
            for (int i = 0; i < categories.arraySize; i++)
            {
                list.Add(categories.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue);
            }

            return list;
        }

        private void DrawCategory(string category, List<string> items)
        {
            _categoriesFoldout[category] = EditorGUILayout.Foldout(_categoriesFoldout[category], category);
            if (_categoriesFoldout[category])
            {
                EditorGUI.indentLevel++;
                var addBtn = false;
                var itemName = "";
                DrawItemHeader(ref addBtn, ref itemName);

                if (addBtn && !items.Contains(itemName))
                {
                    var index = CategoriesNames().IndexOf(category);
                    var propItems = categories.GetArrayElementAtIndex(index).FindPropertyRelative("items");
                    propItems.InsertArrayElementAtIndex(0);
                    categories.serializedObject.ApplyModifiedProperties();
                    propItems.GetArrayElementAtIndex(0).stringValue = itemName;
                    propItems.serializedObject.ApplyModifiedProperties();
                }
                
                items.ForEach(DrawItem);
                EditorGUI.indentLevel--;
            }
        }
        
        private void DrawItemHeader(ref bool btn, ref string input)
        {
            EditorGUILayout.BeginHorizontal();
            input = EditorGUILayout.TextField("Name", input, GUILayout.MinWidth(128));
            btn = GUILayout.Button("+", GUILayout.Width(18));

            EditorGUILayout.EndHorizontal();
        }

        private void DrawItem(string itemName)
        {
            GuiLine();
            EditorGUILayout.LabelField(itemName);
            GuiLine();
        }
        
        void GuiLine(int i_height = 1)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height );

            rect.height = i_height;
            
            EditorGUI.DrawRect(rect, new Color ( 0.5f,0.5f,0.5f, 1 ) );
        }
    }
}