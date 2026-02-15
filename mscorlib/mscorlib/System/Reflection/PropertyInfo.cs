using System;
using System.Diagnostics;
using System.Globalization;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a property and provides access to property metadata.</summary>
	// Token: 0x02000618 RID: 1560
	[Serializable]
	public abstract class PropertyInfo : MemberInfo
	{
		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a property.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a property.</returns>
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x0001D5F1 File Offset: 0x0001B7F1
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		/// <summary>Gets the type of this property.</summary>
		/// <returns>The type of this property.</returns>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002D6A RID: 11626
		public abstract Type PropertyType { get; }

		/// <summary>When overridden in a derived class, returns an array of all the index parameters for the property.</summary>
		/// <returns>An array of type ParameterInfo containing the parameters for the indexes. If the property is not indexed, the array has 0 (zero) elements.</returns>
		// Token: 0x06002D6B RID: 11627
		public abstract ParameterInfo[] GetIndexParameters();

		/// <summary>Gets a value indicating whether the property can be read.</summary>
		/// <returns>true if this property can be read; otherwise, false.</returns>
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002D6C RID: 11628
		public abstract bool CanRead { get; }

		/// <summary>Gets a value indicating whether the property can be written to.</summary>
		/// <returns>true if this property can be written to; otherwise, false.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002D6D RID: 11629
		public abstract bool CanWrite { get; }

		/// <summary>Gets the get accessor for this property.</summary>
		/// <returns>The get accessor for this property.</returns>
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x000B25D1 File Offset: 0x000B07D1
		public virtual MethodInfo GetMethod
		{
			get
			{
				return this.GetGetMethod(true);
			}
		}

		/// <summary>Returns the public get accessor for this property.</summary>
		/// <returns>A MethodInfo object representing the public get accessor for this property, or null if the get accessor is non-public or does not exist.</returns>
		// Token: 0x06002D6F RID: 11631 RVA: 0x000B25DA File Offset: 0x000B07DA
		public MethodInfo GetGetMethod()
		{
			return this.GetGetMethod(false);
		}

		/// <summary>When overridden in a derived class, returns the public or non-public get accessor for this property.</summary>
		/// <returns>A MethodInfo object representing the get accessor for this property, if <paramref name="nonPublic" /> is true. Returns null if <paramref name="nonPublic" /> is false and the get accessor is non-public, or if <paramref name="nonPublic" /> is true but no get accessors exist.</returns>
		/// <param name="nonPublic">Indicates whether a non-public get accessor should be returned. true if a non-public accessor is to be returned; otherwise, false. </param>
		/// <exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method. </exception>
		// Token: 0x06002D70 RID: 11632
		public abstract MethodInfo GetGetMethod(bool nonPublic);

		/// <summary>Gets the set accessor for this property.</summary>
		/// <returns>The set accessor for this property.</returns>
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000B25E3 File Offset: 0x000B07E3
		public virtual MethodInfo SetMethod
		{
			get
			{
				return this.GetSetMethod(true);
			}
		}

		/// <summary>Returns the public set accessor for this property.</summary>
		/// <returns>The MethodInfo object representing the Set method for this property if the set accessor is public, or null if the set accessor is not public.</returns>
		// Token: 0x06002D72 RID: 11634 RVA: 0x000B25EC File Offset: 0x000B07EC
		public MethodInfo GetSetMethod()
		{
			return this.GetSetMethod(false);
		}

		/// <summary>When overridden in a derived class, returns the set accessor for this property.</summary>
		/// <returns>Value Condition A <see cref="T:System.Reflection.MethodInfo" /> object representing the Set method for this property. The set accessor is public.-or- <paramref name="nonPublic" /> is true and the set accessor is non-public. null<paramref name="nonPublic" /> is true, but the property is read-only.-or- <paramref name="nonPublic" /> is false and the set accessor is non-public.-or- There is no set accessor. </returns>
		/// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. true if a non-public accessor is to be returned; otherwise, false. </param>
		/// <exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method. </exception>
		// Token: 0x06002D73 RID: 11635
		public abstract MethodInfo GetSetMethod(bool nonPublic);

		/// <summary>Returns the property value of a specified object.</summary>
		/// <returns>The property value of the specified object.</returns>
		/// <param name="obj">The object whose property value will be returned.</param>
		// Token: 0x06002D74 RID: 11636 RVA: 0x000B25F5 File Offset: 0x000B07F5
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object GetValue(object obj)
		{
			return this.GetValue(obj, null);
		}

		/// <summary>Returns the property value of a specified object with optional index values for indexed properties.</summary>
		/// <returns>The property value of the specified object.</returns>
		/// <param name="obj">The object whose property value will be returned. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.-or- The property's get accessor is not found. </exception>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.There was an illegal attempt to access a private or protected method inside a class. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x06002D75 RID: 11637 RVA: 0x000B25FF File Offset: 0x000B07FF
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		/// <summary>When overridden in a derived class, returns the property value of a specified object that has the specified binding, index, and culture-specific information.</summary>
		/// <returns>The property value of the specified object.</returns>
		/// <param name="obj">The object whose property value will be returned. </param>
		/// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, and SetProperty. You must specify a suitable invocation attribute. For example, to invoke a static member, set the Static flag. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent" /> property will be called successively in search of a match. If this value is null, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.-or- The property's get accessor is not found. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes. </exception>
		/// <exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x06002D76 RID: 11638
		public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		/// <summary>Sets the property value of a specified object.</summary>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="value">The new property value.</param>
		// Token: 0x06002D77 RID: 11639 RVA: 0x000B260C File Offset: 0x000B080C
		[DebuggerHidden]
		[DebuggerStepThrough]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, null);
		}

		/// <summary>Sets the property value of a specified object with optional index values for index properties.</summary>
		/// <param name="obj">The object whose property value will be set. </param>
		/// <param name="value">The new property value. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.-or- The property's set accessor is not found. </exception>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.There was an illegal attempt to access a private or protected method inside a class. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x06002D78 RID: 11640 RVA: 0x000B2617 File Offset: 0x000B0817
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, index, null);
		}

		/// <summary>When overridden in a derived class, sets the property value for a specified object that has the specified binding, index, and culture-specific information.</summary>
		/// <param name="obj">The object whose property value will be set. </param>
		/// <param name="value">The new property value. </param>
		/// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, or SetProperty. You must specify a suitable invocation attribute. For example, to invoke a static member, set the Static flag. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent" /> property will be called successively in search of a match. If this value is null, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.-or- The property's set accessor is not found. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes. </exception>
		/// <exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x06002D79 RID: 11641
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002D7A RID: 11642 RVA: 0x000B1944 File Offset: 0x000AFB44
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002D7B RID: 11643 RVA: 0x000B194D File Offset: 0x000AFB4D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.PropertyInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002D7C RID: 11644 RVA: 0x0004AE04 File Offset: 0x00049004
		public static bool operator ==(PropertyInfo left, PropertyInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.PropertyInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002D7D RID: 11645 RVA: 0x000B2625 File Offset: 0x000B0825
		public static bool operator !=(PropertyInfo left, PropertyInfo right)
		{
			return !(left == right);
		}
	}
}
