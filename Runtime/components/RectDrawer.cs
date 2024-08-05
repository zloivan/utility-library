using IKhom.UtilitiesLibrary.Runtime.helpers;
using UnityEngine;

//using UnityExtensions;

namespace IKhom.UtilitiesLibrary.Runtime.components
{
    [RequireComponent(typeof(RectTransform))]
    public class RectDrawer : MonoBehaviour
    {
        [SerializeField]
#if UNITY_2021_2_OR_NEWER
        private Color _color = new(0.25f, 0.9f, 1f, 0.1f);
#else
        private Color _color = new Color(0.25f, 0.9f, 1f, 0.1f);
#endif
        

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