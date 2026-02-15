using System;
using System.Collections.Generic;

namespace System.Reflection
{
	/// <summary>Contains static methods for retrieving custom attributes.</summary>
	// Token: 0x0200062D RID: 1581
	public static class CustomAttributeExtensions
	{
		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified assembly.</summary>
		/// <returns>A custom attribute that matches <paramref name="attributeType" />, or null if no such attribute is found.</returns>
		/// <param name="element">The assembly to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		// Token: 0x06002E7C RID: 11900 RVA: 0x000B37F0 File Offset: 0x000B19F0
		public static Attribute GetCustomAttribute(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member.</summary>
		/// <returns>A custom attribute that matches <paramref name="attributeType" />, or null if no such attribute is found.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E7D RID: 11901 RVA: 0x000B37F9 File Offset: 0x000B19F9
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified assembly. </summary>
		/// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
		/// <param name="element">The assembly to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		// Token: 0x06002E7E RID: 11902 RVA: 0x000B3802 File Offset: 0x000B1A02
		public static T GetCustomAttribute<T>(this Assembly element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member.</summary>
		/// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E7F RID: 11903 RVA: 0x000B3819 File Offset: 0x000B1A19
		public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member, and optionally inspects the ancestors of that member.</summary>
		/// <returns>A custom attribute that matches <paramref name="attributeType" />, or null if no such attribute is found.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E80 RID: 11904 RVA: 0x000B3830 File Offset: 0x000B1A30
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member, and optionally inspects the ancestors of that member.</summary>
		/// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E81 RID: 11905 RVA: 0x000B383A File Offset: 0x000B1A3A
		public static T GetCustomAttribute<T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		/// <summary>Retrieves a collection of custom attributes that are applied to a specified assembly.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The assembly to inspect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		// Token: 0x06002E82 RID: 11906 RVA: 0x000B3852 File Offset: 0x000B1A52
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		/// <summary>Retrieves a collection of custom attributes that are applied to a specified member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The member to inspect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E83 RID: 11907 RVA: 0x000B385A File Offset: 0x000B1A5A
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="attributeType" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E84 RID: 11908 RVA: 0x000B3862 File Offset: 0x000B1A62
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="T" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The member to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E85 RID: 11909 RVA: 0x000B386B File Offset: 0x000B1A6B
		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified member, and optionally inspects the ancestors of that member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="attributeType" />, or an empty collection if no such attributes exist.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E86 RID: 11910 RVA: 0x000B3882 File Offset: 0x000B1A82
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified member, and optionally inspects the ancestors of that member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="T" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002E87 RID: 11911 RVA: 0x000B388C File Offset: 0x000B1A8C
		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		/// <summary>Indicates whether custom attributes of a specified type are applied to a specified member.</summary>
		/// <returns>true if an attribute of the specified type is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		// Token: 0x06002E88 RID: 11912 RVA: 0x000B38A4 File Offset: 0x000B1AA4
		public static bool IsDefined(this MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}
	}
}
