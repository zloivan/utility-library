// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Utilities
{
    /// <summary>
    /// Provides methods to reflect on private fields of assets in Unity's AssetDatabase.
    /// </summary>
    public static class AssetReflectionPrivateFieldService
    {
        /// <summary>
        /// Retrieves a ReflectionObject for an asset of the specified type.
        /// </summary>
        /// <param name="assetTypeName">The type name of the asset.</param>
        /// <returns>A ReflectionObject for the asset if found, otherwise null.</returns>
        public static ReflectionObject GetAssetReflection(string assetTypeName)
        {
            var assetsReferences = AssetDatabase.FindAssets($"t:{assetTypeName}");

            if (assetsReferences == null || assetsReferences.Length == 0) return null;

            var assetPath = assetsReferences.Select(AssetDatabase.GUIDToAssetPath).ToArray()[0];

            if (string.IsNullOrEmpty(assetPath)) return null;

            var assetType = ReflectionCreator.GetTypeFromAllAssemblies(assetTypeName);

            if (assetType == null) return null;

            var asset = AssetDatabase.LoadAssetAtPath(assetPath, assetType);
            return asset == null ? null : new ReflectionObject(asset);
        }

        /// <summary>
        /// Retrieves a ReflectionObject for an asset of the specified type and path requirement.
        /// </summary>
        /// <param name="assetTypeName">The type name of the asset.</param>
        /// <param name="pathRequirements">A string that the asset's path must contain.</param>
        /// <returns>A ReflectionObject for the asset if found, otherwise null.</returns>
        public static ReflectionObject GetAssetReflection(string assetTypeName, string pathRequirements)
        {
            var assetPath = AssetDatabase.FindAssets($"t:{assetTypeName}").Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault(path => path.Contains(pathRequirements));

            if (string.IsNullOrEmpty(assetPath)) return null;

            var assetType = ReflectionCreator.GetTypeFromAllAssemblies(assetTypeName);

            if (assetType == null) return null;

            var asset = AssetDatabase.LoadAssetAtPath(assetPath, assetType);
            return asset == null ? null : new ReflectionObject(asset);
        }
    }

    /// <summary>
    /// Represents an object with reflection capabilities to access its private fields.
    /// </summary>
    public class ReflectionObject
    {
        private readonly object _obj;
        private readonly Type _objType;

        /// <summary>
        /// Gets the type of the reflected object.
        /// </summary>
        public Type ObjType => _objType;

        /// <summary>
        /// Gets the reflected object.
        /// </summary>
        public object Obj => _obj;

        /// <summary>
        /// Initializes a new instance of the ReflectionObject class.
        /// </summary>
        /// <param name="obj">The object to reflect upon.</param>
        public ReflectionObject(object obj)
        {
            _obj = obj;
            _objType = obj.GetType();
        }

        /// <summary>
        /// Gets the value of a private field.
        /// </summary>
        /// <typeparam name="T">The type of the field value.</typeparam>
        /// <param name="name">The name of the private field.</param>
        /// <returns>The value of the private field.</returns>
        public T GetPrivateFieldValue<T>(string name)
        {
            var fieldInfo = _objType.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)fieldInfo?.GetValue(_obj);
        }

        /// <summary>
        /// Gets the value of a private field.
        /// </summary>
        /// <param name="fullType">The full type name of the object.</param>
        /// <param name="fieldName">The name of the private field.</param>
        /// <returns>The value of the private field.</returns>
        public object GetPrivateFieldValue(string fullType, string fieldName)
        {
            var fieldInfo = _objType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return fieldInfo?.GetValue(_obj);
        }

        /// <summary>
        /// Gets the FieldInfo for a private field.
        /// </summary>
        /// <param name="fullType">The full type name of the object.</param>
        /// <param name="fieldName">The name of the private field.</param>
        /// <returns>The FieldInfo for the private field.</returns>
        public FieldInfo GetPrivateField(string fullType, string fieldName)
        {
            var fieldInfo = _objType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return fieldInfo;
        }

        /// <summary>
        /// Sets the value of a private field.
        /// </summary>
        /// <typeparam name="T">The type of the field value.</typeparam>
        /// <param name="name">The name of the private field.</param>
        /// <param name="value">The value to set.</param>
        public void SetPrivateFieldValue<T>(string name, T value)
        {
            var fieldInfo = _objType.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo?.SetValue(_obj, value);
        }
    }

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
