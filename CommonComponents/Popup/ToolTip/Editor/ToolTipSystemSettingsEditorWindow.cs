using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UIScripts.CommonComponents.Popup.ToolTip.Editor
{
    public class ToolTipSystemSettingsEditorWindow : EditorWindow
    {
        private bool _expanded;
        private string _status = "Header";
        private string _addCategoryName = "Category Name";
        private bool _addCategoryButton;

        private Dictionary<string, bool> _categoriesFoldout = new Dictionary<string, bool>();

        [MenuItem("UI/TooltipSystem/Settings")]
        static void Init()
        {
            GetWindow<ToolTipSystemSettingsEditorWindow>();
        }

        void OnGUI()
        {
            _expanded = EditorGUILayout.BeginFoldoutHeaderGroup(_expanded, _status);
            var toolTipList = ToolTipsNames.Categories.ToList();
            toolTipList.ForEach(tooltip =>
            {
                if (!_categoriesFoldout.ContainsKey(tooltip.Key))
                    _categoriesFoldout.Add(tooltip.Key, false);
            });

            if (_expanded)
            {
                EditorGUI.indentLevel++;
                DrawItemHeader(ref _addCategoryButton, ref _addCategoryName);
                if (_addCategoryButton)
                {
                    ToolTipsNames.AddCategory(_addCategoryName);
                }
                toolTipList.ForEach(DrawCategory);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawCategory(KeyValuePair<string, HashSet<string>> category)
        {
            _categoriesFoldout[category.Key] = EditorGUILayout.Foldout(_categoriesFoldout[category.Key], category.Key);
            if (_categoriesFoldout[category.Key])
            {
                EditorGUI.indentLevel++;
                var addBtn = false;
                var itemName = "";
                DrawItemHeader(ref addBtn, ref itemName);
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
    }
}