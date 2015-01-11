// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchDataMethod.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Devville.Helpers.SharePoint.BatchData
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    ///     Defines Batch Data Method.
    /// </summary>
    /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
    /// <created>12/3/2012</created>
    public class BatchDataMethod
    {
        #region Static Fields

        /// <summary>
        ///     The method format
        /// </summary>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        private static readonly string methodFormat = string.Concat(
            "<Method ID=\"{0}\">", 
            "<SetList>{1}</SetList>", 
            "<SetVar Name=\"Cmd\">{2}</SetVar>", 
            "<SetVar Name=\"ID\">{3}</SetVar>", 
            "{4}", 
            "</Method>");

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BatchDataMethod" /> class.
        /// </summary>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public BatchDataMethod()
        {
            this.ColumnsData = new List<BatchDataColumn>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the columns.
        /// </summary>
        /// <value>
        ///     The columns.
        /// </value>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public List<BatchDataColumn> ColumnsData { get; set; }

        /// <summary>
        ///     Gets or sets the command.
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public BatchDataCommandType Command { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>
        ///     The id.
        /// </value>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the item id.
        /// </summary>
        /// <value>
        ///     The item id.
        /// </value>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public int ItemId { get; set; }

        /// <summary>
        ///     Gets or sets the list id.
        /// </summary>
        /// <value>
        ///     The list id.
        /// </value>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public Guid ListId { get; set; }

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
            return GetMethodXml(this.Id, this.ListId, this.Command, this.ItemId, this.ColumnsData);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the command for method.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        /// <exception cref="System.ArgumentException">
        /// Invalid command!
        /// </exception>
        private static string GetCommandForMethod(BatchDataCommandType command)
        {
            switch (command)
            {
                case BatchDataCommandType.Add:
                case BatchDataCommandType.Update:
                    return "Save";
                case BatchDataCommandType.Delete:
                    return "Delete";
            }

            throw new ArgumentException("Invalid command!");
        }

        /// <summary>
        /// Gets the method XML.
        /// </summary>
        /// <param name="methodId">
        /// The method id.
        /// </param>
        /// <param name="listId">
        /// The list id.
        /// </param>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="itemId">
        /// The item id.
        /// </param>
        /// <param name="columnValues">
        /// The column values.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        private static string GetMethodXml(
            string methodId, 
            Guid listId, 
            BatchDataCommandType command, 
            int itemId, 
            List<BatchDataColumn> columnValues)
        {
            if (string.IsNullOrEmpty(methodId))
            {
                methodId = Guid.NewGuid().ToString();
            }

            string columns = string.Empty;

            columnValues.ForEach(c => columns += Convert.ToString(c));

            string methodItemId = command == BatchDataCommandType.Add
                                      ? "New"
                                      : itemId.ToString(CultureInfo.InvariantCulture);

            string method = string.Format(
                methodFormat, 
                methodId, 
                listId, 
                GetCommandForMethod(command), 
                methodItemId, 
                columns);
            return method;
        }

        #endregion
    }
}