// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System;

namespace IKhom.UtilitiesLibrary.Runtime
{
    /// <summary>
    /// Provides methods to create instances and get types from assemblies using reflection.
    /// </summary>
    public static class ReflectionCreator
    {
        /// <summary>
        /// Creates an instance of a type by its fully qualified name.
        /// </summary>
        /// <param name="strFullyQualifiedName">The fully qualified name of the type.</param>
        /// <returns>An instance of the type if found, otherwise null.</returns>
        public static object CreateInstanceByTypeName(string strFullyQualifiedName)
        {
            var type = Type.GetType(strFullyQualifiedName);

            if (type != null)
                return Activator.CreateInstance(type);

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type);
            }

            return null;
        }

        /// <summary>
        /// Gets a Type object for a type by its fully qualified name from all loaded assemblies.
        /// </summary>
        /// <param name="strFullyQualifiedName">The fully qualified name of the type.</param>
        /// <returns>A Type object if found, otherwise null.</returns>
        public static Type GetTypeFromAllAssemblies(string strFullyQualifiedName)
        {
            var type = Type.GetType(strFullyQualifiedName);

            if (type != null) return type;

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null)
                    return type;
            }

            return null;
        }
    }
}