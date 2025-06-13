using UnityEngine;

namespace IKhom.UtilitiesLibrary.Runtime.components
{
    /// <summary>
    /// The LookAtCamera class is a Unity component that aligns an object to face a camera.
    /// This is typically used to ensure objects such as UI elements, billboards, or other
    /// objects consistently orient themselves towards the player's camera view.
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("If enabled, the object's rotation will be inverted relative to the camera")]
        private bool _invert = true;

        [SerializeField]
        [Tooltip("If enabled, aligns with camera's view plane; if disabled, directly faces the camera")]
        private bool _alignToViewPlane = true;

        [SerializeField]
        [Tooltip("Target camera for alignment (uses Main Camera if not set)")]
        private Camera _cameraToLookAt;

        private void Awake()
        {
            if (!_cameraToLookAt)
                _cameraToLookAt = Camera.main;
        }

        /// <summary>
        /// Called once per frame after all Update calls have been completed.
        /// This method ensures the attached GameObject orients itself towards a specified camera.
        /// Depending on the configuration, the object can either align to the camera's view plane
        /// or directly face the camera while optionally inverting its orientation.
        /// </summary>
        private void LateUpdate()
        {
            if (_alignToViewPlane)
            {
                // Align to camera's view plane (all UI elements will have same rotation)
                transform.rotation = _cameraToLookAt.transform.rotation;

                if (_invert)
                {
                    // Rotate 180 degrees around Y axis to face the camera
                    transform.Rotate(0, 180, 0);
                }
            }
            else
            {
                // Original behavior - look directly at camera
                if (_invert)
                {
                    var lookDirection = (_cameraToLookAt.transform.position - transform.position).normalized;
                    transform.LookAt(transform.position + lookDirection * -1f);
                }
                else
                {
                    transform.LookAt(_cameraToLookAt.transform);
                }
            }
        }
    }
}