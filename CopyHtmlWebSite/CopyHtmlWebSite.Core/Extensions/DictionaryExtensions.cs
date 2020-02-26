// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The dictionary extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;

namespace ShopMeDang.Infrastructure.Extensions
{
    /// <summary>
    /// The dictionary extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// The reader writer lock slim.
        /// </summary>
        private static readonly ReaderWriterLockSlim ReaderWriterLockSlim = new ReaderWriterLockSlim();

        /// <summary>
        /// The add or replace.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TV">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool AddOrReplace<T, TV>(this Dictionary<T, TV> dictionary, T key, TV value)
        {
            ReaderWriterLockSlim.EnterWriteLock();
            try
            {
                if (dictionary.ExistKey(key))
                {
                    dictionary[key] = value;
                }
                else
                {
                    dictionary.Add(key, value);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ReaderWriterLockSlim.ExitWriteLock();
            }
        }

        /// <summary>
        /// The exist.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TV">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Exist<T, TV>(this Dictionary<T, TV> dictionary, T key)
        {
            ReaderWriterLockSlim.EnterReadLock();
            try
            {
                return dictionary.ExistKey(key);
            }
            finally
            {
                ReaderWriterLockSlim.ExitReadLock();
            }
        }

        /// <summary>
        /// The remove key.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TV">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool RemoveKey<T, TV>(this Dictionary<T, TV> dictionary, T key)
        {
            ReaderWriterLockSlim.EnterWriteLock();
            try
            {
                if (ExistKey(dictionary, key))
                {
                    dictionary.Remove(key);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ReaderWriterLockSlim.ExitWriteLock();
            }
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TV">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TV"/>.
        /// </returns>
        public static TV Get<T, TV>(this Dictionary<T, TV> dictionary, T key)
        {
            ReaderWriterLockSlim.EnterReadLock();
            try
            {

                if (dictionary.ExistKey(key))
                {
                    return dictionary[key];
                }
                return default(TV);
            }
            finally
            {
                ReaderWriterLockSlim.ExitReadLock();
            }
        }

        /// <summary>
        /// The exist key.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TV">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool ExistKey<T, TV>(this Dictionary<T, TV> dictionary, T key)
        {
            return dictionary.ContainsKey(key);
        }
    }
}
