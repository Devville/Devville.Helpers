// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchDataColumn.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Devville.Helpers.SharePoint.BatchData
{
    using System;

    using Microsoft.SharePoint.Utilities;

    /// <summary>
    ///     Batch Data Column
    /// </summary>
    /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
    /// <created>12/3/2012</created>
    public class BatchDataColumn
    {
        #region Constants

        /// <summary>
        ///     The column value
        /// </summary>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        private const string ColumnValue = "<SetVar Name=\"urn:schemas-microsoft-com:office:office#{0}\">{1}</SetVar>";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchDataColumn"/> class.
        /// </summary>
        /// <param name="internalName">
        /// Name of the internal.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public BatchDataColumn(string internalName, object value)
        {
            this.InternalName = internalName;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchDataColumn"/> class.
        /// </summary>
        /// <param name="internalName">
        /// Name of the internal.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="isValueHtml">
        /// if set to <c>true</c> [is value HTML].
        /// </param>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/4/2012</created>
        public BatchDataColumn(string internalName, object value, bool isValueHtml)
        {
            this.InternalName = internalName;
            this.Value = value;
            this.IsValueHtml = isValueHtml;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the name of the internal.
        /// </summary>
        /// <value>
        ///     The name of the internal.
        /// </value>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public string InternalName { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is value HTML.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is value HTML; otherwise, <c>false</c>.
        /// </value>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/4/2012</created>
        public bool IsValueHtml { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public object Value { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public override string ToString()
        {
            object value = this.Value is DateTime
                               ? SPUtility.CreateISO8601DateTimeFromSystemDateTime((DateTime)this.Value)
                               : this.IsValueHtml ? string.Format("<![CDATA[{0}]]>", this.Value) : this.Value;

            return string.Format(ColumnValue, this.InternalName, value);
        }

        #endregion
    }
}