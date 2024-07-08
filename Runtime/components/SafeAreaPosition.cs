using System;
using UnityEngine;

namespace IKhom.UtilitiesLibrary.Runtime.components
{
    [DisallowMultipleComponent]
    public class SafeAreaPosition : MonoBehaviour
    {
        [SerializeField]
        private Offset _offset;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            var safeArea = Screen.safeArea;
            var screenRect = new Rect(0, 0, Screen.width, Screen.height);

            var topMargin = (int)(screenRect.height - safeArea.height - safeArea.y);
            var bottomMargin = (int)safeArea.y;

            var leftMargin = (int)safeArea.x;
            var rightMargin = (int)(screenRect.width - safeArea.width - safeArea.x);

            var position = _rectTransform.anchoredPosition;

            _rectTransform.anchoredPosition = _offset switch
            {
                Offset.Top => new Vector2(position.x, position.y - topMargin),
                Offset.Bottom => new Vector2(position.x, position.y + bottomMargin),
                Offset.Left => new Vector2(position.x + leftMargin, position.y),
                Offset.Right => new Vector2(position.x - rightMargin, position.y),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        // TODO проверить каждый вариант
        private enum Offset
        {
            Top,
            Bottom,
            Left,
            Right
        }
    }
}