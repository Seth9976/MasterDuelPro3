using System;

namespace System.Reflection
{
	/// <summary>Obtains information about the attributes of a member and provides access to member metadata.</summary>
	// Token: 0x02000608 RID: 1544
	[Serializable]
	public abstract class MemberInfo : ICustomAttributeProvider
	{
		/// <summary>When overridden in a derived class, gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating the type of the member — method, constructor, event, and so on.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating the type of member.</returns>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002CEF RID: 11503
		public abstract MemberTypes MemberType { get; }

		/// <summary>Gets the name of the current member.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this member.</returns>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002CF0 RID: 11504
		public abstract string Name { get; }

		/// <summary>Gets the class that declares this member.</summary>
		/// <returns>The Type object for the class that declares this member.</returns>
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002CF1 RID: 11505
		public abstract Type DeclaringType { get; }

		/// <summary>Gets the class object that was used to obtain this instance of MemberInfo.</summary>
		/// <returns>The Type object through which this MemberInfo object was obtained.</returns>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002CF2 RID: 11506
		public abstract Type ReflectedType { get; }

		/// <summary>Gets the module in which the type that declares the member represented by the current <see cref="T:System.Reflection.MemberInfo" /> is defined.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> in which the type that declares the member represented by the current <see cref="T:System.Reflection.MemberInfo" /> is defined.</returns>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented.</exception>
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000B1DB0 File Offset: 0x000AFFB0
		public virtual Module Module
		{
			get
			{
				Type type = this as Type;
				if (type != null)
				{
					return type.Module;
				}
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>When overridden in a derived class, indicates whether one or more attributes of the specified type or of its derived types is applied to this member.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" /> or any of its derived types is applied to this member; otherwise, false.</returns>
		/// <param name="attributeType">The type of custom attribute to search for. The search includes derived types. </param>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. This parameter is ignored for properties and events; see Remarks.</param>
		// Token: 0x06002CF4 RID: 11508
		public abstract bool IsDefined(Type attributeType, bool inherit);

		/// <summary>When overridden in a derived class, returns an array of all custom attributes applied to this member. </summary>
		/// <returns>An array that contains all the custom attributes applied to this member, or an array with zero elements if no attributes are defined.</returns>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. This parameter is ignored for properties and events; see Remarks.</param>
		/// <exception cref="T:System.InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context. See How to: Load Assemblies into the Reflection-Only Context.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded. </exception>
		// Token: 0x06002CF5 RID: 11509
		public abstract object[] GetCustomAttributes(bool inherit);

		/// <summary>When overridden in a derived class, returns an array of custom attributes applied to this member and identified by <see cref="T:System.Type" />.</summary>
		/// <returns>An array of custom attributes applied to this member, or an array with zero elements if no attributes assignable to <paramref name="attributeType" /> have been applied.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned. </param>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. This parameter is ignored for properties and events; see Remarks. </param>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="attributeType" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context. See How to: Load Assemblies into the Reflection-Only Context.</exception>
		// Token: 0x06002CF6 RID: 11510
		public abstract object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Gets a value that identifies a metadata element.</summary>
		/// <returns>A value which, in combination with <see cref="P:System.Reflection.MemberInfo.Module" />, uniquely identifies a metadata element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Reflection.MemberInfo" /> represents an array method, such as Address, on an array type whose element type is a dynamic type that has not been completed. To get a metadata token in this case, pass the <see cref="T:System.Reflection.MemberInfo" /> object to the <see cref="M:System.Reflection.Emit.ModuleBuilder.GetMethodToken(System.Reflection.MethodInfo)" /> method; or use the <see cref="M:System.Reflection.Emit.ModuleBuilder.GetArrayMethodToken(System.Type,System.String,System.Reflection.CallingConventions,System.Type,System.Type[])" />  method to get the token directly, instead of using the <see cref="M:System.Reflection.Emit.ModuleBuilder.GetArrayMethod(System.Type,System.String,System.Reflection.CallingConventions,System.Type,System.Type[])" /> method to get a <see cref="T:System.Reflection.MethodInfo" /> first.</exception>
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x000B1DD9 File Offset: 0x000AFFD9
		public virtual int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002CF8 RID: 11512 RVA: 0x000726BE File Offset: 0x000708BE
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002CF9 RID: 11513 RVA: 0x0006EF14 File Offset: 0x0006D114
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MemberInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise false.</returns>
		/// <param name="left">The <see cref="T:System.Reflection.MemberInfo" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">The <see cref="T:System.Reflection.MemberInfo" /> to compare to <paramref name="left" />.</param>
		// Token: 0x06002CFA RID: 11514 RVA: 0x000B1DE0 File Offset: 0x000AFFE0
		public static bool operator ==(MemberInfo left, MemberInfo right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			Type type;
			Type type2;
			if ((type = left as Type) != null && (type2 = right as Type) != null)
			{
				return type == type2;
			}
			MethodBase methodBase;
			MethodBase methodBase2;
			if ((methodBase = left as MethodBase) != null && (methodBase2 = right as MethodBase) != null)
			{
				return methodBase == methodBase2;
			}
			FieldInfo fieldInfo;
			FieldInfo fieldInfo2;
			if ((fieldInfo = left as FieldInfo) != null && (fieldInfo2 = right as FieldInfo) != null)
			{
				return fieldInfo == fieldInfo2;
			}
			EventInfo eventInfo;
			EventInfo eventInfo2;
			if ((eventInfo = left as EventInfo) != null && (eventInfo2 = right as EventInfo) != null)
			{
				return eventInfo == eventInfo2;
			}
			PropertyInfo propertyInfo;
			PropertyInfo propertyInfo2;
			return (propertyInfo = left as PropertyInfo) != null && (propertyInfo2 = right as PropertyInfo) != null && propertyInfo == propertyInfo2;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MemberInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise false.</returns>
		/// <param name="left">The <see cref="T:System.Reflection.MemberInfo" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">The <see cref="T:System.Reflection.MemberInfo" /> to compare to <paramref name="left" />.</param>
		// Token: 0x06002CFB RID: 11515 RVA: 0x000B1ED0 File Offset: 0x000B00D0
		public static bool operator !=(MemberInfo left, MemberInfo right)
		{
			return !(left == right);
		}
	}
}
