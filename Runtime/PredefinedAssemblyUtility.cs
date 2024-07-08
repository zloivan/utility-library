// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System;
using System.Collections.Generic;

namespace IKhom.UtilitiesLibrary.Runtime
{
    public static class PredefinedAssemblyUtility
    {
        private enum AssemblyType
        {
            AssemblyCSharp,
            AssemblyCSharpEditor,
            AssemblyCSharpFirstPass,
            AssemblyCSharpFirstPassEditor
        }

        /// <summary>
        /// Retrieves the AssemblyType enum value corresponding to the given assembly name.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly to identify its type.</param>
        /// <returns>The AssemblyType enum value corresponding to the given assembly name, or null if the assembly name does not match any known assembly.</returns>
        private static AssemblyType? GetAssemblyType(string assemblyName)
        {
            const string ASSEMBLY_CSHARP = "Assembly-CSharp";
            const string ASSEMBLY_CSHARP_EDITOR = "Assembly-CSharp-Editor";
            const string ASSEMBLY_CSHARP_FIRSTPASS = "Assembly-CSharp-firstpass";
            const string ASSEMBLY_CSHARP_FIRSTPASS_EDITOR = "Assembly-CSharp-firstpass-Editor";

            return assemblyName switch
            {
                ASSEMBLY_CSHARP => AssemblyType.AssemblyCSharp,
                ASSEMBLY_CSHARP_EDITOR => AssemblyType.AssemblyCSharpEditor,
                ASSEMBLY_CSHARP_FIRSTPASS => AssemblyType.AssemblyCSharpFirstPass,
                ASSEMBLY_CSHARP_FIRSTPASS_EDITOR => AssemblyType.AssemblyCSharpFirstPassEditor,
                _ => null
            };
        }

        /// <summary>
        /// Adds all types from the specified assembly that are assignable from the given interface type to the provided collection.
        /// </summary>
        /// <param name="assembly">The array of types representing the assembly to search for types.</param>
        /// <param name="interfaceType">The interface type to search for in the assembly.</param>
        /// <param name="types">The collection to add the found types to.</param>
        private static void AddTypesFromAssembly(Type[] assembly, Type interfaceType, ICollection<Type> types)
        {
            if (assembly == null)
                return;

            for (var i = 0; i < assembly.Length; i++)
            {
                var type = assembly[i];

                if (type != interfaceType && interfaceType.IsAssignableFrom(type))
                {
                    types.Add(type);
                }
            }
        }

        /// <summary>
        /// Retrieves all types from the specified assemblies that are assignable from the given interface type.
        /// </summary>
        /// <param name="interfaceType">The interface type to search for in the assemblies.</param>
        /// <returns>A list of types that are assignable from the given interface type and found in the specified assemblies.</returns>
        public static List<Type> GetType(Type interfaceType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var assemblyTypes = new Dictionary<AssemblyType, Type[]>();
            var types = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var assemblyType = GetAssemblyType(assembly.GetName().Name);
                if (assemblyType != null)
                {
                    assemblyTypes.Add((AssemblyType)assemblyType, assembly.GetTypes());
                }
            }

            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharp], interfaceType, types);
            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharpFirstPass], interfaceType, types);

            return types;
        }
    }
}