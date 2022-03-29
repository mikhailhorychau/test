using System;
using System.Collections.Generic;
using System.Linq;
using UIScripts.Screens.SessionPanel;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Utils
{
    public static class Utils
    {
        public static float PointsToTime(this int pointsPerSecond, int value) => (float) value / pointsPerSecond;
        
        public static void SnapTo(this ScrollRect scroller, RectTransform child)
        {
            Canvas.ForceUpdateCanvases();
            Vector2 viewportLocalPosition = scroller.viewport.localPosition;
            Vector2 childLocalPosition   = child.localPosition;
            Vector2 result = new Vector2(
                0 - (viewportLocalPosition.x + childLocalPosition.x),
                0 - (viewportLocalPosition.y + childLocalPosition.y)
            );
            scroller.content.localPosition = result;
        }
        
        public static string GetTransPath(this Transform trans)
        {
            if (!trans.parent)
            {
                return trans.name;

            }
            return GetTransPath(trans.parent) + "/" + trans.name;
        }

        public static UIColorStyle GetColorBySector(this SectorType type)
        {
            switch (type)
            {
                case SectorType.Empty : return UIColorStyle.EmptySector;
                case SectorType.Best : return UIColorStyle.BestSector;
                case SectorType.Faster : return UIColorStyle.FasterSector;
                case SectorType.Slower : return UIColorStyle.SlowerSector;
                default : return UIColorStyle.EmptySector;
            }
        }
        
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[App.runtime.Rnd.Next(s.Length)]).ToArray());
        }

        public static void Then(this bool condition, Action action)
        {
            if (condition)
                action();
        }

        public static List<T> GetTopLevelChildComponents<T>(this Transform transform)
        {
            var components = new List<T>();
            foreach (Transform tr in transform)
            {
                if (tr.TryGetComponent<T>(out var component))
                {
                    components.Add(component);
                }
            }

            return components;
        }
    }
}