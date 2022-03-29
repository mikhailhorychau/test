using System;

namespace UIScripts.Utils.UI
{
    public interface IDestroyableView
    {
        public event Action OnViewDestroy;
    }
}