using UnityEngine;

namespace UIScripts.Screens.V2.BonusRequirement
{
    public class SingleRequirementButton : MonoBehaviour, ISingleRequirementButton
    {
        [RequireInterface(typeof(IButton))] 
        [SerializeField]
        private GameObject button;

        [SerializeField] private RequirementPresenter presenter;

        private IButton _button;

        public IButton Button => _button ??= button.GetComponent<IButton>();
        public RequirementPresenter Requirement => presenter;

        public string Title 
        {
            set => Button.Text = value;
        }

        public void SetRequirementsDone(bool isDone) => Button.SetInteractivity(isDone);
    }

    public interface ISingleRequirementButton
    {
        public IButton Button { get; }
        public RequirementPresenter Requirement { get; }
        
        public string Title { set; }

        public void SetRequirementsDone(bool isDone);
    }
}