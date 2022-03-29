using UnityEngine.EventSystems;

namespace UIScripts
{
    public class UIComponent : UIBehaviour
    {
        // this method executed before all lifecycle methods like Awake(), and works when disabled in hierarchy 
        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            print("dimension change");
            
        }
    }
}