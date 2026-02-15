using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a constructor of a dynamic class.</summary>
	// Token: 0x02000658 RID: 1624
	[ComDefaultInterface(typeof(_ConstructorBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
	{
		// Token: 0x0600312E RID: 12590 RVA: 0x000B9898 File Offset: 0x000B7A98
		internal ConstructorBuilder(TypeBuilder tb, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.attrs = attributes | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
			this.call_conv = callingConvention;
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Elements of the parameterTypes array cannot be null", "parameterTypes");
					}
				}
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, parameterTypes.Length);
			}
			this.type = tb;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			this.table_idx = this.get_next_table_index(this, 6, 1);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.CallingConventions" /> value that depends on whether the declaring type is generic.</summary>
		/// <returns>
		///   <see cref="F:System.Reflection.CallingConventions.HasThis" /> if the declaring type is generic; otherwise, <see cref="F:System.Reflection.CallingConventions.Standard" />. </returns>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000B9960 File Offset: 0x000B7B60
		[MonoTODO]
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x000B9968 File Offset: 0x000B7B68
		internal TypeBuilder TypeBuilder
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns the method implementation flags for this constructor.</summary>
		/// <returns>The method implementation flags for this constructor.</returns>
		// Token: 0x06003131 RID: 12593 RVA: 0x000B9970 File Offset: 0x000B7B70
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		/// <summary>Returns the parameters of this constructor.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.ParameterInfo" /> objects that represent the parameters of this constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called on this constructor's type, in the .NET Framework versions 1.0 and 1.1. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called on this constructor's type, in the .NET Framework version 2.0. </exception>
		// Token: 0x06003132 RID: 12594 RVA: 0x000B9978 File Offset: 0x000B7B78
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created)
			{
				throw this.not_created();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000B9994 File Offset: 0x000B7B94
		internal override ParameterInfo[] GetParametersInternal()
		{
			if (this.parameters == null)
			{
				return EmptyArray<ParameterInfo>.Value;
			}
			ParameterInfo[] array = new ParameterInfo[this.parameters.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				ParameterInfo[] array2 = array;
				int num = i;
				ParameterBuilder[] array3 = this.pinfo;
				array2[num] = RuntimeParameterInfo.New((array3 != null) ? array3[i + 1] : null, this.parameters[i], this, i + 1);
			}
			return array;
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000B99F6 File Offset: 0x000B7BF6
		internal override int GetParametersCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000B9A0A File Offset: 0x000B7C0A
		internal MethodBase RuntimeResolve()
		{
			return this.type.RuntimeResolve().GetConstructor(this);
		}

		/// <summary>Dynamically invokes the constructor reflected by this instance with the specified arguments, under the constraints of the specified Binder.</summary>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <param name="obj">The object that needs to be reinitialized. </param>
		/// <param name="invokeAttr">One of the BindingFlags values that specifies the type of binding that is desired. </param>
		/// <param name="binder">A Binder that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is null, then Binder.DefaultBinding is used. </param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the constructor to be invoked. If there are no parameters, this should be a null reference (Nothing in Visual Basic). </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />. </exception>
		// Token: 0x06003136 RID: 12598 RVA: 0x000B9A1D File Offset: 0x000B7C1D
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.not_supported();
		}

		/// <summary>Invokes the constructor dynamically reflected by this instance on the given object, passing along the specified parameters, and under the constraints of the given binder.</summary>
		/// <returns>Returns an <see cref="T:System.Object" /> that is the return value of the invoked constructor.</returns>
		/// <param name="invokeAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If binder is null, the default binder is used. See <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the constructor to be invoked. If there are no parameters this should be null. </param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. (For example, this is necessary to convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, since 1000 is represented differently by different cultures.) </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />. </exception>
		// Token: 0x06003137 RID: 12599 RVA: 0x000B9A1D File Offset: 0x000B7C1D
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.not_supported();
		}

		/// <summary>Retrieves the internal handle for the method. Use this handle to access the underlying metadata handle.</summary>
		/// <returns>Returns the internal handle for the method. Use this handle to access the underlying metadata handle.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this class. </exception>
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x000B9A1D File Offset: 0x000B7C1D
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Retrieves the attributes for this constructor.</summary>
		/// <returns>Returns the attributes for this constructor.</returns>
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000B9A25 File Offset: 0x000B7C25
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Holds a reference to the <see cref="T:System.Type" /> object from which this object was obtained.</summary>
		/// <returns>Returns the Type object from which this object was obtained.</returns>
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x000B9968 File Offset: 0x000B7B68
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves a reference to the <see cref="T:System.Type" /> object for the type that declares this member.</summary>
		/// <returns>Returns the <see cref="T:System.Type" /> object for the type that declares this member.</returns>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x000B9968 File Offset: 0x000B7B68
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves the name of this constructor.</summary>
		/// <returns>Returns the name of this constructor.</returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x000B9A2D File Offset: 0x000B7C2D
		public override string Name
		{
			get
			{
				if ((this.attrs & MethodAttributes.Static) == MethodAttributes.PrivateScope)
				{
					return ConstructorInfo.ConstructorName;
				}
				return ConstructorInfo.TypeConstructorName;
			}
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <returns>true if the specified custom attribute type is defined; otherwise, false.</returns>
		/// <param name="attributeType">A custom attribute type. </param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />. </exception>
		// Token: 0x0600313D RID: 12605 RVA: 0x000B9A1D File Offset: 0x000B7C1D
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns all the custom attributes defined for this constructor.</summary>
		/// <returns>Returns an array of objects representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance.</returns>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		// Token: 0x0600313E RID: 12606 RVA: 0x000B9A1D File Offset: 0x000B7C1D
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <returns>Returns an array of type <see cref="T:System.Object" /> representing the attributes of this constructor.</returns>
		/// <param name="attributeType">The custom attribute type. </param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		// Token: 0x0600313F RID: 12607 RVA: 0x000B9A1D File Offset: 0x000B7C1D
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Emit.ILGenerator" /> for this constructor.</summary>
		/// <returns>Returns an <see cref="T:System.Reflection.Emit.ILGenerator" /> object for this constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">The constructor is a default constructor.-or-The constructor has <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags indicating that it should not have a method body.</exception>
		// Token: 0x06003140 RID: 12608 RVA: 0x000B9A45 File Offset: 0x000B7C45
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Emit.ILGenerator" /> object, with the specified MSIL stream size, that can be used to build a method body for this constructor.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> for this constructor.</returns>
		/// <param name="streamSize">The size of the MSIL stream, in bytes.</param>
		/// <exception cref="T:System.InvalidOperationException">The constructor is a default constructor.-or-The constructor has <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags indicating that it should not have a method body. </exception>
		// Token: 0x06003141 RID: 12609 RVA: 0x000B9A50 File Offset: 0x000B7C50
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), streamSize);
			return this.ilgen;
		}

		/// <summary>Returns a reference to the module that contains this constructor.</summary>
		/// <returns>The module that contains this constructor.</returns>
		// Token: 0x06003142 RID: 12610 RVA: 0x000B9A9E File Offset: 0x000B7C9E
		public Module GetModule()
		{
			return this.type.Module;
		}

		/// <summary>Returns the <see cref="T:System.Reflection.Emit.MethodToken" /> that represents the token for this constructor.</summary>
		/// <returns>Returns the <see cref="T:System.Reflection.Emit.MethodToken" /> of this constructor.</returns>
		// Token: 0x06003143 RID: 12611 RVA: 0x000B9AAB File Offset: 0x000B7CAB
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		/// <summary>Gets the dynamic module in which this constructor is defined.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object that represents the dynamic module in which this constructor is defined.</returns>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x000B9ABE File Offset: 0x000B7CBE
		public override Module Module
		{
			get
			{
				return this.GetModule();
			}
		}

		/// <summary>Returns this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance as a <see cref="T:System.String" />.</summary>
		/// <returns>Returns a <see cref="T:System.String" /> containing the name, attributes, and exceptions of this constructor, followed by the current Microsoft intermediate language (MSIL) stream.</returns>
		// Token: 0x06003145 RID: 12613 RVA: 0x000B9AC6 File Offset: 0x000B7CC6
		public override string ToString()
		{
			return "ConstructorBuilder ['" + this.type.Name + "']";
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000B9AE4 File Offset: 0x000B7CE4
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || this.ilgen.ILOffset == 0))
			{
				throw new InvalidOperationException("Method '" + this.Name + "' does not have a method body.");
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup(this);
			}
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000B9B54 File Offset: 0x000B7D54
		internal void ResolveUserTypes()
		{
			TypeBuilder.ResolveUserTypes(this.parameters);
			if (this.paramModReq != null)
			{
				Type[][] array = this.paramModReq;
				for (int i = 0; i < array.Length; i++)
				{
					TypeBuilder.ResolveUserTypes(array[i]);
				}
			}
			if (this.paramModOpt != null)
			{
				Type[][] array = this.paramModOpt;
				for (int i = 0; i < array.Length; i++)
				{
					TypeBuilder.ResolveUserTypes(array[i]);
				}
			}
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000B9BB6 File Offset: 0x000B7DB6
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.ilgen != null)
			{
				this.ilgen.FixupTokens(token_map, member_map);
			}
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000B9BD0 File Offset: 0x000B7DD0
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			if (this.ilgen != null && this.ilgen.HasDebugInfo)
			{
				SymbolToken symbolToken = new SymbolToken(this.GetToken().Token);
				symbolWriter.OpenMethod(symbolToken);
				symbolWriter.SetSymAttribute(symbolToken, "__name", Encoding.UTF8.GetBytes(this.Name));
				this.ilgen.GenerateDebugInfo(symbolWriter);
				symbolWriter.CloseMethod();
			}
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000B9C3C File Offset: 0x000B7E3C
		internal override int get_next_table_index(object obj, int table, int count)
		{
			return this.type.get_next_table_index(obj, table, count);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000B9312 File Offset: 0x000B7512
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000B9C4C File Offset: 0x000B7E4C
		private Exception not_created()
		{
			return new NotSupportedException("The type is not yet created.");
		}

		// Token: 0x04001920 RID: 6432
		private RuntimeMethodHandle mhandle;

		// Token: 0x04001921 RID: 6433
		private ILGenerator ilgen;

		// Token: 0x04001922 RID: 6434
		internal Type[] parameters;

		// Token: 0x04001923 RID: 6435
		private MethodAttributes attrs;

		// Token: 0x04001924 RID: 6436
		private MethodImplAttributes iattrs;

		// Token: 0x04001925 RID: 6437
		private int table_idx;

		// Token: 0x04001926 RID: 6438
		private CallingConventions call_conv;

		// Token: 0x04001927 RID: 6439
		private TypeBuilder type;

		// Token: 0x04001928 RID: 6440
		internal ParameterBuilder[] pinfo;

		// Token: 0x04001929 RID: 6441
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x0400192A RID: 6442
		private bool init_locals = true;

		// Token: 0x0400192B RID: 6443
		private Type[][] paramModReq;

		// Token: 0x0400192C RID: 6444
		private Type[][] paramModOpt;

		// Token: 0x0400192D RID: 6445
		private RefEmitPermissionSet[] permissions;
	}
}
