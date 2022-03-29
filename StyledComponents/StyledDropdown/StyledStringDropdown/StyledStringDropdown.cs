using UnityEngine;

namespace UIScripts
{
    public class StyledStringDropdown : StyledDropdown2<StringProps, StyledStringDropdownItem>
    {
        [SerializeField] private TextStyle commonFontStyle;
        [SerializeField] private TextStyle selectedFontStyle;

        protected StyledText styledText;
        protected override void UpdatePlaceholder(StringProps stringProps)
        {
            if (stringProps == null)
                if (currentValue < 0)
                    return;
                else
                    stringProps = new StringProps(currentValue, currentValue.ToString());
                

            if (placeholder != null) 
                placeholder.text = stringProps.value;

            if (!styledText) 
                styledText = placeholder.GetComponent<StyledText>();
            
            styledText.SwapStyle(currentValue != -1 ? selectedFontStyle : commonFontStyle);
        }

        public void SetNextValue()
        {
            var current = items.Find(item => item.GetId() == currentValue);
            var nextIndex = items.IndexOf(current) + 1;
            if (nextIndex >= items.Count) return;
            CurrentValue = items[nextIndex].GetId();
        }

        public void SetPreviousValue()
        {
            var current = items.Find(item => item.GetId() == currentValue);
            var prevIndex = items.IndexOf(current) - 1;
            if (prevIndex < 0) return;
            CurrentValue = items[prevIndex].GetId();
        }
        
    }
}