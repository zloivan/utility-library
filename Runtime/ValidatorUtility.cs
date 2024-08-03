using System;
using System.Collections.Generic;
using System.Linq;
using IKhom.UtilitiesLibrary.Runtime.helpers;
using JetBrains.Annotations;
using UnityEngine;

namespace IKhom.UtilitiesLibrary.Runtime
{
    public static class ValidatorUtility
    {
        private static readonly ILogger Logger = new UtilityLogger();
        private const string LOGGER_TAG = "ValidatorUtility";

        /// <summary>
        /// Validates that the given object is not null.
        /// </summary>
        /// <param name="obj">The object to validate.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateNotNull([NotNull] object obj, [InvokerParameterName] string name)
        {
            if (obj == null)
            {
                Logger.LogError(LOGGER_TAG, $"{name} cannot be null.");
                throw new ArgumentNullException(name, $"{name} cannot be null.");
            }
        }

        /// <summary>
        /// Validates that the given string is not null or empty.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateNotNullOrEmpty([CanBeNull] string str, [InvokerParameterName] string name)
        {
            if (string.IsNullOrEmpty(str))
            {
                Logger.LogError(LOGGER_TAG, $"{name} cannot be null or empty.");
                throw new ArgumentException($"{name} cannot be null or empty.", name);
            }
        }

        /// <summary>
        /// Validates that the given collection is not null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The collection to validate.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateNotNullOrEmpty<T>([NotNull] IEnumerable<T> collection,
            [InvokerParameterName] string name)
        {
            if (collection == null)
            {
                Logger.LogError(LOGGER_TAG, $"{name} collection cannot be null.");
                throw new ArgumentNullException(name, $"{name} collection cannot be null.");
            }

            if (!collection.Any())
            {
                Logger.LogError(LOGGER_TAG, $"{name} collection cannot be empty.");
                throw new ArgumentException($"{name} collection cannot be empty.", name);
            }
        }

        /// <summary>
        /// Validates that the given index is within the bounds of the collection.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The collection to validate against.</param>
        /// <param name="index">The index to validate.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateIndexInRange<T>([NotNull] IList<T> collection,
            int index,
            [InvokerParameterName] string name)
        {
            if (collection == null)
            {
                Logger.LogError(LOGGER_TAG, $"{name} collection cannot be null.");
                throw new ArgumentNullException(name, $"{name} collection cannot be null.");
            }

            if (index < 0 || index >= collection.Count)
            {
                Logger.LogError(LOGGER_TAG, $"Index {index} is out of range for {name} collection.");
                throw new ArgumentOutOfRangeException(name, $"Index {index} is out of range for {name} collection.");
            }
        }

        /// <summary>
        /// Validates that the given number is within the specified range.
        /// </summary>
        /// <param name="number">The number to validate.</param>
        /// <param name="min">The minimum allowable value.</param>
        /// <param name="max">The maximum allowable value.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateNumberInRange(float number, float min, float max, [InvokerParameterName] string name)
        {
            if (!(number < min) && !(number > max)) return;

            Logger.LogError(LOGGER_TAG, $"{name} must be between {min} and {max}. Current value: {number}.");
            throw new ArgumentOutOfRangeException(name,
                $"{name} must be between {min} and {max}. Current value: {number}.");
        }

        /// <summary>
        /// Validates that the given number is positive.
        /// </summary>
        /// <param name="number">The number to validate.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidatePositiveNumber(float number, [InvokerParameterName] string name)
        {
            if (!(number <= 0))
                return;

            Logger.LogError(LOGGER_TAG, $"{name} must be positive. Current value: {number}.");
            throw new ArgumentOutOfRangeException(name, $"{name} must be positive. Current value: {number}.");
        }

        /// <summary>
        /// Validates that the given number is non-negative.
        /// </summary>
        /// <param name="number">The number to validate.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateNonNegativeNumber(float number, [InvokerParameterName] string name)
        {
            if (!(number < 0))
                return;

            Logger.LogError(LOGGER_TAG, $"{name} must be non-negative. Current value: {number}.");
            throw new ArgumentOutOfRangeException(name, $"{name} must be non-negative. Current value: {number}.");
        }

        /// <summary>
        /// Validates that the given number is non-zero.
        /// </summary>
        /// <param name="number">The number to validate.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateNonZeroNumber(float number, [InvokerParameterName] string name)
        {
            if (number != 0)
                return;

            Logger.LogError(LOGGER_TAG, $"{name} must be non-zero. Current value: {number}.");
            throw new ArgumentOutOfRangeException(name, $"{name} must be non-zero. Current value: {number}.");
        }

        /// <summary>
        /// Validates that the given string meets the specified length requirements.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <param name="minLength">The minimum allowable length.</param>
        /// <param name="maxLength">The maximum allowable length.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateStringLength([CanBeNull] string str,
            int minLength,
            int maxLength,
            [InvokerParameterName] string name)
        {
            if (str == null)
            {
                Logger.LogError(LOGGER_TAG, $"{name} cannot be null.");
                throw new ArgumentNullException(name, $"{name} cannot be null.");
            }

            if (str.Length < minLength || str.Length > maxLength)
            {
                Logger.LogError(LOGGER_TAG,
                    $"{name} must be between {minLength} and {maxLength} characters long. Current length: {str.Length}.");
                throw new ArgumentOutOfRangeException(name,
                    $"{name} must be between {minLength} and {maxLength} characters long. Current length: {str.Length}.");
            }
        }

        /// <summary>
        /// Validates that the given value is a defined member of the specified enum type.
        /// </summary>
        /// <typeparam name="TEnum">The enum type to validate against.</typeparam>
        /// <param name="value">The value to validate.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateEnumValue<TEnum>(TEnum value, [InvokerParameterName] string name)
            where TEnum : struct, Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                Logger.LogError(LOGGER_TAG, $"{value} is not a valid value for {name}.");
                throw new ArgumentException($"{value} is not a valid value for {name}.", name);
            }
        }

        /// <summary>
        /// Validates that the given date is within the specified range.
        /// </summary>
        /// <param name="date">The date to validate.</param>
        /// <param name="min">The minimum allowable date.</param>
        /// <param name="max">The maximum allowable date.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateDateInRange(DateTime date,
            DateTime min,
            DateTime max,
            [InvokerParameterName] string name)
        {
            if (date < min || date > max)
            {
                Logger.LogError(LOGGER_TAG, $"{name} must be between {min} and {max}. Current value: {date}.");
                throw new ArgumentOutOfRangeException(name,
                    $"{name} must be between {min} and {max}. Current value: {date}.");
            }
        }

        /// <summary>
        /// Validates that the given object is of the specified type.
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="obj">The object to validate.</param>
        /// <param name="name">The name of the parameter being validated.</param>
        [PublicAPI]
        public static void ValidateType<T>([NotNull] object obj, [InvokerParameterName] string name)
        {
            if (obj == null)
            {
                Logger.LogError(LOGGER_TAG, $"{name} cannot be null.");
                throw new ArgumentNullException(name, $"{name} cannot be null.");
            }

            if (obj is T) return;
            {
                Logger.LogError(LOGGER_TAG, $"{name} must be of type {typeof(T)}. Current type: {obj.GetType()}.");
                throw new ArgumentException($"{name} must be of type {typeof(T)}. Current type: {obj.GetType()}.",
                    name);
            }
        }
    }
}