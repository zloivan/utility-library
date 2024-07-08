using UnityEngine;

namespace IKhom.UtilitiesLibrary.Runtime.components
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}