// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Devville.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     Strings Extension Methods
    /// </summary>
    /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
    /// <created>4/28/2010</created>
    public static class StringExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Formats the with.
        /// </summary>
        /// <param name="format">
        /// A composite format string..
        /// </param>
        /// <param name="dataContainer">
        /// The data container.
        /// </param>
        /// <returns>
        /// A copy of <paramref name="format"/> in which the format items have been replaced by the
        ///     <see cref="T:System.String"/> equivalent of the corresponding instances of <see cref="T:System.Object"/> in
        ///     <paramref name="dataContainer"/>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>4/28/2010</created>
        public static string FormatWith(this string format, object dataContainer)
        {
            return FormatWith(format, null, dataContainer);
        }

        /// <summary>
        /// Formats the with.
        /// </summary>
        /// <param name="format">
        /// A composite format string..
        /// </param>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="dataContainer">
        /// The data container.
        /// </param>
        /// <returns>
        /// A copy of <paramref name="format"/> in which the format items have been replaced by the
        ///     <see cref="T:System.String"/> equivalent of the corresponding instances of <see cref="T:System.Object"/> in
        ///     <paramref name="dataContainer"/>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>4/28/2010</created>
        public static string FormatWith(this string format, IFormatProvider provider, object dataContainer)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            if (format == null)
            {
                throw new ArgumentNullException("dataContainer");
            }

            const string Pattern = @"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+";
            var regex = new Regex(
                Pattern, 
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            var formatValues = new List<object>();
            string rewrittenFormat = regex.Replace(
                format, 
                match =>
                    {
                        Group startGroup = match.Groups["start"];
                        Group propertyGroup = match.Groups["property"];
                        Group formatGroup = match.Groups["format"];
                        Group endGroup = match.Groups["end"];
                        PropertyInfo containerProperty =
                            dataContainer.GetType()
                                .GetProperties()
                                .SingleOrDefault(
                                    p => p.Name.Equals(propertyGroup.Value, StringComparison.InvariantCultureIgnoreCase));
                        formatValues.Add(
                            containerProperty == null
                                ? "{" + propertyGroup.Value + "}"
                                : containerProperty.GetValue(dataContainer, null) ?? string.Empty);
                        return new string('{', startGroup.Captures.Count) + (formatValues.Count - 1) + formatGroup.Value
                               + new string('}', endGroup.Captures.Count);
                    });

            return string.Format(provider, rewrittenFormat, formatValues.ToArray());
        }

        /// <summary>
        /// Removes the whitespaces.
        /// </summary>
        /// <param name="str">
        /// The string.
        /// </param>
        /// <returns>
        /// The clean string
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>12/11/2012</created>
        public static string RemoveWhitespaces(this string str)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                switch (c)
                {
                    case '\r':
                    case '\n':
                    case '\t':
                        continue;
                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}