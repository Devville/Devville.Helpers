// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Devville.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        #region Public Methods and Operators

        /// <summary>
        /// Partitions the specified source.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/29/2014</created>
        public static IEnumerable<IList<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            var partition = new T[size];
            int count = 0;

            foreach (T t in source)
            {
                partition[count] = t;
                count++;

                if (count == size)
                {
                    yield return partition;
                    partition = new T[size];
                    count = 0;
                }
            }

            if (count > 0)
            {
                Array.Resize(ref partition, count);
                yield return partition;
            }
        }

        /// <summary>
        /// Truncates the specified string.
        /// </summary>
        /// <param name="str">
        /// The string.
        /// </param>
        /// <param name="maxLength">
        /// The maximum length.
        /// </param>
        /// <param name="suffix">
        /// The suffix.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/29/2014</created>
        public static string Truncate(string str, int maxLength, string suffix)
        {
            if (str.Length > maxLength)
            {
                string[] words = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var sb = new StringBuilder();
                for (int i = 0; sb.ToString().Length + words[i].Length <= maxLength; i++)
                {
                    sb.Append(words[i]);
                    sb.Append(" ");
                }

                str = sb.ToString().TrimEnd(' ') + suffix;
            }

            return str.Trim();
        }

        #endregion
    }
}