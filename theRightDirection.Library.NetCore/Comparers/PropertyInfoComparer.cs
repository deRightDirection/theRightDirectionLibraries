using System.Collections.Generic;
using System.Reflection;

namespace theRightDirection.Comparers
{
    public sealed class PropertyInfoComparer : IEqualityComparer<PropertyInfo>
    {
        public bool Equals(PropertyInfo x, PropertyInfo y)
        {
            return x.Name.ToLowerInvariant().Equals(y.Name.ToLowerInvariant());
        }

        public int GetHashCode(PropertyInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}