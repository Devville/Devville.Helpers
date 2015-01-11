// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SPQueryHelper.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Devville.Helpers.SharePoint
{
    using Microsoft.SharePoint;

    /// <summary>
    ///     <see cref="SPQuery" /> helper class.
    /// </summary>
    /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
    /// <created>11/28/2012</created>
    public static class SPQueryHelper
    {
        #region Constants

        /// <summary>
        ///     The FieldRef format
        /// </summary>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>11/28/2012</created>
        private const string FieldFormat = "<FieldRef Name='{0}' />";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the <see cref="SPQuery"/> with the provided parameters.
        /// </summary>
        /// <param name="rowsCount">
        /// The rows count.
        /// </param>
        /// <param name="whereQuery">
        /// The where query.
        /// </param>
        /// <param name="useScopeRecursive">
        /// if set to <c>true</c> [use scope recursive].
        /// </param>
        /// <param name="fields">
        /// The fields.
        /// </param>
        /// <returns>
        /// The <see cref="SPQuery"/> with the provided parameters
        /// </returns>
        /// <created>11/28/2012</created>
        /// <author>
        ///     Ahmed Magdy (ahmed.magdy@devville.net)
        /// </author>
        public static SPQuery Get(uint? rowsCount, string whereQuery, bool useScopeRecursive, params string[] fields)
        {
            var query = new SPQuery();

            if (rowsCount != null)
            {
                query.RowLimit = rowsCount.Value;
            }

            query.Query = whereQuery.RemoveWhitespaces();

            if (useScopeRecursive)
            {
                query.ViewAttributes = "Scope=\"Recursive\"";
            }

            foreach (string field in fields)
            {
                query.ViewFields += string.Format(FieldFormat, field);
            }

            return query;
        }

        /// <summary>
        /// Gets the <see cref="SPQuery"/> with the provided parameters.
        /// </summary>
        /// <param name="rowsCount">
        /// The rows count.
        /// </param>
        /// <param name="whereQuery">
        /// The where query.
        /// </param>
        /// <param name="fields">
        /// The fields.
        /// </param>
        /// <returns>
        /// The <see cref="SPQuery"/> with the provided parameters.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>11/28/2012</created>
        public static SPQuery Get(uint? rowsCount, string whereQuery, params string[] fields)
        {
            return Get(rowsCount, whereQuery, false, fields);
        }

        /// <summary>
        /// Gets the <see cref="SPQuery"/> with the provided parameters
        /// </summary>
        /// <param name="whereQuery">
        /// The where query.
        /// </param>
        /// <param name="fields">
        /// The fields.
        /// </param>
        /// <returns>
        /// The <see cref="SPQuery"/> with the provided parameters
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>11/28/2012</created>
        public static SPQuery Get(string whereQuery, params string[] fields)
        {
            return Get(null, whereQuery, fields);
        }

        #endregion
    }
}