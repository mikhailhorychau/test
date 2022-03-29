using System;

namespace UIScripts.Tab
{
    [Serializable]
    public class ToggleState<T>
    {
        public T common;
        public T hover;
        public T pressed;
        public T active;
    }
}