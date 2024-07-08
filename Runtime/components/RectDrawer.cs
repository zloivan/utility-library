using IKhom.UtilitiesLibrary.Runtime.extensions;
using UnityEngine;

//using UnityExtensions;

namespace IKhom.UtilitiesLibrary.Runtime.components
{
    [RequireComponent(typeof(RectTransform))]
    public class RectDrawer : MonoBehaviour
    {
        [SerializeField]
        private Color _color = new(0.25f, 0.9f, 1f, 0.1f);

        private RectTransform _rectTransform;


        private void Start()
        {
#if !UNITY_EDITOR
			Destroy(this);
#endif
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;

            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            var worldRect = _rectTransform.GetWorldRect();

            Gizmos.DrawWireCube(worldRect.center, worldRect.size);
            Gizmos.DrawCube(worldRect.center, worldRect.size);
        }
    }
}