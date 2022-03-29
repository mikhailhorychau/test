using System;
using System.Collections.Generic;
using TMPro;
using UIScripts.Observable;
using UIScripts.Screens.Panels.HeaderMenu.Bonuses;
using UIScripts.Screens.V2.BonusRequirement;
using UIScripts.Utils.UI;
using UnityEngine;

namespace UIScripts.RaceAction
{
    public interface IRaceCommandView : IDestroyableView
    {
        public void SetState(RaceCommandState state);

        public void SetDuration(int duration);
    }

    public class RaceCommandView : MonoBehaviour, IRaceCommandView
    {
        [SerializeField] private StyledImage img;
        [SerializeField] private RaceCommandDurationView durationView;
        
        public event Action OnViewDestroy;

        public static readonly Dictionary<RaceCommandState, UIColorStyle> Colors =
            new Dictionary<RaceCommandState, UIColorStyle>()
            {
                {RaceCommandState.Active, UIColorStyle.TextGreen},
                {RaceCommandState.Common, UIColorStyle.Title},
                {RaceCommandState.Cooldown, UIColorStyle.TextRed}
            };

        private void OnDestroy()
        {
            OnViewDestroy?.Invoke();
        }

        public void SetState(RaceCommandState state)
        {
            var isActiveImage = state != RaceCommandState.Common;
            var imgColor = Colors[state];
            
            img.gameObject.SetActive(isActiveImage);
            img.SwapColor(imgColor, .1f);
        }

        public void SetDuration(int duration)
        {
            durationView.SetValue(duration);
        }
    }

    public enum RaceCommandState
    {
        Active,
        Cooldown,
        Common
    }

    public class RaceCommandsContext
    {
        public RaceCommandPairInfo Overtake { get; set; }
        public RaceCommandPairInfo Tyres { get; set; }
        public RaceCommandPairInfo Engine { get; set; }
    }

    public class RaceCommandInfo
    {
        public int ID { get; set; }
        public int Duration { get; set; }
        public int Cooldown { get; set; }
        public Dictionary<BonusType, int> Requirements { get; set; }

        public RaceCommandInfo()
        {
            
        }
    }

    public class RaceCommandPairInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public RaceCommandInfo First { get; set; }
        public RaceCommandInfo Second { get; set; }
    }

    public class RaceCommandPairModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public RaceCommandButtonModel First { get; set; }
        public RaceCommandButtonModel Second { get; set; }

        public List<RaceCommandButtonModel> AsList() => new List<RaceCommandButtonModel>() {First, Second};
    }

    public class RaceCommandsModel
    {
        // public Dictionary<BonusType, BonusModel> Bonuses { get; set; }
        public RaceCommandPairModel Overtake { get; set; }
        public RaceCommandPairModel Tyres { get; set; }
        public RaceCommandPairModel Engine { get; set; }
    }

    public class RaceCommandModel
    {
        public int ID { get; set; }
        public ObservableInt Duration { get; } = new ObservableInt();
        public ObservableProperty<RaceCommandState> State { get; } = new ObservableProperty<RaceCommandState>();
    }

    public class RaceCommandButtonModel
    {
        public int ID { get; set; }
        public RaceCommandModel CommandModel { get; set; } = new RaceCommandModel();
        public RequirementButtonData ButtonData { get; set; } = new RequirementButtonData();
    }
}