using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UIScripts.Utils.StaticInitializer
{
    public static class StaticInitializer
    {
        private const BindingFlags SetPropertiesFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        public static List<PropertyInfo> GetSetProperties(Type type, Type propertiesType) =>
            type.GetProperties(SetPropertiesFlags)
                .ToList()
                .FindAll(property => property.PropertyType == propertiesType);

        
        public static void SetStaticIfExists(this object obj, Dictionary<string, string> pairs)
        {
            var properties = GetSetProperties(obj.GetType(), typeof(string));
            
            pairs
                .ToList()
                .ForEach(pair =>
                {
                    var property = properties.Find(propertyInfo => propertyInfo.Name == pair.Key);
                    if (property != null)
                    {
                        property.SetValue(obj, pair.Value, null);
                    }
                });
        }

        public static void SetStaticDeeper(this object obj, Dictionary<string, string> pairs)
        {
            
        }
    }
}