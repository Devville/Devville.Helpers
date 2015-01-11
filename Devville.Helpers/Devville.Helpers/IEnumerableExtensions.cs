// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEnumerableExtensions.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Devville.Helpers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The i enumerable extensions.
    /// </summary>
    public static class IEnumerableExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Determines whether the specified collection is empty or not.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified source is empty; otherwise, <c>false</c>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            if (list == null)
            {
                return true;
            }

            var genericCollection = list as ICollection<T>;
            if (genericCollection != null)
            {
                return genericCollection.Count == 0;
            }

            var nonGenericCollection = list as ICollection;
            if (nonGenericCollection != null)
            {
                return nonGenericCollection.Count == 0;
            }

            return !list.Any();
        }

        #endregion
    }
}