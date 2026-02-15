using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a dynamic method that can be compiled, executed, and discarded. Discarded methods are available for garbage collection.</summary>
	// Token: 0x0200065F RID: 1631
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DynamicMethod : MethodInfo
	{
		/// <summary>Creates a dynamic method, specifying the method name, attributes, calling convention, return type, parameter types, the type with which the dynamic method is logically associated, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be null.</param>
		/// <param name="attributes">A bitwise combination of <see cref="T:System.Reflection.MethodAttributes" /> values that specifies the attributes of the dynamic method. The only combination allowed is <see cref="F:System.Reflection.MethodAttributes.Public" /> and <see cref="F:System.Reflection.MethodAttributes.Static" />.</param>
		/// <param name="callingConvention">The calling convention for the dynamic method. Must be <see cref="F:System.Reflection.CallingConventions.Standard" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or null if the method has no return type. </param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or null if the method has no parameters. </param>
		/// <param name="owner">A <see cref="T:System.Type" /> with which the dynamic method is logically associated. The dynamic method has access to all members of the type.</param>
		/// <param name="skipVisibility">true to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is null or <see cref="T:System.Void" />. -or-<paramref name="owner" /> is an interface, an array, an open generic type, or a type parameter of a generic type or method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or-<paramref name="owner" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="attributes" /> is a combination of flags other than <see cref="F:System.Reflection.MethodAttributes.Public" /> and <see cref="F:System.Reflection.MethodAttributes.Static" />.-or-<paramref name="callingConvention" /> is not <see cref="F:System.Reflection.CallingConventions.Standard" />.-or-<paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		// Token: 0x06003186 RID: 12678 RVA: 0x000BA8F4 File Offset: 0x000B8AF4
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
			: this(name, attributes, callingConvention, returnType, parameterTypes, owner, owner.Module, skipVisibility, false)
		{
		}

		/// <summary>Initializes an anonymously hosted dynamic method, specifying the method name, return type, parameter types, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method. </summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be null. </param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or null if the method has no return type. </param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or null if the method has no parameters. </param>
		/// <param name="restrictedSkipVisibility">true to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method, with this restriction: the trust level of the assemblies that contain those types and members must be equal to or less than the trust level of the call stack that emits the dynamic method; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is null or <see cref="T:System.Void" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		// Token: 0x06003187 RID: 12679 RVA: 0x000BA91C File Offset: 0x000B8B1C
		[MonoTODO("Visibility is not restricted")]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, bool restrictedSkipVisibility)
			: this(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, null, restrictedSkipVisibility, true)
		{
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000BA93C File Offset: 0x000B8B3C
		private DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, Module m, bool skipVisibility, bool anonHosted)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			if (m == null && !anonHosted)
			{
				throw new ArgumentNullException("m");
			}
			if (returnType.IsByRef)
			{
				throw new ArgumentException("Return type can't be a byref type", "returnType");
			}
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Parameter " + i.ToString() + " is null", "parameterTypes");
					}
				}
			}
			if (owner != null && (owner.IsArray || owner.IsInterface))
			{
				throw new ArgumentException("Owner can't be an array or an interface.");
			}
			if (m == null)
			{
				m = DynamicMethod.AnonHostModuleHolder.AnonHostModule;
			}
			this.name = name;
			this.attributes = attributes | MethodAttributes.Static;
			this.callingConvention = callingConvention;
			this.returnType = returnType;
			this.parameters = parameterTypes;
			this.owner = owner;
			this.module = m;
			this.skipVisibility = skipVisibility;
		}

		// Token: 0x06003189 RID: 12681
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void create_dynamic_method(DynamicMethod m);

		// Token: 0x0600318A RID: 12682 RVA: 0x000BAA64 File Offset: 0x000B8C64
		private void CreateDynMethod()
		{
			lock (this)
			{
				if (this.mhandle.Value == IntPtr.Zero)
				{
					if (this.ilgen == null || this.ilgen.ILOffset == 0)
					{
						throw new InvalidOperationException("Method '" + this.name + "' does not have a method body.");
					}
					this.ilgen.label_fixup(this);
					try
					{
						this.creating = true;
						if (this.refs != null)
						{
							for (int i = 0; i < this.refs.Length; i++)
							{
								if (this.refs[i] is DynamicMethod)
								{
									DynamicMethod dynamicMethod = (DynamicMethod)this.refs[i];
									if (!dynamicMethod.creating)
									{
										dynamicMethod.CreateDynMethod();
									}
								}
							}
						}
					}
					finally
					{
						this.creating = false;
					}
					DynamicMethod.create_dynamic_method(this);
					this.ilgen = null;
				}
			}
		}

		/// <summary>Completes the dynamic method and creates a delegate that can be used to execute it.</summary>
		/// <returns>A delegate of the specified type, which can be used to execute the dynamic method.</returns>
		/// <param name="delegateType">A delegate type whose signature matches that of the dynamic method. </param>
		/// <exception cref="T:System.InvalidOperationException">The dynamic method has no method body.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="delegateType" /> has the wrong number of parameters or the wrong parameter types.</exception>
		// Token: 0x0600318B RID: 12683 RVA: 0x000BAB60 File Offset: 0x000B8D60
		[ComVisible(true)]
		public sealed override Delegate CreateDelegate(Type delegateType)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			if (this.deleg != null)
			{
				return this.deleg;
			}
			this.CreateDynMethod();
			this.deleg = Delegate.CreateDelegate(delegateType, null, this);
			return this.deleg;
		}

		/// <summary>Completes the dynamic method and creates a delegate that can be used to execute it, specifying the delegate type and an object the delegate is bound to.</summary>
		/// <returns>A delegate of the specified type, which can be used to execute the dynamic method with the specified target object.</returns>
		/// <param name="delegateType">A delegate type whose signature matches that of the dynamic method, minus the first parameter.</param>
		/// <param name="target">An object the delegate is bound to. Must be of the same type as the first parameter of the dynamic method. </param>
		/// <exception cref="T:System.InvalidOperationException">The dynamic method has no method body.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not the same type as the first parameter of the dynamic method, and is not assignable to that type.-or-<paramref name="delegateType" /> has the wrong number of parameters or the wrong parameter types.</exception>
		// Token: 0x0600318C RID: 12684 RVA: 0x000BAB9F File Offset: 0x000B8D9F
		[ComVisible(true)]
		public sealed override Delegate CreateDelegate(Type delegateType, object target)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			this.CreateDynMethod();
			return Delegate.CreateDelegate(delegateType, target, this);
		}

		/// <summary>Returns the base implementation for the method.</summary>
		/// <returns>The base implementation of the method.</returns>
		// Token: 0x0600318D RID: 12685 RVA: 0x00002645 File Offset: 0x00000845
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		/// <summary>Returns all the custom attributes defined for the method.</summary>
		/// <returns>An array of objects representing all the custom attributes of the method.</returns>
		/// <param name="inherit">true to search the method's inheritance chain to find the custom attributes; false to check only the current method. </param>
		// Token: 0x0600318E RID: 12686 RVA: 0x000BABC3 File Offset: 0x000B8DC3
		public override object[] GetCustomAttributes(bool inherit)
		{
			return new object[]
			{
				new MethodImplAttribute(this.GetMethodImplementationFlags())
			};
		}

		/// <summary>Returns the custom attributes of the specified type that have been applied to the method.</summary>
		/// <returns>An array of objects representing the attributes of the method that are of type <paramref name="attributeType" /> or derive from type <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">A <see cref="T:System.Type" /> representing the type of custom attribute to return. </param>
		/// <param name="inherit">true to search the method's inheritance chain to find the custom attributes; false to check only the current method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		// Token: 0x0600318F RID: 12687 RVA: 0x000BABDC File Offset: 0x000B8DDC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (attributeType.IsAssignableFrom(typeof(MethodImplAttribute)))
			{
				return new object[]
				{
					new MethodImplAttribute(this.GetMethodImplementationFlags())
				};
			}
			return EmptyArray<object>.Value;
		}

		/// <summary>Returns a Microsoft intermediate language (MSIL) generator for the method with a default MSIL stream size of 64 bytes.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> object for the method.</returns>
		// Token: 0x06003190 RID: 12688 RVA: 0x000BAC29 File Offset: 0x000B8E29
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		/// <summary>Returns a Microsoft intermediate language (MSIL) generator for the method with the specified MSIL stream size.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> object for the method, with the specified MSIL stream size.</returns>
		/// <param name="streamSize">The size of the MSIL stream, in bytes. </param>
		// Token: 0x06003191 RID: 12689 RVA: 0x000BAC34 File Offset: 0x000B8E34
		public ILGenerator GetILGenerator(int streamSize)
		{
			if ((this.GetMethodImplementationFlags() & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.GetMethodImplementationFlags() & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL)
			{
				throw new InvalidOperationException("Method body should not exist.");
			}
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.Module, new DynamicMethodTokenGenerator(this), streamSize);
			return this.ilgen;
		}

		/// <summary>Returns the implementation flags for the method.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.MethodImplAttributes" /> values representing the implementation flags for the method.</returns>
		// Token: 0x06003192 RID: 12690 RVA: 0x00034325 File Offset: 0x00032525
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MethodImplAttributes.NoInlining;
		}

		/// <summary>Returns the parameters of the dynamic method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ParameterInfo" /> objects that represent the parameters of the dynamic method.</returns>
		// Token: 0x06003193 RID: 12691 RVA: 0x000B3EC6 File Offset: 0x000B20C6
		public override ParameterInfo[] GetParameters()
		{
			return this.GetParametersInternal();
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000BAC90 File Offset: 0x000B8E90
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

		// Token: 0x06003195 RID: 12693 RVA: 0x000BACF2 File Offset: 0x000B8EF2
		internal override int GetParametersCount()
		{
			if (this.parameters != null)
			{
				return this.parameters.Length;
			}
			return 0;
		}

		/// <summary>Invokes the dynamic method using the specified parameters, under the constraints of the specified binder, with the specified culture information.</summary>
		/// <returns>A <see cref="T:System.Object" /> containing the return value of the invoked method.</returns>
		/// <param name="obj">This parameter is ignored for dynamic methods, because they are static. Specify null. </param>
		/// <param name="invokeAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. For more details, see <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the method to be invoked. If there are no parameters this parameter should be null. </param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. For example, this information is needed to correctly convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, because 1000 is represented differently by different cultures. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="F:System.Reflection.CallingConventions.VarArgs" /> calling convention is not supported.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of elements in <paramref name="parameters" /> does not match the number of parameters in the dynamic method.</exception>
		/// <exception cref="T:System.ArgumentException">The type of one or more elements of <paramref name="parameters" /> does not match the type of the corresponding parameter of the dynamic method.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The dynamic method is associated with a module, is not anonymously hosted, and was constructed with <paramref name="skipVisibility" /> set to false, but the dynamic method accesses members that are not public or internal (Friend in Visual Basic).-or-The dynamic method is anonymously hosted and was constructed with <paramref name="skipVisibility" /> set to false, but it accesses members that are not public.-or-The dynamic method contains unverifiable code. See the "Verification" section in Remarks for <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003196 RID: 12694 RVA: 0x000BAD08 File Offset: 0x000B8F08
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object obj2;
			try
			{
				this.CreateDynMethod();
				if (this.method == null)
				{
					this.method = new RuntimeMethodInfo(this.mhandle);
				}
				obj2 = this.method.Invoke(obj, invokeAttr, binder, parameters, culture);
			}
			catch (MethodAccessException ex)
			{
				throw new TargetInvocationException("Method cannot be invoked.", ex);
			}
			return obj2;
		}

		/// <summary>Indicates whether the specified custom attribute type is defined.</summary>
		/// <returns>true if the specified custom attribute type is defined; otherwise, false.</returns>
		/// <param name="attributeType">A <see cref="T:System.Type" /> representing the type of custom attribute to search for. </param>
		/// <param name="inherit">true to search the method's inheritance chain to find the custom attributes; false to check only the current method. </param>
		// Token: 0x06003197 RID: 12695 RVA: 0x000BAD70 File Offset: 0x000B8F70
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return attributeType.IsAssignableFrom(typeof(MethodImplAttribute));
		}

		/// <summary>Returns the signature of the method, represented as a string.</summary>
		/// <returns>A string representing the method signature.</returns>
		// Token: 0x06003198 RID: 12696 RVA: 0x000BAD9C File Offset: 0x000B8F9C
		public override string ToString()
		{
			string text = string.Empty;
			ParameterInfo[] parametersInternal = this.GetParametersInternal();
			for (int i = 0; i < parametersInternal.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += parametersInternal[i].ParameterType.Name;
			}
			return string.Concat(new string[]
			{
				this.ReturnType.Name,
				" ",
				this.Name,
				"(",
				text,
				")"
			});
		}

		/// <summary>Gets the attributes specified when the dynamic method was created.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Reflection.MethodAttributes" /> values representing the attributes for the method.</returns>
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x000BAE26 File Offset: 0x000B9026
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets the calling convention specified when the dynamic method was created.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.CallingConventions" /> values that indicates the calling convention of the method.</returns>
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000BAE2E File Offset: 0x000B902E
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.callingConvention;
			}
		}

		/// <summary>Gets the type that declares the method, which is always null for dynamic methods.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000082D2 File Offset: 0x000064D2
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Not supported for dynamic methods.</summary>
		/// <returns>Not supported for dynamic methods.</returns>
		/// <exception cref="T:System.InvalidOperationException">Not allowed for dynamic methods.</exception>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000BAE36 File Offset: 0x000B9036
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.mhandle;
			}
		}

		/// <summary>Gets the module with which the dynamic method is logically associated.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> with which the current dynamic method is associated.</returns>
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000BAE3E File Offset: 0x000B903E
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		/// <summary>Gets the name of the dynamic method.</summary>
		/// <returns>The simple name of the method.</returns>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000BAE46 File Offset: 0x000B9046
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the class that was used in reflection to obtain the method.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000082D2 File Offset: 0x000064D2
		public override Type ReflectedType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the return parameter of the dynamic method.</summary>
		/// <returns>Always null. </returns>
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO("Not implemented")]
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the type of return value for the dynamic method.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the return value of the current method; <see cref="T:System.Void" /> if the method has no return type.</returns>
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000BAE4E File Offset: 0x000B904E
		public override Type ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000BAE58 File Offset: 0x000B9058
		internal int AddRef(object reference)
		{
			if (this.refs == null)
			{
				this.refs = new object[4];
			}
			if (this.nrefs >= this.refs.Length - 1)
			{
				object[] array = new object[this.refs.Length * 2];
				Array.Copy(this.refs, array, this.refs.Length);
				this.refs = array;
			}
			this.refs[this.nrefs] = reference;
			this.refs[this.nrefs + 1] = null;
			this.nrefs += 2;
			return this.nrefs - 1;
		}

		// Token: 0x04001938 RID: 6456
		private RuntimeMethodHandle mhandle;

		// Token: 0x04001939 RID: 6457
		private string name;

		// Token: 0x0400193A RID: 6458
		private Type returnType;

		// Token: 0x0400193B RID: 6459
		private Type[] parameters;

		// Token: 0x0400193C RID: 6460
		private MethodAttributes attributes;

		// Token: 0x0400193D RID: 6461
		private CallingConventions callingConvention;

		// Token: 0x0400193E RID: 6462
		private Module module;

		// Token: 0x0400193F RID: 6463
		private bool skipVisibility;

		// Token: 0x04001940 RID: 6464
		private bool init_locals = true;

		// Token: 0x04001941 RID: 6465
		private ILGenerator ilgen;

		// Token: 0x04001942 RID: 6466
		private int nrefs;

		// Token: 0x04001943 RID: 6467
		private object[] refs;

		// Token: 0x04001944 RID: 6468
		private IntPtr referenced_by;

		// Token: 0x04001945 RID: 6469
		private Type owner;

		// Token: 0x04001946 RID: 6470
		private Delegate deleg;

		// Token: 0x04001947 RID: 6471
		private RuntimeMethodInfo method;

		// Token: 0x04001948 RID: 6472
		private ParameterBuilder[] pinfo;

		// Token: 0x04001949 RID: 6473
		internal bool creating;

		// Token: 0x0400194A RID: 6474
		private DynamicILInfo il_info;

		// Token: 0x02000660 RID: 1632
		private static class AnonHostModuleHolder
		{
			// Token: 0x060031A3 RID: 12707 RVA: 0x000BAEEC File Offset: 0x000B90EC
			static AnonHostModuleHolder()
			{
				AssemblyName assemblyName = new AssemblyName();
				assemblyName.Name = "Anonymously Hosted DynamicMethods Assembly";
				DynamicMethod.AnonHostModuleHolder.anon_host_module = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run).GetManifestModule();
			}

			// Token: 0x17000714 RID: 1812
			// (get) Token: 0x060031A4 RID: 12708 RVA: 0x000BAF20 File Offset: 0x000B9120
			public static Module AnonHostModule
			{
				get
				{
					return DynamicMethod.AnonHostModuleHolder.anon_host_module;
				}
			}

			// Token: 0x0400194B RID: 6475
			public static readonly Module anon_host_module;
		}
	}
}
