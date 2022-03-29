using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UIScripts.Utils
{
    public static class ListUtils
    {
        public static bool IsEmpty<T>(this List<T> list) => list.Count == 0;

        public static bool IsNullOrEmpty<T>(this List<T> collection) =>
            collection == null || collection.Count == 0;
        
    }
}