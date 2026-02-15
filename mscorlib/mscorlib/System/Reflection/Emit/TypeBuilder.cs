using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines and creates new instances of classes during run time.</summary>
	// Token: 0x02000682 RID: 1666
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_TypeBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TypeBuilder : TypeInfo, _TypeBuilder
	{
		// Token: 0x06003397 RID: 13207 RVA: 0x000C22FD File Offset: 0x000C04FD
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.attrs;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000C2305 File Offset: 0x000C0505
		private TypeBuilder()
		{
			if (RuntimeType.MakeTypeBuilderInstantiation == null)
			{
				RuntimeType.MakeTypeBuilderInstantiation = new Func<Type, Type[], Type>(TypeBuilderInstantiation.MakeGenericType);
			}
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000C2328 File Offset: 0x000C0528
		internal TypeBuilder(ModuleBuilder mb, TypeAttributes attr, int table_idx)
			: this()
		{
			this.parent = null;
			this.attrs = attr;
			this.class_size = 0;
			this.table_idx = table_idx;
			this.tname = ((table_idx == 1) ? "<Module>" : ("type_" + table_idx.ToString()));
			this.nspace = string.Empty;
			this.fullname = TypeIdentifiers.WithoutEscape(this.tname);
			this.pmodule = mb;
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000C239C File Offset: 0x000C059C
		internal TypeBuilder(ModuleBuilder mb, string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packing_size, int type_size, Type nesting_type)
			: this()
		{
			this.parent = TypeBuilder.ResolveUserType(parent);
			this.attrs = attr;
			this.class_size = type_size;
			this.packing_size = packing_size;
			this.nesting_type = nesting_type;
			this.check_name("fullname", name);
			if (parent == null && (attr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
			{
				throw new InvalidOperationException("Interface must be declared abstract.");
			}
			int num = name.LastIndexOf('.');
			if (num != -1)
			{
				this.tname = name.Substring(num + 1);
				this.nspace = name.Substring(0, num);
			}
			else
			{
				this.tname = name;
				this.nspace = string.Empty;
			}
			if (interfaces != null)
			{
				this.interfaces = new Type[interfaces.Length];
				Array.Copy(interfaces, this.interfaces, interfaces.Length);
			}
			this.pmodule = mb;
			if ((attr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && parent == null)
			{
				this.parent = typeof(object);
			}
			this.table_idx = mb.get_next_table_index(this, 2, 1);
			this.fullname = this.GetFullName();
		}

		/// <summary>Retrieves the dynamic assembly that contains this type definition.</summary>
		/// <returns>Read-only. Retrieves the dynamic assembly that contains this type definition.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000C24AF File Offset: 0x000C06AF
		public override Assembly Assembly
		{
			get
			{
				return this.pmodule.Assembly;
			}
		}

		/// <summary>Returns the full name of this type qualified by the display name of the assembly.</summary>
		/// <returns>Read-only. The full name of this type qualified by the display name of the assembly.</returns>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000C24BC File Offset: 0x000C06BC
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.fullname.DisplayName + ", " + this.Assembly.FullName;
			}
		}

		/// <summary>Retrieves the base type of this type.</summary>
		/// <returns>Read-only. Retrieves the base type of this type.</returns>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000C24DE File Offset: 0x000C06DE
		public override Type BaseType
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Returns the type that declared this type.</summary>
		/// <returns>Read-only. The type that declared this type.</returns>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x000C24E6 File Offset: 0x000C06E6
		public override Type DeclaringType
		{
			get
			{
				return this.nesting_type;
			}
		}

		/// <summary>Determines whether this type is derived from a specified type.</summary>
		/// <returns>Read-only. Returns true if this type is the same as the type <paramref name="c" />, or is a subtype of type <paramref name="c" />; otherwise, false.</returns>
		/// <param name="c">A <see cref="T:System.Type" /> that is to be checked. </param>
		// Token: 0x0600339F RID: 13215 RVA: 0x000C24F0 File Offset: 0x000C06F0
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (c == this)
			{
				return false;
			}
			Type baseType = this.parent;
			while (baseType != null)
			{
				if (c == baseType)
				{
					return true;
				}
				baseType = baseType.BaseType;
			}
			return false;
		}

		/// <summary>Returns the underlying system type for this TypeBuilder.</summary>
		/// <returns>Read-only. Returns the underlying system type.</returns>
		/// <exception cref="T:System.InvalidOperationException">This type is an enumeration, but there is no underlying system type. </exception>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000C2538 File Offset: 0x000C0738
		public override Type UnderlyingSystemType
		{
			get
			{
				if (this.is_created)
				{
					return this.created.UnderlyingSystemType;
				}
				if (!this.IsEnum)
				{
					return this;
				}
				if (this.underlying_type != null)
				{
					return this.underlying_type;
				}
				throw new InvalidOperationException("Enumeration type is not defined.");
			}
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000C2578 File Offset: 0x000C0778
		private TypeName GetFullName()
		{
			TypeIdentifier typeIdentifier = TypeIdentifiers.FromInternal(this.tname);
			if (this.nesting_type != null)
			{
				return TypeNames.FromDisplay(this.nesting_type.FullName).NestedName(typeIdentifier);
			}
			if (this.nspace != null && this.nspace.Length > 0)
			{
				return TypeIdentifiers.FromInternal(this.nspace, typeIdentifier);
			}
			return typeIdentifier;
		}

		/// <summary>Retrieves the full path of this type.</summary>
		/// <returns>Read-only. Retrieves the full path of this type.</returns>
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000C25DA File Offset: 0x000C07DA
		public override string FullName
		{
			get
			{
				return this.fullname.DisplayName;
			}
		}

		/// <summary>Retrieves the GUID of this type.</summary>
		/// <returns>Read-only. Retrieves the GUID of this type </returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. </exception>
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000C25E7 File Offset: 0x000C07E7
		public override Guid GUID
		{
			get
			{
				this.check_created();
				return this.created.GUID;
			}
		}

		/// <summary>Retrieves the dynamic module that contains this type definition.</summary>
		/// <returns>Read-only. Retrieves the dynamic module that contains this type definition.</returns>
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x000C25FA File Offset: 0x000C07FA
		public override Module Module
		{
			get
			{
				return this.pmodule;
			}
		}

		/// <summary>Retrieves the name of this type.</summary>
		/// <returns>Read-only. Retrieves the <see cref="T:System.String" /> name of this type.</returns>
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000C2602 File Offset: 0x000C0802
		public override string Name
		{
			get
			{
				return this.tname;
			}
		}

		/// <summary>Retrieves the namespace where this TypeBuilder is defined.</summary>
		/// <returns>Read-only. Retrieves the namespace where this TypeBuilder is defined.</returns>
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x000C260A File Offset: 0x000C080A
		public override string Namespace
		{
			get
			{
				return this.nspace;
			}
		}

		/// <summary>Returns the type that was used to obtain this type.</summary>
		/// <returns>Read-only. The type that was used to obtain this type.</returns>
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x000C24E6 File Offset: 0x000C06E6
		public override Type ReflectedType
		{
			get
			{
				return this.nesting_type;
			}
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000C2614 File Offset: 0x000C0814
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			this.check_created();
			if (!(this.created == typeof(object)))
			{
				return this.created.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
			}
			if (this.ctors == null)
			{
				return null;
			}
			ConstructorBuilder constructorBuilder = null;
			int num = 0;
			foreach (ConstructorBuilder constructorBuilder2 in this.ctors)
			{
				if (callConvention == CallingConventions.Any || constructorBuilder2.CallingConvention == callConvention)
				{
					constructorBuilder = constructorBuilder2;
					num++;
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (types != null)
			{
				MethodBase[] array2 = new MethodBase[num];
				if (num == 1)
				{
					array2[0] = constructorBuilder;
				}
				else
				{
					num = 0;
					foreach (ConstructorBuilder constructorInfo in this.ctors)
					{
						if (callConvention == CallingConventions.Any || constructorInfo.CallingConvention == callConvention)
						{
							array2[num++] = constructorInfo;
						}
					}
				}
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				return (ConstructorInfo)binder.SelectMethod(bindingAttr, array2, types, modifiers);
			}
			if (num > 1)
			{
				throw new AmbiguousMatchException();
			}
			return constructorBuilder;
		}

		/// <summary>Determines whether a custom attribute is applied to the current type.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" />, or an attribute derived from <paramref name="attributeType" />, is defined on this type; otherwise, false.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Type" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not defined.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		// Token: 0x060033A9 RID: 13225 RVA: 0x000C270F File Offset: 0x000C090F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (!this.is_created)
			{
				throw new NotSupportedException();
			}
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		/// <summary>Returns all the custom attributes defined for this type.</summary>
		/// <returns>Returns an array of objects representing all the custom attributes of this type.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Type" />. </exception>
		// Token: 0x060033AA RID: 13226 RVA: 0x000C2727 File Offset: 0x000C0927
		public override object[] GetCustomAttributes(bool inherit)
		{
			this.check_created();
			return this.created.GetCustomAttributes(inherit);
		}

		/// <summary>Returns all the custom attributes of the current type that are assignable to a specified type.</summary>
		/// <returns>An array of custom attributes defined on the current type.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Type" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The type must be a type provided by the underlying runtime system.</exception>
		// Token: 0x060033AB RID: 13227 RVA: 0x000C273B File Offset: 0x000C093B
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			this.check_created();
			return this.created.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Adds a new constructor to the type, with the given attributes and signature.</summary>
		/// <returns>The defined constructor.</returns>
		/// <param name="attributes">The attributes of the constructor. </param>
		/// <param name="callingConvention">The calling convention of the constructor. </param>
		/// <param name="parameterTypes">The parameter types of the constructor. </param>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x060033AC RID: 13228 RVA: 0x000C2750 File Offset: 0x000C0950
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes)
		{
			return this.DefineConstructor(attributes, callingConvention, parameterTypes, null, null);
		}

		/// <summary>Adds a new constructor to the type, with the given attributes, signature, and custom modifiers.</summary>
		/// <returns>The defined constructor.</returns>
		/// <param name="attributes">The attributes of the constructor. </param>
		/// <param name="callingConvention">The calling convention of the constructor. </param>
		/// <param name="parameterTypes">The parameter types of the constructor. </param>
		/// <param name="requiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="optionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="requiredCustomModifiers" /> or <paramref name="optionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x060033AD RID: 13229 RVA: 0x000C2760 File Offset: 0x000C0960
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			this.check_not_created();
			ConstructorBuilder constructorBuilder = new ConstructorBuilder(this, attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers);
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = new ConstructorBuilder[this.ctors.Length + 1];
				Array.Copy(this.ctors, array, this.ctors.Length);
				array[this.ctors.Length] = constructorBuilder;
				this.ctors = array;
			}
			else
			{
				this.ctors = new ConstructorBuilder[1];
				this.ctors[0] = constructorBuilder;
			}
			return constructorBuilder;
		}

		/// <summary>Defines the default constructor. The constructor defined here will simply call the default constructor of the parent.</summary>
		/// <returns>Returns the constructor.</returns>
		/// <param name="attributes">A MethodAttributes object representing the attributes to be applied to the constructor. </param>
		/// <exception cref="T:System.NotSupportedException">The parent type (base type) does not have a default constructor. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x060033AE RID: 13230 RVA: 0x000C27D8 File Offset: 0x000C09D8
		[ComVisible(true)]
		public ConstructorBuilder DefineDefaultConstructor(MethodAttributes attributes)
		{
			Type type;
			if (this.parent != null)
			{
				type = this.parent;
			}
			else
			{
				type = this.pmodule.assemblyb.corlib_object_type;
			}
			Type type2 = type;
			type = type.InternalResolve();
			if (type == typeof(object) || type == typeof(ValueType))
			{
				type = type2;
			}
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (constructor == null)
			{
				throw new NotSupportedException("Parent does not have a default constructor. The default constructor must be explicitly defined.");
			}
			ConstructorBuilder constructorBuilder = this.DefineConstructor(attributes, CallingConventions.Standard, Type.EmptyTypes);
			ILGenerator ilgenerator = constructorBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			return constructorBuilder;
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000C2894 File Offset: 0x000C0A94
		private void append_method(MethodBuilder mb)
		{
			if (this.methods != null)
			{
				if (this.methods.Length == this.num_methods)
				{
					MethodBuilder[] array = new MethodBuilder[this.methods.Length * 2];
					Array.Copy(this.methods, array, this.num_methods);
					this.methods = array;
				}
			}
			else
			{
				this.methods = new MethodBuilder[1];
			}
			this.methods[this.num_methods] = mb;
			this.num_methods++;
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, and method signature.</summary>
		/// <returns>The defined method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the parameters of the method. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The type of the parent of this method is an interface, and this method is not virtual (Overridable in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false. </exception>
		// Token: 0x060033B0 RID: 13232 RVA: 0x000C290C File Offset: 0x000C0B0C
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, calling convention, and method signature.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The calling convention of the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the parameters of the method. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The type of the parent of this method is an interface, and this method is not virtual (Overridable in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false. </exception>
		// Token: 0x060033B1 RID: 13233 RVA: 0x000C291C File Offset: 0x000C0B1C
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, calling convention, method signature, and custom modifiers.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> object representing the newly added method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The calling convention of the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify null.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify null.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The type of the parent of this method is an interface, and this method is not virtual (Overridable in Visual Basic). -or-The size of <paramref name="parameterTypeRequiredCustomModifiers" /> or <paramref name="parameterTypeOptionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x060033B2 RID: 13234 RVA: 0x000C293C File Offset: 0x000C0B3C
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.check_name("name", name);
			this.check_not_created();
			if (base.IsInterface && ((attributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (attributes & MethodAttributes.Virtual) == MethodAttributes.PrivateScope) && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("Interface method must be abstract and virtual.");
			}
			if (returnType == null)
			{
				returnType = this.pmodule.assemblyb.corlib_void_type;
			}
			MethodBuilder methodBuilder = new MethodBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			this.append_method(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Defines a PInvoke method given its name, the name of the DLL in which the method is defined, the name of the entry point, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the PInvoke flags.</summary>
		/// <returns>The defined PInvoke method.</returns>
		/// <param name="name">The name of the PInvoke method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="dllName">The name of the DLL in which the PInvoke method is defined. </param>
		/// <param name="entryName">The name of the entry point in the DLL. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The method's calling convention. </param>
		/// <param name="returnType">The method's return type. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <param name="nativeCallConv">The native calling convention. </param>
		/// <param name="nativeCharSet">The method's native character set. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static.-or- The parent type is an interface.-or- The method is abstract.-or- The method was previously defined.-or- The length of <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x060033B3 RID: 13235 RVA: 0x000C29C0 File Offset: 0x000C0BC0
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, nativeCallConv, nativeCharSet);
		}

		/// <summary>Defines a PInvoke method given its name, the name of the DLL in which the method is defined, the name of the entry point, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, the PInvoke flags, and custom modifiers for the parameters and return type.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the defined PInvoke method.</returns>
		/// <param name="name">The name of the PInvoke method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="dllName">The name of the DLL in which the PInvoke method is defined. </param>
		/// <param name="entryName">The name of the entry point in the DLL. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The method's calling convention. </param>
		/// <param name="returnType">The method's return type. </param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify null.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify null.</param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="nativeCallConv">The native calling convention. </param>
		/// <param name="nativeCharSet">The method's native character set. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static.-or- The parent type is an interface.-or- The method is abstract.-or- The method was previously defined.-or- The length of <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is zero. -or-The size of <paramref name="parameterTypeRequiredCustomModifiers" /> or <paramref name="parameterTypeOptionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x060033B4 RID: 13236 RVA: 0x000C29E8 File Offset: 0x000C0BE8
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			this.check_name("name", name);
			this.check_name("dllName", dllName);
			this.check_name("entryName", entryName);
			if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("PInvoke methods must be static and native and cannot be abstract.");
			}
			if (base.IsInterface)
			{
				throw new ArgumentException("PInvoke methods cannot exist on interfaces.");
			}
			this.check_not_created();
			MethodBuilder methodBuilder = new MethodBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, dllName, entryName, nativeCallConv, nativeCharSet);
			this.append_method(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Adds a new method to the type, with the specified name and method attributes.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The type of the parent of this method is an interface, and this method is not virtual (Overridable in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false. </exception>
		// Token: 0x060033B5 RID: 13237 RVA: 0x000C2A6D File Offset: 0x000C0C6D
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, and calling convention.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The calling convention of the method. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The type of the parent of this method is an interface and this method is not virtual (Overridable in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false. </exception>
		// Token: 0x060033B6 RID: 13238 RVA: 0x000C2A78 File Offset: 0x000C0C78
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention)
		{
			return this.DefineMethod(name, attributes, callingConvention, null, null);
		}

		/// <summary>Adds a new field to the type, with the given name, attributes, and field type.</summary>
		/// <returns>The defined field.</returns>
		/// <param name="fieldName">The name of the field. <paramref name="fieldName" /> cannot contain embedded nulls. </param>
		/// <param name="type">The type of the field </param>
		/// <param name="attributes">The attributes of the field. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="fieldName" /> is zero.-or- <paramref name="type" /> is System.Void.-or- A total size was specified for the parent class of this field. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fieldName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060033B7 RID: 13239 RVA: 0x000C2A85 File Offset: 0x000C0C85
		public FieldBuilder DefineField(string fieldName, Type type, FieldAttributes attributes)
		{
			return this.DefineField(fieldName, type, null, null, attributes);
		}

		/// <summary>Adds a new field to the type, with the given name, attributes, field type, and custom modifiers.</summary>
		/// <returns>The defined field.</returns>
		/// <param name="fieldName">The name of the field. <paramref name="fieldName" /> cannot contain embedded nulls. </param>
		/// <param name="type">The type of the field </param>
		/// <param name="requiredCustomModifiers">An array of types representing the required custom modifiers for the field, such as <see cref="T:Microsoft.VisualC.IsConstModifier" />.</param>
		/// <param name="optionalCustomModifiers">An array of types representing the optional custom modifiers for the field, such as <see cref="T:Microsoft.VisualC.IsConstModifier" />.</param>
		/// <param name="attributes">The attributes of the field. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="fieldName" /> is zero.-or- <paramref name="type" /> is System.Void.-or- A total size was specified for the parent class of this field. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fieldName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x060033B8 RID: 13240 RVA: 0x000C2A94 File Offset: 0x000C0C94
		public FieldBuilder DefineField(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			this.check_name("fieldName", fieldName);
			if (type == typeof(void))
			{
				throw new ArgumentException("Bad field type in defining field.");
			}
			this.check_not_created();
			FieldBuilder fieldBuilder = new FieldBuilder(this, fieldName, type, attributes, requiredCustomModifiers, optionalCustomModifiers);
			if (this.fields != null)
			{
				if (this.fields.Length == this.num_fields)
				{
					FieldBuilder[] array = new FieldBuilder[this.fields.Length * 2];
					Array.Copy(this.fields, array, this.num_fields);
					this.fields = array;
				}
				this.fields[this.num_fields] = fieldBuilder;
				this.num_fields++;
			}
			else
			{
				this.fields = new FieldBuilder[1];
				this.fields[0] = fieldBuilder;
				this.num_fields++;
			}
			if (this.IsEnum && this.underlying_type == null && (attributes & FieldAttributes.Static) == FieldAttributes.PrivateScope)
			{
				this.underlying_type = type;
			}
			return fieldBuilder;
		}

		/// <summary>Adds a new property to the type, with the given name, calling convention, property signature, and custom modifiers.</summary>
		/// <returns>The defined property.</returns>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the property. </param>
		/// <param name="callingConvention">The calling convention of the property accessors. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no required custom modifiers, specify null.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no optional custom modifiers, specify null.</param>
		/// <param name="parameterTypes">The types of the parameters of the property. </param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or- Any of the elements of the <paramref name="parameterTypes" /> array is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x060033B9 RID: 13241 RVA: 0x000C2B88 File Offset: 0x000C0D88
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.check_name("name", name);
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentNullException("parameterTypes");
					}
				}
			}
			this.check_not_created();
			PropertyBuilder propertyBuilder = new PropertyBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			if (this.properties != null)
			{
				Array.Resize<PropertyBuilder>(ref this.properties, this.properties.Length + 1);
				this.properties[this.properties.Length - 1] = propertyBuilder;
			}
			else
			{
				this.properties = new PropertyBuilder[] { propertyBuilder };
			}
			return propertyBuilder;
		}

		// Token: 0x060033BA RID: 13242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TypeInfo create_runtime_class();

		// Token: 0x060033BB RID: 13243 RVA: 0x000C2C2A File Offset: 0x000C0E2A
		private bool is_nested_in(Type t)
		{
			while (t != null)
			{
				if (t == this)
				{
					return true;
				}
				t = t.DeclaringType;
			}
			return false;
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000C2C4C File Offset: 0x000C0E4C
		private bool has_ctor_method()
		{
			MethodAttributes methodAttributes = MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
			for (int i = 0; i < this.num_methods; i++)
			{
				MethodBuilder methodBuilder = this.methods[i];
				if (methodBuilder.Name == ConstructorInfo.ConstructorName && (methodBuilder.Attributes & methodAttributes) == methodAttributes)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates a <see cref="T:System.Type" /> object for the class. After defining fields and methods on the class, CreateType is called in order to load its Type object.</summary>
		/// <returns>Returns the new <see cref="T:System.Type" /> object for this class.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enclosing type has not been created.-or- This type is non-abstract and contains an abstract method.-or- This type is not an abstract class or an interface and has a method without a method body. </exception>
		/// <exception cref="T:System.NotSupportedException">The type contains invalid Microsoft intermediate language (MSIL) code.-or- The branch target is specified using a 1-byte offset, but the target is at a distance greater than 127 bytes from the branch. </exception>
		/// <exception cref="T:System.TypeLoadException">The type cannot be loaded. For example, it contains a static method that has the calling convention <see cref="F:System.Reflection.CallingConventions.HasThis" />.</exception>
		// Token: 0x060033BD RID: 13245 RVA: 0x000C2C99 File Offset: 0x000C0E99
		public Type CreateType()
		{
			return this.CreateTypeInfo();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.TypeInfo" /> object that represents this type.</summary>
		/// <returns>An object that represents this type.</returns>
		// Token: 0x060033BE RID: 13246 RVA: 0x000C2CA4 File Offset: 0x000C0EA4
		public TypeInfo CreateTypeInfo()
		{
			if (this.createTypeCalled)
			{
				return this.created;
			}
			if (!base.IsInterface && this.parent == null && this != this.pmodule.assemblyb.corlib_object_type && this.FullName != "<Module>")
			{
				this.SetParent(this.pmodule.assemblyb.corlib_object_type);
			}
			if (this.fields != null)
			{
				foreach (FieldBuilder fieldBuilder in this.fields)
				{
					if (!(fieldBuilder == null))
					{
						Type fieldType = fieldBuilder.FieldType;
						if (!fieldBuilder.IsStatic && fieldType is TypeBuilder && fieldType.IsValueType && fieldType != this && this.is_nested_in(fieldType))
						{
							TypeBuilder typeBuilder = (TypeBuilder)fieldType;
							if (!typeBuilder.is_created)
							{
								AppDomain.CurrentDomain.DoTypeBuilderResolve(typeBuilder);
								bool is_created = typeBuilder.is_created;
							}
						}
					}
				}
			}
			if (!base.IsInterface && !base.IsValueType && this.ctors == null && this.tname != "<Module>" && ((this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) | TypeAttributes.Sealed) != (TypeAttributes.Abstract | TypeAttributes.Sealed) && !this.has_ctor_method())
			{
				this.DefineDefaultConstructor(MethodAttributes.Public);
			}
			this.createTypeCalled = true;
			if (this.parent != null && this.parent.IsSealed)
			{
				string[] array2 = new string[5];
				array2[0] = "Could not load type '";
				array2[1] = this.FullName;
				array2[2] = "' from assembly '";
				int num = 3;
				Assembly assembly = this.Assembly;
				array2[num] = ((assembly != null) ? assembly.ToString() : null);
				array2[4] = "' because the parent type is sealed.";
				throw new TypeLoadException(string.Concat(array2));
			}
			if (this.parent == this.pmodule.assemblyb.corlib_enum_type && this.methods != null)
			{
				string[] array3 = new string[5];
				array3[0] = "Could not load type '";
				array3[1] = this.FullName;
				array3[2] = "' from assembly '";
				int num2 = 3;
				Assembly assembly2 = this.Assembly;
				array3[num2] = ((assembly2 != null) ? assembly2.ToString() : null);
				array3[4] = "' because it is an enum with methods.";
				throw new TypeLoadException(string.Concat(array3));
			}
			if (this.interfaces != null)
			{
				foreach (Type type in this.interfaces)
				{
					if (type.IsNestedPrivate && type.Assembly != this.Assembly)
					{
						string[] array5 = new string[7];
						array5[0] = "Could not load type '";
						array5[1] = this.FullName;
						array5[2] = "' from assembly '";
						int num3 = 3;
						Assembly assembly3 = this.Assembly;
						array5[num3] = ((assembly3 != null) ? assembly3.ToString() : null);
						array5[4] = "' because it is implements the inaccessible interface '";
						array5[5] = type.FullName;
						array5[6] = "'.";
						throw new TypeLoadException(string.Concat(array5));
					}
				}
			}
			if (this.methods != null)
			{
				bool flag = !base.IsAbstract;
				for (int j = 0; j < this.num_methods; j++)
				{
					MethodBuilder methodBuilder = this.methods[j];
					if (flag && methodBuilder.IsAbstract)
					{
						string text = "Type is concrete but has abstract method ";
						MethodBuilder methodBuilder2 = methodBuilder;
						throw new InvalidOperationException(text + ((methodBuilder2 != null) ? methodBuilder2.ToString() : null));
					}
					methodBuilder.check_override();
					methodBuilder.fixup();
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array6 = this.ctors;
				for (int i = 0; i < array6.Length; i++)
				{
					array6[i].fixup();
				}
			}
			this.ResolveUserTypes();
			this.created = this.create_runtime_class();
			if (this.created != null)
			{
				return this.created;
			}
			return this;
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000C3020 File Offset: 0x000C1220
		private void ResolveUserTypes()
		{
			this.parent = TypeBuilder.ResolveUserType(this.parent);
			TypeBuilder.ResolveUserTypes(this.interfaces);
			if (this.fields != null)
			{
				foreach (FieldBuilder fieldBuilder in this.fields)
				{
					if (fieldBuilder != null)
					{
						fieldBuilder.ResolveUserTypes();
					}
				}
			}
			if (this.methods != null)
			{
				foreach (MethodBuilder methodBuilder in this.methods)
				{
					if (methodBuilder != null)
					{
						methodBuilder.ResolveUserTypes();
					}
				}
			}
			if (this.ctors != null)
			{
				foreach (ConstructorBuilder constructorBuilder in this.ctors)
				{
					if (constructorBuilder != null)
					{
						constructorBuilder.ResolveUserTypes();
					}
				}
			}
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000C30E4 File Offset: 0x000C12E4
		internal static void ResolveUserTypes(Type[] types)
		{
			if (types != null)
			{
				for (int i = 0; i < types.Length; i++)
				{
					types[i] = TypeBuilder.ResolveUserType(types[i]);
				}
			}
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000C3110 File Offset: 0x000C1310
		internal static Type ResolveUserType(Type t)
		{
			if (!(t != null) || (!(t.GetType().Assembly != typeof(int).Assembly) && !(t is TypeDelegator)))
			{
				return t;
			}
			t = t.UnderlyingSystemType;
			if (t != null && (t.GetType().Assembly != typeof(int).Assembly || t is TypeDelegator))
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			return t;
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000C3198 File Offset: 0x000C1398
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.methods != null)
			{
				for (int i = 0; i < this.num_methods; i++)
				{
					this.methods[i].FixupTokens(token_map, member_map);
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = this.ctors;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].FixupTokens(token_map, member_map);
				}
			}
			if (this.subtypes != null)
			{
				TypeBuilder[] array2 = this.subtypes;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].FixupTokens(token_map, member_map);
				}
			}
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000C321C File Offset: 0x000C141C
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			symbolWriter.OpenNamespace(this.Namespace);
			if (this.methods != null)
			{
				for (int i = 0; i < this.num_methods; i++)
				{
					this.methods[i].GenerateDebugInfo(symbolWriter);
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = this.ctors;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].GenerateDebugInfo(symbolWriter);
				}
			}
			symbolWriter.CloseNamespace();
			if (this.subtypes != null)
			{
				for (int k = 0; k < this.subtypes.Length; k++)
				{
					this.subtypes[k].GenerateDebugInfo(symbolWriter);
				}
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the public and non-public constructors defined for this class, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the specified constructors defined for this class. If no constructors are defined, an empty array is returned.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060033C4 RID: 13252 RVA: 0x000C32B1 File Offset: 0x000C14B1
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetConstructors(bindingAttr);
			}
			throw new NotSupportedException();
		}

		/// <summary>Calling this method always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>This method is not supported. No value is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060033C5 RID: 13253 RVA: 0x000339FF File Offset: 0x00031BFF
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns the event with the specified name.</summary>
		/// <returns>An <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name, or null if there are no matches.</returns>
		/// <param name="name">The name of the event to search for. </param>
		/// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limits the search. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x060033C6 RID: 13254 RVA: 0x000C32CD File Offset: 0x000C14CD
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetEvent(name, bindingAttr);
		}

		/// <summary>Returns the public and non-public events that are declared by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the events declared or inherited by this type that match the specified binding flags. An empty array is returned if there are no matching events.</returns>
		/// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limits the search.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060033C7 RID: 13255 RVA: 0x000C32E2 File Offset: 0x000C14E2
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetEvents(bindingAttr);
			}
			throw new NotSupportedException();
		}

		/// <summary>Returns the field specified by the given name.</summary>
		/// <returns>Returns the <see cref="T:System.Reflection.FieldInfo" /> object representing the field declared or inherited by this type with the specified name and public or non-public modifier. If there are no matches then null is returned.</returns>
		/// <param name="name">The name of the field to get. </param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x060033C8 RID: 13256 RVA: 0x000C3300 File Offset: 0x000C1500
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (this.created != null)
			{
				return this.created.GetField(name, bindingAttr);
			}
			if (this.fields == null)
			{
				return null;
			}
			foreach (FieldBuilder fieldInfo in this.fields)
			{
				if (!(fieldInfo == null) && !(fieldInfo.Name != name))
				{
					bool flag = false;
					FieldAttributes attributes = fieldInfo.Attributes;
					if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							return fieldInfo;
						}
					}
				}
			}
			return null;
		}

		/// <summary>Returns the public and non-public fields that are declared by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the public and non-public fields declared or inherited by this type. An empty array is returned if there are no fields, as specified.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060033C9 RID: 13257 RVA: 0x000C33AC File Offset: 0x000C15AC
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			if (this.created != null)
			{
				return this.created.GetFields(bindingAttr);
			}
			if (this.fields == null)
			{
				return new FieldInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (FieldBuilder fieldInfo in this.fields)
			{
				if (!(fieldInfo == null))
				{
					bool flag = false;
					FieldAttributes attributes = fieldInfo.Attributes;
					if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(fieldInfo);
						}
					}
				}
			}
			FieldInfo[] array2 = new FieldInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		/// <summary>Returns the interface implemented (directly or indirectly) by this class with the fully qualified name matching the given interface name.</summary>
		/// <returns>Returns a <see cref="T:System.Type" /> object representing the implemented interface. Returns null if no interface matching name is found.</returns>
		/// <param name="name">The name of the interface. </param>
		/// <param name="ignoreCase">If true, the search is case-insensitive. If false, the search is case-sensitive. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x060033CA RID: 13258 RVA: 0x000C3472 File Offset: 0x000C1672
		public override Type GetInterface(string name, bool ignoreCase)
		{
			this.check_created();
			return this.created.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns an array of all the interfaces implemented on this type and its base types.</summary>
		/// <returns>Returns an array of <see cref="T:System.Type" /> objects representing the implemented interfaces. If none are defined, an empty array is returned.</returns>
		// Token: 0x060033CB RID: 13259 RVA: 0x000C3488 File Offset: 0x000C1688
		public override Type[] GetInterfaces()
		{
			if (this.is_created)
			{
				return this.created.GetInterfaces();
			}
			if (this.interfaces != null)
			{
				Type[] array = new Type[this.interfaces.Length];
				this.interfaces.CopyTo(array, 0);
				return array;
			}
			return Type.EmptyTypes;
		}

		/// <summary>Returns all the public and non-public members declared or inherited by this type, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public members are returned.</returns>
		/// <param name="name">The name of the member. </param>
		/// <param name="type">The type of the member to return. </param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, as in InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x060033CC RID: 13260 RVA: 0x000C34D3 File Offset: 0x000C16D3
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns the members for the public and non-public members declared or inherited by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members declared or inherited by this type. An empty array is returned if there are no matching members.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060033CD RID: 13261 RVA: 0x000C34E9 File Offset: 0x000C16E9
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetMembers(bindingAttr);
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000C3500 File Offset: 0x000C1700
		private MethodInfo[] GetMethodsByName(string name, BindingFlags bindingAttr, bool ignoreCase, Type reflected_type)
		{
			MethodInfo[] array2;
			if ((bindingAttr & BindingFlags.DeclaredOnly) == BindingFlags.Default && this.parent != null)
			{
				MethodInfo[] array = this.parent.GetMethods(bindingAttr);
				ArrayList arrayList = new ArrayList(array.Length);
				bool flag = (bindingAttr & BindingFlags.FlattenHierarchy) > BindingFlags.Default;
				foreach (MethodInfo methodInfo in array)
				{
					MethodAttributes methodAttributes = methodInfo.Attributes;
					if (!methodInfo.IsStatic || flag)
					{
						MethodAttributes methodAttributes2 = methodAttributes & MethodAttributes.MemberAccessMask;
						bool flag2;
						if (methodAttributes2 != MethodAttributes.Private)
						{
							if (methodAttributes2 != MethodAttributes.Assembly)
							{
								if (methodAttributes2 == MethodAttributes.Public)
								{
									flag2 = (bindingAttr & BindingFlags.Public) > BindingFlags.Default;
								}
								else
								{
									flag2 = (bindingAttr & BindingFlags.NonPublic) > BindingFlags.Default;
								}
							}
							else
							{
								flag2 = (bindingAttr & BindingFlags.NonPublic) > BindingFlags.Default;
							}
						}
						else
						{
							flag2 = false;
						}
						if (flag2)
						{
							arrayList.Add(methodInfo);
						}
					}
				}
				if (this.methods == null)
				{
					array2 = new MethodInfo[arrayList.Count];
					arrayList.CopyTo(array2);
				}
				else
				{
					array2 = new MethodInfo[this.methods.Length + arrayList.Count];
					arrayList.CopyTo(array2, 0);
					this.methods.CopyTo(array2, arrayList.Count);
				}
			}
			else
			{
				MethodInfo[] array3 = this.methods;
				array2 = array3;
			}
			if (array2 == null)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList2 = new ArrayList();
			foreach (MethodInfo methodInfo2 in array2)
			{
				if (!(methodInfo2 == null) && (name == null || string.Compare(methodInfo2.Name, name, ignoreCase) == 0))
				{
					bool flag2 = false;
					MethodAttributes methodAttributes = methodInfo2.Attributes;
					if ((methodAttributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag2 = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag2 = true;
					}
					if (flag2)
					{
						flag2 = false;
						if ((methodAttributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag2 = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag2 = true;
						}
						if (flag2)
						{
							arrayList2.Add(methodInfo2);
						}
					}
				}
			}
			MethodInfo[] array4 = new MethodInfo[arrayList2.Count];
			arrayList2.CopyTo(array4);
			return array4;
		}

		/// <summary>Returns all the public and non-public methods declared or inherited by this type, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing the public and non-public methods defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public methods are returned.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060033CF RID: 13263 RVA: 0x000C36C0 File Offset: 0x000C18C0
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodsByName(null, bindingAttr, false, this);
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000C36CC File Offset: 0x000C18CC
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			this.check_created();
			if (types == null)
			{
				return this.created.GetMethod(name, bindingAttr);
			}
			return this.created.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns the public and non-public nested types that are declared by this type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the nested type that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the nested type to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to conduct a case-sensitive search for public methods. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x060033D1 RID: 13265 RVA: 0x000C36FC File Offset: 0x000C18FC
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			this.check_created();
			if (this.subtypes == null)
			{
				return null;
			}
			foreach (TypeBuilder typeBuilder in this.subtypes)
			{
				if (typeBuilder.is_created)
				{
					if ((typeBuilder.attrs & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic)
					{
						if ((bindingAttr & BindingFlags.Public) == BindingFlags.Default)
						{
							goto IL_0055;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default)
					{
						goto IL_0055;
					}
					if (typeBuilder.Name == name)
					{
						return typeBuilder.created;
					}
				}
				IL_0055:;
			}
			return null;
		}

		/// <summary>Returns all the public and non-public properties declared or inherited by this type, as specified.</summary>
		/// <returns>Returns an array of PropertyInfo objects representing the public and non-public properties defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public properties are returned.</returns>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060033D2 RID: 13266 RVA: 0x000C376C File Offset: 0x000C196C
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetProperties(bindingAttr);
			}
			if (this.properties == null)
			{
				return new PropertyInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (PropertyBuilder propertyInfo in this.properties)
			{
				bool flag = false;
				MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
				if (methodInfo == null)
				{
					methodInfo = propertyInfo.GetSetMethod(true);
				}
				if (!(methodInfo == null))
				{
					MethodAttributes attributes = methodInfo.Attributes;
					if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(propertyInfo);
						}
					}
				}
			}
			PropertyInfo[] array2 = new PropertyInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000C384B File Offset: 0x000C1A4B
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000C3853 File Offset: 0x000C1A53
		protected override bool HasElementTypeImpl()
		{
			return this.is_created && this.created.HasElementType;
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the constraints of the specified binder and invocation attributes.</summary>
		/// <returns>Returns the return value of the invoked member.</returns>
		/// <param name="name">The name of the member to invoke. This can be a constructor, method, property, or field. A suitable invocation attribute must be specified. Note that it is possible to invoke the default member of a class by passing an empty string as the name of the member. </param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from BindingFlags. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If binder is null, the default binder is used. See <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="target">The object on which to invoke the specified member. If the member is static, this parameter is ignored. </param>
		/// <param name="args">An argument list. This is an array of Objects that contains the number, order, and type of the parameters of the member to be invoked. If there are no parameters this should be null. </param>
		/// <param name="modifiers">An array of the same length as <paramref name="args" /> with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the metadata. They are used by various interoperability services. See the metadata specs for more details. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is null, the CultureInfo for the current thread is used. (Note that this is necessary to, for example, convert a String that represents 1000 to a Double value, since 1000 is represented differently by different cultures.) </param>
		/// <param name="namedParameters">Each parameter in the <paramref name="namedParameters" /> array gets the value in the corresponding element in the <paramref name="args" /> array. If the length of <paramref name="args" /> is greater than the length of <paramref name="namedParameters" />, the remaining argument values are passed in order. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060033D5 RID: 13269 RVA: 0x000C386C File Offset: 0x000C1A6C
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			this.check_created();
			return this.created.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x00033A39 File Offset: 0x00031C39
		protected override bool IsCOMObjectImpl()
		{
			return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > TypeAttributes.NotPublic;
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x00033991 File Offset: 0x00031B91
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000C3898 File Offset: 0x000C1A98
		protected override bool IsValueTypeImpl()
		{
			if (this == this.pmodule.assemblyb.corlib_value_type || this == this.pmodule.assemblyb.corlib_enum_type)
			{
				return false;
			}
			Type baseType = this.parent;
			while (baseType != null)
			{
				if (baseType == this.pmodule.assemblyb.corlib_value_type)
				{
					return true;
				}
				baseType = baseType.BaseType;
			}
			return false;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a one-dimensional array of the current type, with a lower bound of zero.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a one-dimensional array type whose element type is the current type, with a lower bound of zero.</returns>
		// Token: 0x060033DC RID: 13276 RVA: 0x000B80A1 File Offset: 0x000B62A1
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents an array of the current type, with the specified number of dimensions.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a one-dimensional array of the current type.</returns>
		/// <param name="rank">The number of dimensions for the array. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is not a valid array dimension.</exception>
		// Token: 0x060033DD RID: 13277 RVA: 0x000B80AA File Offset: 0x000B62AA
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef in Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef in Visual Basic).</returns>
		// Token: 0x060033DE RID: 13278 RVA: 0x000B80BD File Offset: 0x000B62BD
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		/// <summary>Substitutes the elements of an array of types for the type parameters of the current generic type definition, and returns the resulting constructed type.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the constructed type formed by substituting the elements of <paramref name="typeArguments" /> for the type parameters of the current generic type. </returns>
		/// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic type definition.</param>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent the definition of a generic type. That is, <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> returns false. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArguments" /> is null.-or- Any element of <paramref name="typeArguments" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">Any element of <paramref name="typeArguments" /> does not satisfy the constraints specified for the corresponding type parameter of the current generic type. </exception>
		// Token: 0x060033DF RID: 13279 RVA: 0x000C390C File Offset: 0x000C1B0C
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			if (!this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException("not a generic type definition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (this.generic_params.Length != typeArguments.Length)
			{
				throw new ArgumentException(string.Format("The type or method has {0} generic parameter(s) but {1} generic argument(s) where provided. A generic argument must be provided for each generic parameter.", this.generic_params.Length, typeArguments.Length), "typeArguments");
			}
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			Type[] array = new Type[typeArguments.Length];
			typeArguments.CopyTo(array, 0);
			return this.pmodule.assemblyb.MakeGenericType(this, array);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the type of an unmanaged pointer to the current type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of an unmanaged pointer to the current type.</returns>
		// Token: 0x060033E0 RID: 13280 RVA: 0x000B80C5 File Offset: 0x000B62C5
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		/// <summary>Not supported in dynamic modules.</summary>
		/// <returns>Read-only.</returns>
		/// <exception cref="T:System.NotSupportedException">Not supported in dynamic modules. </exception>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000C39BA File Offset: 0x000C1BBA
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				this.check_created();
				return this.created.TypeHandle;
			}
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x060033E2 RID: 13282 RVA: 0x000C39D0 File Offset: 0x000C1BD0
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.StructLayoutAttribute")
			{
				byte[] data = customBuilder.Data;
				int num = (int)data[2] | ((int)data[3] << 8);
				this.attrs &= ~TypeAttributes.LayoutMask;
				switch (num)
				{
				case 0:
					this.attrs |= TypeAttributes.SequentialLayout;
					goto IL_00A5;
				case 2:
					this.attrs |= TypeAttributes.ExplicitLayout;
					goto IL_00A5;
				case 3:
					this.attrs |= TypeAttributes.NotPublic;
					goto IL_00A5;
				}
				throw new Exception("Error in customattr");
				IL_00A5:
				Type type = ((customBuilder.Ctor is ConstructorBuilder) ? ((ConstructorBuilder)customBuilder.Ctor).parameters[0] : customBuilder.Ctor.GetParametersInternal()[0].ParameterType);
				int num2 = 6;
				if (type.FullName == "System.Int16")
				{
					num2 = 4;
				}
				int num3 = (int)data[num2++];
				num3 |= (int)data[num2++] << 8;
				for (int i = 0; i < num3; i++)
				{
					num2++;
					int num4;
					if (data[num2++] == 85)
					{
						num4 = CustomAttributeBuilder.decode_len(data, num2, out num2);
						CustomAttributeBuilder.string_from_bytes(data, num2, num4);
						num2 += num4;
					}
					num4 = CustomAttributeBuilder.decode_len(data, num2, out num2);
					string text = CustomAttributeBuilder.string_from_bytes(data, num2, num4);
					num2 += num4;
					int num5 = (int)data[num2++];
					num5 |= (int)data[num2++] << 8;
					num5 |= (int)data[num2++] << 16;
					num5 |= (int)data[num2++] << 24;
					if (!(text == "CharSet"))
					{
						if (!(text == "Pack"))
						{
							if (text == "Size")
							{
								this.class_size = num5;
							}
						}
						else
						{
							this.packing_size = (PackingSize)num5;
						}
					}
					else
					{
						switch (num5)
						{
						case 1:
						case 2:
							this.attrs &= ~TypeAttributes.StringFormatMask;
							break;
						case 3:
							this.attrs &= ~TypeAttributes.AutoClass;
							this.attrs |= TypeAttributes.UnicodeClass;
							break;
						case 4:
							this.attrs &= ~TypeAttributes.UnicodeClass;
							this.attrs |= TypeAttributes.AutoClass;
							break;
						}
					}
				}
				return;
			}
			if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= TypeAttributes.SpecialName;
				return;
			}
			if (fullName == "System.SerializableAttribute")
			{
				this.attrs |= TypeAttributes.Serializable;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.ComImportAttribute")
			{
				this.attrs |= TypeAttributes.Import;
				return;
			}
			if (fullName == "System.Security.SuppressUnmanagedCodeSecurityAttribute")
			{
				this.attrs |= TypeAttributes.HasSecurity;
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
				return;
			}
			this.cattrs = new CustomAttributeBuilder[1];
			this.cattrs[0] = customBuilder;
		}

		/// <summary>Sets the base type of the type currently under construction.</summary>
		/// <param name="parent">The new base type. </param>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-<paramref name="parent" /> is null, and the current instance represents an interface whose attributes do not include <see cref="F:System.Reflection.TypeAttributes.Abstract" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> is an interface. This exception condition is new in the .NET Framework version 2.0. </exception>
		// Token: 0x060033E3 RID: 13283 RVA: 0x000C3D04 File Offset: 0x000C1F04
		public void SetParent(Type parent)
		{
			this.check_not_created();
			if (parent == null)
			{
				if ((this.attrs & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
				{
					if ((this.attrs & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
					{
						throw new InvalidOperationException("Interface must be declared abstract.");
					}
					this.parent = null;
				}
				else
				{
					this.parent = typeof(object);
				}
			}
			else
			{
				this.parent = parent;
			}
			this.parent = TypeBuilder.ResolveUserType(this.parent);
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000C3D77 File Offset: 0x000C1F77
		internal int get_next_table_index(object obj, int table, int count)
		{
			return this.pmodule.get_next_table_index(obj, table, count);
		}

		/// <summary>Returns an interface mapping for the requested interface.</summary>
		/// <returns>Returns the requested interface mapping.</returns>
		/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface for which the mapping is to be retrieved. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x060033E5 RID: 13285 RVA: 0x000C3D87 File Offset: 0x000C1F87
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			if (this.created == null)
			{
				throw new NotSupportedException("This method is not implemented for incomplete types.");
			}
			return this.created.GetInterfaceMap(interfaceType);
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000C3DAE File Offset: 0x000C1FAE
		internal override Type InternalResolve()
		{
			this.check_created();
			return this.created;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000C3DAE File Offset: 0x000C1FAE
		internal override Type RuntimeResolve()
		{
			this.check_created();
			return this.created;
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x000C3DBC File Offset: 0x000C1FBC
		internal bool is_created
		{
			get
			{
				return this.createTypeCalled;
			}
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000B9312 File Offset: 0x000B7512
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000C3DC4 File Offset: 0x000C1FC4
		private void check_not_created()
		{
			if (this.is_created)
			{
				throw new InvalidOperationException("Unable to change after type has been created.");
			}
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000C3DD9 File Offset: 0x000C1FD9
		private void check_created()
		{
			if (!this.is_created)
			{
				throw this.not_supported();
			}
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000C3DEA File Offset: 0x000C1FEA
		private void check_name(string argName, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(argName);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal", argName);
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException("Illegal name", argName);
			}
		}

		/// <summary>Returns the name of the type excluding the namespace.</summary>
		/// <returns>Read-only. The name of the type excluding the namespace.</returns>
		// Token: 0x060033ED RID: 13293 RVA: 0x000C3E1F File Offset: 0x000C201F
		public override string ToString()
		{
			return this.FullName;
		}

		/// <summary>Gets a value that indicates whether a specified <see cref="T:System.Type" /> can be assigned to this object.</summary>
		/// <returns>true if the <paramref name="c" /> parameter and the current type represent the same type, or if the current type is in the inheritance hierarchy of <paramref name="c" />, or if the current type is an interface that <paramref name="c" /> supports. false if none of these conditions are valid, or if <paramref name="c" /> is null.</returns>
		/// <param name="c">The object to test. </param>
		// Token: 0x060033EE RID: 13294 RVA: 0x000C3E27 File Offset: 0x000C2027
		[MonoTODO]
		public override bool IsAssignableFrom(Type c)
		{
			return base.IsAssignableFrom(c);
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000C3E30 File Offset: 0x000C2030
		[MonoTODO("arrays")]
		internal bool IsAssignableTo(Type c)
		{
			if (c == this)
			{
				return true;
			}
			if (c.IsInterface)
			{
				if (this.parent != null && this.is_created && c.IsAssignableFrom(this.parent))
				{
					return true;
				}
				if (this.interfaces == null)
				{
					return false;
				}
				foreach (Type type in this.interfaces)
				{
					if (c.IsAssignableFrom(type))
					{
						return true;
					}
				}
				if (!this.is_created)
				{
					return false;
				}
			}
			if (this.parent == null)
			{
				return c == typeof(object);
			}
			return c.IsAssignableFrom(this.parent);
		}

		/// <summary>Returns a value that indicates whether the current dynamic type has been created.</summary>
		/// <returns>true if the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method has been called; otherwise, false. </returns>
		// Token: 0x060033F0 RID: 13296 RVA: 0x000C3ED9 File Offset: 0x000C20D9
		public bool IsCreated()
		{
			return this.is_created;
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects representing the type arguments of a generic type or the type parameters of a generic type definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects. The elements of the array represent the type arguments of a generic type or the type parameters of a generic type definition.</returns>
		// Token: 0x060033F1 RID: 13297 RVA: 0x000C3EE4 File Offset: 0x000C20E4
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return null;
			}
			Type[] array = new Type[this.generic_params.Length];
			this.generic_params.CopyTo(array, 0);
			return array;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a generic type definition from which the current type can be obtained.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a generic type definition from which the current type can be obtained.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type is not generic. That is, <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> returns false.</exception>
		// Token: 0x060033F2 RID: 13298 RVA: 0x000C3F17 File Offset: 0x000C2117
		public override Type GetGenericTypeDefinition()
		{
			if (this.generic_params == null)
			{
				throw new InvalidOperationException("Type is not generic");
			}
			return this;
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x000C3F2D File Offset: 0x000C212D
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type parameter.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type parameter; otherwise, false.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060033F4 RID: 13300 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates the covariance and special constraints of the current generic type parameter. </summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that describes the covariance and special constraints of the current generic type parameter.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x00033991 File Offset: 0x00031B91
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return GenericParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> represents a generic type definition from which other generic types can be constructed.</summary>
		/// <returns>true if this <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type definition; otherwise, false.</returns>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x000C3F2D File Offset: 0x000C212D
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type. </summary>
		/// <returns>true if the type represented by the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object is generic; otherwise, false.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000C3F38 File Offset: 0x000C2138
		public override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition;
			}
		}

		/// <summary>Gets the position of a type parameter in the type parameter list of the generic type that declared the parameter.</summary>
		/// <returns>If the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type parameter, the position of the type parameter in the type parameter list of the generic type that declared the parameter; otherwise, undefined.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060033F8 RID: 13304 RVA: 0x00033991 File Offset: 0x00031B91
		[MonoTODO]
		public override int GenericParameterPosition
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the method that declared the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> that represents the method that declared the current type, if the current type is a generic type parameter; otherwise, null.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x000082D2 File Offset: 0x000064D2
		public override MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x00033991 File Offset: 0x00031B91
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>true if this object represents a constructed generic type; otherwise, false.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060033FB RID: 13307 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B02 RID: 6914
		private string tname;

		// Token: 0x04001B03 RID: 6915
		private string nspace;

		// Token: 0x04001B04 RID: 6916
		private Type parent;

		// Token: 0x04001B05 RID: 6917
		private Type nesting_type;

		// Token: 0x04001B06 RID: 6918
		internal Type[] interfaces;

		// Token: 0x04001B07 RID: 6919
		internal int num_methods;

		// Token: 0x04001B08 RID: 6920
		internal MethodBuilder[] methods;

		// Token: 0x04001B09 RID: 6921
		internal ConstructorBuilder[] ctors;

		// Token: 0x04001B0A RID: 6922
		internal PropertyBuilder[] properties;

		// Token: 0x04001B0B RID: 6923
		internal int num_fields;

		// Token: 0x04001B0C RID: 6924
		internal FieldBuilder[] fields;

		// Token: 0x04001B0D RID: 6925
		internal EventBuilder[] events;

		// Token: 0x04001B0E RID: 6926
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04001B0F RID: 6927
		internal TypeBuilder[] subtypes;

		// Token: 0x04001B10 RID: 6928
		internal TypeAttributes attrs;

		// Token: 0x04001B11 RID: 6929
		private int table_idx;

		// Token: 0x04001B12 RID: 6930
		private ModuleBuilder pmodule;

		// Token: 0x04001B13 RID: 6931
		private int class_size;

		// Token: 0x04001B14 RID: 6932
		private PackingSize packing_size;

		// Token: 0x04001B15 RID: 6933
		private IntPtr generic_container;

		// Token: 0x04001B16 RID: 6934
		private GenericTypeParameterBuilder[] generic_params;

		// Token: 0x04001B17 RID: 6935
		private RefEmitPermissionSet[] permissions;

		// Token: 0x04001B18 RID: 6936
		private TypeInfo created;

		// Token: 0x04001B19 RID: 6937
		private int state;

		// Token: 0x04001B1A RID: 6938
		private TypeName fullname;

		// Token: 0x04001B1B RID: 6939
		private bool createTypeCalled;

		// Token: 0x04001B1C RID: 6940
		private Type underlying_type;

		/// <summary>Represents that total size for the type is not specified.</summary>
		// Token: 0x04001B1D RID: 6941
		public const int UnspecifiedTypeSize = 0;
	}
}
