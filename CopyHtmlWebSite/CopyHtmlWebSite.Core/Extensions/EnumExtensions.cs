// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The enum extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ShopMeDang.Infrastructure.Extensions
{
    /// <summary>
    /// The enum extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// The safe parse to enum.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T ToEnum<T>(this string value, T defaultValue = default(T)) where T : struct
        {
            T t;
            if (Enum.TryParse(value, true, out t))
            {
                return t;
            }
            return defaultValue;
        }

        /// <summary>
        /// The safe parse to enumerable.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerable<string> values) where T : struct
        {
            foreach (var str in values)
            {
                T t;
                if (Enum.TryParse(str, true, out t))
                {
                    yield return t;
                }
            }
        }

        /// <summary>
        /// The to enumerable.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="ReadOnlyCollection"/>.
        /// </returns>
        public static ReadOnlyCollection<T> ToEnumerable<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList().AsReadOnly();
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum e)
        {
            var type = e.GetType();
            var memberInfo = type.GetMember(e.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(TAttribute), false);
            var description = (TAttribute)attributes[0];
            return description;
        }

        public static string GetDescriptionAttribute(this Enum e)
        {
            var type = e.GetType();
            var memberInfo = type.GetMember(e.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = (DescriptionAttribute)attributes[0];
            return description.Description;
        }
    }
}
