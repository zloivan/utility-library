using UnityEngine;

namespace Utilities.Components
{
    [DisallowMultipleComponent]
    public class SafeArea : MonoBehaviour
    {
        [SerializeField]
        private bool _top;

        [SerializeField]
        private bool _bottom;

        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            SetAnchors();
        }

        private void SetAnchors()
        {
            var safeArea = Screen.safeArea;

            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            if (_top) _rectTransform.anchorMax = anchorMax;
            if (_bottom) _rectTransform.anchorMin = anchorMin;
        }
    }
}