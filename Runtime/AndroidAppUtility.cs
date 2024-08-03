using JetBrains.Annotations;
using UnityEngine;

namespace IKhom.UtilitiesLibrary.Runtime
{
    /// <summary>
    /// Minimizes the Android application by moving the task to the back.
    /// </summary>
    /// <remarks>
    /// This method is only applicable when the application is running on the Android platform.
    /// </remarks>
    /// <exception cref="System.InvalidOperationException">
    /// Thrown when the application is not running on the Android platform.
    /// </exception>
    /// <seealso cref="Application.platform"/>
    /// <seealso cref="RuntimePlatform.Android"/>
    public static class AndroidAppUtility
    {
        private const string COM_UNITY_3D_PLAYER_UNITY_PLAYER = "com.unity3d.player.UnityPlayer";
        private const string JAVA_OBJECT_CURRENT_ACTIVITY = "currentActivity";
        private const string ACTION = "moveTaskToBack";


        /// <summary>
        /// Minimizes the Android application by moving the task to the back.
        /// </summary>
        [PublicAPI]
        public static void Minimize()
        {
#if UNITY_EDITOR
            Debug.LogWarning("App Minimized!");
#endif

            if (Application.platform != RuntimePlatform.Android)
                return;

            var player = new AndroidJavaClass(COM_UNITY_3D_PLAYER_UNITY_PLAYER);
            var activity = player.GetStatic<AndroidJavaObject>(JAVA_OBJECT_CURRENT_ACTIVITY);
            activity.Call<bool>(ACTION, true);
        }
    }
}