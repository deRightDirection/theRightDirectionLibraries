using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using theRightDirection.Attributes;
using theRightDirection.Comparers;

namespace theRightDirection
{
    public static partial class Extensions
    {
        public static IEnumerable<string> ListProperties(this object from, bool includePropertiesFromBaseType = false)
        {
            IEnumerable<PropertyInfo> sourceProperties = GetProperties(from.GetType(), includePropertiesFromBaseType);
            return sourceProperties.Select(x => x.Name);
        }
        public static void CopyProperties(this object from, object to, bool includePropertiesFromBaseType = false, string[] excludedProperties = null)
        {
            if (to == null)
            {
                return;
            }
            IEnumerable<PropertyInfo> sourceProperties = GetProperties(from.GetType(), includePropertiesFromBaseType);
            IEnumerable<PropertyInfo> targetProperties = GetProperties(to.GetType(), includePropertiesFromBaseType);
            IEnumerable<PropertyInfo> commonProperties = sourceProperties.Intersect(targetProperties, new PropertyInfoComparer());
            foreach (var commonProperty in commonProperties)
            {
                if (excludedProperties != null
                  && excludedProperties.Contains(commonProperty.Name))
                    continue;
                var hasExcludeAttribute = FindAttribute(commonProperty);
                if (!hasExcludeAttribute)
                {
                    var value = commonProperty.GetValue(from, null);
                    if (commonProperty.CanWrite)
                    {
                        commonProperty.SetValue(to, value, null);
                    }
                }
            }
        }

        public static bool Compare<T>(this T object1, T object2)
        {
            //Get the type of the object
            Type type = typeof(T);

            //return false if any of the object is false
            if (object1 == null || object2 == null)
                return false;

            //Loop through each properties inside class and get values for the property from both the objects and compare
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.Name != "ExtensionData")
                {
                    string Object1Value = string.Empty;
                    string Object2Value = string.Empty;
                    if (type.GetProperty(property.Name).GetValue(object1, null) != null)
                        Object1Value = type.GetProperty(property.Name).GetValue(object1, null).ToString();
                    if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                        Object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                    if (Object1Value.Trim() != Object2Value.Trim())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static int GetHashCodeOnProperties(this object inspect)
        {
            return inspect.GetType().GetTypeInfo().DeclaredProperties.Select(o => o.GetValue(inspect)).GetListHashCode();
        }

        public static int GetListHashCode(this IEnumerable<object> sequence)
        {
            return sequence.Where(x => x != null)
                .Select(item => item.GetHashCode())
                .Aggregate((total, nextCode) => total ^ nextCode);
        }

        private static bool FindAttribute(PropertyInfo commonProperty)
        {
            var attribute = commonProperty.GetCustomAttribute<ExcludeFromCopyPropertyAttribute>();
            return attribute != null;
        }

        private static IEnumerable<PropertyInfo> GetProperties(Type type, bool recursive)
        {
            if (recursive)
            {
                List<PropertyInfo> properties = new List<PropertyInfo>();
                properties.AddRange(GetProperties(type));
                var typeInfo = type.GetTypeInfo();
                while (typeInfo.BaseType != null)
                {
                    var baseType = typeInfo.BaseType;
                    properties.AddRange(GetProperties(baseType));
                    typeInfo = baseType.GetTypeInfo();
                }
                return properties;
            }
            return GetProperties(type);
        }

        private static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.DeclaredProperties;
        }
    }
}