using UnityEngine.Events;

namespace UIScripts.Abstract
{
    public interface IClickable
    {
        public UnityEvent OnClick { get; }
    }
}