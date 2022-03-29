using UnityEngine;
using UnityEngine.Pool;

namespace UIScripts.Utils
{
    public static class CanvasExt
    {
        public static Canvas GetRootCanvas(this GameObject gameObject)
        {
            var list = ListPool<Canvas>.Get();
            gameObject.GetComponentsInParent(false, list);

            if (list.Count == 0)
                return null;

            var root = list[list.Count - 1];
            foreach (var canvas in list)
            {
                if (canvas.isRootCanvas || canvas.overrideSorting)
                {
                    root = canvas;
                    break;
                }
            }

            ListPool<Canvas>.Release(list);

            return root;
        }
    }
}