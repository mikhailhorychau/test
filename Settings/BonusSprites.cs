using System;
using UIScripts.Screens.Panels.HeaderMenu.Bonuses;
using UnityEngine;

namespace UIScripts
{
    [Serializable]
    public class BonusSprites
    {
        private const string FOLDER_PATH = "UI/Images/Icons/Bonuses/";
        private const string PREFIX = "Bonuses_i";
        private const string REPUTATION = FOLDER_PATH + PREFIX + "TeamRep";
        private const string TRUST_LVL = FOLDER_PATH + PREFIX + "Trust";
        private const string AUTHORITY_POINTS = FOLDER_PATH + PREFIX + "Authority";

        [SerializeField] private Sprite teamReputation;
        [SerializeField] private Sprite trustLvl;
        [SerializeField] private Sprite authorityPoints;
        [SerializeField] private Sprite constructorPoints;
        [SerializeField] private Sprite mechanicPoints;
        [SerializeField] private Sprite negotiationPoints;
        [SerializeField] private Sprite researchPoints;
        [SerializeField] private Sprite pilotExperiencePoints;
        [SerializeField] private Sprite tyresExperiencePoints;
        [SerializeField] private Sprite engineUsingExperiencePoints;
        [SerializeField] private Sprite duration;

        public void Initialize()
        {
            
        }
        
        public Sprite Pick(BonusType type)
        {
            return type switch
            {
                BonusType.Default => null,
                BonusType.TeamReputation => teamReputation,
                BonusType.TrustLvl => trustLvl,
                BonusType.AuthorityPoints => authorityPoints,
                BonusType.ConstructorPoints => constructorPoints,
                BonusType.MechanicPoints => mechanicPoints,
                BonusType.NegotiationPoints => negotiationPoints,
                BonusType.ResearchPoints => researchPoints,
                BonusType.PilotExperiencePoints => pilotExperiencePoints,
                BonusType.TyresExperiencePoints => tyresExperiencePoints,
                BonusType.EngineUsingExperiencePoints => engineUsingExperiencePoints,
                BonusType.Duration => duration,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}