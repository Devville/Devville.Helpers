// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessBatchDataHelper.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Devville.Helpers.SharePoint.BatchData
{
    using System;
    using System.Collections.Generic;

    using Microsoft.SharePoint;

    /// <summary>
    ///     <see cref="SPWeb" /> ProcessBatchData Helper class.
    /// </summary>
    /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
    /// <created>12/3/2012</created>
    public static class ProcessBatchDataHelper
    {
        #region Constants

        /// <summary>
        ///     The batch format
        /// </summary>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/9/2012</created>
        private const string BatchFormat =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?><ows:Batch OnError=\"{0}\">{1}</ows:Batch>";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the batch with return on error by default.
        /// </summary>
        /// <param name="methods">
        /// The methods.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/3/2012</created>
        public static string GetBatch(List<BatchDataMethod> methods)
        {
            return GetBatch(methods, OnErrorAction.Return);
        }

        /// <summary>
        /// Gets the batch.
        /// </summary>
        /// <param name="methods">
        /// The methods.
        /// </param>
        /// <param name="errorAction">
        /// The error action.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/9/2012</created>
        public static string GetBatch(List<BatchDataMethod> methods, OnErrorAction errorAction)
        {
            string methodsXml = string.Empty;
            methods.ForEach(m => methodsXml += Convert.ToString(m));
            string errorString = OnErrorString(errorAction);
            string batch = string.Format(BatchFormat, errorString, methodsXml);
            return batch;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when [error string].
        /// </summary>
        /// <param name="errorAction">
        /// The error action.
        /// </param>
        /// <returns>
        /// The on error action.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/9/2012</created>
        private static string OnErrorString(OnErrorAction errorAction)
        {
            switch (errorAction)
            {
                case OnErrorAction.Continue:
                    return "Continue";
                case OnErrorAction.Return:
                default:
                    return "Return";
            }
        }

        #endregion
    }
}