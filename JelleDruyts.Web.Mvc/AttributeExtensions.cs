using System;
using System.Reflection;

namespace JelleDruyts.Web.Mvc
{
    /// <summary>
    /// Provides extensions for dealing with custom attributes.
    /// </summary>
    public static class AttributeExtensions
    {
        #region FindAttributeValue

        /// <summary>
        /// Finds an attribute on a specified member and returns its value.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <typeparam name="TAttributeValue">The type of the attribute's value.</typeparam>
        /// <param name="member">The member for which to find the attribute.</param>
        /// <param name="valueSelector">The value selector.</param>
        /// <returns>The value of the attribute.</returns>
        public static TAttributeValue FindAttributeValue<TAttribute, TAttributeValue>(this ICustomAttributeProvider member, Func<TAttribute, TAttributeValue> valueSelector) where TAttribute : Attribute
        {
            return member.FindAttributeValue(valueSelector, default(TAttributeValue));
        }

        /// <summary>
        /// Finds an attribute on a specified member and returns its value.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <typeparam name="TAttributeValue">The type of the attribute's value.</typeparam>
        /// <param name="member">The member for which to find the attribute.</param>
        /// <param name="valueSelector">The value selector.</param>
        /// <param name="defaultValue">The default value to return if the attribute was not found.</param>
        /// <returns>The value of the attribute.</returns>
        public static TAttributeValue FindAttributeValue<TAttribute, TAttributeValue>(this ICustomAttributeProvider member, Func<TAttribute, TAttributeValue> valueSelector, TAttributeValue defaultValue) where TAttribute : Attribute
        {
            var attribute = FindAttribute<TAttribute>(member);
            if (attribute == null)
            {
                if (defaultValue != null)
                {
                    return defaultValue;
                }
                else
                {
                    return default(TAttributeValue);
                }
            }
            else
            {
                return valueSelector(attribute);
            }
        }

        #endregion

        #region FindAttribute

        /// <summary>
        /// Finds an attribute on a specified member.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="member">The member for which to find the attribute.</param>
        /// <returns>The found attribute, or <see langword="null"/> if it could not be found on the member.</returns>
        public static TAttribute FindAttribute<TAttribute>(this ICustomAttributeProvider member) where TAttribute : Attribute
        {
            var attributes = member.GetCustomAttributes(typeof(TAttribute), true);
            if (attributes == null || attributes.Length == 0)
            {
                return null;
            }
            else
            {
                return (TAttribute)attributes[0];
            }
        }

        #endregion
    }
}