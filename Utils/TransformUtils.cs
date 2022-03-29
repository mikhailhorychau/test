using UnityEditor;
using UnityEngine;

namespace UIScripts.Utils
{
    public static class TransformUtils
    {
        public static void Clear(this Transform transform)
        {
            foreach (Transform tr in transform)
            {
                Object.Destroy(tr.gameObject);
            }
        } 
    }
}