// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
using UnityEngine;
using Object = UnityEngine.Object;


namespace Utilities
{
    public static class Logger
    {
        public static void Log(object message, Color color)
        {
            Debug.unityLogger.Log(LogType.Log, ColorizeMessage(message.ToString(), color));
        }

        public static void Log(object message, Color color, Object context)
        {
            Debug.unityLogger.Log(LogType.Log, ColorizeMessage(message.ToString(), color), context);
        }

        public static void LogWarning(object message, Color color)
        {
            Debug.unityLogger.Log(LogType.Warning, ColorizeMessage(message.ToString(), color));
        }

        public static void LogWarning(object message, Color color, Object context)
        {
            Debug.unityLogger.Log(LogType.Warning, ColorizeMessage(message.ToString(), color), context);
        }

        public static void LogError(object message, Color color)
        {
            Debug.unityLogger.Log(LogType.Error, ColorizeMessage(message.ToString(), color));
        }

        public static void LogError(object message, Color color, Object context)
        {
            Debug.unityLogger.Log(LogType.Error, ColorizeMessage(message.ToString(), color), context);
        }

        private static object ColorizeMessage(object message, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>";
        }
    }
}