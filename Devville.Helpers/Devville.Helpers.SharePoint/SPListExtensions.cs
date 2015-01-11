// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SPListExtensions.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Devville.Helpers.SharePoint
{
    using System;

    using Microsoft.SharePoint;

    /// <summary>
    ///     <see cref="SPList" /> extension methods
    /// </summary>
    public static class SPListExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the item by id, if it does not exists then return null instead of the tradition
        ///     <see cref="ArgumentException"/>
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="itemId">
        /// The item id.
        /// </param>
        /// <returns>
        /// The item by id
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>11/28/2012</created>
        public static SPListItem GetItem(this SPList list, int itemId)
        {
            string query = @"
                        <Where>
                           <Eq>
                              <FieldRef Name='ID' />
                              <Value Type='Counter'>" + itemId + @"</Value>
                           </Eq>
                        </Where>";
            SPListItemCollection items = list.GetItems(SPQueryHelper.Get(1, query, true, new string[0]));
            return items.Count == 0 ? null : items[0];
        }

        #endregion
    }
}