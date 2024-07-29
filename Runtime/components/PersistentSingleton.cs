using JetBrains.Annotations;
using UnityEngine;

namespace IKhom.UtilitiesLibrary.Runtime.components
{
    /// <summary>
    /// A generic singleton class that ensures only one instance of the specified component type exists.
    /// The instance persists across scenes and can optionally detach from its parent on Awake.
    /// </summary>
    /// <typeparam name="T">The type of the component to be used as a singleton.</typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        [Header("Persistent Singleton")]
        [Tooltip("If this is true, this singleton will auto detach if it finds itself parented on Awake.")]
        public bool _unParentOnAwake = true;

        /// <summary>
        /// Checks if an instance of the singleton exists.
        /// </summary>
        [PublicAPI]
        public static bool HasInstance => Current != null;

        /// <summary>
        /// The current instance of the singleton.
        /// </summary>
        [PublicAPI]
        public static T Current { get; private set; }

        /// <summary>
        /// Gets the instance of the singleton, creating it if it doesn't already exist.
        /// </summary>
        [PublicAPI]
        public static T Instance
        {
            get
            {
                if (Current != null)
                    return Current;
                Current = FindFirstObjectByType<T>();

                if (Current != null)
                    return Current;

                var obj = new GameObject
                {
                    name = typeof(T).Name + "AutoCreated"
                };
                Current = obj.AddComponent<T>();

                return Current;
            }
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Initializes the singleton instance.
        /// </summary>
        private void Awake() => InitializeSingleton();

        /// <summary>
        /// Initializes the singleton instance, setting it to persist across scenes and ensuring only one instance exists.
        /// </summary>
        private void InitializeSingleton()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (_unParentOnAwake)
            {
                transform.SetParent(null);
            }

            if (Current == null)
            {
                Current = this as T;
                DontDestroyOnLoad(transform.gameObject);
                enabled = true;
            }
            else
            {
                if (this != Current)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
