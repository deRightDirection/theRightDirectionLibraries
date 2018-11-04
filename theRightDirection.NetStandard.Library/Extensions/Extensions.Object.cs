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