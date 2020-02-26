// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtension.cs" company="">
//   
// </copyright>
// <summary>
//   The object extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace ShopMeDang.Infrastructure.Extensions
{
    /// <summary>
    /// The object extension.
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// The is null.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsNull(this object val)
        {
            return val == null;
        }

        /// <summary>
        /// The is not null.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsNotNull(this object val)
        {
            return val != null;
        }

        public static TResult GetValue<TSource, TResult>(this TSource source, Func<TSource, TResult> property, TResult defaultValue = default(TResult))
        {
            if (source == null) return defaultValue;
            return property.Invoke(source);
        }

        public static TResult GetValue<TSource, TResult>(this TSource source, Func<TSource, TResult> property, Expression<Func<TSource, bool>> predicate, TResult defaultValue = default(TResult))
        {
            if (source == null) return defaultValue;
            var result = ExpressionInvoke(source, predicate);
            if (!result) return defaultValue;
            return property.Invoke(source);
        }

        public static bool IsNotNullAnd<TSource>(this TSource source, Expression<Func<TSource, bool>> predicate)
        {
            if (source == null) return false;
            return ExpressionInvoke(source, predicate);
        }

        public static bool IsNullOr<TSource>(this TSource source, Expression<Func<TSource, bool>> predicate)
        {
            if (source == null) return true;
            return ExpressionInvoke(source, predicate);
        }

        public static bool IsNullOr<TSource>(this TSource source, Expression<Func<bool>> predicate)
        {
            if (source == null) return true;
            return ExpressionInvoke(source, predicate);
        }

        private static TResult ExpressionInvoke<TSource, TResult>(TSource source, Expression<Func<TSource, TResult>> predicate)
        {
            var func = predicate.Compile();
            return func.Invoke(source);
        }

        private static TResult ExpressionInvoke<TSource, TResult>(TSource source, Expression<Func<TResult>> predicate)
        {
            var func = predicate.Compile();
            return func.Invoke();
        }
    }
}