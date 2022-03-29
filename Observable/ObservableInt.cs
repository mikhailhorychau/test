using System;

namespace UIScripts.Observable
{
    [Serializable]
    public class ObservableInt : ObservableProperty<int>
    {
        public ObservableInt()
        {
        }

        public ObservableInt(int value) : base(value)
        {
        }
    }
}