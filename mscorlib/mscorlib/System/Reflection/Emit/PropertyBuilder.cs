using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines the properties for a type.</summary>
	// Token: 0x0200067D RID: 1661
	[ComDefaultInterface(typeof(_PropertyBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
	{
		// Token: 0x0600336F RID: 13167 RVA: 0x000C1E38 File Offset: 0x000C0038
		internal PropertyBuilder(TypeBuilder tb, string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.name = name;
			this.attrs = attributes;
			this.callingConvention = callingConvention;
			this.type = returnType;
			this.returnModReq = returnModReq;
			this.returnModOpt = returnModOpt;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			if (parameterTypes != null)
			{
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, this.parameters.Length);
			}
			this.typeb = tb;
			this.table_idx = tb.get_next_table_index(this, 23, 1);
		}

		/// <summary>Gets a value indicating whether the property can be read.</summary>
		/// <returns>true if this property can be read; otherwise, false.</returns>
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000C1EC8 File Offset: 0x000C00C8
		public override bool CanRead
		{
			get
			{
				return this.get_method != null;
			}
		}

		/// <summary>Gets a value indicating whether the property can be written to.</summary>
		/// <returns>true if this property can be written to; otherwise, false.</returns>
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x000C1ED6 File Offset: 0x000C00D6
		public override bool CanWrite
		{
			get
			{
				return this.set_method != null;
			}
		}

		/// <summary>Gets the class that declares this member.</summary>
		/// <returns>The Type object for the class that declares this member.</returns>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x000C1EE4 File Offset: 0x000C00E4
		public override Type DeclaringType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Gets the name of this member.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this member.</returns>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06003373 RID: 13171 RVA: 0x000C1EEC File Offset: 0x000C00EC
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the type of the field of this property.</summary>
		/// <returns>The type of this property.</returns>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x000C1EF4 File Offset: 0x000C00F4
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the class object that was used to obtain this instance of MemberInfo.</summary>
		/// <returns>The Type object through which this MemberInfo object was obtained.</returns>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x000C1EE4 File Offset: 0x000C00E4
		public override Type ReflectedType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Returns an array of all the custom attributes for this property.</summary>
		/// <returns>An array of all the custom attributes.</returns>
		/// <param name="inherit">If true, walks up this property's inheritance chain to find the custom attributes </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06003376 RID: 13174 RVA: 0x000C1EFC File Offset: 0x000C00FC
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns an array of custom attributes identified by <see cref="T:System.Type" />.</summary>
		/// <returns>An array of custom attributes defined on this reflected member, or null if no attributes are defined on this member.</returns>
		/// <param name="attributeType">An array of custom attributes identified by type. </param>
		/// <param name="inherit">If true, walks up this property's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06003377 RID: 13175 RVA: 0x000C1EFC File Offset: 0x000C00FC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns the public and non-public get accessor for this property.</summary>
		/// <returns>A MethodInfo object representing the get accessor for this property, if <paramref name="nonPublic" /> is true. Returns null if <paramref name="nonPublic" /> is false and the get accessor is non-public, or if <paramref name="nonPublic" /> is true but no get accessors exist.</returns>
		/// <param name="nonPublic">Indicates whether non-public get accessors should be returned. true if non-public methods are to be included; otherwise, false. </param>
		// Token: 0x06003378 RID: 13176 RVA: 0x000C1F04 File Offset: 0x000C0104
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			return this.get_method;
		}

		/// <summary>Returns an array of all the index parameters for the property.</summary>
		/// <returns>An array of type ParameterInfo containing the parameters for the indexes.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06003379 RID: 13177 RVA: 0x000C1EFC File Offset: 0x000C00FC
		public override ParameterInfo[] GetIndexParameters()
		{
			throw this.not_supported();
		}

		/// <summary>Returns the set accessor for this property.</summary>
		/// <returns>Value Condition A <see cref="T:System.Reflection.MethodInfo" /> object representing the Set method for this property. The set accessor is public.<paramref name="nonPublic" /> is true and non-public methods can be returned. null <paramref name="nonPublic" /> is true, but the property is read-only.<paramref name="nonPublic" /> is false and the set accessor is non-public. </returns>
		/// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. true if non-public methods are to be included; otherwise, false. </param>
		// Token: 0x0600337A RID: 13178 RVA: 0x000C1F0C File Offset: 0x000C010C
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			return this.set_method;
		}

		/// <summary>Gets the value of the indexed property by calling the property's getter method.</summary>
		/// <returns>The value of the specified indexed property.</returns>
		/// <param name="obj">The object whose property value will be returned. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x0600337B RID: 13179 RVA: 0x000082D2 File Offset: 0x000064D2
		public override object GetValue(object obj, object[] index)
		{
			return null;
		}

		/// <summary>Gets the value of a property having the specified binding, index, and CultureInfo.</summary>
		/// <returns>The property value for <paramref name="obj" />.</returns>
		/// <param name="obj">The object whose property value will be returned. </param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from BindingFlags : InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, or SetProperty. A suitable invocation attribute must be specified. If a static member is to be invoked, the Static flag of BindingFlags must be set. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <param name="culture">The CultureInfo object that represents the culture for which the resource is to be localized. Note that if the resource is not localized for this culture, the CultureInfo.Parent method will be called successively in search of a match. If this value is null, the CultureInfo is obtained from the CultureInfo.CurrentUICulture property. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x0600337C RID: 13180 RVA: 0x000C1EFC File Offset: 0x000C00FC
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw this.not_supported();
		}

		/// <summary>Indicates whether one or more instance of <paramref name="attributeType" /> is defined on this property.</summary>
		/// <returns>true if one or more instance of <paramref name="attributeType" /> is defined on this property; otherwise false.</returns>
		/// <param name="attributeType">The Type object to which the custom attributes are applied. </param>
		/// <param name="inherit">Specifies whether to walk up this property's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x0600337D RID: 13181 RVA: 0x000C1EFC File Offset: 0x000C00FC
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Sets the method that gets the property value.</summary>
		/// <param name="mdBuilder">A MethodBuilder object that represents the method that gets the property value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x0600337E RID: 13182 RVA: 0x000C1F14 File Offset: 0x000C0114
		public void SetGetMethod(MethodBuilder mdBuilder)
		{
			this.get_method = mdBuilder;
		}

		/// <summary>Sets the value of the property with optional index values for index properties.</summary>
		/// <param name="obj">The object whose property value will be set. </param>
		/// <param name="value">The new value for this property. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x0600337F RID: 13183 RVA: 0x00002C89 File Offset: 0x00000E89
		public override void SetValue(object obj, object value, object[] index)
		{
		}

		/// <summary>Sets the property value for the given object to the given value.</summary>
		/// <param name="obj">The object whose property value will be returned. </param>
		/// <param name="value">The new value for this property. </param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from BindingFlags : InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, or SetProperty. A suitable invocation attribute must be specified. If a static member is to be invoked, the Static flag of BindingFlags must be set. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <param name="culture">The CultureInfo object that represents the culture for which the resource is to be localized. Note that if the resource is not localized for this culture, the CultureInfo.Parent method will be called successively in search of a match. If this value is null, the CultureInfo is obtained from the CultureInfo.CurrentUICulture property. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06003380 RID: 13184 RVA: 0x00002C89 File Offset: 0x00000E89
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
		}

		/// <summary>Gets the module in which the type that declares the current property is being defined.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> in which the type that declares the current property is defined.</returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000BB37C File Offset: 0x000B957C
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000B9312 File Offset: 0x000B7512
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x04001AE4 RID: 6884
		private PropertyAttributes attrs;

		// Token: 0x04001AE5 RID: 6885
		private string name;

		// Token: 0x04001AE6 RID: 6886
		private Type type;

		// Token: 0x04001AE7 RID: 6887
		private Type[] parameters;

		// Token: 0x04001AE8 RID: 6888
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04001AE9 RID: 6889
		private object def_value;

		// Token: 0x04001AEA RID: 6890
		private MethodBuilder set_method;

		// Token: 0x04001AEB RID: 6891
		private MethodBuilder get_method;

		// Token: 0x04001AEC RID: 6892
		private int table_idx;

		// Token: 0x04001AED RID: 6893
		internal TypeBuilder typeb;

		// Token: 0x04001AEE RID: 6894
		private Type[] returnModReq;

		// Token: 0x04001AEF RID: 6895
		private Type[] returnModOpt;

		// Token: 0x04001AF0 RID: 6896
		private Type[][] paramModReq;

		// Token: 0x04001AF1 RID: 6897
		private Type[][] paramModOpt;

		// Token: 0x04001AF2 RID: 6898
		private CallingConventions callingConvention;
	}
}
