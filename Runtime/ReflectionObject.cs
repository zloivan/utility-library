// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System;
using System.Reflection;

namespace IKhom.UtilitiesLibrary.Runtime
{
    /// <summary>
    /// Represents an object with reflection capabilities to access its private fields.
    /// </summary>
    public class ReflectionObject
    {
        private readonly object _obj;
        private readonly Type _objType;

        /// <summary>
        /// Gets the type of the reflected object.
        /// </summary>
        public Type ObjType => _objType;

        /// <summary>
        /// Gets the reflected object.
        /// </summary>
        public object Obj => _obj;

        /// <summary>
        /// Initializes a new instance of the ReflectionObject class.
        /// </summary>
        /// <param name="obj">The object to reflect upon.</param>
        public ReflectionObject(object obj)
        {
            _obj = obj;
            _objType = obj.GetType();
        }

        /// <summary>
        /// Gets the value of a private field.
        /// </summary>
        /// <typeparam name="T">The type of the field value.</typeparam>
        /// <param name="name">The name of the private field.</param>
        /// <returns>The value of the private field.</returns>
        public T GetPrivateFieldValue<T>(string name)
        {
            var fieldInfo = _objType.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)fieldInfo?.GetValue(_obj);
        }

        /// <summary>
        /// Gets the value of a private field.
        /// </summary>
        /// <param name="fullType">The full type name of the object.</param>
        /// <param name="fieldName">The name of the private field.</param>
        /// <returns>The value of the private field.</returns>
        public object GetPrivateFieldValue(string fullType, string fieldName)
        {
            var fieldInfo = _objType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return fieldInfo?.GetValue(_obj);
        }

        /// <summary>
        /// Gets the FieldInfo for a private field.
        /// </summary>
        /// <param name="fullType">The full type name of the object.</param>
        /// <param name="fieldName">The name of the private field.</param>
        /// <returns>The FieldInfo for the private field.</returns>
        public FieldInfo GetPrivateField(string fullType, string fieldName)
        {
            var fieldInfo = _objType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return fieldInfo;
        }

        /// <summary>
        /// Sets the value of a private field.
        /// </summary>
        /// <typeparam name="T">The type of the field value.</typeparam>
        /// <param name="name">The name of the private field.</param>
        /// <param name="value">The value to set.</param>
        public void SetPrivateFieldValue<T>(string name, T value)
        {
            var fieldInfo = _objType.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo?.SetValue(_obj, value);
        }
    }
}