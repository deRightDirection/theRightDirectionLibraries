namespace theRightDirection
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines a collection of extensions for Types.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the <see cref="Type"/> from the specified typeName.
        /// </summary>
        /// <param name="typeName">
        /// The type name as a <see cref="string"/>.
        /// </param>
        /// <returns>
        /// Returns the <see cref="Type"/> if exists; else null.
        /// </returns>
        public static Type GetTypeByName(this string typeName)
        {
            // Attempt to get the type straight from the speficied string.
            var result = Type.GetType(typeName);

            if (result != null)
            {
                return result;
            }

            // Attempt to see if the type is a common system type.
            var proxyType = DateTimeKind.Local;
            var assembly = proxyType.GetType().GetTypeInfo().Assembly;

            return assembly.ExportedTypes.FirstOrDefault(typeInfo => typeInfo.Name == typeName);
        }

        /// <summary>
        /// Gets the assembly name from a given type.
        /// </summary>
        /// <param name="type">
        /// The type to get the assembly name from.
        /// </param>
        /// <returns>
        /// Returns the assembly name.
        /// </returns>
        public static string GetAssemblyName(this Type type)
        {
            if (type == null) return string.Empty;

            var assemblyName = type.AssemblyQualifiedName;

            if (!assemblyName.Contains(",")) return assemblyName;

            var assemblySplit = assemblyName.Split(',');
            return assemblySplit[1];
        }
    }
}