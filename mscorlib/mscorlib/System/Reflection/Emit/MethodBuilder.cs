using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a method (or constructor) on a dynamic class.</summary>
	// Token: 0x02000673 RID: 1651
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_MethodBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MethodBuilder : MethodInfo, _MethodBuilder
	{
		// Token: 0x06003297 RID: 12951 RVA: 0x000BDB28 File Offset: 0x000BBD28
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.name = name;
			this.attrs = attributes;
			this.call_conv = callingConvention;
			this.rtype = returnType;
			this.returnModReq = returnModReq;
			this.returnModOpt = returnModOpt;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				this.call_conv |= CallingConventions.HasThis;
			}
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
			this.table_idx = this.get_next_table_index(this, 6, 1);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000BDC1C File Offset: 0x000BBE1C
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt, string dllName, string entryName, CallingConvention nativeCConv, CharSet nativeCharset)
			: this(tb, name, attributes, callingConvention, returnType, returnModReq, returnModOpt, parameterTypes, paramModReq, paramModOpt)
		{
			this.pi_dll = dllName;
			this.pi_entry = entryName;
			this.native_cc = nativeCConv;
			this.charset = nativeCharset;
		}

		/// <summary>Not supported for this type.</summary>
		/// <returns>Not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x000339FF File Offset: 0x00031BFF
		public override bool ContainsGenericParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x000BDC60 File Offset: 0x000BBE60
		internal TypeBuilder TypeBuilder
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves the internal handle for the method. Use this handle to access the underlying metadata handle.</summary>
		/// <returns>Read-only. The internal handle for the method. Use this handle to access the underlying metadata handle.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="P:System.Reflection.MethodBase.MethodHandle" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000BDC68 File Offset: 0x000BBE68
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.NotSupported();
			}
		}

		/// <summary>Gets the return type of the method represented by this <see cref="T:System.Reflection.Emit.MethodBuilder" />.</summary>
		/// <returns>The return type of the method.</returns>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x000BDC70 File Offset: 0x000BBE70
		public override Type ReturnType
		{
			get
			{
				return this.rtype;
			}
		}

		/// <summary>Retrieves the class that was used in reflection to obtain this object.</summary>
		/// <returns>Read-only. The type used to obtain this method.</returns>
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x000BDC60 File Offset: 0x000BBE60
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns the type that declares this method.</summary>
		/// <returns>Read-only. The type that declares this method.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000BDC60 File Offset: 0x000BBE60
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves the name of this method.</summary>
		/// <returns>Read-only. Retrieves a string containing the simple name of this method.</returns>
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x000BDC78 File Offset: 0x000BBE78
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Retrieves the attributes for this method.</summary>
		/// <returns>Read-only. Retrieves the MethodAttributes for this method.</returns>
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000BDC80 File Offset: 0x000BBE80
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Returns the calling convention of the method.</summary>
		/// <returns>Read-only. The calling convention of the method.</returns>
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000BDC88 File Offset: 0x000BBE88
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		/// <summary>Returns the MethodToken that represents the token for this method.</summary>
		/// <returns>Returns the MethodToken of this method.</returns>
		// Token: 0x060032A2 RID: 12962 RVA: 0x000BDC90 File Offset: 0x000BBE90
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		/// <summary>Return the base implementation for a method.</summary>
		/// <returns>The base implementation of this method.</returns>
		// Token: 0x060032A3 RID: 12963 RVA: 0x00002645 File Offset: 0x00000845
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		/// <summary>Returns the implementation flags for the method.</summary>
		/// <returns>Returns the implementation flags for the method.</returns>
		// Token: 0x060032A4 RID: 12964 RVA: 0x000BDCA3 File Offset: 0x000BBEA3
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		/// <summary>Returns the parameters of this method.</summary>
		/// <returns>An array of ParameterInfo objects that represent the parameters of the method.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call GetParameters on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x060032A5 RID: 12965 RVA: 0x000BDCAB File Offset: 0x000BBEAB
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created)
			{
				throw this.NotSupported();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000BDCC8 File Offset: 0x000BBEC8
		internal override ParameterInfo[] GetParametersInternal()
		{
			if (this.parameters == null)
			{
				return null;
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

		// Token: 0x060032A7 RID: 12967 RVA: 0x000BDD26 File Offset: 0x000BBF26
		internal override int GetParametersCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000BDD3A File Offset: 0x000BBF3A
		internal MethodBase RuntimeResolve()
		{
			return this.type.RuntimeResolve().GetMethod(this);
		}

		/// <summary>Returns a reference to the module that contains this method.</summary>
		/// <returns>Returns a reference to the module that contains this method.</returns>
		// Token: 0x060032A9 RID: 12969 RVA: 0x000BDD4D File Offset: 0x000BBF4D
		public Module GetModule()
		{
			return this.type.Module;
		}

		/// <summary>Dynamically invokes the method reflected by this instance on the given object, passing along the specified parameters, and under the constraints of the given binder.</summary>
		/// <returns>Returns an object containing the return value of the invoked method.</returns>
		/// <param name="obj">The object on which to invoke the specified method. If the method is static, this parameter is ignored. </param>
		/// <param name="invokeAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If binder is null, the default binder is used. For more details, see <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the method to be invoked. If there are no parameters this should be null. </param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. (Note that this is necessary to, for example, convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, since 1000 is represented differently by different cultures.) </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x060032AA RID: 12970 RVA: 0x000BDC68 File Offset: 0x000BBE68
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.NotSupported();
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <returns>true if the specified custom attribute type is defined; otherwise, false.</returns>
		/// <param name="attributeType">The custom attribute type. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x060032AB RID: 12971 RVA: 0x000BDC68 File Offset: 0x000BBE68
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.NotSupported();
		}

		/// <summary>Returns all the custom attributes defined for this method.</summary>
		/// <returns>Returns an array of objects representing all the custom attributes of this method.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x060032AC RID: 12972 RVA: 0x000BDD5A File Offset: 0x000BBF5A
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.NotSupported();
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <returns>Returns an array of objects representing the attributes of this method that are of type <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The custom attribute type. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x060032AD RID: 12973 RVA: 0x000BDD77 File Offset: 0x000BBF77
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.NotSupported();
		}

		/// <summary>Returns an ILGenerator for this method with a default Microsoft intermediate language (MSIL) stream size of 64 bytes.</summary>
		/// <returns>Returns an ILGenerator object for this method.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method should not have a body because of its <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags, for example because it has the <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" /> flag. -or-The method is a generic method, but not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x060032AE RID: 12974 RVA: 0x000BDD95 File Offset: 0x000BBF95
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		/// <summary>Returns an ILGenerator for this method with the specified Microsoft intermediate language (MSIL) stream size.</summary>
		/// <returns>Returns an ILGenerator object for this method.</returns>
		/// <param name="size">The size of the MSIL stream, in bytes. </param>
		/// <exception cref="T:System.InvalidOperationException">The method should not have a body because of its <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags, for example because it has the <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" /> flag. -or-The method is a generic method, but not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.   </exception>
		// Token: 0x060032AF RID: 12975 RVA: 0x000BDDA0 File Offset: 0x000BBFA0
		public ILGenerator GetILGenerator(int size)
		{
			if ((this.iattrs & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.iattrs & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL)
			{
				throw new InvalidOperationException("Method body should not exist.");
			}
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), size);
			return this.ilgen;
		}

		/// <summary>Sets the parameter attributes and the name of a parameter of this method, or of the return value of this method. Returns a ParameterBuilder that can be used to apply custom attributes.</summary>
		/// <returns>Returns a ParameterBuilder object that represents a parameter of this method or the return value of this method.</returns>
		/// <param name="position">The position of the parameter in the parameter list. Parameters are indexed beginning with the number 1 for the first parameter; the number 0 represents the return value of the method. </param>
		/// <param name="attributes">The parameter attributes of the parameter. </param>
		/// <param name="strParamName">The name of the parameter. The name can be the null string. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The method has no parameters.-or- <paramref name="position" /> is less than zero.-or- <paramref name="position" /> is greater than the number of the method's parameters. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x060032B0 RID: 12976 RVA: 0x000BDE10 File Offset: 0x000BC010
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
		{
			this.RejectIfCreated();
			if (position < 0 || this.parameters == null || position > this.parameters.Length)
			{
				throw new ArgumentOutOfRangeException("position");
			}
			ParameterBuilder parameterBuilder = new ParameterBuilder(this, position, attributes, strParamName);
			if (this.pinfo == null)
			{
				this.pinfo = new ParameterBuilder[this.parameters.Length + 1];
			}
			this.pinfo[position] = parameterBuilder;
			return parameterBuilder;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000BDE78 File Offset: 0x000BC078
		internal void check_override()
		{
			if (this.override_methods != null)
			{
				foreach (MethodInfo methodInfo in this.override_methods)
				{
					if (methodInfo.IsVirtual && !base.IsVirtual)
					{
						throw new TypeLoadException(string.Format("Method '{0}' override '{1}' but it is not virtual", this.name, methodInfo));
					}
				}
			}
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000BDED0 File Offset: 0x000BC0D0
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || this.ilgen.ILOffset == 0) && (this.code == null || this.code.Length == 0))
			{
				throw new InvalidOperationException(string.Format("Method '{0}.{1}' does not have a method body.", this.DeclaringType.FullName, this.Name));
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup(this);
			}
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000BDF54 File Offset: 0x000BC154
		internal void ResolveUserTypes()
		{
			this.rtype = TypeBuilder.ResolveUserType(this.rtype);
			TypeBuilder.ResolveUserTypes(this.parameters);
			TypeBuilder.ResolveUserTypes(this.returnModReq);
			TypeBuilder.ResolveUserTypes(this.returnModOpt);
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

		// Token: 0x060032B4 RID: 12980 RVA: 0x000BDFDD File Offset: 0x000BC1DD
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.ilgen != null)
			{
				this.ilgen.FixupTokens(token_map, member_map);
			}
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000BDFF4 File Offset: 0x000BC1F4
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

		/// <summary>Returns this MethodBuilder instance as a string.</summary>
		/// <returns>Returns a string containing the name, attributes, method signature, exceptions, and local signature of this method followed by the current Microsoft intermediate language (MSIL) stream.</returns>
		// Token: 0x060032B6 RID: 12982 RVA: 0x000BE060 File Offset: 0x000BC260
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"MethodBuilder [",
				this.type.Name,
				"::",
				this.name,
				"]"
			});
		}

		/// <summary>Determines whether the given object is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of MethodBuilder and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this MethodBuilder instance. </param>
		// Token: 0x060032B7 RID: 12983 RVA: 0x000BE09C File Offset: 0x000BC29C
		[MonoTODO]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Gets the hash code for this method.</summary>
		/// <returns>The hash code for this method.</returns>
		// Token: 0x060032B8 RID: 12984 RVA: 0x000BE0A5 File Offset: 0x000BC2A5
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000BE0B2 File Offset: 0x000BC2B2
		internal override int get_next_table_index(object obj, int table, int count)
		{
			return this.type.get_next_table_index(obj, table, count);
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000BE0C2 File Offset: 0x000BC2C2
		private void RejectIfCreated()
		{
			if (this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000B9312 File Offset: 0x000B7512
		private Exception NotSupported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		/// <summary>Returns a generic method constructed from the current generic method definition using the specified generic type arguments.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> representing the generic method constructed from the current generic method definition using the specified generic type arguments.</returns>
		/// <param name="typeArguments">An array of <see cref="T:System.Type" /> objects that represent the type arguments for the generic method.</param>
		// Token: 0x060032BC RID: 12988 RVA: 0x000BE0DC File Offset: 0x000BC2DC
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException("Method is not a generic method definition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (this.generic_params.Length != typeArguments.Length)
			{
				throw new ArgumentException("Incorrect length", "typeArguments");
			}
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			return new MethodOnTypeBuilderInst(this, typeArguments);
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Reflection.Emit.MethodBuilder" /> object represents the definition of a generic method.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.Emit.MethodBuilder" /> object represents the definition of a generic method; otherwise, false.</returns>
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060032BD RID: 12989 RVA: 0x000BE154 File Offset: 0x000BC354
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Gets a value indicating whether the method is a generic method.</summary>
		/// <returns>true if the method is generic; otherwise, false.</returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x000BE154 File Offset: 0x000BC354
		public override bool IsGenericMethod
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Returns this method.</summary>
		/// <returns>The current instance of <see cref="T:System.Reflection.Emit.MethodBuilder" />. </returns>
		/// <exception cref="T:System.InvalidOperationException">The current method is not generic. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property returns false.</exception>
		// Token: 0x060032BF RID: 12991 RVA: 0x000BE15F File Offset: 0x000BC35F
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
			return this;
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that represent the type parameters of the method, if it is generic.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects representing the type parameters, if the method is generic, or null if the method is not generic. </returns>
		// Token: 0x060032C0 RID: 12992 RVA: 0x000BE170 File Offset: 0x000BC370
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return null;
			}
			Type[] array = new Type[this.generic_params.Length];
			for (int i = 0; i < this.generic_params.Length; i++)
			{
				array[i] = this.generic_params[i];
			}
			return array;
		}

		/// <summary>Sets the return type of the method.</summary>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that represents the return type of the method.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x060032C1 RID: 12993 RVA: 0x000BE1B4 File Offset: 0x000BC3B4
		public void SetReturnType(Type returnType)
		{
			this.rtype = returnType;
		}

		/// <summary>Sets the number and types of parameters for a method. </summary>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects representing the parameter types.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x060032C2 RID: 12994 RVA: 0x000BE1C0 File Offset: 0x000BC3C0
		public void SetParameters(params Type[] parameterTypes)
		{
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
		}

		/// <summary>Gets the module in which the current method is being defined.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> in which the member represented by the current <see cref="T:System.Reflection.MemberInfo" /> is being defined.</returns>
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000BE216 File Offset: 0x000BC416
		public override Module Module
		{
			get
			{
				return this.GetModule();
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.ParameterInfo" /> object that contains information about the return type of the method, such as whether the return type has custom modifiers. </summary>
		/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> object that contains information about the return type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The declaring type has not been created.</exception>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000BE21E File Offset: 0x000BC41E
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return base.ReturnParameter;
			}
		}

		// Token: 0x040019A8 RID: 6568
		private RuntimeMethodHandle mhandle;

		// Token: 0x040019A9 RID: 6569
		private Type rtype;

		// Token: 0x040019AA RID: 6570
		internal Type[] parameters;

		// Token: 0x040019AB RID: 6571
		private MethodAttributes attrs;

		// Token: 0x040019AC RID: 6572
		private MethodImplAttributes iattrs;

		// Token: 0x040019AD RID: 6573
		private string name;

		// Token: 0x040019AE RID: 6574
		private int table_idx;

		// Token: 0x040019AF RID: 6575
		private byte[] code;

		// Token: 0x040019B0 RID: 6576
		private ILGenerator ilgen;

		// Token: 0x040019B1 RID: 6577
		private TypeBuilder type;

		// Token: 0x040019B2 RID: 6578
		internal ParameterBuilder[] pinfo;

		// Token: 0x040019B3 RID: 6579
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040019B4 RID: 6580
		private MethodInfo[] override_methods;

		// Token: 0x040019B5 RID: 6581
		private string pi_dll;

		// Token: 0x040019B6 RID: 6582
		private string pi_entry;

		// Token: 0x040019B7 RID: 6583
		private CharSet charset;

		// Token: 0x040019B8 RID: 6584
		private uint extra_flags;

		// Token: 0x040019B9 RID: 6585
		private CallingConvention native_cc;

		// Token: 0x040019BA RID: 6586
		private CallingConventions call_conv;

		// Token: 0x040019BB RID: 6587
		private bool init_locals = true;

		// Token: 0x040019BC RID: 6588
		private IntPtr generic_container;

		// Token: 0x040019BD RID: 6589
		internal GenericTypeParameterBuilder[] generic_params;

		// Token: 0x040019BE RID: 6590
		private Type[] returnModReq;

		// Token: 0x040019BF RID: 6591
		private Type[] returnModOpt;

		// Token: 0x040019C0 RID: 6592
		private Type[][] paramModReq;

		// Token: 0x040019C1 RID: 6593
		private Type[][] paramModOpt;

		// Token: 0x040019C2 RID: 6594
		private RefEmitPermissionSet[] permissions;
	}
}
