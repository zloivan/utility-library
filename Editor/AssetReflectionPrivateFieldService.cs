// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System.Linq;
using IKhom.UtilitiesLibrary.Runtime;
using UnityEditor;

namespace IKhom.UtilitiesLibrary.Editor
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
}
