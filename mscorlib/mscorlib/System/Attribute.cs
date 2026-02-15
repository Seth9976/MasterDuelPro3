using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the base class for custom attributes.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000195 RID: 405
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Attribute))]
	[ComVisible(true)]
	[Serializable]
	public abstract class Attribute : _Attribute
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x0003DB21 File Offset: 0x0003BD21
		private static Attribute[] InternalGetCustomAttributes(PropertyInfo element, Type type, bool inherit)
		{
			return (Attribute[])MonoCustomAttrs.GetCustomAttributes(element, type, inherit);
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0003DB21 File Offset: 0x0003BD21
		private static Attribute[] InternalGetCustomAttributes(EventInfo element, Type type, bool inherit)
		{
			return (Attribute[])MonoCustomAttrs.GetCustomAttributes(element, type, inherit);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0003DB30 File Offset: 0x0003BD30
		private static Attribute[] InternalParamGetCustomAttributes(ParameterInfo parameter, Type attributeType, bool inherit)
		{
			if (parameter.Member.MemberType != MemberTypes.Method)
			{
				return null;
			}
			MethodInfo methodInfo = (MethodInfo)parameter.Member;
			MethodInfo baseDefinition = methodInfo.GetBaseDefinition();
			if (attributeType == null)
			{
				attributeType = typeof(Attribute);
			}
			if (methodInfo == baseDefinition)
			{
				return (Attribute[])parameter.GetCustomAttributes(attributeType, inherit);
			}
			List<Type> list = new List<Type>();
			List<Attribute> list2 = new List<Attribute>();
			for (;;)
			{
				foreach (Attribute attribute in (Attribute[])methodInfo.GetParametersInternal()[parameter.Position].GetCustomAttributes(attributeType, false))
				{
					Type type = attribute.GetType();
					if (!list.Contains(type))
					{
						list.Add(type);
						list2.Add(attribute);
					}
				}
				MethodInfo baseMethod = ((RuntimeMethodInfo)methodInfo).GetBaseMethod();
				if (baseMethod == methodInfo)
				{
					break;
				}
				methodInfo = baseMethod;
			}
			Attribute[] array2 = (Attribute[])Array.CreateInstance(attributeType, list2.Count);
			list2.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		private static bool InternalIsDefined(PropertyInfo element, Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(element, attributeType, inherit);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		private static bool InternalIsDefined(EventInfo element, Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(element, attributeType, inherit);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a member of a type. Parameters specify the member, and the type of the custom attribute to search for.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="type" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <param name="type">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003DC34 File Offset: 0x0003BE34
		public static Attribute[] GetCustomAttributes(MemberInfo element, Type type)
		{
			return Attribute.GetCustomAttributes(element, type, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a member of a type. Parameters specify the member, the type of the custom attribute to search for, and whether to search ancestors of the member.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="type" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <param name="type">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003DC40 File Offset: 0x0003BE40
		public static Attribute[] GetCustomAttributes(MemberInfo element, Type type, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsSubclassOf(typeof(Attribute)) && type != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			MemberTypes memberType = element.MemberType;
			if (memberType == MemberTypes.Event)
			{
				return Attribute.InternalGetCustomAttributes((EventInfo)element, type, inherit);
			}
			if (memberType == MemberTypes.Property)
			{
				return Attribute.InternalGetCustomAttributes((PropertyInfo)element, type, inherit);
			}
			return element.GetCustomAttributes(type, inherit) as Attribute[];
		}

		/// <summary>Retrieves an array of the custom attributes applied to a member of a type. A parameter specifies the member.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003DCE2 File Offset: 0x0003BEE2
		public static Attribute[] GetCustomAttributes(MemberInfo element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a member of a type. Parameters specify the member, the type of the custom attribute to search for, and whether to search ancestors of the member.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003DCEC File Offset: 0x0003BEEC
		public static Attribute[] GetCustomAttributes(MemberInfo element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			MemberTypes memberType = element.MemberType;
			if (memberType == MemberTypes.Event)
			{
				return Attribute.InternalGetCustomAttributes((EventInfo)element, typeof(Attribute), inherit);
			}
			if (memberType == MemberTypes.Property)
			{
				return Attribute.InternalGetCustomAttributes((PropertyInfo)element, typeof(Attribute), inherit);
			}
			return element.GetCustomAttributes(typeof(Attribute), inherit) as Attribute[];
		}

		/// <summary>Determines whether any custom attributes are applied to a member of a type. Parameters specify the member, and the type of the custom attribute to search for.</summary>
		/// <returns>true if a custom attribute of type <paramref name="attributeType" /> is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, type, or property member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003DD61 File Offset: 0x0003BF61
		public static bool IsDefined(MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		/// <summary>Determines whether any custom attributes are applied to a member of a type. Parameters specify the member, the type of the custom attribute to search for, and whether to search ancestors of the member.</summary>
		/// <returns>true if a custom attribute of type <paramref name="attributeType" /> is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, type, or property member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003DD6C File Offset: 0x0003BF6C
		public static bool IsDefined(MemberInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			MemberTypes memberType = element.MemberType;
			if (memberType == MemberTypes.Event)
			{
				return Attribute.InternalIsDefined((EventInfo)element, attributeType, inherit);
			}
			if (memberType == MemberTypes.Property)
			{
				return Attribute.InternalIsDefined((PropertyInfo)element, attributeType, inherit);
			}
			return element.IsDefined(attributeType, inherit);
		}

		/// <summary>Retrieves a custom attribute applied to a member of a type. Parameters specify the member, and the type of the custom attribute to search for.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003DE09 File Offset: 0x0003C009
		public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		/// <summary>Retrieves a custom attribute applied to a member of a type. Parameters specify the member, the type of the custom attribute to search for, and whether to search ancestors of the member.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003DE14 File Offset: 0x0003C014
		public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(Environment.GetResourceString("Multiple custom attributes of the same type found."));
		}

		/// <summary>Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method parameter, the type of the custom attribute to search for, and whether to search ancestors of the method parameter.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="attributeType" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.ParameterInfo" /> class that describes a parameter of a member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003DE4C File Offset: 0x0003C04C
		public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			if (element.Member == null)
			{
				throw new ArgumentException(Environment.GetResourceString("The ParameterInfo object is not valid."), "element");
			}
			if (element.Member.MemberType == MemberTypes.Method && inherit)
			{
				return Attribute.InternalParamGetCustomAttributes(element, attributeType, inherit);
			}
			return element.GetCustomAttributes(attributeType, inherit) as Attribute[];
		}

		/// <summary>Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method parameter, and whether to search ancestors of the method parameter.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.ParameterInfo" /> class that describes a parameter of a member of a class. </param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Reflection.ParameterInfo.Member" /> property of <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003DEFC File Offset: 0x0003C0FC
		public static Attribute[] GetCustomAttributes(ParameterInfo element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Member == null)
			{
				throw new ArgumentException(Environment.GetResourceString("The ParameterInfo object is not valid."), "element");
			}
			if (element.Member.MemberType == MemberTypes.Method && inherit)
			{
				return Attribute.InternalParamGetCustomAttributes(element, null, inherit);
			}
			return element.GetCustomAttributes(typeof(Attribute), inherit) as Attribute[];
		}

		/// <summary>Retrieves an array of the custom attributes applied to a module. Parameters specify the module, and an ignored search option.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Module" /> class that describes a portable executable file. </param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDA RID: 3802 RVA: 0x0003DF6B File Offset: 0x0003C16B
		public static Attribute[] GetCustomAttributes(Module element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (Attribute[])element.GetCustomAttributes(typeof(Attribute), inherit);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a module. Parameters specify the module, the type of the custom attribute to search for, and an ignored search option.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="attributeType" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Module" /> class that describes a portable executable file. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDB RID: 3803 RVA: 0x0003DF98 File Offset: 0x0003C198
		public static Attribute[] GetCustomAttributes(Module element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			return (Attribute[])element.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Retrieves an array of the custom attributes applied to an assembly. Parameters specify the assembly, and the type of the custom attribute to search for.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="attributeType" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDC RID: 3804 RVA: 0x0003E00E File Offset: 0x0003C20E
		public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to an assembly. Parameters specify the assembly, the type of the custom attribute to search for, and an ignored search option.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="attributeType" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDD RID: 3805 RVA: 0x0003E018 File Offset: 0x0003C218
		public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			return (Attribute[])element.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Retrieves an array of the custom attributes applied to an assembly. A parameter specifies the assembly.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDE RID: 3806 RVA: 0x0003E08E File Offset: 0x0003C28E
		public static Attribute[] GetCustomAttributes(Assembly element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to an assembly. Parameters specify the assembly, and an ignored search option.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDF RID: 3807 RVA: 0x0003E097 File Offset: 0x0003C297
		public static Attribute[] GetCustomAttributes(Assembly element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (Attribute[])element.GetCustomAttributes(typeof(Attribute), inherit);
		}

		/// <summary>Retrieves a custom attribute applied to a specified assembly. Parameters specify the assembly and the type of the custom attribute to search for.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003E0C3 File Offset: 0x0003C2C3
		public static Attribute GetCustomAttribute(Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		/// <summary>Retrieves a custom attribute applied to an assembly. Parameters specify the assembly, the type of the custom attribute to search for, and an ignored search option.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003E0D0 File Offset: 0x0003C2D0
		public static Attribute GetCustomAttribute(Assembly element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(Environment.GetResourceString("Multiple custom attributes of the same type found."));
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003E108 File Offset: 0x0003C308
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			RuntimeType runtimeType = (RuntimeType)base.GetType();
			if ((RuntimeType)obj.GetType() != runtimeType)
			{
				return false;
			}
			FieldInfo[] fields = runtimeType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < fields.Length; i++)
			{
				object obj2 = ((RtFieldInfo)fields[i]).UnsafeGetValue(this);
				object obj3 = ((RtFieldInfo)fields[i]).UnsafeGetValue(obj);
				if (!Attribute.AreFieldValuesEqual(obj2, obj3))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003E184 File Offset: 0x0003C384
		private static bool AreFieldValuesEqual(object thisValue, object thatValue)
		{
			if (thisValue == null && thatValue == null)
			{
				return true;
			}
			if (thisValue == null || thatValue == null)
			{
				return false;
			}
			if (thisValue.GetType().IsArray)
			{
				if (!thisValue.GetType().Equals(thatValue.GetType()))
				{
					return false;
				}
				Array array = thisValue as Array;
				Array array2 = thatValue as Array;
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (!Attribute.AreFieldValuesEqual(array.GetValue(i), array2.GetValue(i)))
					{
						return false;
					}
				}
			}
			else if (!thisValue.Equals(thatValue))
			{
				return false;
			}
			return true;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003E218 File Offset: 0x0003C418
		public override int GetHashCode()
		{
			Type type = base.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			object obj = null;
			for (int i = 0; i < fields.Length; i++)
			{
				object obj2 = ((RtFieldInfo)fields[i]).UnsafeGetValue(this);
				if (obj2 != null && !obj2.GetType().IsArray)
				{
					obj = obj2;
				}
				if (obj != null)
				{
					break;
				}
			}
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return type.GetHashCode();
		}

		/// <summary>When implemented in a derived class, gets a unique identifier for this <see cref="T:System.Attribute" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0003395C File Offset: 0x00031B5C
		public virtual object TypeId
		{
			get
			{
				return base.GetType();
			}
		}

		/// <summary>When overridden in a derived class, returns a value that indicates whether this instance equals a specified object.</summary>
		/// <returns>true if this instance equals <paramref name="obj" />; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance of <see cref="T:System.Attribute" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003E27D File Offset: 0x0003C47D
		public virtual bool Match(object obj)
		{
			return this.Equals(obj);
		}

		/// <summary>When overridden in a derived class, indicates whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000EE8 RID: 3816 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsDefaultAttribute()
		{
			return false;
		}
	}
}
