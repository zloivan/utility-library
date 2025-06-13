using UnityEngine;

namespace IKhom.UtilitiesLibrary.Runtime.components
{
    /// <summary>
    /// A generic singleton class that ensures only one instance of the specified component type exists.
    /// The instance can optionally persist across scenes and provides thread-safe access.
    /// </summary>
    /// <typeparam name="T">The type of the component to be used as a singleton.</typeparam>
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : Component
    {
        [Header("Singleton Settings")]
        [Tooltip("If true, this singleton will persist across scene loads")]
        [SerializeField] protected bool _persistAcrossScenes = true;
        
        [Tooltip("If true, this singleton will auto detach from its parent on Awake")]
        [SerializeField] protected bool _unParentOnAwake = true;

        private static T _instance;
        private static readonly object _lock = new();
        private static bool _applicationIsQuitting;

        /// <summary>
        /// Checks if an instance of the singleton exists and is not being destroyed.
        /// </summary>
        public static bool HasInstance => _instance && !_applicationIsQuitting;

        /// <summary>
        /// Gets the singleton instance. Creates one if it doesn't exist.
        /// Returns null if application is quitting.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance '{typeof(T).Name}' already destroyed. Returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance) return _instance;
                    _instance = FindFirstObjectByType<T>();

                    if (_instance) return _instance;
                    var go = new GameObject($"{typeof(T).Name} (Singleton)");
                    _instance = go.AddComponent<T>();

                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (!Application.isPlaying)
                return;

            InitializeSingleton();
        }

        private void InitializeSingleton()
        {
            if (_unParentOnAwake)
            {
                transform.SetParent(null);
            }

            if (!_instance)
            {
                _instance = this as T;
                
                if (_persistAcrossScenes)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (_instance != this)
            {
                Debug.LogWarning($"[Singleton] Another instance of {typeof(T).Name} already exists. Destroying this one.");
                Destroy(gameObject);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}