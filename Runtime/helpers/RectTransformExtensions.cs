using UnityEngine;

namespace IKhom.UtilitiesLibrary.Runtime.helpers
{
    internal static class RectTransformExtensions
    {
        /// <summary>
        /// Gets the world space Rect of the RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to get the world Rect for.</param>
        /// <returns>A Rect representing the world space bounds of the RectTransform.</returns>
        internal static Rect GetWorldRect(this RectTransform rectTransform)
        {
            var corners = new Vector3[4];

            rectTransform.GetWorldCorners(corners);

            var bounds = new Bounds(corners[0], Vector3.zero);
            bounds.Encapsulate(corners[2]);

            return new Rect(new Vector2(bounds.min.x, bounds.min.y), bounds.size);
        }
    }
}