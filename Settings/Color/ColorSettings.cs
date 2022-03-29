using System;
using UnityEngine;

namespace UIScripts
{
    [Serializable]
    public class ColorSettings
    {
        public Color text;
        public Color title;
        public Color textGreen;
        public Color textYellow;
        public Color textRed;
        public Color textBestParameter;
        public Color selection;
        public Color background1;
        public Color background2;
        public Color mainBackground;
        public Color active;
        public Color activeHover;
        public Color progressGreen;
        public Color building;
        public Color bestSector;
        public Color fasterSector;
        public Color slowerSector;
        public Color skillBarStart;
        public Color skillBarMiddle;
        public Color skillBarEnd;
        public Color playerTableColor;
        public Color contractWorkColor;
        public Color contractClientColor;

        public Color Pick(UIColorStyle uiColorStyle)
        {
            switch (uiColorStyle)
            {
                case UIColorStyle.Active : return active;
                case UIColorStyle.Background1 : return background1;
                case UIColorStyle.Background2 : return background2;
                case UIColorStyle.ActiveHover : return activeHover;
                case UIColorStyle.Selection : return selection;
                case UIColorStyle.Text : return text;
                case UIColorStyle.Title : return title;
                case UIColorStyle.MainBackground : return mainBackground;
                case UIColorStyle.TextGreen : return textGreen;
                case UIColorStyle.TextRed : return textRed;
                case UIColorStyle.TextYellow : return textYellow;
                case UIColorStyle.TextBestParameter : return textBestParameter;
                case UIColorStyle.Clear : return Color.clear;
                case UIColorStyle.ProgressGreen : return progressGreen;
                case UIColorStyle.Building : return building;
                case UIColorStyle.White : return Color.white;
                case UIColorStyle.Black : return Color.black;
                case UIColorStyle.Red : return Color.red;
                case UIColorStyle.BestSector : return bestSector;
                case UIColorStyle.FasterSector : return fasterSector;
                case UIColorStyle.SlowerSector : return slowerSector;
                case UIColorStyle.EmptySector : return selection;
                case UIColorStyle.SkillBarStart : return skillBarStart;
                case UIColorStyle.SkillBarMiddle : return skillBarMiddle;
                case UIColorStyle.SkillBarEnd : return skillBarEnd;
                case UIColorStyle.PlayerTableColor : return playerTableColor;
                case UIColorStyle.ContractWorkColor : return contractWorkColor;
                case UIColorStyle.ContractClientColor : return contractClientColor;
                default: return text;
            }
        }
    }
}