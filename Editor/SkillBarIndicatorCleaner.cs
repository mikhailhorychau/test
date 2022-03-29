using System.Linq;
using UnityEditor;

using UnityEngine;

namespace UIScripts.Editor
{
    public class SkillBarIndicatorCleaner : EditorWindow
    {
        private bool _changesIndicators;
        private bool _bannedIndicators;
        private bool _changeButton;

        [MenuItem("Window/UITools/SkillBarIndicatorCleaner")]
        static void Init()
        {
            SkillBarIndicatorCleaner window = (SkillBarIndicatorCleaner) GetWindow(typeof(SkillBarIndicatorCleaner));
            window.Show();
        }

        private void OnGUI()
        {
            _changesIndicators = EditorGUILayout.Toggle("Changes indicator", _changesIndicators);
            _bannedIndicators = EditorGUILayout.Toggle("Banned indicator", _bannedIndicators);
            
            _changeButton = GUILayout.Button("Change");

            if (_changeButton)
            {
                var stage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
                if (stage != null)
                {
                    stage.prefabContentsRoot.GetComponentsInChildren<SkillBar>()
                        .ToList()
                        .ForEach(ClearSkillBar);
                }
            }
        }

        private void ClearSkillBar(SkillBar skillBar)
        {
            // PrefabUtility.UnpackPrefabInstance(skillBar.gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            skillBar.BannedIndicators.Clear();
            skillBar.ChangesIndicators.Clear();
            DestroyImmediate(skillBar.transform.Find("ChangesIndicators").gameObject);
            DestroyImmediate(skillBar.transform.Find("BannedIndicators").gameObject);
            
        }
    }
}