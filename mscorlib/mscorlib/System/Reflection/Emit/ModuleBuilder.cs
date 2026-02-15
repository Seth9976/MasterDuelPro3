using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a module in a dynamic assembly.</summary>
	// Token: 0x02000676 RID: 1654
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_ModuleBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public class ModuleBuilder : Module, _ModuleBuilder
	{
		/// <summary>For a description of this member, see <see cref="M:System.Runtime.InteropServices._ModuleBuilder.GetIDsOfNames(System.Guid@,System.IntPtr,System.UInt32,System.UInt32,System.IntPtr)" />. </summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060032E9 RID: 13033 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _ModuleBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Runtime.InteropServices._ModuleBuilder.GetTypeInfo(System.UInt32,System.UInt32,System.IntPtr)" />.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">A pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060032EA RID: 13034 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _ModuleBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Runtime.InteropServices._ModuleBuilder.GetTypeInfoCount(System.UInt32@)" />.</summary>
		/// <param name="pcTInfo">The location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060032EB RID: 13035 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _ModuleBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Runtime.InteropServices._ModuleBuilder.Invoke(System.UInt32,System.Guid@,System.UInt32,System.Int16,System.IntPtr,System.IntPtr,System.IntPtr,System.IntPtr)" />.</summary>
		/// <param name="dispIdMember">The member ID.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060032EC RID: 13036 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _ModuleBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060032ED RID: 13037
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void basic_init(ModuleBuilder ab);

		// Token: 0x060032EE RID: 13038
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrappers_type(ModuleBuilder mb, Type ab);

		// Token: 0x060032EF RID: 13039 RVA: 0x000BE630 File Offset: 0x000BC830
		internal ModuleBuilder(AssemblyBuilder assb, string name, string fullyqname, bool emitSymbolInfo, bool transient)
		{
			this.scopename = name;
			this.name = name;
			this.fqname = fullyqname;
			this.assemblyb = assb;
			this.assembly = assb;
			this.transient = transient;
			this.guid = Guid.FastNewGuidArray();
			this.table_idx = this.get_next_table_index(this, 0, 1);
			this.name_cache = new Dictionary<TypeName, TypeBuilder>();
			this.us_string_cache = new Dictionary<string, int>(512);
			ModuleBuilder.basic_init(this);
			this.CreateGlobalType();
			if (assb.IsRun)
			{
				Type type = new TypeBuilder(this, TypeAttributes.Abstract, 16777215).CreateType();
				ModuleBuilder.set_wrappers_type(this, type);
			}
			if (emitSymbolInfo)
			{
				Assembly assembly = Assembly.LoadWithPartialName("Mono.CompilerServices.SymbolWriter");
				Type type2 = null;
				if (assembly != null)
				{
					type2 = assembly.GetType("Mono.CompilerServices.SymbolWriter.SymbolWriterImpl");
				}
				if (type2 == null)
				{
					ModuleBuilder.WarnAboutSymbolWriter("Failed to load the default Mono.CompilerServices.SymbolWriter assembly");
				}
				else
				{
					try
					{
						this.symbolWriter = (ISymbolWriter)Activator.CreateInstance(type2, new object[] { this });
					}
					catch (MissingMethodException)
					{
						ModuleBuilder.WarnAboutSymbolWriter("The default Mono.CompilerServices.SymbolWriter is not available on this platform");
						return;
					}
				}
				string text = this.fqname;
				if (this.assemblyb.AssemblyDir != null)
				{
					text = Path.Combine(this.assemblyb.AssemblyDir, text);
				}
				this.symbolWriter.Initialize(IntPtr.Zero, text, true);
			}
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000BE794 File Offset: 0x000BC994
		private static void WarnAboutSymbolWriter(string message)
		{
			if (ModuleBuilder.has_warned_about_symbolWriter)
			{
				return;
			}
			ModuleBuilder.has_warned_about_symbolWriter = true;
			Console.Error.WriteLine("WARNING: {0}", message);
		}

		/// <summary>Gets a String representing the fully qualified name and path to this module.</summary>
		/// <returns>The fully qualified module name.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x000BE7B4 File Offset: 0x000BC9B4
		public override string FullyQualifiedName
		{
			get
			{
				string text = this.fqname;
				if (text == null)
				{
					return null;
				}
				if (this.assemblyb.AssemblyDir != null)
				{
					text = Path.Combine(this.assemblyb.AssemblyDir, text);
					text = Path.GetFullPath(text);
				}
				return text;
			}
		}

		/// <summary>Returns a value that indicates whether this dynamic module is transient.</summary>
		/// <returns>true if this dynamic module is transient; otherwise, false.</returns>
		// Token: 0x060032F2 RID: 13042 RVA: 0x000BE7F4 File Offset: 0x000BC9F4
		public bool IsTransient()
		{
			return this.transient;
		}

		/// <summary>Completes the global function definitions and global data definitions for this dynamic module.</summary>
		/// <exception cref="T:System.InvalidOperationException">This method was called previously. </exception>
		// Token: 0x060032F3 RID: 13043 RVA: 0x000BE7FC File Offset: 0x000BC9FC
		public void CreateGlobalFunctions()
		{
			if (this.global_type_created != null)
			{
				throw new InvalidOperationException("global methods already created");
			}
			if (this.global_type != null)
			{
				this.global_type_created = this.global_type.CreateType();
			}
		}

		/// <summary>Defines an initialized data field in the .sdata section of the portable executable (PE) file.</summary>
		/// <returns>A field to reference the data.</returns>
		/// <param name="name">The name used to refer to the data. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="data">The binary large object (BLOB) of data. </param>
		/// <param name="attributes">The attributes for the field. The default is Static. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The size of <paramref name="data" /> is less than or equal to zero or greater than or equal to 0x3f0000. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="data" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> has been previously called. </exception>
		// Token: 0x060032F4 RID: 13044 RVA: 0x000BE838 File Offset: 0x000BCA38
		public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			FieldAttributes fieldAttributes = attributes & ~(FieldAttributes.RTSpecialName | FieldAttributes.HasFieldMarshal | FieldAttributes.HasDefault | FieldAttributes.HasFieldRVA);
			FieldBuilder fieldBuilder = this.DefineDataImpl(name, data.Length, fieldAttributes | FieldAttributes.HasFieldRVA);
			fieldBuilder.SetRVAData(data);
			return fieldBuilder;
		}

		/// <summary>Defines an uninitialized data field in the .sdata section of the portable executable (PE) file.</summary>
		/// <returns>A field to reference the data.</returns>
		/// <param name="name">The name used to refer to the data. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="size">The size of the data field. </param>
		/// <param name="attributes">The attributes for the field. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- <paramref name="size" /> is less than or equal to zero, or greater than or equal to 0x003f0000. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> has been previously called. </exception>
		// Token: 0x060032F5 RID: 13045 RVA: 0x000BE873 File Offset: 0x000BCA73
		public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
		{
			return this.DefineDataImpl(name, size, attributes & ~(FieldAttributes.RTSpecialName | FieldAttributes.HasFieldMarshal | FieldAttributes.HasDefault | FieldAttributes.HasFieldRVA));
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000BE884 File Offset: 0x000BCA84
		private FieldBuilder DefineDataImpl(string name, int size, FieldAttributes attributes)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("name cannot be empty", "name");
			}
			if (this.global_type_created != null)
			{
				throw new InvalidOperationException("global fields already created");
			}
			if (size <= 0 || size >= 4128768)
			{
				throw new ArgumentException("Data size must be > 0 and < 0x3f0000", null);
			}
			this.CreateGlobalType();
			string text = "$ArrayType$" + size.ToString();
			Type type = this.GetType(text, false, false);
			if (type == null)
			{
				TypeBuilder typeBuilder = this.DefineType(text, TypeAttributes.Public | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed, this.assemblyb.corlib_value_type, null, PackingSize.Size1, size);
				typeBuilder.CreateType();
				type = typeBuilder;
			}
			FieldBuilder fieldBuilder = this.global_type.DefineField(name, type, attributes | FieldAttributes.Static);
			if (this.global_fields != null)
			{
				FieldBuilder[] array = new FieldBuilder[this.global_fields.Length + 1];
				Array.Copy(this.global_fields, array, this.global_fields.Length);
				array[this.global_fields.Length] = fieldBuilder;
				this.global_fields = array;
			}
			else
			{
				this.global_fields = new FieldBuilder[1];
				this.global_fields[0] = fieldBuilder;
			}
			return fieldBuilder;
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000BE9A4 File Offset: 0x000BCBA4
		private void addGlobalMethod(MethodBuilder mb)
		{
			if (this.global_methods != null)
			{
				MethodBuilder[] array = new MethodBuilder[this.global_methods.Length + 1];
				Array.Copy(this.global_methods, array, this.global_methods.Length);
				array[this.global_methods.Length] = mb;
				this.global_methods = array;
				return;
			}
			this.global_methods = new MethodBuilder[1];
			this.global_methods[0] = mb;
		}

		/// <summary>Defines a global method with the specified name, attributes, return type, and parameter types.</summary>
		/// <returns>The defined global method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. <paramref name="attributes" /> must include <see cref="F:System.Reflection.MethodAttributes.Static" />. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static. That is, <paramref name="attributes" /> does not include <see cref="F:System.Reflection.MethodAttributes.Static" />.-or- The length of <paramref name="name" /> is zero -or-An element in the <see cref="T:System.Type" /> array is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> has been previously called. </exception>
		// Token: 0x060032F8 RID: 13048 RVA: 0x000BEA05 File Offset: 0x000BCC05
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineGlobalMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		/// <summary>Defines a global method with the specified name, attributes, calling convention, return type, and parameter types.</summary>
		/// <returns>The defined global method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. <paramref name="attributes" /> must include <see cref="F:System.Reflection.MethodAttributes.Static" />.</param>
		/// <param name="callingConvention">The calling convention for the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static. That is, <paramref name="attributes" /> does not include <see cref="F:System.Reflection.MethodAttributes.Static" />.-or-An element in the <see cref="T:System.Type" /> array is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> has been previously called. </exception>
		// Token: 0x060032F9 RID: 13049 RVA: 0x000BEA14 File Offset: 0x000BCC14
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineGlobalMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Defines a global method with the specified name, attributes, calling convention, return type, custom modifiers for the return type, parameter types, and custom modifiers for the parameter types.</summary>
		/// <returns>The defined global method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded null characters. </param>
		/// <param name="attributes">The attributes of the method. <paramref name="attributes" /> must include <see cref="F:System.Reflection.MethodAttributes.Static" />.</param>
		/// <param name="callingConvention">The calling convention for the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="requiredReturnTypeCustomModifiers">An array of types representing the required custom modifiers for the return type, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsBoxed" />. If the return type has no required custom modifiers, specify null. </param>
		/// <param name="optionalReturnTypeCustomModifiers">An array of types representing the optional custom modifiers for the return type, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsBoxed" />. If the return type has no optional custom modifiers, specify null. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <param name="requiredParameterTypeCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter of the global method. If a particular argument has no required custom modifiers, specify null instead of an array of types. If the global method has no arguments, or if none of the arguments have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="optionalParameterTypeCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter. If a particular argument has no optional custom modifiers, specify null instead of an array of types. If the global method has no arguments, or if none of the arguments have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentException">The method is not static. That is, <paramref name="attributes" /> does not include <see cref="F:System.Reflection.MethodAttributes.Static" />.-or-An element in the <see cref="T:System.Type" /> array is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> method has been previously called. </exception>
		// Token: 0x060032FA RID: 13050 RVA: 0x000BEA34 File Offset: 0x000BCC34
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("global methods must be static");
			}
			if (this.global_type_created != null)
			{
				throw new InvalidOperationException("global methods already created");
			}
			this.CreateGlobalType();
			MethodBuilder methodBuilder = this.global_type.DefineMethod(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
			this.addGlobalMethod(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Defines a PInvoke method with the specified name, the name of the DLL in which the method is defined, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the PInvoke flags.</summary>
		/// <returns>The defined PInvoke method.</returns>
		/// <param name="name">The name of the PInvoke method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="dllName">The name of the DLL in which the PInvoke method is defined. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The method's calling convention. </param>
		/// <param name="returnType">The method's return type. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <param name="nativeCallConv">The native calling convention. </param>
		/// <param name="nativeCharSet">The method's native character set. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static or if the containing type is an interface.-or- The method is abstract.-or- The method was previously defined. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="dllName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /></exception>
		// Token: 0x060032FB RID: 13051 RVA: 0x000BEAA4 File Offset: 0x000BCCA4
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, name, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
		}

		/// <summary>Defines a PInvoke method with the specified name, the name of the DLL in which the method is defined, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the PInvoke flags.</summary>
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
		/// <exception cref="T:System.ArgumentException">The method is not static or if the containing type is an interface or if the method is abstract of if the method was previously defined. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="dllName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /></exception>
		// Token: 0x060032FC RID: 13052 RVA: 0x000BEAC8 File Offset: 0x000BCCC8
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("global methods must be static");
			}
			if (this.global_type_created != null)
			{
				throw new InvalidOperationException("global methods already created");
			}
			this.CreateGlobalType();
			MethodBuilder methodBuilder = this.global_type.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
			this.addGlobalMethod(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Constructs a TypeBuilder for a private type with the specified name in this module. </summary>
		/// <returns>A private type with the specified name.</returns>
		/// <param name="name">The full path of the type, including the namespace. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x060032FD RID: 13053 RVA: 0x000BEB37 File Offset: 0x000BCD37
		public TypeBuilder DefineType(string name)
		{
			return this.DefineType(name, TypeAttributes.NotPublic);
		}

		/// <summary>Constructs a TypeBuilder given the type name and the type attributes.</summary>
		/// <returns>A TypeBuilder created with all of the requested attributes.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the defined type. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x060032FE RID: 13054 RVA: 0x000BEB41 File Offset: 0x000BCD41
		public TypeBuilder DefineType(string name, TypeAttributes attr)
		{
			if ((attr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
			{
				return this.DefineType(name, attr, null, null);
			}
			return this.DefineType(name, attr, typeof(object), null);
		}

		/// <summary>Constructs a TypeBuilder given type name, its attributes, and the type that the defined type extends.</summary>
		/// <returns>A TypeBuilder created with all of the requested attributes.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attribute to be associated with the type. </param>
		/// <param name="parent">The type that the defined type extends. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x060032FF RID: 13055 RVA: 0x000BEB67 File Offset: 0x000BCD67
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent)
		{
			return this.DefineType(name, attr, parent, null);
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000BEB74 File Offset: 0x000BCD74
		private void AddType(TypeBuilder tb)
		{
			if (this.types != null)
			{
				if (this.types.Length == this.num_types)
				{
					TypeBuilder[] array = new TypeBuilder[this.types.Length * 2];
					Array.Copy(this.types, array, this.num_types);
					this.types = array;
				}
			}
			else
			{
				this.types = new TypeBuilder[1];
			}
			this.types[this.num_types] = tb;
			this.num_types++;
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000BEBEC File Offset: 0x000BCDEC
		private TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packingSize, int typesize)
		{
			if (name == null)
			{
				throw new ArgumentNullException("fullname");
			}
			TypeIdentifier typeIdentifier = TypeIdentifiers.FromInternal(name);
			if (this.name_cache.ContainsKey(typeIdentifier))
			{
				throw new ArgumentException("Duplicate type name within an assembly.");
			}
			TypeBuilder typeBuilder = new TypeBuilder(this, name, attr, parent, interfaces, packingSize, typesize, null);
			this.AddType(typeBuilder);
			this.name_cache.Add(typeIdentifier, typeBuilder);
			return typeBuilder;
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000BEC4D File Offset: 0x000BCE4D
		internal void RegisterTypeName(TypeBuilder tb, TypeName name)
		{
			this.name_cache.Add(name, tb);
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000BEC5C File Offset: 0x000BCE5C
		internal TypeBuilder GetRegisteredType(TypeName name)
		{
			TypeBuilder typeBuilder = null;
			this.name_cache.TryGetValue(name, out typeBuilder);
			return typeBuilder;
		}

		/// <summary>Constructs a TypeBuilder given the type name, attributes, the type that the defined type extends, and the interfaces that the defined type implements.</summary>
		/// <returns>A TypeBuilder created with all of the requested attributes.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes to be associated with the type. </param>
		/// <param name="parent">The type that the defined type extends. </param>
		/// <param name="interfaces">The list of interfaces that the type implements. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06003304 RID: 13060 RVA: 0x000BEC7B File Offset: 0x000BCE7B
		[ComVisible(true)]
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
		{
			return this.DefineType(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
		}

		/// <summary>Constructs a TypeBuilder given the type name, the attributes, the type that the defined type extends, and the total size of the type.</summary>
		/// <returns>A TypeBuilder object.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the defined type. </param>
		/// <param name="parent">The type that the defined type extends. </param>
		/// <param name="typesize">The total size of the type. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06003305 RID: 13061 RVA: 0x000BEC8A File Offset: 0x000BCE8A
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, int typesize)
		{
			return this.DefineType(name, attr, parent, null, PackingSize.Unspecified, typesize);
		}

		/// <summary>Constructs a TypeBuilder given the type name, the attributes, the type that the defined type extends, and the packing size of the type.</summary>
		/// <returns>A TypeBuilder object.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the defined type. </param>
		/// <param name="parent">The type that the defined type extends. </param>
		/// <param name="packsize">The packing size of the type. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06003306 RID: 13062 RVA: 0x000BEC99 File Offset: 0x000BCE99
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packsize)
		{
			return this.DefineType(name, attr, parent, null, packsize, 0);
		}

		/// <summary>Constructs a TypeBuilder given the type name, attributes, the type that the defined type extends, the packing size of the defined type, and the total size of the defined type.</summary>
		/// <returns>A TypeBuilder created with all of the requested attributes.</returns>
		/// <param name="name">The full path of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the defined type. </param>
		/// <param name="parent">The type that the defined type extends. </param>
		/// <param name="packingSize">The packing size of the type. </param>
		/// <param name="typesize">The total size of the type. </param>
		/// <exception cref="T:System.ArgumentException">A type with the given name exists in the parent assembly of this module.-or- Nested type attributes are set on a type that is not nested. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06003307 RID: 13063 RVA: 0x000BECA8 File Offset: 0x000BCEA8
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packingSize, int typesize)
		{
			return this.DefineType(name, attr, parent, null, packingSize, typesize);
		}

		/// <summary>Returns the named method on an array class.</summary>
		/// <returns>The named method on an array class.</returns>
		/// <param name="arrayClass">An array class. </param>
		/// <param name="methodName">The name of a method on the array class. </param>
		/// <param name="callingConvention">The method's calling convention. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="arrayClass" /> is not an array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="arrayClass" /> or <paramref name="methodName" /> is null. </exception>
		// Token: 0x06003308 RID: 13064 RVA: 0x000BECB8 File Offset: 0x000BCEB8
		public MethodInfo GetArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return new MonoArrayMethod(arrayClass, methodName, callingConvention, returnType, parameterTypes);
		}

		/// <summary>Defines an enumeration type that is a value type with a single non-static field called <paramref name="value__" /> of the specified type.</summary>
		/// <returns>The defined enumeration.</returns>
		/// <param name="name">The full path of the enumeration type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="visibility">The type attributes for the enumeration. The attributes are any bits defined by <see cref="F:System.Reflection.TypeAttributes.VisibilityMask" />. </param>
		/// <param name="underlyingType">The underlying type for the enumeration. This must be a built-in integer type. </param>
		/// <exception cref="T:System.ArgumentException">Attributes other than visibility attributes are provided.-or- An enumeration with the given name exists in the parent assembly of this module.-or- The visibility attributes do not match the scope of the enumeration. For example, <see cref="F:System.Reflection.TypeAttributes.NestedPublic" /> is specified for <paramref name="visibility" />, but the enumeration is not a nested type. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06003309 RID: 13065 RVA: 0x000BECC8 File Offset: 0x000BCEC8
		public EnumBuilder DefineEnum(string name, TypeAttributes visibility, Type underlyingType)
		{
			TypeIdentifier typeIdentifier = TypeIdentifiers.FromInternal(name);
			if (this.name_cache.ContainsKey(typeIdentifier))
			{
				throw new ArgumentException("Duplicate type name within an assembly.");
			}
			EnumBuilder enumBuilder = new EnumBuilder(this, name, visibility, underlyingType);
			TypeBuilder typeBuilder = enumBuilder.GetTypeBuilder();
			this.AddType(typeBuilder);
			this.name_cache.Add(typeIdentifier, typeBuilder);
			return enumBuilder;
		}

		/// <summary>Gets the named type defined in the module.</summary>
		/// <returns>The requested type, if the type is defined in this module; otherwise, null.</returns>
		/// <param name="className">The name of the <see cref="T:System.Type" /> to get. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="className" /> is zero or is greater than 1023. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The requested <see cref="T:System.Type" /> is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect non-public objects outside the current assembly. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">An error is encountered while loading the <see cref="T:System.Type" />. </exception>
		// Token: 0x0600330A RID: 13066 RVA: 0x000B223A File Offset: 0x000B043A
		[ComVisible(true)]
		public override Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		/// <summary>Gets the named type defined in the module, optionally ignoring the case of the type name.</summary>
		/// <returns>The requested type, if the type is defined in this module; otherwise, null.</returns>
		/// <param name="className">The name of the <see cref="T:System.Type" /> to get. </param>
		/// <param name="ignoreCase">If true, the search is case-insensitive. If false, the search is case-sensitive. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="className" /> is zero or is greater than 1023. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The requested <see cref="T:System.Type" /> is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect non-public objects outside the current assembly. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		// Token: 0x0600330B RID: 13067 RVA: 0x000B2245 File Offset: 0x000B0445
		[ComVisible(true)]
		public override Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000BED1C File Offset: 0x000BCF1C
		private TypeBuilder search_in_array(TypeBuilder[] arr, int validElementsInArray, TypeName className)
		{
			for (int i = 0; i < validElementsInArray; i++)
			{
				if (string.Compare(className.DisplayName, arr[i].FullName, true, CultureInfo.InvariantCulture) == 0)
				{
					return arr[i];
				}
			}
			return null;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000BED58 File Offset: 0x000BCF58
		private TypeBuilder search_nested_in_array(TypeBuilder[] arr, int validElementsInArray, TypeName className)
		{
			for (int i = 0; i < validElementsInArray; i++)
			{
				if (string.Compare(className.DisplayName, arr[i].Name, true, CultureInfo.InvariantCulture) == 0)
				{
					return arr[i];
				}
			}
			return null;
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000BED94 File Offset: 0x000BCF94
		private TypeBuilder GetMaybeNested(TypeBuilder t, IEnumerable<TypeName> nested)
		{
			TypeBuilder typeBuilder = t;
			foreach (TypeName typeName in nested)
			{
				if (typeBuilder.subtypes == null)
				{
					return null;
				}
				typeBuilder = this.search_nested_in_array(typeBuilder.subtypes, typeBuilder.subtypes.Length, typeName);
				if (typeBuilder == null)
				{
					return null;
				}
			}
			return typeBuilder;
		}

		/// <summary>Gets the named type defined in the module, optionally ignoring the case of the type name. Optionally throws an exception if the type is not found.</summary>
		/// <returns>The specified type, if the type is declared in this module; otherwise, null.</returns>
		/// <param name="className">The name of the <see cref="T:System.Type" /> to get. </param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. </param>
		/// <param name="ignoreCase">If true, the search is case-insensitive. If false, the search is case-sensitive. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="className" /> is zero or is greater than 1023. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The requested <see cref="T:System.Type" /> is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect non-public objects outside the current assembly. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the specified type is not found. </exception>
		// Token: 0x0600330F RID: 13071 RVA: 0x000BEE0C File Offset: 0x000BD00C
		[ComVisible(true)]
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			if (className.Length == 0)
			{
				throw new ArgumentException("className");
			}
			TypeBuilder typeBuilder = null;
			if (this.types == null && throwOnError)
			{
				throw new TypeLoadException(className);
			}
			TypeSpec typeSpec = TypeSpec.Parse(className);
			if (!ignoreCase)
			{
				TypeName typeName = typeSpec.TypeNameWithoutModifiers();
				this.name_cache.TryGetValue(typeName, out typeBuilder);
			}
			else
			{
				if (this.types != null)
				{
					typeBuilder = this.search_in_array(this.types, this.num_types, typeSpec.Name);
				}
				if (!typeSpec.IsNested && typeBuilder != null)
				{
					typeBuilder = this.GetMaybeNested(typeBuilder, typeSpec.Nested);
				}
			}
			if (typeBuilder == null && throwOnError)
			{
				throw new TypeLoadException(className);
			}
			if (typeBuilder != null && (typeSpec.HasModifiers || typeSpec.IsByRef))
			{
				Type type = typeBuilder;
				if (typeBuilder != null)
				{
					TypeBuilder typeBuilder2 = typeBuilder;
					if (typeBuilder2.is_created)
					{
						type = typeBuilder2.CreateType();
					}
				}
				foreach (ModifierSpec modifierSpec in typeSpec.Modifiers)
				{
					if (modifierSpec is PointerSpec)
					{
						type = type.MakePointerType();
					}
					else if (modifierSpec is ArraySpec)
					{
						ArraySpec arraySpec = modifierSpec as ArraySpec;
						if (arraySpec.IsBound)
						{
							return null;
						}
						if (arraySpec.Rank == 1)
						{
							type = type.MakeArrayType();
						}
						else
						{
							type = type.MakeArrayType(arraySpec.Rank);
						}
					}
				}
				if (typeSpec.IsByRef)
				{
					type = type.MakeByRefType();
				}
				typeBuilder = type as TypeBuilder;
				if (typeBuilder == null)
				{
					return type;
				}
			}
			IL_0186:
			if (typeBuilder != null && typeBuilder.is_created)
			{
				return typeBuilder.CreateType();
			}
			return typeBuilder;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000BEFCC File Offset: 0x000BD1CC
		internal int get_next_table_index(object obj, int table, int count)
		{
			if (this.table_indexes == null)
			{
				this.table_indexes = new int[64];
				for (int i = 0; i < 64; i++)
				{
					this.table_indexes[i] = 1;
				}
				this.table_indexes[2] = 2;
			}
			int num = this.table_indexes[table];
			this.table_indexes[table] += count;
			return num;
		}

		/// <summary>Applies a custom attribute to this module by using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class that specifies the custom attribute to apply. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is null. </exception>
		// Token: 0x06003311 RID: 13073 RVA: 0x000BF028 File Offset: 0x000BD228
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
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

		/// <summary>Applies a custom attribute to this module by using a specified binary large object (BLOB) that represents the attribute.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte BLOB representing the attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		// Token: 0x06003312 RID: 13074 RVA: 0x000BF082 File Offset: 0x000BD282
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Returns the symbol writer associated with this dynamic module.</summary>
		/// <returns>The symbol writer associated with this dynamic module.</returns>
		// Token: 0x06003313 RID: 13075 RVA: 0x000BF091 File Offset: 0x000BD291
		public ISymbolWriter GetSymWriter()
		{
			return this.symbolWriter;
		}

		/// <summary>Defines a document for source.</summary>
		/// <returns>The defined document.</returns>
		/// <param name="url">The URL for the document. </param>
		/// <param name="language">The GUID that identifies the document language. This can be <see cref="F:System.Guid.Empty" />. </param>
		/// <param name="languageVendor">The GUID that identifies the document language vendor. This can be <see cref="F:System.Guid.Empty" />. </param>
		/// <param name="documentType">The GUID that identifies the document type. This can be <see cref="F:System.Guid.Empty" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. This is a change from earlier versions of the .NET Framework.</exception>
		/// <exception cref="T:System.InvalidOperationException">This method is called on a dynamic module that is not a debug module. </exception>
		// Token: 0x06003314 RID: 13076 RVA: 0x000BF099 File Offset: 0x000BD299
		public ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			if (this.symbolWriter != null)
			{
				return this.symbolWriter.DefineDocument(url, language, languageVendor, documentType);
			}
			return null;
		}

		/// <summary>Returns all the classes defined within this module.</summary>
		/// <returns>An array that contains the types defined within the module that is reflected by this instance.</returns>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003315 RID: 13077 RVA: 0x000BF0B8 File Offset: 0x000BD2B8
		public override Type[] GetTypes()
		{
			if (this.types == null)
			{
				return Type.EmptyTypes;
			}
			int num = this.num_types;
			Type[] array = new Type[num];
			Array.Copy(this.types, array, num);
			for (int i = 0; i < array.Length; i++)
			{
				if (this.types[i].is_created)
				{
					array[i] = this.types[i].CreateType();
				}
			}
			return array;
		}

		/// <summary>Defines the named managed embedded resource with the given attributes that is to be stored in this module.</summary>
		/// <returns>A resource writer for the defined resource.</returns>
		/// <param name="name">The name of the resource. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="description">The description of the resource. </param>
		/// <param name="attribute">The resource attributes. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This module is transient.-or- The containing assembly is not persistable. </exception>
		// Token: 0x06003316 RID: 13078 RVA: 0x000BF11C File Offset: 0x000BD31C
		public IResourceWriter DefineResource(string name, string description, ResourceAttributes attribute)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("name cannot be empty");
			}
			if (this.transient)
			{
				throw new InvalidOperationException("The module is transient");
			}
			if (!this.assemblyb.IsSave)
			{
				throw new InvalidOperationException("The assembly is transient");
			}
			ResourceWriter resourceWriter = new ResourceWriter(new MemoryStream());
			if (this.resource_writers == null)
			{
				this.resource_writers = new Hashtable();
			}
			this.resource_writers[name] = resourceWriter;
			if (this.resources != null)
			{
				MonoResource[] array = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, array, this.resources.Length);
				this.resources = array;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].attrs = attribute;
			return resourceWriter;
		}

		/// <summary>Defines the named managed embedded resource to be stored in this module.</summary>
		/// <returns>A resource writer for the defined resource.</returns>
		/// <param name="name">The name of the resource. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="description">The description of the resource. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This module is transient.-or- The containing assembly is not persistable. </exception>
		// Token: 0x06003317 RID: 13079 RVA: 0x000BF216 File Offset: 0x000BD416
		public IResourceWriter DefineResource(string name, string description)
		{
			return this.DefineResource(name, description, ResourceAttributes.Public);
		}

		/// <summary>Defines an unmanaged embedded resource given an opaque binary large object (BLOB) of bytes.</summary>
		/// <param name="resource">An opaque BLOB that represents an unmanaged resource </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource has already been defined in the module's assembly. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resource" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003318 RID: 13080 RVA: 0x000BF221 File Offset: 0x000BD421
		[MonoTODO]
		public void DefineUnmanagedResource(byte[] resource)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			throw new NotImplementedException();
		}

		/// <summary>Defines an unmanaged resource given the name of Win32 resource file.</summary>
		/// <param name="resourceFileName">The name of the unmanaged resource file. </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource has already been defined in the module's assembly.-or- <paramref name="resourceFileName" /> is the empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceFileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="resourceFileName" /> is not found. -or- <paramref name="resourceFileName" /> is a directory. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003319 RID: 13081 RVA: 0x000BF238 File Offset: 0x000BD438
		[MonoTODO]
		public void DefineUnmanagedResource(string resourceFileName)
		{
			if (resourceFileName == null)
			{
				throw new ArgumentNullException("resourceFileName");
			}
			if (resourceFileName == string.Empty)
			{
				throw new ArgumentException("resourceFileName");
			}
			if (!File.Exists(resourceFileName) || Directory.Exists(resourceFileName))
			{
				throw new FileNotFoundException("File '" + resourceFileName + "' does not exist or is a directory.");
			}
			throw new NotImplementedException();
		}

		/// <summary>Defines a binary large object (BLOB) that represents a manifest resource to be embedded in the dynamic assembly.</summary>
		/// <param name="name">The case-sensitive name for the resource.</param>
		/// <param name="stream">A stream that contains the bytes for the resource.</param>
		/// <param name="attribute">An enumeration value that specifies whether the resource is public or private.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or-<paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a zero-length string.</exception>
		/// <exception cref="T:System.InvalidOperationException">The dynamic assembly that contains the current module is transient; that is, no file name was specified when <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineDynamicModule(System.String,System.String)" /> was called.</exception>
		// Token: 0x0600331A RID: 13082 RVA: 0x000BF298 File Offset: 0x000BD498
		public void DefineManifestResource(string name, Stream stream, ResourceAttributes attribute)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("name cannot be empty");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.transient)
			{
				throw new InvalidOperationException("The module is transient");
			}
			if (!this.assemblyb.IsSave)
			{
				throw new InvalidOperationException("The assembly is transient");
			}
			if (this.resources != null)
			{
				MonoResource[] array = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, array, this.resources.Length);
				this.resources = array;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].attrs = attribute;
			this.resources[num].stream = stream;
		}

		/// <summary>This method does nothing.</summary>
		/// <param name="name">The name of the custom attribute </param>
		/// <param name="data">An opaque binary large object (BLOB) of bytes that represents the value of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		// Token: 0x0600331B RID: 13083 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO]
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the user entry point.</summary>
		/// <param name="entryPoint">The user entry point. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryPoint" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method is called on a dynamic module that is not a debug module.-or- <paramref name="entryPoint" /> is not contained in this dynamic module. </exception>
		// Token: 0x0600331C RID: 13084 RVA: 0x000BF386 File Offset: 0x000BD586
		[MonoTODO]
		public void SetUserEntryPoint(MethodInfo entryPoint)
		{
			if (entryPoint == null)
			{
				throw new ArgumentNullException("entryPoint");
			}
			if (entryPoint.DeclaringType.Module != this)
			{
				throw new InvalidOperationException("entryPoint is not contained in this module");
			}
			throw new NotImplementedException();
		}

		/// <summary>Returns the token used to identify the specified method within this module.</summary>
		/// <returns>The token used to identify the specified method within this module.</returns>
		/// <param name="method">The method to get a token for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The declaring type for the method is not in this module. </exception>
		// Token: 0x0600331D RID: 13085 RVA: 0x000BF3BF File Offset: 0x000BD5BF
		public MethodToken GetMethodToken(MethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return new MethodToken(this.GetToken(method));
		}

		/// <summary>Returns the token used to identify the method that has the specified attributes and parameter types within this module.</summary>
		/// <returns>The token used to identify the specified method within this module.</returns>
		/// <param name="method">The method to get a token for.</param>
		/// <param name="optionalParameterTypes">A collection of the types of the optional parameters to the method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The declaring type for the method is not in this module. </exception>
		// Token: 0x0600331E RID: 13086 RVA: 0x000BF3E1 File Offset: 0x000BD5E1
		public MethodToken GetMethodToken(MethodInfo method, IEnumerable<Type> optionalParameterTypes)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return new MethodToken(this.GetToken(method, optionalParameterTypes));
		}

		/// <summary>Returns the token for the named method on an array class.</summary>
		/// <returns>The token for the named method on an array class.</returns>
		/// <param name="arrayClass">The object for the array. </param>
		/// <param name="methodName">A string that contains the name of the method. </param>
		/// <param name="callingConvention">The calling convention for the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the parameters of the method. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="arrayClass" /> is not an array.-or- The length of <paramref name="methodName" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="arrayClass" /> or <paramref name="methodName" /> is null. </exception>
		// Token: 0x0600331F RID: 13087 RVA: 0x000BF404 File Offset: 0x000BD604
		public MethodToken GetArrayMethodToken(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.GetMethodToken(this.GetArrayMethod(arrayClass, methodName, callingConvention, returnType, parameterTypes));
		}

		/// <summary>Returns the token used to identify the specified constructor within this module.</summary>
		/// <returns>The token used to identify the specified constructor within this module.</returns>
		/// <param name="con">The constructor to get a token for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		// Token: 0x06003320 RID: 13088 RVA: 0x000BF419 File Offset: 0x000BD619
		[ComVisible(true)]
		public MethodToken GetConstructorToken(ConstructorInfo con)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			return new MethodToken(this.GetToken(con));
		}

		/// <summary>Returns the token used to identify the constructor that has the specified attributes and parameter types within this module.</summary>
		/// <returns>The token used to identify the specified constructor within this module.</returns>
		/// <param name="constructor">The constructor to get a token for.</param>
		/// <param name="optionalParameterTypes">A collection of the types of the optional parameters to the constructor.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="constructor" /> is null. </exception>
		// Token: 0x06003321 RID: 13089 RVA: 0x000BF43B File Offset: 0x000BD63B
		public MethodToken GetConstructorToken(ConstructorInfo constructor, IEnumerable<Type> optionalParameterTypes)
		{
			if (constructor == null)
			{
				throw new ArgumentNullException("constructor");
			}
			return new MethodToken(this.GetToken(constructor, optionalParameterTypes));
		}

		/// <summary>Returns the token used to identify the specified field within this module.</summary>
		/// <returns>The token used to identify the specified field within this module.</returns>
		/// <param name="field">The field to get a token for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="field" /> is null. </exception>
		// Token: 0x06003322 RID: 13090 RVA: 0x000BF45E File Offset: 0x000BD65E
		public FieldToken GetFieldToken(FieldInfo field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			return new FieldToken(this.GetToken(field));
		}

		/// <summary>Defines a token for the signature that has the specified character array and signature length.</summary>
		/// <returns>A token for the specified signature.</returns>
		/// <param name="sigBytes">The signature binary large object (BLOB). </param>
		/// <param name="sigLength">The length of the signature BLOB. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sigBytes" /> is null. </exception>
		// Token: 0x06003323 RID: 13091 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO]
		public SignatureToken GetSignatureToken(byte[] sigBytes, int sigLength)
		{
			throw new NotImplementedException();
		}

		/// <summary>Defines a token for the signature that is defined by the specified <see cref="T:System.Reflection.Emit.SignatureHelper" />.</summary>
		/// <returns>A token for the defined signature.</returns>
		/// <param name="sigHelper">The signature. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sigHelper" /> is null. </exception>
		// Token: 0x06003324 RID: 13092 RVA: 0x000BF480 File Offset: 0x000BD680
		public SignatureToken GetSignatureToken(SignatureHelper sigHelper)
		{
			if (sigHelper == null)
			{
				throw new ArgumentNullException("sigHelper");
			}
			return new SignatureToken(this.GetToken(sigHelper));
		}

		/// <summary>Returns the token of the given string in the module’s constant pool.</summary>
		/// <returns>The token of the string in the constant pool.</returns>
		/// <param name="str">The string to add to the module's constant pool. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x06003325 RID: 13093 RVA: 0x000BF49C File Offset: 0x000BD69C
		public StringToken GetStringConstant(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return new StringToken(this.GetToken(str));
		}

		/// <summary>Returns the token used to identify the specified type within this module.</summary>
		/// <returns>The token used to identify the given type within this module.</returns>
		/// <param name="type">The type object that represents the class type. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is a ByRef type. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This is a non-transient module that references a transient module. </exception>
		// Token: 0x06003326 RID: 13094 RVA: 0x000BF4B8 File Offset: 0x000BD6B8
		public TypeToken GetTypeToken(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsByRef)
			{
				throw new ArgumentException("type can't be a byref type", "type");
			}
			if (!this.IsTransient() && type.Module is ModuleBuilder && ((ModuleBuilder)type.Module).IsTransient())
			{
				throw new InvalidOperationException("a non-transient module can't reference a transient module");
			}
			return new TypeToken(this.GetToken(type));
		}

		/// <summary>Returns the token used to identify the type with the specified name.</summary>
		/// <returns>The token used to identify the type with the specified name within this module.</returns>
		/// <param name="name">The name of the class, including the namespace. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is the empty string ("").-or-<paramref name="name" /> represents a ByRef type. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or-The type specified by <paramref name="name" /> could not be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">This is a non-transient module that references a transient module. </exception>
		// Token: 0x06003327 RID: 13095 RVA: 0x000BF52F File Offset: 0x000BD72F
		public TypeToken GetTypeToken(string name)
		{
			return this.GetTypeToken(this.GetType(name));
		}

		// Token: 0x06003328 RID: 13096
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int getUSIndex(ModuleBuilder mb, string str);

		// Token: 0x06003329 RID: 13097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int getToken(ModuleBuilder mb, object obj, bool create_open_instance);

		// Token: 0x0600332A RID: 13098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int getMethodToken(ModuleBuilder mb, MethodBase method, Type[] opt_param_types);

		// Token: 0x0600332B RID: 13099 RVA: 0x000BF540 File Offset: 0x000BD740
		internal int GetToken(string str)
		{
			int usindex;
			if (!this.us_string_cache.TryGetValue(str, out usindex))
			{
				usindex = ModuleBuilder.getUSIndex(this, str);
				this.us_string_cache[str] = usindex;
			}
			return usindex;
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000BF574 File Offset: 0x000BD774
		private int GetPseudoToken(MemberInfo member, bool create_open_instance)
		{
			Dictionary<MemberInfo, int> dictionary = (create_open_instance ? this.inst_tokens_open : this.inst_tokens);
			int num;
			if (dictionary == null)
			{
				dictionary = new Dictionary<MemberInfo, int>(ReferenceEqualityComparer<MemberInfo>.Instance);
				if (create_open_instance)
				{
					this.inst_tokens_open = dictionary;
				}
				else
				{
					this.inst_tokens = dictionary;
				}
			}
			else if (dictionary.TryGetValue(member, out num))
			{
				return num;
			}
			if (member is TypeBuilderInstantiation || member is SymbolType)
			{
				num = ModuleBuilder.typespec_tokengen--;
			}
			else if (member is FieldOnTypeBuilderInst)
			{
				num = ModuleBuilder.memberref_tokengen--;
			}
			else if (member is ConstructorOnTypeBuilderInst)
			{
				num = ModuleBuilder.memberref_tokengen--;
			}
			else if (member is MethodOnTypeBuilderInst)
			{
				num = ModuleBuilder.memberref_tokengen--;
			}
			else if (member is FieldBuilder)
			{
				num = ModuleBuilder.memberref_tokengen--;
			}
			else if (member is TypeBuilder)
			{
				if (create_open_instance && (member as TypeBuilder).ContainsGenericParameters)
				{
					num = ModuleBuilder.typespec_tokengen--;
				}
				else if (member.Module == this)
				{
					num = ModuleBuilder.typedef_tokengen--;
				}
				else
				{
					num = ModuleBuilder.typeref_tokengen--;
				}
			}
			else
			{
				if (member is EnumBuilder)
				{
					num = this.GetPseudoToken((member as EnumBuilder).GetTypeBuilder(), create_open_instance);
					dictionary[member] = num;
					return num;
				}
				if (member is ConstructorBuilder)
				{
					if (member.Module == this && !(member as ConstructorBuilder).TypeBuilder.ContainsGenericParameters)
					{
						num = ModuleBuilder.methoddef_tokengen--;
					}
					else
					{
						num = ModuleBuilder.memberref_tokengen--;
					}
				}
				else if (member is MethodBuilder)
				{
					MethodBuilder methodBuilder = member as MethodBuilder;
					if (member.Module == this && !methodBuilder.TypeBuilder.ContainsGenericParameters && !methodBuilder.IsGenericMethodDefinition)
					{
						num = ModuleBuilder.methoddef_tokengen--;
					}
					else
					{
						num = ModuleBuilder.memberref_tokengen--;
					}
				}
				else
				{
					if (!(member is GenericTypeParameterBuilder))
					{
						throw new NotImplementedException();
					}
					num = ModuleBuilder.typespec_tokengen--;
				}
			}
			dictionary[member] = num;
			this.RegisterToken(member, num);
			return num;
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000BF7A2 File Offset: 0x000BD9A2
		internal int GetToken(MemberInfo member)
		{
			if (member is ConstructorBuilder || member is MethodBuilder || member is FieldBuilder)
			{
				return this.GetPseudoToken(member, false);
			}
			return ModuleBuilder.getToken(this, member, true);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000BF7D0 File Offset: 0x000BD9D0
		internal int GetToken(MemberInfo member, bool create_open_instance)
		{
			if (member is TypeBuilderInstantiation || member is FieldOnTypeBuilderInst || member is ConstructorOnTypeBuilderInst || member is MethodOnTypeBuilderInst || member is SymbolType || member is FieldBuilder || member is TypeBuilder || member is ConstructorBuilder || member is MethodBuilder || member is GenericTypeParameterBuilder || member is EnumBuilder)
			{
				return this.GetPseudoToken(member, create_open_instance);
			}
			return ModuleBuilder.getToken(this, member, create_open_instance);
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000BF848 File Offset: 0x000BDA48
		internal int GetToken(MethodBase method, IEnumerable<Type> opt_param_types)
		{
			if (method is ConstructorBuilder || method is MethodBuilder)
			{
				return this.GetPseudoToken(method, false);
			}
			if (opt_param_types == null)
			{
				return ModuleBuilder.getToken(this, method, true);
			}
			List<Type> list = new List<Type>(opt_param_types);
			return ModuleBuilder.getMethodToken(this, method, list.ToArray());
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000BF88E File Offset: 0x000BDA8E
		internal int GetToken(MethodBase method, Type[] opt_param_types)
		{
			if (method is ConstructorBuilder || method is MethodBuilder)
			{
				return this.GetPseudoToken(method, false);
			}
			return ModuleBuilder.getMethodToken(this, method, opt_param_types);
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000BF8B1 File Offset: 0x000BDAB1
		internal int GetToken(SignatureHelper helper)
		{
			return ModuleBuilder.getToken(this, helper, true);
		}

		// Token: 0x06003332 RID: 13106
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RegisterToken(object obj, int token);

		// Token: 0x06003333 RID: 13107
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object GetRegisteredToken(int token);

		// Token: 0x06003334 RID: 13108 RVA: 0x000BF8BB File Offset: 0x000BDABB
		internal TokenGenerator GetTokenGenerator()
		{
			if (this.token_gen == null)
			{
				this.token_gen = new ModuleBuilderTokenGenerator(this);
			}
			return this.token_gen;
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000BF8D8 File Offset: 0x000BDAD8
		internal static object RuntimeResolve(object obj)
		{
			if (obj is MethodBuilder)
			{
				return (obj as MethodBuilder).RuntimeResolve();
			}
			if (obj is ConstructorBuilder)
			{
				return (obj as ConstructorBuilder).RuntimeResolve();
			}
			if (obj is FieldBuilder)
			{
				return (obj as FieldBuilder).RuntimeResolve();
			}
			if (obj is GenericTypeParameterBuilder)
			{
				return (obj as GenericTypeParameterBuilder).RuntimeResolve();
			}
			if (obj is FieldOnTypeBuilderInst)
			{
				return (obj as FieldOnTypeBuilderInst).RuntimeResolve();
			}
			if (obj is MethodOnTypeBuilderInst)
			{
				return (obj as MethodOnTypeBuilderInst).RuntimeResolve();
			}
			if (obj is ConstructorOnTypeBuilderInst)
			{
				return (obj as ConstructorOnTypeBuilderInst).RuntimeResolve();
			}
			if (obj is Type)
			{
				return (obj as Type).RuntimeResolve();
			}
			throw new NotImplementedException(obj.GetType().FullName);
		}

		// Token: 0x06003336 RID: 13110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void build_metadata(ModuleBuilder mb);

		// Token: 0x06003337 RID: 13111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void WriteToFile(IntPtr handle);

		// Token: 0x06003338 RID: 13112 RVA: 0x000BF998 File Offset: 0x000BDB98
		private void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map, Dictionary<MemberInfo, int> inst_tokens, bool open)
		{
			foreach (KeyValuePair<MemberInfo, int> keyValuePair in inst_tokens)
			{
				MemberInfo key = keyValuePair.Key;
				int value = keyValuePair.Value;
				MemberInfo memberInfo;
				if (key is TypeBuilderInstantiation || key is SymbolType)
				{
					memberInfo = (key as Type).RuntimeResolve();
				}
				else if (key is FieldOnTypeBuilderInst)
				{
					memberInfo = (key as FieldOnTypeBuilderInst).RuntimeResolve();
				}
				else if (key is ConstructorOnTypeBuilderInst)
				{
					memberInfo = (key as ConstructorOnTypeBuilderInst).RuntimeResolve();
				}
				else if (key is MethodOnTypeBuilderInst)
				{
					memberInfo = (key as MethodOnTypeBuilderInst).RuntimeResolve();
				}
				else if (key is FieldBuilder)
				{
					memberInfo = (key as FieldBuilder).RuntimeResolve();
				}
				else if (key is TypeBuilder)
				{
					memberInfo = (key as TypeBuilder).RuntimeResolve();
				}
				else if (key is EnumBuilder)
				{
					memberInfo = (key as EnumBuilder).RuntimeResolve();
				}
				else if (key is ConstructorBuilder)
				{
					memberInfo = (key as ConstructorBuilder).RuntimeResolve();
				}
				else if (key is MethodBuilder)
				{
					memberInfo = (key as MethodBuilder).RuntimeResolve();
				}
				else
				{
					if (!(key is GenericTypeParameterBuilder))
					{
						throw new NotImplementedException();
					}
					memberInfo = (key as GenericTypeParameterBuilder).RuntimeResolve();
				}
				int num = this.GetToken(memberInfo, open);
				token_map[value] = num;
				member_map[value] = memberInfo;
				this.RegisterToken(memberInfo, value);
			}
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000BFB2C File Offset: 0x000BDD2C
		private void FixupTokens()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Dictionary<int, MemberInfo> dictionary2 = new Dictionary<int, MemberInfo>();
			if (this.inst_tokens != null)
			{
				this.FixupTokens(dictionary, dictionary2, this.inst_tokens, false);
			}
			if (this.inst_tokens_open != null)
			{
				this.FixupTokens(dictionary, dictionary2, this.inst_tokens_open, true);
			}
			if (this.types != null)
			{
				for (int i = 0; i < this.num_types; i++)
				{
					this.types[i].FixupTokens(dictionary, dictionary2);
				}
			}
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000BFB9C File Offset: 0x000BDD9C
		internal void Save()
		{
			if (this.transient && !this.is_main)
			{
				return;
			}
			if (this.types != null)
			{
				for (int i = 0; i < this.num_types; i++)
				{
					if (!this.types[i].is_created)
					{
						throw new NotSupportedException("Type '" + this.types[i].FullName + "' was not completed.");
					}
				}
			}
			this.FixupTokens();
			if (this.global_type != null && this.global_type_created == null)
			{
				this.global_type_created = this.global_type.CreateType();
			}
			if (this.resources != null)
			{
				for (int j = 0; j < this.resources.Length; j++)
				{
					IResourceWriter resourceWriter;
					if (this.resource_writers != null && (resourceWriter = this.resource_writers[this.resources[j].name] as IResourceWriter) != null)
					{
						ResourceWriter resourceWriter2 = (ResourceWriter)resourceWriter;
						resourceWriter2.Generate();
						MemoryStream memoryStream = (MemoryStream)resourceWriter2._output;
						this.resources[j].data = new byte[memoryStream.Length];
						memoryStream.Seek(0L, SeekOrigin.Begin);
						memoryStream.Read(this.resources[j].data, 0, (int)memoryStream.Length);
					}
					else
					{
						Stream stream = this.resources[j].stream;
						if (stream != null)
						{
							try
							{
								long length = stream.Length;
								this.resources[j].data = new byte[length];
								stream.Seek(0L, SeekOrigin.Begin);
								stream.Read(this.resources[j].data, 0, (int)length);
							}
							catch
							{
							}
						}
					}
				}
			}
			ModuleBuilder.build_metadata(this);
			string text = this.fqname;
			if (this.assemblyb.AssemblyDir != null)
			{
				text = Path.Combine(this.assemblyb.AssemblyDir, text);
			}
			try
			{
				File.Delete(text);
			}
			catch
			{
			}
			using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
			{
				this.WriteToFile(fileStream.Handle);
			}
			File.SetAttributes(text, (FileAttributes)(-2147483648));
			if (this.types != null && this.symbolWriter != null)
			{
				for (int k = 0; k < this.num_types; k++)
				{
					this.types[k].GenerateDebugInfo(this.symbolWriter);
				}
				this.symbolWriter.Close();
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000BFE2C File Offset: 0x000BE02C
		internal string FileName
		{
			get
			{
				return this.fqname;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (set) Token: 0x0600333C RID: 13116 RVA: 0x000BFE34 File Offset: 0x000BE034
		internal bool IsMain
		{
			set
			{
				this.is_main = value;
			}
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000BFE3D File Offset: 0x000BE03D
		internal void CreateGlobalType()
		{
			if (this.global_type == null)
			{
				this.global_type = new TypeBuilder(this, TypeAttributes.NotPublic, 1);
			}
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000BFE5B File Offset: 0x000BE05B
		internal override Guid GetModuleVersionId()
		{
			return new Guid(this.guid);
		}

		/// <summary>Gets the dynamic assembly that defined this instance of <see cref="T:System.Reflection.Emit.ModuleBuilder" />.</summary>
		/// <returns>The dynamic assembly that defined the current dynamic module.</returns>
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000BFE68 File Offset: 0x000BE068
		public override Assembly Assembly
		{
			get
			{
				return this.assemblyb;
			}
		}

		/// <summary>A string that indicates that this is an in-memory module.</summary>
		/// <returns>Text that indicates that this is an in-memory module.</returns>
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x000BFE70 File Offset: 0x000BE070
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a string that represents the name of the dynamic module.</summary>
		/// <returns>The name of the dynamic module.</returns>
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000BFE70 File Offset: 0x000BE070
		public override string ScopeName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a universally unique identifier (UUID) that can be used to distinguish between two versions of a module.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that can be used to distinguish between two versions of a module.</returns>
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000B6DEC File Offset: 0x000B4FEC
		public override Guid ModuleVersionId
		{
			get
			{
				return this.GetModuleVersionId();
			}
		}

		/// <summary>Gets a value indicating whether the object is a resource.</summary>
		/// <returns>true if the object is a resource; otherwise, false.</returns>
		// Token: 0x06003343 RID: 13123 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool IsResource()
		{
			return false;
		}

		/// <summary>Returns the module-level method that matches the specified criteria.</summary>
		/// <returns>A method that is defined at the module level, and matches the specified criteria; or null if such a method does not exist.</returns>
		/// <param name="name">The method name. </param>
		/// <param name="bindingAttr">A combination of BindingFlags bit flags used to control the search. </param>
		/// <param name="binder">An object that implements Binder, containing properties related to this method. </param>
		/// <param name="callConvention">The calling convention for the method. </param>
		/// <param name="types">The parameter types of the method. </param>
		/// <param name="modifiers">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null, <paramref name="types" /> is null, or an element of <paramref name="types" /> is null. </exception>
		// Token: 0x06003344 RID: 13124 RVA: 0x000BFE78 File Offset: 0x000BE078
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (this.global_type_created == null)
			{
				return null;
			}
			if (types == null)
			{
				return this.global_type_created.GetMethod(name);
			}
			return this.global_type_created.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns the field identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a field in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a field in the scope of the current module.-or-<paramref name="metadataToken" /> identifies a field whose parent TypeSpec has a signature containing element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06003345 RID: 13125 RVA: 0x000BFEAF File Offset: 0x000BE0AF
		public override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveField(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		/// <summary>Returns the type or member identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberInfo" /> object representing the type or member that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type or member in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type or member in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec or TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.-or-<paramref name="metadataToken" /> identifies a property or event.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06003346 RID: 13126 RVA: 0x000BFEC0 File Offset: 0x000BE0C0
		public override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveMember(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000BFED4 File Offset: 0x000BE0D4
		internal MemberInfo ResolveOrGetRegisteredToken(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			MemberInfo memberInfo = RuntimeModule.ResolveMemberToken(this._impl, metadataToken, RuntimeModule.ptrs_from_types(genericTypeArguments), RuntimeModule.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (memberInfo != null)
			{
				return memberInfo;
			}
			memberInfo = this.GetRegisteredToken(metadataToken) as MemberInfo;
			if (memberInfo == null)
			{
				throw RuntimeModule.resolve_token_exception(this.Name, metadataToken, resolveTokenError, "MemberInfo");
			}
			return memberInfo;
		}

		/// <summary>Returns the method or constructor identified by the specified metadata token, in the context defined by the specified generic type parameters. </summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06003348 RID: 13128 RVA: 0x000BFF31 File Offset: 0x000BE131
		public override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveMethod(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		/// <summary>Returns the string identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a string value from the metadata string heap.</returns>
		/// <param name="metadataToken">A metadata token that identifies a string in the string heap of the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a string in the scope of the current module. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06003349 RID: 13129 RVA: 0x000BFF42 File Offset: 0x000BE142
		public override string ResolveString(int metadataToken)
		{
			return RuntimeModule.ResolveString(this, this._impl, metadataToken);
		}

		/// <summary>Returns the signature blob identified by a metadata token.</summary>
		/// <returns>An array of bytes representing the signature blob.</returns>
		/// <param name="metadataToken">A metadata token that identifies a signature in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a valid MemberRef, MethodDef, TypeSpec, signature, or FieldDef token in the scope of the current module.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600334A RID: 13130 RVA: 0x000BFF51 File Offset: 0x000BE151
		public override byte[] ResolveSignature(int metadataToken)
		{
			return RuntimeModule.ResolveSignature(this, this._impl, metadataToken);
		}

		/// <summary>Returns the type identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600334B RID: 13131 RVA: 0x000BFF60 File Offset: 0x000BE160
		public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return RuntimeModule.ResolveType(this, this._impl, metadataToken, genericTypeArguments, genericMethodArguments);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to the specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x0600334C RID: 13132 RVA: 0x000BFF71 File Offset: 0x000BE171
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600334D RID: 13133 RVA: 0x000BFF7A File Offset: 0x000BE17A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether the specified attribute type has been applied to this module.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" /> have been applied to this module; otherwise, false.</returns>
		/// <param name="attributeType">The type of custom attribute to test for.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x0600334E RID: 13134 RVA: 0x000BFF82 File Offset: 0x000BE182
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return base.IsDefined(attributeType, inherit);
		}

		/// <summary>Returns all the custom attributes that have been applied to the current <see cref="T:System.Reflection.Emit.ModuleBuilder" />.</summary>
		/// <returns>An array that contains the custom attributes; the array is empty if there are no attributes.</returns>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		// Token: 0x0600334F RID: 13135 RVA: 0x000BFF8C File Offset: 0x000BE18C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.GetCustomAttributes(null, inherit);
		}

		/// <summary>Returns all the custom attributes that have been applied to the current <see cref="T:System.Reflection.Emit.ModuleBuilder" />, and that derive from a specified attribute type.</summary>
		/// <returns>An array that contains the custom attributes that are derived, at any level, from <paramref name="attributeType" />; the array is empty if there are no such attributes.</returns>
		/// <param name="attributeType">The base type from which attributes derive.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x06003350 RID: 13136 RVA: 0x000BFF98 File Offset: 0x000BE198
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.cattrs == null || this.cattrs.Length == 0)
			{
				return Array.Empty<object>();
			}
			if (attributeType is TypeBuilder)
			{
				throw new InvalidOperationException("First argument to GetCustomAttributes can't be a TypeBuilder");
			}
			List<object> list = new List<object>();
			for (int i = 0; i < this.cattrs.Length; i++)
			{
				Type type = this.cattrs[i].Ctor.GetType();
				if (type is TypeBuilder)
				{
					throw new InvalidOperationException("Can't construct custom attribute for TypeBuilder type");
				}
				if (attributeType == null || attributeType.IsAssignableFrom(type))
				{
					list.Add(this.cattrs[i].Invoke());
				}
			}
			return list.ToArray();
		}

		/// <summary>Returns a module-level field, defined in the .sdata region of the portable executable (PE) file, that has the specified name and binding attributes.</summary>
		/// <returns>A field that has the specified name and binding attributes, or null if the field does not exist.</returns>
		/// <param name="name">The field name. </param>
		/// <param name="bindingAttr">A combination of the BindingFlags bit flags used to control the search. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003351 RID: 13137 RVA: 0x000C0039 File Offset: 0x000BE239
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (this.global_type_created == null)
			{
				throw new InvalidOperationException("Module-level fields cannot be retrieved until after the CreateGlobalFunctions method has been called for the module.");
			}
			return this.global_type_created.GetField(name, bindingAttr);
		}

		/// <summary>Returns all fields defined in the .sdata region of the portable executable (PE) file that match the specified binding flags.</summary>
		/// <returns>An array of fields that match the specified flags; the array is empty if no such fields exist.</returns>
		/// <param name="bindingFlags">A combination of the BindingFlags bit flags used to control the search.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003352 RID: 13138 RVA: 0x000C0061 File Offset: 0x000BE261
		public override FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			if (this.global_type_created == null)
			{
				throw new InvalidOperationException("Module-level fields cannot be retrieved until after the CreateGlobalFunctions method has been called for the module.");
			}
			return this.global_type_created.GetFields(bindingFlags);
		}

		/// <summary>Returns all the methods that have been defined at the module level for the current <see cref="T:System.Reflection.Emit.ModuleBuilder" />, and that match the specified binding flags.</summary>
		/// <returns>An array that contains all the module-level methods that match <paramref name="bindingFlags" />.</returns>
		/// <param name="bindingFlags">A combination of BindingFlags bit flags used to control the search.</param>
		// Token: 0x06003353 RID: 13139 RVA: 0x000C0088 File Offset: 0x000BE288
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			if (this.global_type_created == null)
			{
				throw new InvalidOperationException("Module-level methods cannot be retrieved until after the CreateGlobalFunctions method has been called for the module.");
			}
			return this.global_type_created.GetMethods(bindingFlags);
		}

		/// <summary>Gets a token that identifies the current dynamic module in metadata.</summary>
		/// <returns>An integer token that identifies the current module in metadata.</returns>
		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x000B6E9F File Offset: 0x000B509F
		public override int MetadataToken
		{
			get
			{
				return RuntimeModule.get_MetadataToken(this);
			}
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000176B9 File Offset: 0x000158B9
		internal ModuleBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040019C8 RID: 6600
		internal IntPtr _impl;

		// Token: 0x040019C9 RID: 6601
		internal Assembly assembly;

		// Token: 0x040019CA RID: 6602
		internal string fqname;

		// Token: 0x040019CB RID: 6603
		internal string name;

		// Token: 0x040019CC RID: 6604
		internal string scopename;

		// Token: 0x040019CD RID: 6605
		internal bool is_resource;

		// Token: 0x040019CE RID: 6606
		internal int token;

		// Token: 0x040019CF RID: 6607
		private UIntPtr dynamic_image;

		// Token: 0x040019D0 RID: 6608
		private int num_types;

		// Token: 0x040019D1 RID: 6609
		private TypeBuilder[] types;

		// Token: 0x040019D2 RID: 6610
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040019D3 RID: 6611
		private byte[] guid;

		// Token: 0x040019D4 RID: 6612
		private int table_idx;

		// Token: 0x040019D5 RID: 6613
		internal AssemblyBuilder assemblyb;

		// Token: 0x040019D6 RID: 6614
		private MethodBuilder[] global_methods;

		// Token: 0x040019D7 RID: 6615
		private FieldBuilder[] global_fields;

		// Token: 0x040019D8 RID: 6616
		private bool is_main;

		// Token: 0x040019D9 RID: 6617
		private MonoResource[] resources;

		// Token: 0x040019DA RID: 6618
		private IntPtr unparented_classes;

		// Token: 0x040019DB RID: 6619
		private int[] table_indexes;

		// Token: 0x040019DC RID: 6620
		private TypeBuilder global_type;

		// Token: 0x040019DD RID: 6621
		private Type global_type_created;

		// Token: 0x040019DE RID: 6622
		private Dictionary<TypeName, TypeBuilder> name_cache;

		// Token: 0x040019DF RID: 6623
		private Dictionary<string, int> us_string_cache;

		// Token: 0x040019E0 RID: 6624
		private bool transient;

		// Token: 0x040019E1 RID: 6625
		private ModuleBuilderTokenGenerator token_gen;

		// Token: 0x040019E2 RID: 6626
		private Hashtable resource_writers;

		// Token: 0x040019E3 RID: 6627
		private ISymbolWriter symbolWriter;

		// Token: 0x040019E4 RID: 6628
		private static bool has_warned_about_symbolWriter;

		// Token: 0x040019E5 RID: 6629
		private static int typeref_tokengen = 33554431;

		// Token: 0x040019E6 RID: 6630
		private static int typedef_tokengen = 50331647;

		// Token: 0x040019E7 RID: 6631
		private static int typespec_tokengen = 469762047;

		// Token: 0x040019E8 RID: 6632
		private static int memberref_tokengen = 184549375;

		// Token: 0x040019E9 RID: 6633
		private static int methoddef_tokengen = 117440511;

		// Token: 0x040019EA RID: 6634
		private Dictionary<MemberInfo, int> inst_tokens;

		// Token: 0x040019EB RID: 6635
		private Dictionary<MemberInfo, int> inst_tokens_open;
	}
}
