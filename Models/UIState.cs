using System;

namespace UIScripts
{
    [Serializable]
    public struct UIState<T>
    {
        public T common;
        public T hover;
        public T pressed;
        public T disabled;
    }
}