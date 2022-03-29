using System;
using UnityEngine;

namespace UIScripts
{
    public class RequireInterfaceAttribute : PropertyAttribute
    {
        public Type Type { get; }
        public RequireInterfaceAttribute(Type type)
        {
            Type = type;
        }
    }
}