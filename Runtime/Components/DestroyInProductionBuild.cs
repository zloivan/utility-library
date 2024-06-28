using UnityEngine;

namespace Utilities.Components
{
    public class DestroyInProductionBuild : MonoBehaviour
    {
        private void Start()
        {
            if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
                return;

            if (Debug.isDebugBuild == false)
                Destroy(gameObject);
        }
    }
}