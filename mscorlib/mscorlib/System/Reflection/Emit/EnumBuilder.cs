using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes and represents an enumeration type.</summary>
	// Token: 0x02000662 RID: 1634
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_EnumBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class EnumBuilder : TypeInfo, _EnumBuilder
	{
		// Token: 0x060031AA RID: 12714 RVA: 0x000BAF44 File Offset: 0x000B9144
		internal EnumBuilder(ModuleBuilder mb, string name, TypeAttributes visibility, Type underlyingType)
		{
			this._tb = new TypeBuilder(mb, name, visibility | TypeAttributes.Sealed, typeof(Enum), null, PackingSize.Unspecified, 0, null);
			this._underlyingType = underlyingType;
			this._underlyingField = this._tb.DefineField("value__", underlyingType, FieldAttributes.Private | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
			this.setup_enum_type(this._tb);
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000BAFAA File Offset: 0x000B91AA
		internal TypeBuilder GetTypeBuilder()
		{
			return this._tb;
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000BAFB2 File Offset: 0x000B91B2
		internal override Type InternalResolve()
		{
			return this._tb.InternalResolve();
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000BAFBF File Offset: 0x000B91BF
		internal override Type RuntimeResolve()
		{
			return this._tb.RuntimeResolve();
		}

		/// <summary>Retrieves the dynamic assembly that contains this enum definition.</summary>
		/// <returns>Read-only. The dynamic assembly that contains this enum definition.</returns>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000BAFCC File Offset: 0x000B91CC
		public override Assembly Assembly
		{
			get
			{
				return this._tb.Assembly;
			}
		}

		/// <summary>Returns the full path of this enum qualified by the display name of the parent assembly.</summary>
		/// <returns>Read-only. The full path of this enum qualified by the display name of the parent assembly.</returns>
		/// <exception cref="T:System.NotSupportedException">If <see cref="M:System.Reflection.Emit.EnumBuilder.CreateType" /> has not been called previously. </exception>
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x000BAFD9 File Offset: 0x000B91D9
		public override string AssemblyQualifiedName
		{
			get
			{
				return this._tb.AssemblyQualifiedName;
			}
		}

		/// <summary>Returns the parent <see cref="T:System.Type" /> of this type which is always <see cref="T:System.Enum" />.</summary>
		/// <returns>Read-only. The parent <see cref="T:System.Type" /> of this type.</returns>
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x000BAFE6 File Offset: 0x000B91E6
		public override Type BaseType
		{
			get
			{
				return this._tb.BaseType;
			}
		}

		/// <summary>Returns the type that declared this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</summary>
		/// <returns>Read-only. The type that declared this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</returns>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x000BAFF3 File Offset: 0x000B91F3
		public override Type DeclaringType
		{
			get
			{
				return this._tb.DeclaringType;
			}
		}

		/// <summary>Returns the full path of this enum.</summary>
		/// <returns>Read-only. The full path of this enum.</returns>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060031B2 RID: 12722 RVA: 0x000BB000 File Offset: 0x000B9200
		public override string FullName
		{
			get
			{
				return this._tb.FullName;
			}
		}

		/// <summary>Returns the GUID of this enum.</summary>
		/// <returns>Read-only. The GUID of this enum.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060031B3 RID: 12723 RVA: 0x000BB00D File Offset: 0x000B920D
		public override Guid GUID
		{
			get
			{
				return this._tb.GUID;
			}
		}

		/// <summary>Retrieves the dynamic module that contains this <see cref="T:System.Reflection.Emit.EnumBuilder" /> definition.</summary>
		/// <returns>Read-only. The dynamic module that contains this <see cref="T:System.Reflection.Emit.EnumBuilder" /> definition.</returns>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x000BB01A File Offset: 0x000B921A
		public override Module Module
		{
			get
			{
				return this._tb.Module;
			}
		}

		/// <summary>Returns the name of this enum.</summary>
		/// <returns>Read-only. The name of this enum.</returns>
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x000BB027 File Offset: 0x000B9227
		public override string Name
		{
			get
			{
				return this._tb.Name;
			}
		}

		/// <summary>Returns the namespace of this enum.</summary>
		/// <returns>Read-only. The namespace of this enum.</returns>
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x000BB034 File Offset: 0x000B9234
		public override string Namespace
		{
			get
			{
				return this._tb.Namespace;
			}
		}

		/// <summary>Returns the type that was used to obtain this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</summary>
		/// <returns>Read-only. The type that was used to obtain this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</returns>
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x000BB041 File Offset: 0x000B9241
		public override Type ReflectedType
		{
			get
			{
				return this._tb.ReflectedType;
			}
		}

		/// <summary>Retrieves the internal handle for this enum.</summary>
		/// <returns>Read-only. The internal handle for this enum.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not currently supported. </exception>
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x000BB04E File Offset: 0x000B924E
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this._tb.TypeHandle;
			}
		}

		/// <summary>Returns the underlying system type for this enum.</summary>
		/// <returns>Read-only. Returns the underlying system type.</returns>
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060031B9 RID: 12729 RVA: 0x000BB05B File Offset: 0x000B925B
		public override Type UnderlyingSystemType
		{
			get
			{
				return this._underlyingType;
			}
		}

		/// <summary>Returns the underlying integer type of the current enumeration, which is set when the enumeration builder is defined.</summary>
		/// <returns>The underlying type.</returns>
		// Token: 0x060031BA RID: 12730 RVA: 0x000BB05B File Offset: 0x000B925B
		public override Type GetEnumUnderlyingType()
		{
			return this._underlyingType;
		}

		// Token: 0x060031BB RID: 12731
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void setup_enum_type(Type t);

		// Token: 0x060031BC RID: 12732 RVA: 0x000BB063 File Offset: 0x000B9263
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this._tb.attrs;
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000BB070 File Offset: 0x000B9270
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this._tb.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the public and non-public constructors defined for this class, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the specified constructors defined for this class. If no constructors are defined, an empty array is returned.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060031BE RID: 12734 RVA: 0x000BB084 File Offset: 0x000B9284
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this._tb.GetConstructors(bindingAttr);
		}

		/// <summary>Returns all the custom attributes defined for this constructor.</summary>
		/// <returns>Returns an array of objects representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x060031BF RID: 12735 RVA: 0x000BB092 File Offset: 0x000B9292
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._tb.GetCustomAttributes(inherit);
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <returns>Returns an array of objects representing the attributes of this constructor that are of <see cref="T:System.Type" /><paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The Type object to which the custom attributes are applied. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x060031C0 RID: 12736 RVA: 0x000BB0A0 File Offset: 0x000B92A0
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._tb.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Calling this method always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>This method is not supported. No value is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		// Token: 0x060031C1 RID: 12737 RVA: 0x000BB0AF File Offset: 0x000B92AF
		public override Type GetElementType()
		{
			return this._tb.GetElementType();
		}

		/// <summary>Returns the event with the specified name.</summary>
		/// <returns>Returns an <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name. If there are no matches, null is returned.</returns>
		/// <param name="name">The name of the event to get. </param>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x060031C2 RID: 12738 RVA: 0x000BB0BC File Offset: 0x000B92BC
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetEvent(name, bindingAttr);
		}

		/// <summary>Returns the public and non-public events that are declared by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public and non-public events declared or inherited by this type. An empty array is returned if there are no events, as specified.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060031C3 RID: 12739 RVA: 0x000BB0CB File Offset: 0x000B92CB
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this._tb.GetEvents(bindingAttr);
		}

		/// <summary>Returns the field specified by the given name.</summary>
		/// <returns>Returns the <see cref="T:System.Reflection.FieldInfo" /> object representing the field declared or inherited by this type with the specified name and public or non-public modifier. If there are no matches, then null is returned.</returns>
		/// <param name="name">The name of the field to get. </param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x060031C4 RID: 12740 RVA: 0x000BB0D9 File Offset: 0x000B92D9
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetField(name, bindingAttr);
		}

		/// <summary>Returns the public and non-public fields that are declared by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the public and non-public fields declared or inherited by this type. An empty array is returned if there are no fields, as specified.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060031C5 RID: 12741 RVA: 0x000BB0E8 File Offset: 0x000B92E8
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this._tb.GetFields(bindingAttr);
		}

		/// <summary>Returns the interface implemented (directly or indirectly) by this type, with the specified fully-qualified name.</summary>
		/// <returns>Returns a <see cref="T:System.Type" /> object representing the implemented interface. Returns null if no interface matching name is found.</returns>
		/// <param name="name">The name of the interface. </param>
		/// <param name="ignoreCase">If true, the search is case-insensitive. If false, the search is case-sensitive. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x060031C6 RID: 12742 RVA: 0x000BB0F6 File Offset: 0x000B92F6
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this._tb.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns an interface mapping for the interface requested.</summary>
		/// <returns>The requested interface mapping.</returns>
		/// <param name="interfaceType">The type of the interface for which the interface mapping is to be retrieved. </param>
		/// <exception cref="T:System.ArgumentException">The type does not implement the interface. </exception>
		// Token: 0x060031C7 RID: 12743 RVA: 0x000BB105 File Offset: 0x000B9305
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this._tb.GetInterfaceMap(interfaceType);
		}

		/// <summary>Returns an array of all the interfaces implemented on this a class and its base classes.</summary>
		/// <returns>Returns an array of <see cref="T:System.Type" /> objects representing the implemented interfaces. If none are defined, an empty array is returned.</returns>
		// Token: 0x060031C8 RID: 12744 RVA: 0x000BB113 File Offset: 0x000B9313
		public override Type[] GetInterfaces()
		{
			return this._tb.GetInterfaces();
		}

		/// <summary>Returns all members with the specified name, type, and binding that are declared or inherited by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public members are returned.</returns>
		/// <param name="name">The name of the member. </param>
		/// <param name="type">The type of member that is to be returned. </param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x060031C9 RID: 12745 RVA: 0x000BB120 File Offset: 0x000B9320
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this._tb.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns the specified members declared or inherited by this type,.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members declared or inherited by this type. An empty array is returned if there are no matching members.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060031CA RID: 12746 RVA: 0x000BB130 File Offset: 0x000B9330
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this._tb.GetMembers(bindingAttr);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000BB13E File Offset: 0x000B933E
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this._tb.GetMethod(name, bindingAttr);
			}
			return this._tb.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns all the public and non-public methods declared or inherited by this type, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing the public and non-public methods defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public methods are returned.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060031CC RID: 12748 RVA: 0x000BB166 File Offset: 0x000B9366
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this._tb.GetMethods(bindingAttr);
		}

		/// <summary>Returns the specified nested type that is declared by this type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the nested type that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the nested type to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to conduct a case-sensitive search for public methods. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x060031CD RID: 12749 RVA: 0x000BB174 File Offset: 0x000B9374
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetNestedType(name, bindingAttr);
		}

		/// <summary>Returns all the public and non-public properties declared or inherited by this type, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing the public and non-public properties defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public properties are returned.</returns>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060031CE RID: 12750 RVA: 0x000BB183 File Offset: 0x000B9383
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this._tb.GetProperties(bindingAttr);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000BB191 File Offset: 0x000B9391
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000BB199 File Offset: 0x000B9399
		protected override bool HasElementTypeImpl()
		{
			return this._tb.HasElementType;
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the contraints of the specified binder and invocation attributes.</summary>
		/// <returns>Returns the return value of the invoked member.</returns>
		/// <param name="name">The name of the member to invoke. This can be a constructor, method, property, or field. A suitable invocation attribute must be specified. Note that it is possible to invoke the default member of a class by passing an empty string as the name of the member. </param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from BindingFlags. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If binder is null, the default binder is used. See <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="target">The object on which to invoke the specified member. If the member is static, this parameter is ignored. </param>
		/// <param name="args">An argument list. This is an array of objects that contains the number, order, and type of the parameters of the member to be invoked. If there are no parameters this should be null. </param>
		/// <param name="modifiers">An array of the same length as <paramref name="args" /> with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the metadata. They are used by various interoperability services. See the metadata specs for details such as this. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is null, the CultureInfo for the current thread is used. (Note that this is necessary to, for example, convert a string that represents 1000 to a double value, since 1000 is represented differently by different cultures.) </param>
		/// <param name="namedParameters">Each parameter in the <paramref name="namedParameters" /> array gets the value in the corresponding element in the <paramref name="args" /> array. If the length of <paramref name="args" /> is greater than the length of <paramref name="namedParameters" />, the remaining argument values are passed in order. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060031D1 RID: 12753 RVA: 0x000BB1A8 File Offset: 0x000B93A8
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this._tb.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x0000C091 File Offset: 0x0000A291
		protected override bool IsValueTypeImpl()
		{
			return true;
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <returns>true if one or more instance of <paramref name="attributeType" /> is defined on this member; otherwise, false.</returns>
		/// <param name="attributeType">The Type object to which the custom attributes are applied. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x060031D8 RID: 12760 RVA: 0x000BB1CD File Offset: 0x000B93CD
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._tb.IsDefined(attributeType, inherit);
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000B80A1 File Offset: 0x000B62A1
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is less than 1.</exception>
		// Token: 0x060031DA RID: 12762 RVA: 0x000B80AA File Offset: 0x000B62AA
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000B80BD File Offset: 0x000B62BD
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000B80C5 File Offset: 0x000B62C5
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000B9312 File Offset: 0x000B7512
		private Exception CreateNotSupportedException()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x00033991 File Offset: 0x00031B91
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>true if this object represents a constructed generic type; otherwise, false.</returns>
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400194D RID: 6477
		private TypeBuilder _tb;

		// Token: 0x0400194E RID: 6478
		private FieldBuilder _underlyingField;

		// Token: 0x0400194F RID: 6479
		private Type _underlyingType;
	}
}
