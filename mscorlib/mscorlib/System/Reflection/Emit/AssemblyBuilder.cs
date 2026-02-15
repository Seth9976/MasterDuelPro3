using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;
using Mono.Security;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a dynamic assembly.</summary>
	// Token: 0x02000656 RID: 1622
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_AssemblyBuilder))]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AssemblyBuilder : Assembly, _AssemblyBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060030DB RID: 12507 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _AssemblyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060030DC RID: 12508 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _AssemblyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060030DD RID: 12509 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _AssemblyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060030DE RID: 12510 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _AssemblyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060030DF RID: 12511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void basic_init(AssemblyBuilder ab);

		// Token: 0x060030E0 RID: 12512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateNativeCustomAttributes(AssemblyBuilder ab);

		// Token: 0x060030E1 RID: 12513 RVA: 0x000B818C File Offset: 0x000B638C
		internal AssemblyBuilder(AssemblyName n, string directory, AssemblyBuilderAccess access, bool corlib_internal)
		{
			this.pekind = PEFileKinds.Dll;
			this.corlib_object_type = typeof(object);
			this.corlib_value_type = typeof(ValueType);
			this.corlib_enum_type = typeof(Enum);
			this.corlib_void_type = typeof(void);
			base..ctor();
			if ((access & (AssemblyBuilderAccess)2048) != (AssemblyBuilderAccess)0)
			{
				throw new NotImplementedException("COMPILER_ACCESS is no longer supperted, use a newer mcs.");
			}
			if (!Enum.IsDefined(typeof(AssemblyBuilderAccess), access))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument value {0} is not valid.", (int)access), "access");
			}
			this.name = n.Name;
			this.access = (uint)access;
			this.flags = (uint)n.Flags;
			if (this.IsSave && (directory == null || directory.Length == 0))
			{
				this.dir = Directory.GetCurrentDirectory();
			}
			else
			{
				this.dir = directory;
			}
			if (n.CultureInfo != null)
			{
				this.culture = n.CultureInfo.Name;
				this.versioninfo_culture = n.CultureInfo.Name;
			}
			Version version = n.Version;
			if (version != null)
			{
				this.version = version.ToString();
			}
			if (n.KeyPair != null)
			{
				this.sn = n.KeyPair.StrongName();
			}
			else
			{
				byte[] publicKey = n.GetPublicKey();
				if (publicKey != null && publicKey.Length != 0)
				{
					this.sn = new Mono.Security.StrongName(publicKey);
				}
			}
			if (this.sn != null)
			{
				this.flags |= 1U;
			}
			this.corlib_internal = corlib_internal;
			if (this.sn != null)
			{
				this.pktoken = new byte[this.sn.PublicKeyToken.Length * 2];
				int num = 0;
				foreach (byte b in this.sn.PublicKeyToken)
				{
					string text = b.ToString("x2");
					this.pktoken[num++] = (byte)text[0];
					this.pktoken[num++] = (byte)text[1];
				}
			}
			AssemblyBuilder.basic_init(this);
		}

		/// <summary>Gets the location of the assembly, as specified originally (such as in an <see cref="T:System.Reflection.AssemblyName" /> object).</summary>
		/// <returns>The location of the assembly, as specified originally.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x000B8396 File Offset: 0x000B6596
		public override string CodeBase
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x000B5880 File Offset: 0x000B3A80
		public override string EscapedCodeBase
		{
			get
			{
				return RuntimeAssembly.GetCodeBase(this, true);
			}
		}

		/// <summary>Returns the entry point of this assembly.</summary>
		/// <returns>The entry point of this assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000B839E File Offset: 0x000B659E
		public override MethodInfo EntryPoint
		{
			get
			{
				return this.entry_point;
			}
		}

		/// <summary>Gets the location, in codebase format, of the loaded file that contains the manifest if it is not shadow-copied.</summary>
		/// <returns>The location of the loaded file that contains the manifest. If the loaded file has been shadow-copied, the Location is that of the file before being shadow-copied.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060030E5 RID: 12517 RVA: 0x000B8396 File Offset: 0x000B6596
		public override string Location
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Gets the version of the common language runtime that will be saved in the file containing the manifest.</summary>
		/// <returns>A string representing the common language runtime version.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x000B5891 File Offset: 0x000B3A91
		public override string ImageRuntimeVersion
		{
			get
			{
				return RuntimeAssembly.InternalImageRuntimeVersion(this);
			}
		}

		/// <summary>Gets a value indicating whether the dynamic assembly is in the reflection-only context.</summary>
		/// <returns>true if the dynamic assembly is in the reflection-only context; otherwise, false.</returns>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060030E7 RID: 12519 RVA: 0x000B83A6 File Offset: 0x000B65A6
		public override bool ReflectionOnly
		{
			get
			{
				return this.access == 6U;
			}
		}

		/// <summary>Adds an existing resource file to this assembly.</summary>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path; the file must be in the same directory as the assembly to which it is added. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.-or- There is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="fileName" /> is zero, or if <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> is not found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060030E8 RID: 12520 RVA: 0x000B83B1 File Offset: 0x000B65B1
		public void AddResourceFile(string name, string fileName)
		{
			this.AddResourceFile(name, fileName, ResourceAttributes.Public);
		}

		/// <summary>Adds an existing resource file to this assembly.</summary>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path; the file must be in the same directory as the assembly to which it is added. </param>
		/// <param name="attribute">The resource attributes. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.-or- There is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero or if the length of <paramref name="fileName" /> is zero.-or- <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">If the file <paramref name="fileName" /> is not found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060030E9 RID: 12521 RVA: 0x000B83BC File Offset: 0x000B65BC
		public void AddResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
			this.AddResourceFile(name, fileName, attribute, true);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000B83C8 File Offset: 0x000B65C8
		private void AddResourceFile(string name, string fileName, ResourceAttributes attribute, bool fileNeedsToExists)
		{
			this.check_name_and_filename(name, fileName, fileNeedsToExists);
			if (this.dir != null)
			{
				fileName = Path.Combine(this.dir, fileName);
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
			this.resources[num].filename = fileName;
			this.resources[num].attrs = attribute;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000B8478 File Offset: 0x000B6678
		internal void AddPermissionRequests(PermissionSet required, PermissionSet optional, PermissionSet refused)
		{
			if (this.created)
			{
				throw new InvalidOperationException("Assembly was already saved.");
			}
			this._minimum = required;
			this._optional = optional;
			this._refuse = refused;
			if (required != null)
			{
				this.permissions_minimum = new RefEmitPermissionSet[1];
				this.permissions_minimum[0] = new RefEmitPermissionSet(SecurityAction.RequestMinimum, required.ToXml().ToString());
			}
			if (optional != null)
			{
				this.permissions_optional = new RefEmitPermissionSet[1];
				this.permissions_optional[0] = new RefEmitPermissionSet(SecurityAction.RequestOptional, optional.ToXml().ToString());
			}
			if (refused != null)
			{
				this.permissions_refused = new RefEmitPermissionSet[1];
				this.permissions_refused[0] = new RefEmitPermissionSet(SecurityAction.RequestRefuse, refused.ToXml().ToString());
			}
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000B8533 File Offset: 0x000B6733
		internal void EmbedResourceFile(string name, string fileName)
		{
			this.EmbedResourceFile(name, fileName, ResourceAttributes.Public);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000B8540 File Offset: 0x000B6740
		private void EmbedResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
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
			try
			{
				FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				long length = fileStream.Length;
				this.resources[num].data = new byte[length];
				fileStream.Read(this.resources[num].data, 0, (int)length);
				fileStream.Close();
			}
			catch
			{
			}
		}

		/// <summary>Defines a dynamic assembly that has the specified name and access rights.</summary>
		/// <returns>An object that represents the new assembly.</returns>
		/// <param name="name">The name of the assembly.</param>
		/// <param name="access">The access rights of the assembly.</param>
		// Token: 0x060030EE RID: 12526 RVA: 0x000B861C File Offset: 0x000B681C
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return new AssemblyBuilder(name, null, access, false);
		}

		/// <summary>Defines a new assembly that has the specified name, access rights, and attributes.</summary>
		/// <returns>An object that represents the new assembly.</returns>
		/// <param name="name">The name of the assembly.</param>
		/// <param name="access">The access rights of the assembly.</param>
		/// <param name="assemblyAttributes">A collection that contains the attributes of the assembly.</param>
		// Token: 0x060030EF RID: 12527 RVA: 0x000B8638 File Offset: 0x000B6838
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(name, access);
			foreach (CustomAttributeBuilder customAttributeBuilder in assemblyAttributes)
			{
				assemblyBuilder.SetCustomAttribute(customAttributeBuilder);
			}
			return assemblyBuilder;
		}

		/// <summary>Defines a named transient dynamic module in this assembly.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> begins with white space.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060030F0 RID: 12528 RVA: 0x000B868C File Offset: 0x000B688C
		public ModuleBuilder DefineDynamicModule(string name)
		{
			return this.DefineDynamicModule(name, name, false, true);
		}

		/// <summary>Defines a named transient dynamic module in this assembly and specifies whether symbol information should be emitted.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <param name="emitSymbolInfo">true if symbol information is to be emitted; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> begins with white space.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060030F1 RID: 12529 RVA: 0x000B8698 File Offset: 0x000B6898
		public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, name, emitSymbolInfo, true);
		}

		/// <summary>Defines a persistable dynamic module with the given name that will be saved to the specified file. No symbol information is emitted.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> object representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <param name="fileName">The name of the file to which the dynamic module should be saved. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> or <paramref name="fileName" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260.-or- <paramref name="fileName" /> contains a path specification (a directory component, for example).-or- There is a conflict with the name of another file that belongs to this assembly. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been previously saved. </exception>
		/// <exception cref="T:System.NotSupportedException">This assembly was called on a dynamic assembly with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" /> attribute. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060030F2 RID: 12530 RVA: 0x000B86A4 File Offset: 0x000B68A4
		public ModuleBuilder DefineDynamicModule(string name, string fileName)
		{
			return this.DefineDynamicModule(name, fileName, false, false);
		}

		/// <summary>Defines a persistable dynamic module, specifying the module name, the name of the file to which the module will be saved, and whether symbol information should be emitted using the default symbol writer.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> object representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <param name="fileName">The name of the file to which the dynamic module should be saved. </param>
		/// <param name="emitSymbolInfo">If true, symbolic information is written using the default symbol writer. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> or <paramref name="fileName" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260.-or- <paramref name="fileName" /> contains a path specification (a directory component, for example).-or- There is a conflict with the name of another file that belongs to this assembly. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been previously saved. </exception>
		/// <exception cref="T:System.NotSupportedException">This assembly was called on a dynamic assembly with the <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" /> attribute. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060030F3 RID: 12531 RVA: 0x000B86B0 File Offset: 0x000B68B0
		public ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, fileName, emitSymbolInfo, false);
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000B86BC File Offset: 0x000B68BC
		private ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo, bool transient)
		{
			this.check_name_and_filename(name, fileName, false);
			if (!transient)
			{
				if (Path.GetExtension(fileName) == string.Empty)
				{
					throw new ArgumentException("Module file name '" + fileName + "' must have file extension.");
				}
				if (!this.IsSave)
				{
					throw new NotSupportedException("Persistable modules are not supported in a dynamic assembly created with AssemblyBuilderAccess.Run");
				}
				if (this.created)
				{
					throw new InvalidOperationException("Assembly was already saved.");
				}
			}
			ModuleBuilder moduleBuilder = new ModuleBuilder(this, name, fileName, emitSymbolInfo, transient);
			if (this.modules != null && this.is_module_only)
			{
				throw new InvalidOperationException("A module-only assembly can only contain one module.");
			}
			if (this.modules != null)
			{
				ModuleBuilder[] array = new ModuleBuilder[this.modules.Length + 1];
				Array.Copy(this.modules, array, this.modules.Length);
				this.modules = array;
			}
			else
			{
				this.modules = new ModuleBuilder[1];
			}
			this.modules[this.modules.Length - 1] = moduleBuilder;
			return moduleBuilder;
		}

		/// <summary>Defines a standalone managed resource for this assembly with the default public resource attribute.</summary>
		/// <returns>A <see cref="T:System.Resources.ResourceWriter" /> object for the specified resource.</returns>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="description">A textual description of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.-or- There is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="fileName" /> is zero.-or- <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060030F5 RID: 12533 RVA: 0x000B879F File Offset: 0x000B699F
		public IResourceWriter DefineResource(string name, string description, string fileName)
		{
			return this.DefineResource(name, description, fileName, ResourceAttributes.Public);
		}

		/// <summary>Defines a standalone managed resource for this assembly. Attributes can be specified for the managed resource.</summary>
		/// <returns>A <see cref="T:System.Resources.ResourceWriter" /> object for the specified resource.</returns>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="description">A textual description of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path. </param>
		/// <param name="attribute">The resource attributes. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined or if there is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="fileName" /> is zero.-or- <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060030F6 RID: 12534 RVA: 0x000B87AC File Offset: 0x000B69AC
		public IResourceWriter DefineResource(string name, string description, string fileName, ResourceAttributes attribute)
		{
			this.AddResourceFile(name, fileName, attribute, false);
			IResourceWriter resourceWriter = new ResourceWriter(fileName);
			if (this.resource_writers == null)
			{
				this.resource_writers = new ArrayList();
			}
			this.resource_writers.Add(resourceWriter);
			return resourceWriter;
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000B87EC File Offset: 0x000B69EC
		private void AddUnmanagedResource(Win32Resource res)
		{
			MemoryStream memoryStream = new MemoryStream();
			res.WriteTo(memoryStream);
			if (this.win32_resources != null)
			{
				MonoWin32Resource[] array = new MonoWin32Resource[this.win32_resources.Length + 1];
				Array.Copy(this.win32_resources, array, this.win32_resources.Length);
				this.win32_resources = array;
			}
			else
			{
				this.win32_resources = new MonoWin32Resource[1];
			}
			this.win32_resources[this.win32_resources.Length - 1] = new MonoWin32Resource(res.Type.Id, res.Name.Id, res.Language, memoryStream.ToArray());
		}

		/// <summary>Defines an unmanaged resource for this assembly as an opaque blob of bytes.</summary>
		/// <param name="resource">The opaque blob of bytes representing the unmanaged resource. </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource was previously defined. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resource" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060030F8 RID: 12536 RVA: 0x000B8883 File Offset: 0x000B6A83
		[MonoTODO("Not currently implemenented")]
		public void DefineUnmanagedResource(byte[] resource)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Unmanaged;
			throw new NotImplementedException();
		}

		/// <summary>Defines an unmanaged resource file for this assembly given the name of the resource file.</summary>
		/// <param name="resourceFileName">The name of the resource file. </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource was previously defined.-or- The file <paramref name="resourceFileName" /> is not readable.-or- <paramref name="resourceFileName" /> is the empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceFileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="resourceFileName" /> is not found.-or- <paramref name="resourceFileName" /> is a directory. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060030F9 RID: 12537 RVA: 0x000B88B4 File Offset: 0x000B6AB4
		public void DefineUnmanagedResource(string resourceFileName)
		{
			if (resourceFileName == null)
			{
				throw new ArgumentNullException("resourceFileName");
			}
			if (resourceFileName.Length == 0)
			{
				throw new ArgumentException("resourceFileName");
			}
			if (!File.Exists(resourceFileName) || Directory.Exists(resourceFileName))
			{
				throw new FileNotFoundException("File '" + resourceFileName + "' does not exist or is a directory.");
			}
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Unmanaged;
			using (FileStream fileStream = new FileStream(resourceFileName, FileMode.Open, FileAccess.Read))
			{
				foreach (object obj in new Win32ResFileReader(fileStream).ReadResources())
				{
					Win32EncodedResource win32EncodedResource = (Win32EncodedResource)obj;
					if (win32EncodedResource.Name.IsName || win32EncodedResource.Type.IsName)
					{
						throw new InvalidOperationException("resource files with named resources or non-default resource types are not supported.");
					}
					this.AddUnmanagedResource(win32EncodedResource);
				}
			}
		}

		/// <summary>Defines an unmanaged version information resource using the information specified in the assembly's AssemblyName object and the assembly's custom attributes.</summary>
		/// <exception cref="T:System.ArgumentException">An unmanaged version information resource was previously defined.-or- The unmanaged version information is too large to persist. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060030FA RID: 12538 RVA: 0x000B89B8 File Offset: 0x000B6BB8
		public void DefineVersionInfoResource()
		{
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Assembly;
			this.version_res = new Win32VersionResource(1, 0, false);
		}

		/// <summary>Defines an unmanaged version information resource for this assembly with the given specifications.</summary>
		/// <param name="product">The name of the product with which this assembly is distributed. </param>
		/// <param name="productVersion">The version of the product with which this assembly is distributed. </param>
		/// <param name="company">The name of the company that produced this assembly. </param>
		/// <param name="copyright">Describes all copyright notices, trademarks, and registered trademarks that apply to this assembly. This should include the full text of all notices, legal symbols, copyright dates, trademark numbers, and so on. In English, this string should be in the format "Copyright Microsoft Corp. 1990-2001". </param>
		/// <param name="trademark">Describes all trademarks and registered trademarks that apply to this assembly. This should include the full text of all notices, legal symbols, trademark numbers, and so on. In English, this string should be in the format "Windows is a trademark of Microsoft Corporation". </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged version information resource was previously defined.-or- The unmanaged version information is too large to persist. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060030FB RID: 12539 RVA: 0x000B89E4 File Offset: 0x000B6BE4
		public void DefineVersionInfoResource(string product, string productVersion, string company, string copyright, string trademark)
		{
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Explicit;
			this.version_res = new Win32VersionResource(1, 0, false);
			this.version_res.ProductName = ((product != null) ? product : " ");
			this.version_res.ProductVersion = ((productVersion != null) ? productVersion : " ");
			this.version_res.CompanyName = ((company != null) ? company : " ");
			this.version_res.LegalCopyright = ((copyright != null) ? copyright : " ");
			this.version_res.LegalTrademarks = ((trademark != null) ? trademark : " ");
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000B8A8C File Offset: 0x000B6C8C
		private void DefineVersionInfoResourceImpl(string fileName)
		{
			if (this.versioninfo_culture != null)
			{
				this.version_res.FileLanguage = new CultureInfo(this.versioninfo_culture).LCID;
			}
			this.version_res.Version = ((this.version == null) ? "0.0.0.0" : this.version);
			if (this.cattrs != null)
			{
				NativeResourceType nativeResourceType = this.native_resource;
				if (nativeResourceType != NativeResourceType.Assembly)
				{
					if (nativeResourceType == NativeResourceType.Explicit)
					{
						foreach (CustomAttributeBuilder customAttributeBuilder in this.cattrs)
						{
							string fullName = customAttributeBuilder.Ctor.ReflectedType.FullName;
							if (fullName == "System.Reflection.AssemblyCultureAttribute")
							{
								this.version_res.FileLanguage = new CultureInfo(customAttributeBuilder.string_arg()).LCID;
							}
							else if (fullName == "System.Reflection.AssemblyDescriptionAttribute")
							{
								this.version_res.Comments = customAttributeBuilder.string_arg();
							}
						}
					}
				}
				else
				{
					foreach (CustomAttributeBuilder customAttributeBuilder2 in this.cattrs)
					{
						string fullName2 = customAttributeBuilder2.Ctor.ReflectedType.FullName;
						if (fullName2 == "System.Reflection.AssemblyProductAttribute")
						{
							this.version_res.ProductName = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCompanyAttribute")
						{
							this.version_res.CompanyName = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCopyrightAttribute")
						{
							this.version_res.LegalCopyright = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyTrademarkAttribute")
						{
							this.version_res.LegalTrademarks = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCultureAttribute")
						{
							this.version_res.FileLanguage = new CultureInfo(customAttributeBuilder2.string_arg()).LCID;
						}
						else if (fullName2 == "System.Reflection.AssemblyFileVersionAttribute")
						{
							this.version_res.FileVersion = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyInformationalVersionAttribute")
						{
							this.version_res.ProductVersion = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyTitleAttribute")
						{
							this.version_res.FileDescription = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyDescriptionAttribute")
						{
							this.version_res.Comments = customAttributeBuilder2.string_arg();
						}
					}
				}
			}
			this.version_res.OriginalFilename = fileName;
			this.version_res.InternalName = Path.GetFileNameWithoutExtension(fileName);
			this.AddUnmanagedResource(this.version_res);
		}

		/// <summary>Returns the dynamic module with the specified name.</summary>
		/// <returns>A ModuleBuilder object representing the requested dynamic module.</returns>
		/// <param name="name">The name of the requested dynamic module. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060030FD RID: 12541 RVA: 0x000B8D14 File Offset: 0x000B6F14
		public ModuleBuilder GetDynamicModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					if (this.modules[i].name == name)
					{
						return this.modules[i];
					}
				}
			}
			return null;
		}

		/// <summary>Gets the exported types defined in this assembly.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> containing the exported types defined in this assembly.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060030FE RID: 12542 RVA: 0x000B8396 File Offset: 0x000B6596
		public override Type[] GetExportedTypes()
		{
			throw this.not_supported();
		}

		/// <summary>Gets a <see cref="T:System.IO.FileStream" /> for the specified file in the file table of the manifest of this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> for the specified file, or null, if the file is not found.</returns>
		/// <param name="name">The name of the specified file. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060030FF RID: 12543 RVA: 0x000B8396 File Offset: 0x000B6596
		public override FileStream GetFile(string name)
		{
			throw this.not_supported();
		}

		/// <summary>Gets the files in the file table of an assembly manifest, specifying whether to include resource modules.</summary>
		/// <returns>An array of <see cref="T:System.IO.FileStream" /> objects.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003100 RID: 12544 RVA: 0x000B8396 File Offset: 0x000B6596
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			throw this.not_supported();
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000B8D81 File Offset: 0x000B6F81
		internal override Module[] GetModulesInternal()
		{
			if (this.modules == null)
			{
				return new Module[0];
			}
			return (Module[])this.modules.Clone();
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000B8DA4 File Offset: 0x000B6FA4
		internal override Type[] GetTypes(bool exportedOnly)
		{
			Type[] array = null;
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					Type[] types = this.modules[i].GetTypes();
					if (array == null)
					{
						array = types;
					}
					else
					{
						Type[] array2 = new Type[array.Length + types.Length];
						Array.Copy(array, 0, array2, 0, array.Length);
						Array.Copy(types, 0, array2, array.Length, types.Length);
					}
				}
			}
			if (this.loaded_modules != null)
			{
				for (int j = 0; j < this.loaded_modules.Length; j++)
				{
					Type[] types2 = this.loaded_modules[j].GetTypes();
					if (array == null)
					{
						array = types2;
					}
					else
					{
						Type[] array3 = new Type[array.Length + types2.Length];
						Array.Copy(array, 0, array3, 0, array.Length);
						Array.Copy(types2, 0, array3, array.Length, types2.Length);
					}
				}
			}
			if (array != null)
			{
				List<Exception> list = null;
				foreach (Type type in array)
				{
					if (type is TypeBuilder)
					{
						if (list == null)
						{
							list = new List<Exception>();
						}
						list.Add(new TypeLoadException(string.Format("Type '{0}' is not finished", type.FullName)));
					}
				}
				if (list != null)
				{
					throw new ReflectionTypeLoadException(new Type[list.Count], list.ToArray());
				}
			}
			if (array != null)
			{
				return array;
			}
			return Type.EmptyTypes;
		}

		/// <summary>Returns information about how the given resource has been persisted.</summary>
		/// <returns>
		///   <see cref="T:System.Reflection.ManifestResourceInfo" /> populated with information about the resource's topology, or null if the resource is not found.</returns>
		/// <param name="resourceName">The name of the resource. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003103 RID: 12547 RVA: 0x000B8396 File Offset: 0x000B6596
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <returns>An array of type String containing the names of all the resources.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported on a dynamic assembly. To get the manifest resource names, use <see cref="M:System.Reflection.Assembly.GetManifestResourceNames" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003104 RID: 12548 RVA: 0x000B8396 File Offset: 0x000B6596
		public override string[] GetManifestResourceNames()
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		/// <param name="name">The name of the manifest resource being requested. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003105 RID: 12549 RVA: 0x000B8396 File Offset: 0x000B6596
		public override Stream GetManifestResourceStream(string name)
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource, scoped by the namespace of the specified type, from this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		/// <param name="type">The type whose namespace is used to scope the manifest resource name. </param>
		/// <param name="name">The name of the manifest resource being requested. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003106 RID: 12550 RVA: 0x000B8396 File Offset: 0x000B6596
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			throw this.not_supported();
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x000B8EE9 File Offset: 0x000B70E9
		internal bool IsSave
		{
			get
			{
				return this.access != 1U;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000B8EF7 File Offset: 0x000B70F7
		internal bool IsRun
		{
			get
			{
				return this.access == 1U || this.access == 3U || this.access == 9U;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000B8F17 File Offset: 0x000B7117
		internal string AssemblyDir
		{
			get
			{
				return this.dir;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000B8F1F File Offset: 0x000B711F
		// (set) Token: 0x0600310B RID: 12555 RVA: 0x000B8F27 File Offset: 0x000B7127
		internal bool IsModuleOnly
		{
			get
			{
				return this.is_module_only;
			}
			set
			{
				this.is_module_only = value;
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000B8F30 File Offset: 0x000B7130
		internal override Module GetManifestModule()
		{
			if (this.manifest_module == null)
			{
				this.manifest_module = this.DefineDynamicModule("Default Dynamic Module");
			}
			return this.manifest_module;
		}

		/// <summary>Saves this dynamic assembly to disk, specifying the nature of code in the assembly's executables and the target platform.</summary>
		/// <param name="assemblyFileName">The file name of the assembly.</param>
		/// <param name="portableExecutableKind">A bitwise combination of the <see cref="T:System.Reflection.PortableExecutableKinds" /> values that specifies the nature of the code.</param>
		/// <param name="imageFileMachine">One of the <see cref="T:System.Reflection.ImageFileMachine" /> values that specifies the target platform.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="assemblyFileName" /> is 0.-or- There are two or more modules resource files in the assembly with the same name.-or- The target directory of the assembly is invalid.-or- <paramref name="assemblyFileName" /> is not a simple file name (for example, has a directory or drive component), or more than one unmanaged resource, including a version information resources, was defined in this assembly.-or- The CultureInfo string in <see cref="T:System.Reflection.AssemblyCultureAttribute" /> is not a valid string and <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" /> was called prior to calling this method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFileName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been saved before.-or- This assembly has access Run<see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" /></exception>
		/// <exception cref="T:System.IO.IOException">An output error occurs during the save. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called for any of the types in the modules of the assembly to be written to disk. </exception>
		// Token: 0x0600310D RID: 12557 RVA: 0x000B8F58 File Offset: 0x000B7158
		[MonoLimitation("No support for PE32+ assemblies for AMD64 and IA64")]
		public void Save(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			this.peKind = portableExecutableKind;
			this.machine = imageFileMachine;
			if ((this.peKind & PortableExecutableKinds.PE32Plus) != PortableExecutableKinds.NotAPortableExecutableImage || (this.peKind & PortableExecutableKinds.Unmanaged32Bit) != PortableExecutableKinds.NotAPortableExecutableImage)
			{
				throw new NotImplementedException(this.peKind.ToString());
			}
			if (this.machine == ImageFileMachine.IA64 || this.machine == ImageFileMachine.AMD64)
			{
				throw new NotImplementedException(this.machine.ToString());
			}
			if (this.resource_writers != null)
			{
				foreach (object obj in this.resource_writers)
				{
					IResourceWriter resourceWriter = (IResourceWriter)obj;
					resourceWriter.Generate();
					resourceWriter.Close();
				}
			}
			ModuleBuilder moduleBuilder = null;
			if (this.modules != null)
			{
				foreach (ModuleBuilder moduleBuilder2 in this.modules)
				{
					if (moduleBuilder2.FileName == assemblyFileName)
					{
						moduleBuilder = moduleBuilder2;
					}
				}
			}
			if (moduleBuilder == null)
			{
				moduleBuilder = this.DefineDynamicModule("RefEmit_OnDiskManifestModule", assemblyFileName);
			}
			if (!this.is_module_only)
			{
				moduleBuilder.IsMain = true;
			}
			if (this.entry_point != null && this.entry_point.DeclaringType.Module != moduleBuilder)
			{
				Type[] array2;
				if (this.entry_point.GetParametersCount() == 1)
				{
					array2 = new Type[] { typeof(string) };
				}
				else
				{
					array2 = Type.EmptyTypes;
				}
				MethodBuilder methodBuilder = moduleBuilder.DefineGlobalMethod("__EntryPoint$", MethodAttributes.Static, this.entry_point.ReturnType, array2);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				if (array2.Length == 1)
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
				}
				ilgenerator.Emit(OpCodes.Tailcall);
				ilgenerator.Emit(OpCodes.Call, this.entry_point);
				ilgenerator.Emit(OpCodes.Ret);
				this.entry_point = methodBuilder;
			}
			if (this.version_res != null)
			{
				this.DefineVersionInfoResourceImpl(assemblyFileName);
			}
			if (this.sn != null)
			{
				this.public_key = this.sn.PublicKey;
			}
			foreach (ModuleBuilder moduleBuilder3 in this.modules)
			{
				if (moduleBuilder3 != moduleBuilder)
				{
					moduleBuilder3.Save();
				}
			}
			moduleBuilder.Save();
			if (this.sn != null && this.sn.CanSign)
			{
				this.sn.Sign(Path.Combine(this.AssemblyDir, assemblyFileName));
			}
			this.created = true;
		}

		/// <summary>Saves this dynamic assembly to disk.</summary>
		/// <param name="assemblyFileName">The file name of the assembly. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="assemblyFileName" /> is 0.-or- There are two or more modules resource files in the assembly with the same name.-or- The target directory of the assembly is invalid.-or- <paramref name="assemblyFileName" /> is not a simple file name (for example, has a directory or drive component), or more than one unmanaged resource, including a version information resource, was defined in this assembly.-or- The CultureInfo string in <see cref="T:System.Reflection.AssemblyCultureAttribute" /> is not a valid string and <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" /> was called prior to calling this method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFileName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been saved before.-or- This assembly has access Run<see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" /></exception>
		/// <exception cref="T:System.IO.IOException">An output error occurs during the save. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called for any of the types in the modules of the assembly to be written to disk. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600310E RID: 12558 RVA: 0x000B91DC File Offset: 0x000B73DC
		public void Save(string assemblyFileName)
		{
			this.Save(assemblyFileName, PortableExecutableKinds.ILOnly, ImageFileMachine.I386);
		}

		/// <summary>Sets the entry point for this dynamic assembly, assuming that a console application is being built.</summary>
		/// <param name="entryMethod">A reference to the method that represents the entry point for this dynamic assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryMethod" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="entryMethod" /> is not contained within this assembly. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600310F RID: 12559 RVA: 0x000B91EB File Offset: 0x000B73EB
		public void SetEntryPoint(MethodInfo entryMethod)
		{
			this.SetEntryPoint(entryMethod, PEFileKinds.ConsoleApplication);
		}

		/// <summary>Sets the entry point for this assembly and defines the type of the portable executable (PE file) being built.</summary>
		/// <param name="entryMethod">A reference to the method that represents the entry point for this dynamic assembly. </param>
		/// <param name="fileKind">The type of the assembly executable being built. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryMethod" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="entryMethod" /> is not contained within this assembly. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003110 RID: 12560 RVA: 0x000B91F8 File Offset: 0x000B73F8
		public void SetEntryPoint(MethodInfo entryMethod, PEFileKinds fileKind)
		{
			if (entryMethod == null)
			{
				throw new ArgumentNullException("entryMethod");
			}
			if (entryMethod.DeclaringType.Assembly != this)
			{
				throw new InvalidOperationException("Entry method is not defined in the same assembly.");
			}
			this.entry_point = entryMethod;
			this.pekind = fileKind;
		}

		/// <summary>Set a custom attribute on this assembly using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003111 RID: 12561 RVA: 0x000B9248 File Offset: 0x000B7448
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
			}
			else
			{
				this.cattrs = new CustomAttributeBuilder[1];
				this.cattrs[0] = customBuilder;
			}
			if (customBuilder.Ctor != null && customBuilder.Ctor.DeclaringType == typeof(RuntimeCompatibilityAttribute))
			{
				AssemblyBuilder.UpdateNativeCustomAttributes(this);
			}
		}

		/// <summary>Set a custom attribute on this assembly using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="con" /> is not a RuntimeConstructorInfo.</exception>
		// Token: 0x06003112 RID: 12562 RVA: 0x000B92E1 File Offset: 0x000B74E1
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000B9312 File Offset: 0x000B7512
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000B9320 File Offset: 0x000B7520
		private void check_name_and_filename(string name, string fileName, bool fileNeedsToExists)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "fileName");
			}
			if (Path.GetFileName(fileName) != fileName)
			{
				throw new ArgumentException("fileName '" + fileName + "' must not include a path.", "fileName");
			}
			string text = fileName;
			if (this.dir != null)
			{
				text = Path.Combine(this.dir, fileName);
			}
			if (fileNeedsToExists && !File.Exists(text))
			{
				throw new FileNotFoundException("Could not find file '" + fileName + "'");
			}
			if (this.resources != null)
			{
				for (int i = 0; i < this.resources.Length; i++)
				{
					if (this.resources[i].filename == text)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.resources[i].name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
			if (this.modules != null)
			{
				for (int j = 0; j < this.modules.Length; j++)
				{
					if (!this.modules[j].IsTransient() && this.modules[j].FileName == fileName)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.modules[j].Name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000B94D4 File Offset: 0x000B76D4
		private string create_assembly_version(string version)
		{
			string[] array = version.Split('.', StringSplitOptions.None);
			int[] array2 = new int[4];
			if (array.Length < 0 || array.Length > 4)
			{
				throw new ArgumentException("The version specified '" + version + "' is invalid");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "*")
				{
					DateTime now = DateTime.Now;
					if (i == 2)
					{
						array2[2] = (now - new DateTime(2000, 1, 1)).Days;
						if (array.Length == 3)
						{
							array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
						}
					}
					else
					{
						if (i != 3)
						{
							throw new ArgumentException("The version specified '" + version + "' is invalid");
						}
						array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
					}
				}
				else
				{
					try
					{
						array2[i] = int.Parse(array[i]);
					}
					catch (FormatException)
					{
						throw new ArgumentException("The version specified '" + version + "' is invalid");
					}
				}
			}
			return string.Concat(new string[]
			{
				array2[0].ToString(),
				".",
				array2[1].ToString(),
				".",
				array2[2].ToString(),
				".",
				array2[3].ToString()
			});
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000B9668 File Offset: 0x000B7868
		private string GetCultureString(string str)
		{
			if (!(str == "neutral"))
			{
				return str;
			}
			return string.Empty;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000B967E File Offset: 0x000B787E
		internal Type MakeGenericType(Type gtd, Type[] typeArguments)
		{
			return new TypeBuilderInstantiation(gtd, typeArguments);
		}

		/// <summary>Gets the specified type from the types that have been defined and created in the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.</summary>
		/// <returns>The specified type, or null if the type is not found or has not been created yet. </returns>
		/// <param name="name">The name of the type to search for.</param>
		/// <param name="throwOnError">true to throw an exception if the type is not found; otherwise, false.</param>
		/// <param name="ignoreCase">true to ignore the case of the type name when searching; otherwise, false.</param>
		// Token: 0x06003118 RID: 12568 RVA: 0x000B9688 File Offset: 0x000B7888
		public override Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException(name);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name", "Name cannot be empty");
			}
			Type type = base.InternalGetType(null, name, throwOnError, ignoreCase);
			if (!(type is TypeBuilder))
			{
				return type;
			}
			if (throwOnError)
			{
				throw new TypeLoadException(string.Format("Could not load type '{0}' from assembly '{1}'", name, this.name));
			}
			return null;
		}

		/// <summary>Gets the specified module in this assembly.</summary>
		/// <returns>The module being requested, or null if the module is not found.</returns>
		/// <param name="name">The name of the requested module.</param>
		// Token: 0x06003119 RID: 12569 RVA: 0x000B96E8 File Offset: 0x000B78E8
		public override Module GetModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Name can't be empty");
			}
			if (this.modules == null)
			{
				return null;
			}
			foreach (ModuleBuilder module in this.modules)
			{
				if (module.ScopeName == name)
				{
					return module;
				}
			}
			return null;
		}

		/// <summary>Gets all the modules that are part of this assembly, and optionally includes resource modules.</summary>
		/// <returns>The modules that are part of this assembly.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false.</param>
		// Token: 0x0600311A RID: 12570 RVA: 0x000B974C File Offset: 0x000B794C
		public override Module[] GetModules(bool getResourceModules)
		{
			Module[] modulesInternal = this.GetModulesInternal();
			if (!getResourceModules)
			{
				List<Module> list = new List<Module>(modulesInternal.Length);
				foreach (Module module in modulesInternal)
				{
					if (!module.IsResource())
					{
						list.Add(module);
					}
				}
				return list.ToArray();
			}
			return modulesInternal;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.AssemblyName" /> that was specified when the current dynamic assembly was created, and sets the code base as specified.</summary>
		/// <returns>The name of the dynamic assembly.</returns>
		/// <param name="copiedName">true to set the code base to the location of the assembly after it is shadow-copied; false to set the code base to the original location.</param>
		// Token: 0x0600311B RID: 12571 RVA: 0x000B979C File Offset: 0x000B799C
		public override AssemblyName GetName(bool copiedName)
		{
			AssemblyName assemblyName = AssemblyName.Create(this, false);
			if (this.sn != null)
			{
				assemblyName.SetPublicKey(this.sn.PublicKey);
				assemblyName.SetPublicKeyToken(this.sn.PublicKeyToken);
			}
			return assemblyName;
		}

		/// <summary>Gets an incomplete list of <see cref="T:System.Reflection.AssemblyName" /> objects for the assemblies that are referenced by this <see cref="T:System.Reflection.Emit.AssemblyBuilder" />. </summary>
		/// <returns>An array of assembly names for the referenced assemblies. This array is not a complete list.</returns>
		// Token: 0x0600311C RID: 12572 RVA: 0x000B575D File Offset: 0x000B395D
		[MonoTODO("This always returns an empty array")]
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return Assembly.GetReferencedAssemblies(this);
		}

		/// <summary>Returns all the loaded modules that are part of this assembly, and optionally includes resource modules. </summary>
		/// <returns>The loaded modules that are part of this assembly.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false.</param>
		// Token: 0x0600311D RID: 12573 RVA: 0x000B57B6 File Offset: 0x000B39B6
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		/// <summary>Gets the satellite assembly for the specified culture.</summary>
		/// <returns>The specified satellite assembly.</returns>
		/// <param name="culture">The specified culture. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the CultureInfo did not match the one specified. </exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly. </exception>
		// Token: 0x0600311E RID: 12574 RVA: 0x000B97DC File Offset: 0x000B79DC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, null, true, ref stackCrawlMark);
		}

		/// <summary>Gets the specified version of the satellite assembly for the specified culture.</summary>
		/// <returns>The specified satellite assembly.</returns>
		/// <param name="culture">The specified culture. </param>
		/// <param name="version">The version of the satellite assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the CultureInfo or the version did not match the one specified. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found. </exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly. </exception>
		// Token: 0x0600311F RID: 12575 RVA: 0x000B97F8 File Offset: 0x000B79F8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, version, true, ref stackCrawlMark);
		}

		/// <summary>Gets the module in the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> that contains the assembly manifest.</summary>
		/// <returns>The manifest module.</returns>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000B57F6 File Offset: 0x000B39F6
		public override Module ManifestModule
		{
			get
			{
				return this.GetManifestModule();
			}
		}

		/// <summary>Gets a value that indicates whether the assembly was loaded from the global assembly cache.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool GlobalAssemblyCache
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates that the current assembly is a dynamic assembly.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x0000C091 File Offset: 0x0000A291
		public override bool IsDynamic
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to the specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06003123 RID: 12579 RVA: 0x000B9812 File Offset: 0x000B7A12
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003124 RID: 12580 RVA: 0x000B5B52 File Offset: 0x000B3D52
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000B981B File Offset: 0x000B7A1B
		public override string ToString()
		{
			if (this.assemblyName != null)
			{
				return this.assemblyName;
			}
			this.assemblyName = this.FullName;
			return this.assemblyName;
		}

		/// <summary>Returns a value that indicates whether one or more instances of the specified attribute type is applied to this member.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" /> is applied to this dynamic assembly; otherwise, false.</returns>
		/// <param name="attributeType">The type of attribute to test for.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		// Token: 0x06003126 RID: 12582 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		/// <summary>Returns all the custom attributes that have been applied to the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.</summary>
		/// <returns>An array that contains the custom attributes; the array is empty if there are no attributes.</returns>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		// Token: 0x06003127 RID: 12583 RVA: 0x000B3EED File Offset: 0x000B20ED
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		/// <summary>Returns all the custom attributes that have been applied to the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" />, and that derive from a specified attribute type.</summary>
		/// <returns>An array that contains the custom attributes that are derived at any level from <paramref name="attributeType" />; the array is empty if there are no such attributes.</returns>
		/// <param name="attributeType">The base type from which attributes derive.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x06003128 RID: 12584 RVA: 0x000B3EF6 File Offset: 0x000B20F6
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		/// <summary>Gets the display name of the current dynamic assembly. </summary>
		/// <returns>The display name of the dynamic assembly.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000B5889 File Offset: 0x000B3A89
		public override string FullName
		{
			get
			{
				return RuntimeAssembly.get_fullname(this);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000B983E File Offset: 0x000B7A3E
		internal override IntPtr MonoAssembly
		{
			get
			{
				return this._mono_assembly;
			}
		}

		/// <summary>Gets the evidence for this assembly.</summary>
		/// <returns>The evidence for this assembly.</returns>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000B5BAA File Offset: 0x000B3DAA
		public override Evidence Evidence
		{
			get
			{
				return this.UnprotectedGetEvidence();
			}
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x000B9848 File Offset: 0x000B7A48
		internal override Evidence UnprotectedGetEvidence()
		{
			if (this._evidence == null)
			{
				lock (this)
				{
					this._evidence = Evidence.GetDefaultHostEvidence(this);
				}
			}
			return this._evidence;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000176B9 File Offset: 0x000158B9
		internal AssemblyBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040018EC RID: 6380
		internal IntPtr _mono_assembly;

		// Token: 0x040018ED RID: 6381
		internal Evidence _evidence;

		// Token: 0x040018EE RID: 6382
		private UIntPtr dynamic_assembly;

		// Token: 0x040018EF RID: 6383
		private MethodInfo entry_point;

		// Token: 0x040018F0 RID: 6384
		private ModuleBuilder[] modules;

		// Token: 0x040018F1 RID: 6385
		private string name;

		// Token: 0x040018F2 RID: 6386
		private string dir;

		// Token: 0x040018F3 RID: 6387
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040018F4 RID: 6388
		private MonoResource[] resources;

		// Token: 0x040018F5 RID: 6389
		private byte[] public_key;

		// Token: 0x040018F6 RID: 6390
		private string version;

		// Token: 0x040018F7 RID: 6391
		private string culture;

		// Token: 0x040018F8 RID: 6392
		private uint algid;

		// Token: 0x040018F9 RID: 6393
		private uint flags;

		// Token: 0x040018FA RID: 6394
		private PEFileKinds pekind;

		// Token: 0x040018FB RID: 6395
		private bool delay_sign;

		// Token: 0x040018FC RID: 6396
		private uint access;

		// Token: 0x040018FD RID: 6397
		private Module[] loaded_modules;

		// Token: 0x040018FE RID: 6398
		private MonoWin32Resource[] win32_resources;

		// Token: 0x040018FF RID: 6399
		private RefEmitPermissionSet[] permissions_minimum;

		// Token: 0x04001900 RID: 6400
		private RefEmitPermissionSet[] permissions_optional;

		// Token: 0x04001901 RID: 6401
		private RefEmitPermissionSet[] permissions_refused;

		// Token: 0x04001902 RID: 6402
		private PortableExecutableKinds peKind;

		// Token: 0x04001903 RID: 6403
		private ImageFileMachine machine;

		// Token: 0x04001904 RID: 6404
		private bool corlib_internal;

		// Token: 0x04001905 RID: 6405
		private Type[] type_forwarders;

		// Token: 0x04001906 RID: 6406
		private byte[] pktoken;

		// Token: 0x04001907 RID: 6407
		internal PermissionSet _minimum;

		// Token: 0x04001908 RID: 6408
		internal PermissionSet _optional;

		// Token: 0x04001909 RID: 6409
		internal PermissionSet _refuse;

		// Token: 0x0400190A RID: 6410
		internal PermissionSet _granted;

		// Token: 0x0400190B RID: 6411
		internal PermissionSet _denied;

		// Token: 0x0400190C RID: 6412
		private string assemblyName;

		// Token: 0x0400190D RID: 6413
		internal Type corlib_object_type;

		// Token: 0x0400190E RID: 6414
		internal Type corlib_value_type;

		// Token: 0x0400190F RID: 6415
		internal Type corlib_enum_type;

		// Token: 0x04001910 RID: 6416
		internal Type corlib_void_type;

		// Token: 0x04001911 RID: 6417
		private ArrayList resource_writers;

		// Token: 0x04001912 RID: 6418
		private Win32VersionResource version_res;

		// Token: 0x04001913 RID: 6419
		private bool created;

		// Token: 0x04001914 RID: 6420
		private bool is_module_only;

		// Token: 0x04001915 RID: 6421
		private Mono.Security.StrongName sn;

		// Token: 0x04001916 RID: 6422
		private NativeResourceType native_resource;

		// Token: 0x04001917 RID: 6423
		private string versioninfo_culture;

		// Token: 0x04001918 RID: 6424
		private const AssemblyBuilderAccess COMPILER_ACCESS = (AssemblyBuilderAccess)2048;

		// Token: 0x04001919 RID: 6425
		private ModuleBuilder manifest_module;
	}
}
