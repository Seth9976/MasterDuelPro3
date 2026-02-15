using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Mono;

namespace System.Reflection
{
	/// <summary>Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.</summary>
	// Token: 0x02000632 RID: 1586
	[ComDefaultInterface(typeof(_Assembly))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class Assembly : ICustomAttributeProvider, _Assembly, IEvidenceFactory, ISerializable
	{
		/// <summary>Occurs when the common language runtime class loader cannot resolve a reference to an internal module of an assembly through normal means.</summary>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06002E9F RID: 11935 RVA: 0x00033EF4 File Offset: 0x000320F4
		// (remove) Token: 0x06002EA0 RID: 11936 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual event ModuleResolveEventHandler ModuleResolve
		{
			add
			{
				throw new NotImplementedException();
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the location of the assembly as specified originally, for example, in an <see cref="T:System.Reflection.AssemblyName" /> object.</summary>
		/// <returns>The location of the assembly as specified originally.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual string CodeBase
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the URI, including escape characters, that represents the codebase.</summary>
		/// <returns>A URI with escape characters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual string EscapedCodeBase
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the display name of the assembly.</summary>
		/// <returns>The display name of the assembly.</returns>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual string FullName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the entry point of this assembly.</summary>
		/// <returns>An object that represents the entry point of this assembly. If no entry point is found (for example, the assembly is a DLL), null is returned.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual MethodInfo EntryPoint
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the evidence for this assembly.</summary>
		/// <returns>The evidence for this assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual Evidence Evidence
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x00033EF4 File Offset: 0x000320F4
		internal virtual Evidence UnprotectedGetEvidence()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x00033EF4 File Offset: 0x000320F4
		internal virtual IntPtr MonoAssembly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000662 RID: 1634
		// (set) Token: 0x06002EA8 RID: 11944 RVA: 0x00033EF4 File Offset: 0x000320F4
		internal virtual bool FromByteArray
		{
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the full path or UNC location of the loaded file that contains the manifest.</summary>
		/// <returns>The location of the loaded file that contains the manifest. If the loaded file was shadow-copied, the location is that of the file after being shadow-copied. If the assembly is loaded from a byte array, such as when using the <see cref="M:System.Reflection.Assembly.Load(System.Byte[])" /> method overload, the value returned is an empty string ("").</returns>
		/// <exception cref="T:System.NotSupportedException">The current assembly is a dynamic assembly, represented by an <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> object. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual string Location
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a string representing the version of the common language runtime (CLR) saved in the file containing the manifest.</summary>
		/// <returns>The CLR version folder name. This is not a full path.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002EAA RID: 11946 RVA: 0x00033EF4 File Offset: 0x000320F4
		[ComVisible(false)]
		public virtual string ImageRuntimeVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets serialization information with all of the data needed to reinstantiate this assembly.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06002EAB RID: 11947 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether or not a specified attribute has been applied to the assembly.</summary>
		/// <returns>true if the attribute has been applied to the assembly; otherwise, false.</returns>
		/// <param name="attributeType">The type of the attribute to be checked for this assembly. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> uses an invalid type.</exception>
		// Token: 0x06002EAC RID: 11948 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets all the custom attributes for this assembly.</summary>
		/// <returns>An array that contains the custom attributes for this assembly.</returns>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />. </param>
		// Token: 0x06002EAD RID: 11949 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the custom attributes for this assembly as specified by type.</summary>
		/// <returns>An array that contains the custom attributes for this assembly as specified by <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The type for which the custom attributes are to be returned. </param>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a runtime type. </exception>
		// Token: 0x06002EAE RID: 11950 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the files in the file table of an assembly manifest.</summary>
		/// <returns>An array of streams that contain the files.</returns>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">A file was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">A file was not a valid assembly. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EAF RID: 11951 RVA: 0x000B3FC9 File Offset: 0x000B21C9
		public virtual FileStream[] GetFiles()
		{
			return this.GetFiles(false);
		}

		/// <summary>Gets the files in the file table of an assembly manifest, specifying whether to include resource modules.</summary>
		/// <returns>An array of streams that contain the files.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">A file was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">A file was not a valid assembly. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EB0 RID: 11952 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual FileStream[] GetFiles(bool getResourceModules)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.IO.FileStream" /> for the specified file in the file table of the manifest of this assembly.</summary>
		/// <returns>A stream that contains the specified file, or null if the file is not found.</returns>
		/// <param name="name">The name of the specified file. Do not include the path to the file.</param>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EB1 RID: 11953 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual FileStream GetFile(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <returns>The manifest resource; or null if no resources were specified during compilation or if the resource is not visible to the caller.</returns>
		/// <param name="name">The case-sensitive name of the manifest resource being requested. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.NotImplementedException">Resource length is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06002EB2 RID: 11954 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual Stream GetManifestResourceStream(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads the specified manifest resource, scoped by the namespace of the specified type, from this assembly.</summary>
		/// <returns>The manifest resource; or null if no resources were specified during compilation or if the resource is not visible to the caller.</returns>
		/// <param name="type">The type whose namespace is used to scope the manifest resource name. </param>
		/// <param name="name">The case-sensitive name of the manifest resource being requested. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.NotImplementedException">Resource length is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06002EB3 RID: 11955 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual Stream GetManifestResourceStream(Type type, string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000B3FD4 File Offset: 0x000B21D4
		internal Stream GetManifestResourceStream(Type type, string name, bool skipSecurityCheck, ref StackCrawlMark stackMark)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (type == null)
			{
				if (name == null)
				{
					throw new ArgumentNullException("type");
				}
			}
			else
			{
				string @namespace = type.Namespace;
				if (@namespace != null)
				{
					stringBuilder.Append(@namespace);
					if (name != null)
					{
						stringBuilder.Append(Type.Delimiter);
					}
				}
			}
			if (name != null)
			{
				stringBuilder.Append(name);
			}
			return this.GetManifestResourceStream(stringBuilder.ToString());
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000B4036 File Offset: 0x000B2236
		internal Stream GetManifestResourceStream(string name, ref StackCrawlMark stackMark, bool skipSecurityCheck)
		{
			return this.GetManifestResourceStream(null, name, skipSecurityCheck, ref stackMark);
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000B4042 File Offset: 0x000B2242
		internal string GetSimpleName()
		{
			return this.GetName(true).Name;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000B4050 File Offset: 0x000B2250
		internal byte[] GetPublicKey()
		{
			return this.GetName(true).GetPublicKey();
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000B405E File Offset: 0x000B225E
		internal Version GetVersion()
		{
			return this.GetName(true).Version;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000B406C File Offset: 0x000B226C
		private AssemblyNameFlags GetFlags()
		{
			return this.GetName(true).Flags;
		}

		// Token: 0x06002EBA RID: 11962
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal virtual extern Type[] GetTypes(bool exportedOnly);

		/// <summary>Gets the types defined in this assembly.</summary>
		/// <returns>An array that contains all the types that are defined in this assembly.</returns>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The assembly contains one or more types that cannot be loaded. The array returned by the <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> property of this exception contains a <see cref="T:System.Type" /> object for each type that was loaded and null for each type that could not be loaded, while the <see cref="P:System.Reflection.ReflectionTypeLoadException.LoaderExceptions" /> property contains an exception for each type that could not be loaded.</exception>
		// Token: 0x06002EBB RID: 11963 RVA: 0x000B407A File Offset: 0x000B227A
		public virtual Type[] GetTypes()
		{
			return this.GetTypes(false);
		}

		/// <summary>Gets the public types defined in this assembly that are visible outside the assembly.</summary>
		/// <returns>An array that represents the types defined in this assembly that are visible outside the assembly.</returns>
		/// <exception cref="T:System.NotSupportedException">The assembly is a dynamic assembly.</exception>
		// Token: 0x06002EBC RID: 11964 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual Type[] GetExportedTypes()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance and optionally throws an exception if the type is not found.</summary>
		/// <returns>An object that represents the specified class.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <param name="throwOnError">true to throw an exception if the type is not found; false to return null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid.-or- The length of <paramref name="name" /> exceeds 1024 characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true, and the type cannot be found.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="name" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06002EBD RID: 11965 RVA: 0x000B4083 File Offset: 0x000B2283
		public virtual Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance.</summary>
		/// <returns>An object that represents the specified class, or null if the class is not found.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.<paramref name="name" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version. </exception>
		// Token: 0x06002EBE RID: 11966 RVA: 0x000B408E File Offset: 0x000B228E
		public virtual Type GetType(string name)
		{
			return this.GetType(name, false, false);
		}

		// Token: 0x06002EBF RID: 11967
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type InternalGetType(Module module, string name, bool throwOnError, bool ignoreCase);

		// Token: 0x06002EC0 RID: 11968
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalGetAssemblyName(string assemblyFile, out MonoAssemblyName aname, out string codebase);

		/// <summary>Gets an <see cref="T:System.Reflection.AssemblyName" /> for this assembly, setting the codebase as specified by <paramref name="copiedName" />.</summary>
		/// <returns>An object that contains the fully parsed display name for this assembly.</returns>
		/// <param name="copiedName">true to set the <see cref="P:System.Reflection.Assembly.CodeBase" /> to the location of the assembly after it was shadow copied; false to set <see cref="P:System.Reflection.Assembly.CodeBase" /> to the original location. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EC1 RID: 11969 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual AssemblyName GetName(bool copiedName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an <see cref="T:System.Reflection.AssemblyName" /> for this assembly.</summary>
		/// <returns>An object that contains the fully parsed display name for this assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EC2 RID: 11970 RVA: 0x000B4099 File Offset: 0x000B2299
		public virtual AssemblyName GetName()
		{
			return this.GetName(false);
		}

		/// <summary>Returns the full name of the assembly, also known as the display name.</summary>
		/// <returns>The full name of the assembly, or the class name if the full name of the assembly cannot be determined.</returns>
		// Token: 0x06002EC3 RID: 11971 RVA: 0x000726C7 File Offset: 0x000708C7
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>Creates the name of a type qualified by the display name of its assembly.</summary>
		/// <returns>The full name of the type qualified by the display name of the assembly.</returns>
		/// <param name="assemblyName">The display name of an assembly. </param>
		/// <param name="typeName">The full name of a type. </param>
		// Token: 0x06002EC4 RID: 11972 RVA: 0x000B40A2 File Offset: 0x000B22A2
		public static string CreateQualifiedName(string assemblyName, string typeName)
		{
			return typeName + ", " + assemblyName;
		}

		/// <summary>Gets the currently loaded assembly in which the specified class is defined.</summary>
		/// <returns>The assembly in which the specified class is defined.</returns>
		/// <param name="type">An object representing a class in the assembly that will be returned. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		// Token: 0x06002EC5 RID: 11973 RVA: 0x000B40B0 File Offset: 0x000B22B0
		public static Assembly GetAssembly(Type type)
		{
			if (type != null)
			{
				return type.Assembly;
			}
			throw new ArgumentNullException("type");
		}

		/// <summary>Gets the process executable in the default application domain. In other application domains, this is the first executable that was executed by <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" />.</summary>
		/// <returns>The assembly that is the process executable in the default application domain, or the first executable that was executed by <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" />. Can return null when called from unmanaged code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002EC6 RID: 11974
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetEntryAssembly();

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000B40CC File Offset: 0x000B22CC
		internal Assembly GetSatelliteAssembly(CultureInfo culture, Version version, bool throwOnError, ref StackCrawlMark stackMark)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			string text = this.GetSimpleName() + ".resources";
			return this.InternalGetSatelliteAssembly(text, culture, version, true, ref stackMark);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000B4104 File Offset: 0x000B2304
		internal RuntimeAssembly InternalGetSatelliteAssembly(string name, CultureInfo culture, Version version, bool throwOnFileNotFound, ref StackCrawlMark stackMark)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.SetPublicKey(this.GetPublicKey());
			assemblyName.Flags = this.GetFlags() | AssemblyNameFlags.PublicKey;
			if (version == null)
			{
				assemblyName.Version = this.GetVersion();
			}
			else
			{
				assemblyName.Version = version;
			}
			assemblyName.CultureInfo = culture;
			assemblyName.Name = name;
			try
			{
				Assembly assembly = AppDomain.CurrentDomain.LoadSatellite(assemblyName, false, ref stackMark);
				if (assembly != null)
				{
					return (RuntimeAssembly)assembly;
				}
			}
			catch (FileNotFoundException)
			{
			}
			if (string.IsNullOrEmpty(this.Location))
			{
				return null;
			}
			string text = Path.Combine(Path.GetDirectoryName(this.Location), Path.Combine(culture.Name, assemblyName.Name + ".dll"));
			RuntimeAssembly runtimeAssembly;
			try
			{
				runtimeAssembly = (RuntimeAssembly)Assembly.LoadFrom(text, false, ref stackMark);
			}
			catch
			{
				if (throwOnFileNotFound || File.Exists(text))
				{
					throw;
				}
				runtimeAssembly = null;
			}
			return runtimeAssembly;
		}

		/// <summary>Returns the type of the current instance.</summary>
		/// <returns>An object that represents the <see cref="T:System.Reflection.Assembly" /> type.</returns>
		// Token: 0x06002EC9 RID: 11977 RVA: 0x0003395C File Offset: 0x00031B5C
		Type _Assembly.GetType()
		{
			return base.GetType();
		}

		// Token: 0x06002ECA RID: 11978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly LoadFrom(string assemblyFile, bool refOnly, ref StackCrawlMark stackMark);

		// Token: 0x06002ECB RID: 11979
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly LoadFile_internal(string assemblyFile, ref StackCrawlMark stackMark);

		/// <summary>Loads an assembly given its file name or path.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002ECC RID: 11980 RVA: 0x000B4204 File Offset: 0x000B2404
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Assembly.LoadFrom(assemblyFile, false, ref stackCrawlMark);
		}

		/// <summary>Loads an assembly given its file name or path and supplying security evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.-or-The <paramref name="securityEvidence" /> is not ambiguous and is determined to be invalid.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002ECD RID: 11981 RVA: 0x000B421C File Offset: 0x000B241C
		[Obsolete]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Assembly assembly = Assembly.LoadFrom(assemblyFile, false, ref stackCrawlMark);
			if (assembly != null && securityEvidence != null)
			{
				assembly.Evidence.Merge(securityEvidence);
			}
			return assembly;
		}

		/// <summary>Loads an assembly given its file name or path, security evidence, hash value, and hash algorithm.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <param name="hashValue">The value of the computed hash code. </param>
		/// <param name="hashAlgorithm">The hash algorithm used for hashing files and for generating the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.-or-The <paramref name="securityEvidence" /> is not ambiguous and is determined to be invalid. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002ECE RID: 11982 RVA: 0x00033EF4 File Offset: 0x000320F4
		[Obsolete]
		[MonoTODO("This overload is not currently implemented")]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads an assembly given its file name or path, hash value, and hash algorithm.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <param name="hashValue">The value of the computed hash code. </param>
		/// <param name="hashAlgorithm">The hash algorithm used for hashing files and for generating the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a file name extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information. -or-<paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		// Token: 0x06002ECF RID: 11983 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO]
		public static Assembly LoadFrom(string assemblyFile, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads an assembly into the load-from context, bypassing some security checks.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-<paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		// Token: 0x06002ED0 RID: 11984 RVA: 0x000B4250 File Offset: 0x000B2450
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly UnsafeLoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Assembly.LoadFrom(assemblyFile, false, ref stackCrawlMark);
		}

		/// <summary>Loads an assembly given its path, loading the assembly into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="path">The path of the assembly file. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The <paramref name="path" /> parameter is an empty string ("") or does not exist. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="path" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="path" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="securityEvidence" /> is not null. By default, legacy CAS policy is not enabled in the .NET Framework 4; when it is not enabled, <paramref name="securityEvidence" /> must be null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002ED1 RID: 11985 RVA: 0x000B4268 File Offset: 0x000B2468
		[Obsolete]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFile(string path, Evidence securityEvidence)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Path can't be empty", "path");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Assembly assembly = Assembly.LoadFile_internal(path, ref stackCrawlMark);
			if (assembly != null && securityEvidence != null)
			{
				throw new NotImplementedException();
			}
			return assembly;
		}

		/// <summary>Loads the contents of an assembly file on the specified path.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="path">The path of the file to load. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The <paramref name="path" /> parameter is an empty string ("") or does not exist. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="path" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="path" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002ED2 RID: 11986 RVA: 0x000B42BC File Offset: 0x000B24BC
		public static Assembly LoadFile(string path)
		{
			return Assembly.LoadFile(path, null);
		}

		/// <summary>Loads an assembly given the long form of its name.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The long form of the assembly name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyString" /> is a zero-length string. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002ED3 RID: 11987 RVA: 0x000B42C5 File Offset: 0x000B24C5
		public static Assembly Load(string assemblyString)
		{
			return AppDomain.CurrentDomain.Load(assemblyString);
		}

		/// <summary>Loads an assembly given its display name, loading the assembly into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The display name of the assembly. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.-or-An assembly or module was loaded twice with two different evidences. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002ED4 RID: 11988 RVA: 0x000B42D2 File Offset: 0x000B24D2
		[Obsolete]
		public static Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyString, assemblySecurity);
		}

		/// <summary>Loads an assembly given its <see cref="T:System.Reflection.AssemblyName" />.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyRef">The object that describes the assembly to be loaded. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002ED5 RID: 11989 RVA: 0x000B42E0 File Offset: 0x000B24E0
		public static Assembly Load(AssemblyName assemblyRef)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef);
		}

		/// <summary>Loads an assembly given its <see cref="T:System.Reflection.AssemblyName" />. The assembly is loaded into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyRef">The object that describes the assembly to be loaded. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002ED6 RID: 11990 RVA: 0x000B42ED File Offset: 0x000B24ED
		[Obsolete]
		public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef, assemblySecurity);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the application domain of the caller.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002ED7 RID: 11991 RVA: 0x000B42FB File Offset: 0x000B24FB
		public static Assembly Load(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly, optionally including symbols for the assembly. The assembly is loaded into the application domain of the caller.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">A byte array that contains the raw bytes representing the symbols for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002ED8 RID: 11992 RVA: 0x000B4308 File Offset: 0x000B2508
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly, optionally including symbols and evidence for the assembly. The assembly is loaded into the application domain of the caller.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">A byte array that contains the raw bytes representing the symbols for the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="securityEvidence" /> is not null.  By default, legacy CAS policy is not enabled in the .NET Framework 4; when it is not enabled, <paramref name="securityEvidence" /> must be null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002ED9 RID: 11993 RVA: 0x000B4316 File Offset: 0x000B2516
		[Obsolete]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore, securityEvidence);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly, optionally including symbols and specifying the source for the security context. The assembly is loaded into the application domain of the caller.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">A byte array that contains the raw bytes representing the symbols for the assembly. </param>
		/// <param name="securityContextSource">The source of the security context. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-<paramref name="rawAssembly" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="securityContextSource" /> is not one of the enumeration values.</exception>
		// Token: 0x06002EDA RID: 11994 RVA: 0x000B4308 File Offset: 0x000B2508
		[MonoLimitation("Argument securityContextSource is ignored")]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore);
		}

		/// <summary>Loads the assembly from a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the reflection-only context of the caller's application domain.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="rawAssembly" /> cannot be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002EDB RID: 11995 RVA: 0x000B4325 File Offset: 0x000B2525
		public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, null, null, true);
		}

		/// <summary>Loads an assembly into the reflection-only context, given its display name.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The display name of the assembly, as returned by the <see cref="P:System.Reflection.AssemblyName.FullName" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyString" /> is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="assemblyString" /> is found, but cannot be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002EDC RID: 11996 RVA: 0x000B4338 File Offset: 0x000B2538
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoad(string assemblyString)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return AppDomain.CurrentDomain.Load(assemblyString, null, true, ref stackCrawlMark);
		}

		/// <summary>Loads an assembly into the reflection-only context, given its path.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The path of the file that contains the manifest of the assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a file name extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="assemblyFile" /> is found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyFile" /> is an empty string (""). </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002EDD RID: 11997 RVA: 0x000B4358 File Offset: 0x000B2558
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Assembly.LoadFrom(assemblyFile, true, ref stackCrawlMark);
		}

		/// <summary>Loads an assembly from the application directory or from the global assembly cache using a partial name.</summary>
		/// <returns>The loaded assembly. If <paramref name="partialName" /> is not found, this method returns null.</returns>
		/// <param name="partialName">The display name of the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partialName" /> parameter is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="partialName" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002EDE RID: 11998 RVA: 0x000B437E File Offset: 0x000B257E
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static Assembly LoadWithPartialName(string partialName)
		{
			return Assembly.LoadWithPartialName(partialName, null);
		}

		/// <summary>Loads the module, internal to this assembly, with a common object file format (COFF)-based image containing an emitted module, or a resource file.</summary>
		/// <returns>The loaded module.</returns>
		/// <param name="moduleName">The name of the module. This string must correspond to a file name in this assembly's manifest. </param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="moduleName" /> or <paramref name="rawModule" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="moduleName" /> does not match a file entry in this assembly's manifest. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawModule" /> is not a valid module. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002EDF RID: 11999 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO("Not implemented")]
		public Module LoadModule(string moduleName, byte[] rawModule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads the module, internal to this assembly, with a common object file format (COFF)-based image containing an emitted module, or a resource file. The raw bytes representing the symbols for the module are also loaded.</summary>
		/// <returns>The loaded module.</returns>
		/// <param name="moduleName">The name of the module. This string must correspond to a file name in this assembly's manifest. </param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource. </param>
		/// <param name="rawSymbolStore">A byte array containing the raw bytes representing the symbols for the module. Must be null if this is a resource file. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="moduleName" /> or <paramref name="rawModule" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="moduleName" /> does not match a file entry in this assembly's manifest. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawModule" /> is not a valid module. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002EE0 RID: 12000 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO("Not implemented")]
		public virtual Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002EE1 RID: 12001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly load_with_partial_name(string name, Evidence e);

		/// <summary>Loads an assembly from the application directory or from the global assembly cache using a partial name. The assembly is loaded into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly. If <paramref name="partialName" /> is not found, this method returns null.</returns>
		/// <param name="partialName">The display name of the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different sets of evidence. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partialName" /> parameter is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="partialName" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002EE2 RID: 12002 RVA: 0x000B4387 File Offset: 0x000B2587
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence)
		{
			return Assembly.LoadWithPartialName(partialName, securityEvidence, true);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000B4391 File Offset: 0x000B2591
		internal static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence, bool oldBehavior)
		{
			if (!oldBehavior)
			{
				throw new NotImplementedException();
			}
			if (partialName == null)
			{
				throw new NullReferenceException();
			}
			return Assembly.load_with_partial_name(partialName, securityEvidence);
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, using case-sensitive search.</summary>
		/// <returns>An instance of the specified type created with the default constructor; or null if <paramref name="typeName" /> is not found. The type is resolved using the default binder, without specifying culture or activation attributes, and with <see cref="T:System.Reflection.BindingFlags" /> set to Public or Instance. </returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character.-or-The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="typeName" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002EE4 RID: 12004 RVA: 0x000B43AC File Offset: 0x000B25AC
		public object CreateInstance(string typeName)
		{
			return this.CreateInstance(typeName, false);
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search.</summary>
		/// <returns>An instance of the specified type created with the default constructor; or null if <paramref name="typeName" /> is not found. The type is resolved using the default binder, without specifying culture or activation attributes, and with <see cref="T:System.Reflection.BindingFlags" /> set to Public or Instance.</returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character. -or-The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="typeName" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002EE5 RID: 12005 RVA: 0x000B43B8 File Offset: 0x000B25B8
		public object CreateInstance(string typeName, bool ignoreCase)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object obj;
			try
			{
				obj = Activator.CreateInstance(type);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return obj;
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search and having the specified culture, arguments, and binding and activation attributes.</summary>
		/// <returns>An instance of the specified type, or null if <paramref name="typeName" /> is not found. The supplied arguments are used to resolve the type, and to bind the constructor that is used to create the instance.</returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array that contains the arguments to be passed to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to be invoked. If the default constructor is desired, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is null, the CultureInfo for the current thread is used. (This is necessary to convert a String that represents 1000 to a Double value, for example, since 1000 is represented differently by different cultures.) </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character. -or-The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">A non-empty activation attributes array is passed to a type that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="typeName" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002EE6 RID: 12006 RVA: 0x000B4404 File Offset: 0x000B2604
		public virtual object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object obj;
			try
			{
				obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return obj;
		}

		/// <summary>Gets all the loaded modules that are part of this assembly.</summary>
		/// <returns>An array of modules.</returns>
		// Token: 0x06002EE7 RID: 12007 RVA: 0x000B4458 File Offset: 0x000B2658
		public Module[] GetLoadedModules()
		{
			return this.GetLoadedModules(false);
		}

		/// <summary>Gets all the modules that are part of this assembly.</summary>
		/// <returns>An array of modules.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The module to be loaded does not specify a file name extension. </exception>
		// Token: 0x06002EE8 RID: 12008 RVA: 0x000B4461 File Offset: 0x000B2661
		public Module[] GetModules()
		{
			return this.GetModules(false);
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x00033EF4 File Offset: 0x000320F4
		internal virtual Module[] GetModulesInternal()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the assembly that contains the code that is currently executing.</summary>
		/// <returns>The assembly that contains the code that is currently executing. </returns>
		// Token: 0x06002EEA RID: 12010
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetExecutingAssembly();

		/// <summary>Returns the <see cref="T:System.Reflection.Assembly" /> of the method that invoked the currently executing method.</summary>
		/// <returns>The Assembly object of the method that invoked the currently executing method.</returns>
		// Token: 0x06002EEB RID: 12011
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetCallingAssembly();

		// Token: 0x06002EEC RID: 12012
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalGetReferencedAssemblies(Assembly module);

		/// <summary>Returns the names of all the resources in this assembly.</summary>
		/// <returns>An array that contains the names of all the resources.</returns>
		// Token: 0x06002EED RID: 12013 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual string[] GetManifestResourceNames()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000B446C File Offset: 0x000B266C
		internal unsafe static AssemblyName[] GetReferencedAssemblies(Assembly module)
		{
			AssemblyName[] array2;
			using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(Assembly.InternalGetReferencedAssemblies(module)))
			{
				int length = safeGPtrArrayHandle.Length;
				try
				{
					AssemblyName[] array = new AssemblyName[length];
					for (int i = 0; i < length; i++)
					{
						AssemblyName assemblyName = new AssemblyName();
						MonoAssemblyName* ptr = (MonoAssemblyName*)(void*)safeGPtrArrayHandle[i];
						assemblyName.FillName(ptr, null, true, false, true, true);
						array[i] = assemblyName;
					}
					array2 = array;
				}
				finally
				{
					for (int j = 0; j < length; j++)
					{
						MonoAssemblyName* ptr2 = (MonoAssemblyName*)(void*)safeGPtrArrayHandle[j];
						RuntimeMarshal.FreeAssemblyName(ref *ptr2, true);
					}
				}
			}
			return array2;
		}

		/// <summary>Returns information about how the given resource has been persisted.</summary>
		/// <returns>An object that is populated with information about the resource's topology, or null if the resource is not found.</returns>
		/// <param name="resourceName">The case-sensitive name of the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="resourceName" /> parameter is an empty string (""). </exception>
		// Token: 0x06002EEF RID: 12015 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the host context with which the assembly was loaded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the host context with which the assembly was loaded, if any.</returns>
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x0004737A File Offset: 0x0004557A
		[MonoTODO("Currently it always returns zero")]
		[ComVisible(false)]
		public virtual long HostContext
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x00033EF4 File Offset: 0x000320F4
		internal virtual Module GetManifestModule()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value indicating whether this assembly was loaded into the reflection-only context.</summary>
		/// <returns>true if the assembly was loaded into the reflection-only context, rather than the execution context; otherwise, false.</returns>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x00033EF4 File Offset: 0x000320F4
		[ComVisible(false)]
		public virtual bool ReflectionOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002EF3 RID: 12019 RVA: 0x0006EF14 File Offset: 0x0006D114
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines whether this assembly and the specified object are equal.</summary>
		/// <returns>true if <paramref name="o" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with this instance. </param>
		// Token: 0x06002EF4 RID: 12020 RVA: 0x000726BE File Offset: 0x000708BE
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x00033EF4 File Offset: 0x000320F4
		internal virtual PermissionSet GrantedPermissionSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x00033EF4 File Offset: 0x000320F4
		internal virtual PermissionSet DeniedPermissionSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the grant set of the current assembly.</summary>
		/// <returns>The grant set of the current assembly.</returns>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual PermissionSet PermissionSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates which set of security rules the common language runtime (CLR) enforces for this assembly.</summary>
		/// <returns>The security rule set that the CLR enforces for this assembly.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x000B4524 File Offset: 0x000B2724
		public virtual SecurityRuleSet SecurityRuleSet
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000B452B File Offset: 0x000B272B
		private static Exception CreateNIE()
		{
			return new NotImplementedException("Derived classes must implement it");
		}

		/// <summary>Returns information about the attributes that have been applied to the current <see cref="T:System.Reflection.Assembly" />, expressed as <see cref="T:System.Reflection.CustomAttributeData" /> objects.</summary>
		/// <returns>A generic list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the current assembly.</returns>
		// Token: 0x06002EFA RID: 12026 RVA: 0x00033EF4 File Offset: 0x000320F4
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value that indicates whether the current assembly is loaded with full trust.</summary>
		/// <returns>true if the current assembly is loaded with full trust; otherwise, false.</returns>
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x0000C091 File Offset: 0x0000A291
		[MonoTODO]
		public bool IsFullyTrusted
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance, with the options of ignoring the case, and of throwing an exception if the type is not found.</summary>
		/// <returns>An object that represents the specified class.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <param name="throwOnError">true to throw an exception if the type is not found; false to return null. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid.-or- The length of <paramref name="name" /> exceeds 1024 characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true, and the type cannot be found.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="name" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06002EFC RID: 12028 RVA: 0x000B4524 File Offset: 0x000B2724
		public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the specified module in this assembly.</summary>
		/// <returns>The module being requested, or null if the module is not found.</returns>
		/// <param name="name">The name of the module being requested. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		// Token: 0x06002EFD RID: 12029 RVA: 0x000B4524 File Offset: 0x000B2724
		public virtual Module GetModule(string name)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the <see cref="T:System.Reflection.AssemblyName" /> objects for all the assemblies referenced by this assembly.</summary>
		/// <returns>An array that contains the fully parsed display names of all the assemblies referenced by this assembly.</returns>
		// Token: 0x06002EFE RID: 12030 RVA: 0x000B4524 File Offset: 0x000B2724
		public virtual AssemblyName[] GetReferencedAssemblies()
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets all the modules that are part of this assembly, specifying whether to include resource modules.</summary>
		/// <returns>An array of modules.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		// Token: 0x06002EFF RID: 12031 RVA: 0x000B4524 File Offset: 0x000B2724
		public virtual Module[] GetModules(bool getResourceModules)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets all the loaded modules that are part of this assembly, specifying whether to include resource modules.</summary>
		/// <returns>An array of modules.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		// Token: 0x06002F00 RID: 12032 RVA: 0x000B4524 File Offset: 0x000B2724
		[MonoTODO("Always returns the same as GetModules")]
		public virtual Module[] GetLoadedModules(bool getResourceModules)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the satellite assembly for the specified culture.</summary>
		/// <returns>The specified satellite assembly.</returns>
		/// <param name="culture">The specified culture. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the CultureInfo did not match the one specified. </exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly. </exception>
		// Token: 0x06002F01 RID: 12033 RVA: 0x000B4524 File Offset: 0x000B2724
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			throw Assembly.CreateNIE();
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
		// Token: 0x06002F02 RID: 12034 RVA: 0x000B4524 File Offset: 0x000B2724
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the module that contains the manifest for the current assembly. </summary>
		/// <returns>The module that contains the manifest for the assembly. </returns>
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x000B4524 File Offset: 0x000B2724
		public virtual Module ManifestModule
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		/// <summary>Gets a value indicating whether the assembly was loaded from the global assembly cache.</summary>
		/// <returns>true if the assembly was loaded from the global assembly cache; otherwise, false.</returns>
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000B4524 File Offset: 0x000B2724
		public virtual bool GlobalAssemblyCache
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		/// <summary>Gets a value that indicates whether the current assembly was generated dynamically in the current process by using reflection emit.</summary>
		/// <returns>true if the current assembly was generated dynamically in the current process; otherwise, false.</returns>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06002F05 RID: 12037 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsDynamic
		{
			get
			{
				return false;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Assembly" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The assembly to compare to <paramref name="right" />. </param>
		/// <param name="right">The assembly to compare to <paramref name="left" />.</param>
		// Token: 0x06002F06 RID: 12038 RVA: 0x000B4537 File Offset: 0x000B2737
		public static bool operator ==(Assembly left, Assembly right)
		{
			return left == right || (!((left == null) ^ (right == null)) && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Assembly" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The assembly to compare to <paramref name="right" />.</param>
		/// <param name="right">The assembly to compare to <paramref name="left" />.</param>
		// Token: 0x06002F07 RID: 12039 RVA: 0x000B4553 File Offset: 0x000B2753
		public static bool operator !=(Assembly left, Assembly right)
		{
			return left != right && (((left == null) ^ (right == null)) || !left.Equals(right));
		}

		/// <summary>Gets a collection of the types defined in this assembly.</summary>
		/// <returns>A collection of the types defined in this assembly.</returns>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000B4572 File Offset: 0x000B2772
		public virtual IEnumerable<TypeInfo> DefinedTypes
		{
			get
			{
				foreach (Type type in this.GetTypes())
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		/// <summary>Gets a collection of the public types defined in this assembly that are visible outside the assembly.</summary>
		/// <returns>A collection of the public types defined in this assembly that are visible outside the assembly.</returns>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000B4582 File Offset: 0x000B2782
		public virtual IEnumerable<Type> ExportedTypes
		{
			get
			{
				return this.GetExportedTypes();
			}
		}

		/// <summary>Gets a collection that contains the modules in this assembly.</summary>
		/// <returns>A collection that contains the modules in this assembly.</returns>
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000B458A File Offset: 0x000B278A
		public virtual IEnumerable<Module> Modules
		{
			get
			{
				return this.GetModules();
			}
		}

		/// <summary>Gets a collection that contains this assembly's custom attributes.</summary>
		/// <returns>A collection that contains this assembly's custom attributes.</returns>
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000B4592 File Offset: 0x000B2792
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000145B3 File Offset: 0x000127B3
		public virtual Type[] GetForwardedTypes()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x02000633 RID: 1587
		internal class ResolveEventHolder
		{
			// Token: 0x1400000D RID: 13
			// (add) Token: 0x06002F0E RID: 12046 RVA: 0x000B459C File Offset: 0x000B279C
			// (remove) Token: 0x06002F0F RID: 12047 RVA: 0x000B45D4 File Offset: 0x000B27D4
			public event ModuleResolveEventHandler ModuleResolve;
		}
	}
}
