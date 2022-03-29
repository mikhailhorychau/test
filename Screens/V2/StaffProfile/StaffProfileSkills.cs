using System;
using UIScripts.CommonComponents;
using UnityEngine;

namespace UIScripts.Screens.V2.StaffProfile
{
    [Serializable]
    public class StaffProfileSkills
    {
        [SerializeField] private SkillProgressParam leadership;
        [SerializeField] private SkillProgressParam level;

        public SkillProgressParam Leadership => leadership;
        public SkillProgressParam Level => level;
    }
}