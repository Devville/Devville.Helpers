// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parser.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Devville.Helpers
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     The Parser Extension Methods for Safely Type Conversion.
    ///     Reference: http://outcoldman.ru/en/blog/show/196
    /// </summary>
    /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
    /// <created>4/6/2010</created>
    public static class Parser
    {
        #region Public Methods and Operators

        /// <summary>
        /// Try cast <paramref name="obj"/> value to type <typeparamref name="T"/>,
        ///     if can't will return default(<typeparamref name="T"/>)
        /// </summary>
        /// <typeparam name="T">
        /// The type to parse to.
        /// </typeparam>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// Casted type of the <paramref name="obj"/>
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>4/6/2010</created>
        public static T To<T>(this object obj)
        {
            return To(obj, default(T));
        }

        /// <summary>
        /// Try cast <paramref name="obj"/> value to type <typeparamref name="T"/>,
        ///     if can't will return <paramref name="defaultValue"/>
        /// </summary>
        /// <typeparam name="T">
        /// The type to parse to.
        /// </typeparam>
        /// <param name="obj">
        /// The object to parse.
        /// </param>
        /// <param name="defaultValue">
        /// Default value if failed to parse.
        /// </param>
        /// <returns>
        /// Casted type of the <paramref name="obj"/>
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>4/6/2010</created>
        public static T To<T>(this object obj, T defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            if (obj is T)
            {
                return (T)obj;
            }

            Type type = typeof(T);

            // Place convert to reference types here
            if (type == typeof(string))
            {
                return (T)(object)obj.ToString();
            }

            Type underlyingType = Nullable.GetUnderlyingType(type);

            if (underlyingType != null)
            {
                return To(obj, defaultValue, underlyingType);
            }

            return To(obj, defaultValue, type);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Toes the specified obj.
        /// </summary>
        /// <typeparam name="T">
        /// The target type.
        /// </typeparam>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <param name="type">
        /// The type to parse to.
        /// </param>
        /// <returns>
        /// Casted type of the <paramref name="obj"/>
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>4/6/2010</created>
        private static T To<T>(object obj, T defaultValue, Type type)
        {
            // Place convert to structures types here
            if (type == typeof(short))
            {
                short value;
                if (short.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(ushort))
            {
                ushort value;
                if (ushort.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(int))
            {
                int value;

                if (int.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(uint))
            {
                uint value;

                if (uint.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(long))
            {
                long value;
                if (long.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(ulong))
            {
                ulong value;
                if (ulong.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(float))
            {
                float value;
                if (float.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(double))
            {
                double value;
                if (double.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(decimal))
            {
                decimal value;
                if (decimal.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(bool))
            {
                bool value;
                if (bool.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(DateTime))
            {
                DateTime value;
                if (DateTime.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(byte))
            {
                byte value;
                if (byte.TryParse(obj.ToString(), out value))
                {
                    return (T)(object)value;
                }

                return defaultValue;
            }

            if (type == typeof(Guid))
            {
                const string GuidRegEx =
                    @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$";
                var regEx = new Regex(GuidRegEx);
                if (regEx.IsMatch(obj.ToString()))
                {
                    return (T)(object)new Guid(obj.ToString());
                }

                return defaultValue;
            }

            if (type.IsEnum)
            {
                if (Enum.IsDefined(type, obj))
                {
                    return (T)Enum.Parse(type, obj.ToString());
                }

                return defaultValue;
            }

            throw new NotSupportedException(
                string.Format("Couldn't parse \"{0}\" as {1} to Type \"{2}\"", obj, type, typeof(T)));
        }

        #endregion
    }
}