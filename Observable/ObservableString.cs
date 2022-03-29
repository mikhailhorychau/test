using System;

namespace UIScripts.Observable
{
    [Serializable]
    public class ObservableString : ObservableProperty<string>
    {
        public void SetValue(int value) => Value = value.ToString();
    }
}