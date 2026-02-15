using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using Mono.Security;

namespace System
{
	/// <summary>Represents an application domain, which is an isolated environment where applications execute. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001B5 RID: 437
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_AppDomain))]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AppDomain : MarshalByRefObject, _AppDomain, IEvidenceFactory
	{
		// Token: 0x06001060 RID: 4192 RVA: 0x00033991 File Offset: 0x00031B91
		internal static bool IsAppXModel()
		{
			return false;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00033991 File Offset: 0x00031B91
		internal static bool IsAppXDesignMode()
		{
			return false;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00002C89 File Offset: 0x00000E89
		internal static void CheckReflectionOnlyLoadSupported()
		{
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00002C89 File Offset: 0x00000E89
		internal static void CheckLoadFromSupported()
		{
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0003E751 File Offset: 0x0003C951
		private AppDomain()
		{
		}

		// Token: 0x06001065 RID: 4197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AppDomainSetup getSetup();

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0004586C File Offset: 0x00043A6C
		private AppDomainSetup SetupInformationNoCopy
		{
			get
			{
				return this.getSetup();
			}
		}

		/// <summary>Gets the application domain configuration information for this instance.</summary>
		/// <returns>The application domain initialization information.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00045874 File Offset: 0x00043A74
		public AppDomainSetup SetupInformation
		{
			get
			{
				return new AppDomainSetup(this.getSetup());
			}
		}

		/// <summary>Gets information describing permissions granted to an application and whether the application has a trust level that allows it to run.</summary>
		/// <returns>An object that encapsulates permission and trust information for the application in the application domain. </returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO]
		public ApplicationTrust ApplicationTrust
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the base directory that the assembly resolver uses to probe for assemblies.</summary>
		/// <returns>The base directory that the assembly resolver uses to probe for assemblies.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00045884 File Offset: 0x00043A84
		public string BaseDirectory
		{
			get
			{
				string applicationBase = this.SetupInformationNoCopy.ApplicationBase;
				if (SecurityManager.SecurityEnabled && applicationBase != null && applicationBase.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, applicationBase).Demand();
				}
				return applicationBase;
			}
		}

		/// <summary>Gets the path under the base directory where the assembly resolver should probe for private assemblies.</summary>
		/// <returns>The path under the base directory where the assembly resolver should probe for private assemblies.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x000458C0 File Offset: 0x00043AC0
		public string RelativeSearchPath
		{
			get
			{
				string privateBinPath = this.SetupInformationNoCopy.PrivateBinPath;
				if (SecurityManager.SecurityEnabled && privateBinPath != null && privateBinPath.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, privateBinPath).Demand();
				}
				return privateBinPath;
			}
		}

		/// <summary>Gets the directory that the assembly resolver uses to probe for dynamically created assemblies.</summary>
		/// <returns>The directory that the assembly resolver uses to probe for dynamically created assemblies.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x000458FC File Offset: 0x00043AFC
		public string DynamicDirectory
		{
			get
			{
				AppDomainSetup setupInformationNoCopy = this.SetupInformationNoCopy;
				if (setupInformationNoCopy.DynamicBase == null)
				{
					return null;
				}
				string text = Path.Combine(setupInformationNoCopy.DynamicBase, setupInformationNoCopy.ApplicationName);
				if (SecurityManager.SecurityEnabled && text != null && text.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				return text;
			}
		}

		/// <summary>Gets an indication whether the application domain is configured to shadow copy files.</summary>
		/// <returns>true if the application domain is configured to shadow copy files; otherwise, false.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0004594C File Offset: 0x00043B4C
		public bool ShadowCopyFiles
		{
			get
			{
				return this.SetupInformationNoCopy.ShadowCopyFiles == "true";
			}
		}

		// Token: 0x0600106D RID: 4205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string getFriendlyName();

		/// <summary>Gets the friendly name of this application domain.</summary>
		/// <returns>The friendly name of this application domain.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00045963 File Offset: 0x00043B63
		public string FriendlyName
		{
			get
			{
				return this.getFriendlyName();
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Policy.Evidence" /> associated with this application domain.</summary>
		/// <returns>The evidence associated with this application domain.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x0004596C File Offset: 0x00043B6C
		public Evidence Evidence
		{
			get
			{
				if (this._evidence == null)
				{
					lock (this)
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly == null)
						{
							if (this == AppDomain.DefaultDomain)
							{
								return new Evidence();
							}
							this._evidence = AppDomain.DefaultDomain.Evidence;
						}
						else
						{
							this._evidence = Evidence.GetDefaultHostEvidence(entryAssembly);
						}
					}
				}
				return new Evidence(this._evidence);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x000459F4 File Offset: 0x00043BF4
		internal IPrincipal DefaultPrincipal
		{
			get
			{
				if (AppDomain._principal == null)
				{
					PrincipalPolicy principalPolicy = this._principalPolicy;
					if (principalPolicy != PrincipalPolicy.UnauthenticatedPrincipal)
					{
						if (principalPolicy == PrincipalPolicy.WindowsPrincipal)
						{
							AppDomain._principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
						}
					}
					else
					{
						AppDomain._principal = new GenericPrincipal(new GenericIdentity(string.Empty, string.Empty), null);
					}
				}
				return AppDomain._principal;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00045A48 File Offset: 0x00043C48
		internal PermissionSet GrantedPermissionSet
		{
			get
			{
				return this._granted;
			}
		}

		/// <summary>Gets the permission set of a sandboxed application domain.</summary>
		/// <returns>The permission set of the sandboxed application domain.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00045A50 File Offset: 0x00043C50
		public PermissionSet PermissionSet
		{
			get
			{
				PermissionSet permissionSet;
				if ((permissionSet = this._granted) == null)
				{
					permissionSet = (this._granted = new PermissionSet(PermissionState.Unrestricted));
				}
				return permissionSet;
			}
		}

		// Token: 0x06001073 RID: 4211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain getCurDomain();

		/// <summary>Gets the current application domain for the current <see cref="T:System.Threading.Thread" />.</summary>
		/// <returns>The current application domain.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00045A76 File Offset: 0x00043C76
		public static AppDomain CurrentDomain
		{
			get
			{
				return AppDomain.getCurDomain();
			}
		}

		// Token: 0x06001075 RID: 4213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain getRootDomain();

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00045A80 File Offset: 0x00043C80
		internal static AppDomain DefaultDomain
		{
			get
			{
				if (AppDomain.default_domain == null)
				{
					AppDomain rootDomain = AppDomain.getRootDomain();
					if (rootDomain == AppDomain.CurrentDomain)
					{
						AppDomain.default_domain = rootDomain;
					}
					else
					{
						AppDomain.default_domain = (AppDomain)RemotingServices.GetDomainProxy(rootDomain);
					}
				}
				return AppDomain.default_domain;
			}
		}

		/// <summary>Appends the specified directory name to the private path list.</summary>
		/// <param name="path">The name of the directory to be appended to the private path. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001077 RID: 4215 RVA: 0x00045AC0 File Offset: 0x00043CC0
		[Obsolete("AppDomain.AppendPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead.")]
		public void AppendPrivatePath(string path)
		{
			if (path == null || path.Length == 0)
			{
				return;
			}
			AppDomainSetup setupInformationNoCopy = this.SetupInformationNoCopy;
			string text = setupInformationNoCopy.PrivateBinPath;
			if (text == null || text.Length == 0)
			{
				setupInformationNoCopy.PrivateBinPath = path;
				return;
			}
			text = text.Trim();
			if (text[text.Length - 1] != Path.PathSeparator)
			{
				text += Path.PathSeparator.ToString();
			}
			setupInformationNoCopy.PrivateBinPath = text + path;
		}

		/// <summary>Resets the path that specifies the location of private assemblies to the empty string ("").</summary>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001078 RID: 4216 RVA: 0x00045B34 File Offset: 0x00043D34
		[Obsolete("AppDomain.ClearPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead.")]
		public void ClearPrivatePath()
		{
			this.SetupInformationNoCopy.PrivateBinPath = string.Empty;
		}

		/// <summary>Resets the list of directories containing shadow copied assemblies to the empty string ("").</summary>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001079 RID: 4217 RVA: 0x00045B46 File Offset: 0x00043D46
		[Obsolete("Use AppDomainSetup.ShadowCopyDirectories")]
		public void ClearShadowCopyPath()
		{
			this.SetupInformationNoCopy.ShadowCopyDirectories = string.Empty;
		}

		/// <summary>Creates a new instance of a specified COM type. Parameters specify the name of a file that contains an assembly containing the type and the name of the type.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyName">The name of a file containing an assembly that defines the requested type. </param>
		/// <param name="typeName">The name of the requested type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">The type cannot be loaded. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">No public parameterless constructor was found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.MemberAccessException">
		///   <paramref name="typeName" /> is an abstract class. -or-This member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyName" /> is an empty string (""). </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">The COM object that is being referred to is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600107A RID: 4218 RVA: 0x00045B58 File Offset: 0x00043D58
		public ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
		{
			return Activator.CreateComInstanceFrom(assemblyName, typeName);
		}

		/// <summary>Creates a new instance of a specified COM type. Parameters specify the name of a file that contains an assembly containing the type and the name of the type.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyFile">The name of a file containing an assembly that defines the requested type. </param>
		/// <param name="typeName">The name of the requested type. </param>
		/// <param name="hashValue">Represents the value of the computed hash code. </param>
		/// <param name="hashAlgorithm">Represents the hash algorithm used by the assembly manifest. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">The type cannot be loaded. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">No public parameterless constructor was found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.MemberAccessException">
		///   <paramref name="typeName" /> is an abstract class. -or-This member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyFile" /> is the empty string (""). </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">The COM object that is being referred to is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600107B RID: 4219 RVA: 0x00045B61 File Offset: 0x00043D61
		public ObjectHandle CreateComInstanceFrom(string assemblyFile, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			return Activator.CreateComInstanceFrom(assemblyFile, typeName, hashValue, hashAlgorithm);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00045B6D File Offset: 0x00043D6D
		internal ObjectHandle InternalCreateInstanceWithNoSecurity(string assemblyName, string typeName)
		{
			return this.CreateInstance(assemblyName, typeName);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00045B78 File Offset: 0x00043D78
		internal ObjectHandle InternalCreateInstanceWithNoSecurity(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			return this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00045B9A File Offset: 0x00043D9A
		internal ObjectHandle InternalCreateInstanceFromWithNoSecurity(string assemblyName, string typeName)
		{
			return this.CreateInstanceFrom(assemblyName, typeName);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00045BA4 File Offset: 0x00043DA4
		internal ObjectHandle InternalCreateInstanceFromWithNoSecurity(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			return this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001080 RID: 4224 RVA: 0x00045BC6 File Offset: 0x00043DC6
		public ObjectHandle CreateInstance(string assemblyName, string typeName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly. A parameter specifies an array of activation attributes.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001081 RID: 4225 RVA: 0x00045BDD File Offset: 0x00043DDD
		public ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, activationAttributes);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly. Parameters specify a binder, binding flags, constructor arguments, culture-specific information used to interpret arguments, activation attributes, and authorization to create the type.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />.-or-<paramref name="securityAttributes" /> is not null. When legacy CAS policy is not enabled, <paramref name="securityAttributes" /> should be null.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001082 RID: 4226 RVA: 0x00045BF8 File Offset: 0x00043DF8
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		/// <summary>Creates a new instance of the specified type. Parameters specify the assembly where the type is defined, and the name of the type.</summary>
		/// <returns>An instance of the object specified by <paramref name="typeName" />.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001083 RID: 4227 RVA: 0x00045C28 File Offset: 0x00043E28
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type. Parameters specify the assembly where the type is defined, the name of the type, and an array of activation attributes.</summary>
		/// <returns>An instance of the object specified by <paramref name="typeName" />.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001084 RID: 4228 RVA: 0x00045C4C File Offset: 0x00043E4C
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type. Parameters specify the name of the type, and how it is found and created.</summary>
		/// <returns>An instance of the object specified by <paramref name="typeName" />.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">A culture-specific object used to govern the coercion of types. If <paramref name="culture" /> is null, the CultureInfo for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001085 RID: 4229 RVA: 0x00045C70 File Offset: 0x00043E70
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly. Parameters specify a binder, binding flags, constructor arguments, culture-specific information used to interpret arguments, and optional activation attributes.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-<paramref name="assemblyName" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		// Token: 0x06001086 RID: 4230 RVA: 0x00045CA0 File Offset: 0x00043EA0
		public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly, specifying whether the case of the type name is ignored; the binding attributes and the binder that are used to select the type to be created; the arguments of the constructor; the culture; and the activation attributes.</summary>
		/// <returns>An instance of the object specified by <paramref name="typeName" />.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">A culture-specific object used to govern the coercion of types. If <paramref name="culture" /> is null, the CultureInfo for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-<paramref name="assemblyName" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		// Token: 0x06001087 RID: 4231 RVA: 0x00045CD0 File Offset: 0x00043ED0
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>An object that is a wrapper for the new instance, or null if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />  method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-<paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		// Token: 0x06001088 RID: 4232 RVA: 0x00045CFC File Offset: 0x00043EFC
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file, specifying whether the case of the type name is ignored; the binding attributes and the binder that are used to select the type to be created; the arguments of the constructor; the culture; and the activation attributes.</summary>
		/// <returns>The requested object, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="assemblyFile">The file name and path of the assembly that defines the requested type. </param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-<paramref name="assemblyName" /> was compiled with a later version of the common language runtime that the version that is currently loaded.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		// Token: 0x06001089 RID: 4233 RVA: 0x00045D2C File Offset: 0x00043F2C
		public object CreateInstanceFromAndUnwrap(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>An object that is a wrapper for the new instance, or null if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />  method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">No parameterless public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600108A RID: 4234 RVA: 0x00045D58 File Offset: 0x00043F58
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>An object that is a wrapper for the new instance, or null if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />  method. </param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600108B RID: 4235 RVA: 0x00045D6F File Offset: 0x00043F6F
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, activationAttributes);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>An object that is a wrapper for the new instance, or null if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />  method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />.-or-<paramref name="securityAttributes" /> is not null. When legacy CAS policy is not enabled, <paramref name="securityAttributes" /> should be null.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600108C RID: 4236 RVA: 0x00045D88 File Offset: 0x00043F88
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>The requested object, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="assemblyName">The file name and path of the assembly that defines the requested type. </param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No parameterless public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600108D RID: 4237 RVA: 0x00045DB8 File Offset: 0x00043FB8
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>The requested object, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="assemblyName">The file name and path of the assembly that defines the requested type. </param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly (see the <see cref="P:System.Type.FullName" /> property). </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No parameterless public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600108E RID: 4238 RVA: 0x00045DDC File Offset: 0x00043FDC
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>The requested object, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="assemblyName">The file name and path of the assembly that defines the requested type. </param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600108F RID: 4239 RVA: 0x00045E00 File Offset: 0x00044000
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Defines a dynamic assembly with the specified name and access mode.</summary>
		/// <returns>A dynamic assembly with the specified name and access mode.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The access mode for the dynamic assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001090 RID: 4240 RVA: 0x00045E30 File Offset: 0x00044030
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			return this.DefineDynamicAssembly(name, access, null, null, null, null, null, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, and evidence.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001091 RID: 4241 RVA: 0x00045E4C File Offset: 0x0004404C
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence)
		{
			return this.DefineDynamicAssembly(name, access, null, evidence, null, null, null, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, and storage directory.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is null, the directory defaults to the current directory. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001092 RID: 4242 RVA: 0x00045E68 File Offset: 0x00044068
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, null, null, null, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, storage directory, and evidence.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is null, the directory defaults to the current directory. </param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001093 RID: 4243 RVA: 0x00045E84 File Offset: 0x00044084
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence)
		{
			return this.DefineDynamicAssembly(name, access, dir, evidence, null, null, null, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, and permission requests.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001094 RID: 4244 RVA: 0x00045EA0 File Offset: 0x000440A0
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, null, null, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, evidence, and permission requests.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001095 RID: 4245 RVA: 0x00045EC0 File Offset: 0x000440C0
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, null, evidence, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, storage directory, and permission requests.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is null, the directory defaults to the current directory. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001096 RID: 4246 RVA: 0x00045EE0 File Offset: 0x000440E0
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, storage directory, evidence, and permission requests.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is null, the directory defaults to the current directory. </param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001097 RID: 4247 RVA: 0x00045F00 File Offset: 0x00044100
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, storage directory, evidence, permission requests, and synchronization option.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the dynamic assembly will be saved. If <paramref name="dir" /> is null, the directory defaults to the current directory. </param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <param name="isSynchronized">true to synchronize the creation of modules, types, and members in the dynamic assembly; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001098 RID: 4248 RVA: 0x00045F1F File Offset: 0x0004411F
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			AppDomain.ValidateAssemblyName(name.Name);
			AssemblyBuilder assemblyBuilder = new AssemblyBuilder(name, dir, access, false);
			assemblyBuilder.AddPermissionRequests(requiredPermissions, optionalPermissions, refusedPermissions);
			return assemblyBuilder;
		}

		/// <summary>Defines a dynamic assembly with the specified name, access mode, storage directory, evidence, permission requests, synchronization option, and custom attributes.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the dynamic assembly will be saved. If <paramref name="dir" /> is null, the current directory is used. </param>
		/// <param name="evidence">The evidence that is supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <param name="isSynchronized">true to synchronize the creation of modules, types, and members in the dynamic assembly; otherwise, false. </param>
		/// <param name="assemblyAttributes">An enumerable list of attributes to be applied to the assembly, or null if there are no attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> starts with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		// Token: 0x06001099 RID: 4249 RVA: 0x00045F50 File Offset: 0x00044150
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			AssemblyBuilder assemblyBuilder = this.DefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, isSynchronized);
			if (assemblyAttributes != null)
			{
				foreach (CustomAttributeBuilder customAttributeBuilder in assemblyAttributes)
				{
					assemblyBuilder.SetCustomAttribute(customAttributeBuilder);
				}
			}
			return assemblyBuilder;
		}

		/// <summary>Defines a dynamic assembly with the specified name, access mode, and custom attributes.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The access mode for the dynamic assembly. </param>
		/// <param name="assemblyAttributes">An enumerable list of attributes to be applied to the assembly, or null if there are no attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> starts with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		// Token: 0x0600109A RID: 4250 RVA: 0x00045FB4 File Offset: 0x000441B4
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			return this.DefineDynamicAssembly(name, access, null, null, null, null, null, false, assemblyAttributes);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, storage directory, and synchronization option.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the dynamic assembly will be saved. If <paramref name="dir" /> is null, the current directory is used. </param>
		/// <param name="isSynchronized">true to synchronize the creation of modules, types, and members in the dynamic assembly; otherwise, false. </param>
		/// <param name="assemblyAttributes">An enumerable list of attributes to be applied to the assembly, or null if there are no attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> starts with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		// Token: 0x0600109B RID: 4251 RVA: 0x00045FD0 File Offset: 0x000441D0
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, null, null, null, isSynchronized, assemblyAttributes);
		}

		/// <summary>Defines a dynamic assembly with the specified name, access mode, and custom attributes, and using the specified source for its security context.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The access mode for the dynamic assembly. </param>
		/// <param name="assemblyAttributes">An enumerable list of attributes to be applied to the assembly, or null if there are no attributes.</param>
		/// <param name="securityContextSource">The source of the security context.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> starts with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="securityContextSource" /> was not one of the enumeration values.</exception>
		// Token: 0x0600109C RID: 4252 RVA: 0x00045FEE File Offset: 0x000441EE
		[MonoLimitation("The argument securityContextSource is ignored")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes, SecurityContextSource securityContextSource)
		{
			return this.DefineDynamicAssembly(name, access, assemblyAttributes);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00045FF9 File Offset: 0x000441F9
		internal AssemblyBuilder DefineInternalDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			return new AssemblyBuilder(name, null, access, true);
		}

		/// <summary>Executes the code in another application domain that is identified by the specified delegate.</summary>
		/// <param name="callBackDelegate">A delegate that specifies a method to call. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBackDelegate" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600109E RID: 4254 RVA: 0x00046004 File Offset: 0x00044204
		public void DoCallBack(CrossAppDomainDelegate callBackDelegate)
		{
			if (callBackDelegate != null)
			{
				callBackDelegate();
			}
		}

		/// <summary>Executes the assembly contained in the specified file.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600109F RID: 4255 RVA: 0x0004600F File Offset: 0x0004420F
		public int ExecuteAssembly(string assemblyFile)
		{
			return this.ExecuteAssembly(assemblyFile, null, null);
		}

		/// <summary>Executes the assembly contained in the specified file, using the specified evidence.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010A0 RID: 4256 RVA: 0x0004601A File Offset: 0x0004421A
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity)
		{
			return this.ExecuteAssembly(assemblyFile, assemblySecurity, null);
		}

		/// <summary>Executes the assembly contained in the specified file, using the specified evidence and arguments.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute. </param>
		/// <param name="assemblySecurity">The supplied evidence for the assembly. </param>
		/// <param name="args">The arguments to the entry point of the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblySecurity" /> is not null. When legacy CAS policy is not enabled, <paramref name="assemblySecurity" /> should be null. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010A1 RID: 4257 RVA: 0x00046028 File Offset: 0x00044228
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, assemblySecurity);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly contained in the specified file, using the specified evidence, arguments, hash value, and hash algorithm.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute. </param>
		/// <param name="assemblySecurity">The supplied evidence for the assembly. </param>
		/// <param name="args">The arguments to the entry point of the assembly. </param>
		/// <param name="hashValue">Represents the value of the computed hash code. </param>
		/// <param name="hashAlgorithm">Represents the hash algorithm used by the assembly manifest. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblySecurity" /> is not null. When legacy CAS policy is not enabled, <paramref name="assemblySecurity" /> should be null. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010A2 RID: 4258 RVA: 0x00046048 File Offset: 0x00044248
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, assemblySecurity, hashValue, hashAlgorithm);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly contained in the specified file, using the specified arguments.</summary>
		/// <returns>The value that is returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute.</param>
		/// <param name="args">The arguments to the entry point of the assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-<paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		// Token: 0x060010A3 RID: 4259 RVA: 0x0004606C File Offset: 0x0004426C
		public int ExecuteAssembly(string assemblyFile, string[] args)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, null);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly contained in the specified file, using the specified arguments, hash value, and hash algorithm.</summary>
		/// <returns>The value that is returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute.</param>
		/// <param name="args">The arguments to the entry point of the assembly. </param>
		/// <param name="hashValue">Represents the value of the computed hash code. </param>
		/// <param name="hashAlgorithm">Represents the hash algorithm used by the assembly manifest. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-<paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		// Token: 0x060010A4 RID: 4260 RVA: 0x0004608C File Offset: 0x0004428C
		public int ExecuteAssembly(string assemblyFile, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, null, hashValue, hashAlgorithm);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x000460AC File Offset: 0x000442AC
		private int ExecuteAssemblyInternal(Assembly a, string[] args)
		{
			if (a.EntryPoint == null)
			{
				throw new MissingMethodException("Entry point not found in assembly '" + a.FullName + "'.");
			}
			return this.ExecuteAssembly(a, args);
		}

		// Token: 0x060010A6 RID: 4262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int ExecuteAssembly(Assembly a, string[] args);

		// Token: 0x060010A7 RID: 4263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Assembly[] GetAssemblies(bool refOnly);

		/// <summary>Gets the assemblies that have been loaded into the execution context of this application domain.</summary>
		/// <returns>An array of assemblies in this application domain.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010A8 RID: 4264 RVA: 0x000460DF File Offset: 0x000442DF
		public Assembly[] GetAssemblies()
		{
			return this.GetAssemblies(false);
		}

		/// <summary>Gets the value stored in the current application domain for the specified name.</summary>
		/// <returns>The value of the <paramref name="name" /> property, or null if the property does not exist.</returns>
		/// <param name="name">The name of a predefined application domain property, or the name of an application domain property you have defined.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060010A9 RID: 4265
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetData(string name);

		/// <summary>Gets the type of the current instance.</summary>
		/// <returns>The type of the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010AA RID: 4266 RVA: 0x0003395C File Offset: 0x00031B5C
		public new Type GetType()
		{
			return base.GetType();
		}

		/// <summary>Gives the <see cref="T:System.AppDomain" /> an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010AB RID: 4267 RVA: 0x000082D2 File Offset: 0x000064D2
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060010AC RID: 4268
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Assembly LoadAssembly(string assemblyRef, Evidence securityEvidence, bool refOnly, ref StackCrawlMark stackMark);

		/// <summary>Loads an <see cref="T:System.Reflection.Assembly" /> given its <see cref="T:System.Reflection.AssemblyName" />.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyRef">An object that describes the assembly to load. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010AD RID: 4269 RVA: 0x000460E8 File Offset: 0x000442E8
		public Assembly Load(AssemblyName assemblyRef)
		{
			return this.Load(assemblyRef, null);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000460F2 File Offset: 0x000442F2
		internal Assembly LoadSatellite(AssemblyName assemblyRef, bool throwOnError, ref StackCrawlMark stackMark)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			Assembly assembly = this.LoadAssembly(assemblyRef.FullName, null, false, ref stackMark);
			if (assembly == null && throwOnError)
			{
				throw new FileNotFoundException(null, assemblyRef.Name);
			}
			return assembly;
		}

		/// <summary>Loads an <see cref="T:System.Reflection.Assembly" /> given its <see cref="T:System.Reflection.AssemblyName" />.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyRef">An object that describes the assembly to load. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is null</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010AF RID: 4271 RVA: 0x0004612C File Offset: 0x0004432C
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			if (assemblyRef.Name == null || assemblyRef.Name.Length == 0)
			{
				if (assemblyRef.CodeBase != null)
				{
					return Assembly.LoadFrom(assemblyRef.CodeBase, assemblySecurity);
				}
				throw new ArgumentException(Locale.GetText("assemblyRef.Name cannot be empty."), "assemblyRef");
			}
			else
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				Assembly assembly = this.LoadAssembly(assemblyRef.FullName, assemblySecurity, false, ref stackCrawlMark);
				if (assembly != null)
				{
					return assembly;
				}
				if (assemblyRef.CodeBase == null)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				string text = assemblyRef.CodeBase;
				if (text.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
				{
					text = new Uri(text).LocalPath;
				}
				try
				{
					assembly = Assembly.LoadFrom(text, assemblySecurity);
				}
				catch
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				AssemblyName name = assembly.GetName();
				if (assemblyRef.Name != name.Name)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				if (assemblyRef.Version != null && assemblyRef.Version != new Version(0, 0, 0, 0) && assemblyRef.Version != name.Version)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				if (assemblyRef.CultureInfo != null && assemblyRef.CultureInfo.Equals(name))
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				byte[] publicKeyToken = assemblyRef.GetPublicKeyToken();
				if (publicKeyToken != null && publicKeyToken.Length != 0)
				{
					byte[] publicKeyToken2 = name.GetPublicKeyToken();
					if (publicKeyToken2 == null || publicKeyToken.Length != publicKeyToken2.Length)
					{
						throw new FileNotFoundException(null, assemblyRef.Name);
					}
					for (int i = publicKeyToken.Length - 1; i >= 0; i--)
					{
						if (publicKeyToken2[i] != publicKeyToken[i])
						{
							throw new FileNotFoundException(null, assemblyRef.Name);
						}
					}
				}
				return assembly;
			}
		}

		/// <summary>Loads an <see cref="T:System.Reflection.Assembly" /> given its display name.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B0 RID: 4272 RVA: 0x000462F4 File Offset: 0x000444F4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Assembly Load(string assemblyString)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.Load(assemblyString, null, false, ref stackCrawlMark);
		}

		/// <summary>Loads an <see cref="T:System.Reflection.Assembly" /> given its display name.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B1 RID: 4273 RVA: 0x00046310 File Offset: 0x00044510
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.Load(assemblyString, assemblySecurity, false, ref stackCrawlMark);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0004632A File Offset: 0x0004452A
		internal Assembly Load(string assemblyString, Evidence assemblySecurity, bool refonly, ref StackCrawlMark stackMark)
		{
			if (assemblyString == null)
			{
				throw new ArgumentNullException("assemblyString");
			}
			if (assemblyString.Length == 0)
			{
				throw new ArgumentException("assemblyString cannot have zero length");
			}
			Assembly assembly = this.LoadAssembly(assemblyString, assemblySecurity, refonly, ref stackMark);
			if (assembly == null)
			{
				throw new FileNotFoundException(null, assemblyString);
			}
			return assembly;
		}

		/// <summary>Loads the <see cref="T:System.Reflection.Assembly" /> with a common object file format (COFF) based image containing an emitted <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">An array of type byte that is a COFF-based image containing an emitted assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B3 RID: 4275 RVA: 0x00046369 File Offset: 0x00044569
		public Assembly Load(byte[] rawAssembly)
		{
			return this.Load(rawAssembly, null, null);
		}

		/// <summary>Loads the <see cref="T:System.Reflection.Assembly" /> with a common object file format (COFF) based image containing an emitted <see cref="T:System.Reflection.Assembly" />. The raw bytes representing the symbols for the <see cref="T:System.Reflection.Assembly" /> are also loaded.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">An array of type byte that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">An array of type byte containing the raw bytes representing the symbols for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B4 RID: 4276 RVA: 0x00046374 File Offset: 0x00044574
		public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			return this.Load(rawAssembly, rawSymbolStore, null);
		}

		// Token: 0x060010B5 RID: 4277
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Assembly LoadAssemblyRaw(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence, bool refonly);

		/// <summary>Loads the <see cref="T:System.Reflection.Assembly" /> with a common object file format (COFF) based image containing an emitted <see cref="T:System.Reflection.Assembly" />. The raw bytes representing the symbols for the <see cref="T:System.Reflection.Assembly" /> are also loaded.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">An array of type byte that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">An array of type byte containing the raw bytes representing the symbols for the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="securityEvidence" /> is not null. When legacy CAS policy is not enabled, <paramref name="securityEvidence" /> should be null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B6 RID: 4278 RVA: 0x0004637F File Offset: 0x0004457F
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			return this.Load(rawAssembly, rawSymbolStore, securityEvidence, false);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0004638B File Offset: 0x0004458B
		internal Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence, bool refonly)
		{
			if (rawAssembly == null)
			{
				throw new ArgumentNullException("rawAssembly");
			}
			Assembly assembly = this.LoadAssemblyRaw(rawAssembly, rawSymbolStore, securityEvidence, refonly);
			assembly.FromByteArray = true;
			return assembly;
		}

		/// <summary>Establishes the security policy level for this application domain.</summary>
		/// <param name="domainPolicy">The security policy level. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domainPolicy" /> is null. </exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">The security policy level has already been set. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlDomainPolicy" />
		/// </PermissionSet>
		// Token: 0x060010B8 RID: 4280 RVA: 0x000463B0 File Offset: 0x000445B0
		[Obsolete("AppDomain policy levels are obsolete")]
		public void SetAppDomainPolicy(PolicyLevel domainPolicy)
		{
			if (domainPolicy == null)
			{
				throw new ArgumentNullException("domainPolicy");
			}
			if (this._granted != null)
			{
				throw new PolicyException(Locale.GetText("An AppDomain policy is already specified."));
			}
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			PolicyStatement policyStatement = domainPolicy.Resolve(this._evidence);
			this._granted = policyStatement.PermissionSet;
		}

		/// <summary>Establishes the specified directory path as the location where assemblies are shadow copied.</summary>
		/// <param name="path">The fully qualified path to the shadow copy location. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010B9 RID: 4281 RVA: 0x0004640A File Offset: 0x0004460A
		[Obsolete("Use AppDomainSetup.SetCachePath")]
		public void SetCachePath(string path)
		{
			this.SetupInformationNoCopy.CachePath = path;
		}

		/// <summary>Specifies how principal and identity objects should be attached to a thread if the thread attempts to bind to a principal while executing in this application domain.</summary>
		/// <param name="policy">One of the <see cref="T:System.Security.Principal.PrincipalPolicy" /> values that specifies the type of the principal object to attach to threads. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060010BA RID: 4282 RVA: 0x00046418 File Offset: 0x00044618
		public void SetPrincipalPolicy(PrincipalPolicy policy)
		{
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			this._principalPolicy = policy;
			AppDomain._principal = null;
		}

		/// <summary>Turns on shadow copying.</summary>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010BB RID: 4283 RVA: 0x00046435 File Offset: 0x00044635
		[Obsolete("Use AppDomainSetup.ShadowCopyFiles")]
		public void SetShadowCopyFiles()
		{
			this.SetupInformationNoCopy.ShadowCopyFiles = "true";
		}

		/// <summary>Establishes the specified directory path as the location of assemblies to be shadow copied.</summary>
		/// <param name="path">A list of directory names, where each name is separated by a semicolon. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010BC RID: 4284 RVA: 0x00046447 File Offset: 0x00044647
		[Obsolete("Use AppDomainSetup.ShadowCopyDirectories")]
		public void SetShadowCopyPath(string path)
		{
			this.SetupInformationNoCopy.ShadowCopyDirectories = path;
		}

		/// <summary>Sets the default principal object to be attached to threads if they attempt to bind to a principal while executing in this application domain.</summary>
		/// <param name="principal">The principal object to attach to threads. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="principal" /> is null. </exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">The thread principal has already been set. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x060010BD RID: 4285 RVA: 0x00046455 File Offset: 0x00044655
		public void SetThreadPrincipal(IPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			if (AppDomain._principal != null)
			{
				throw new PolicyException(Locale.GetText("principal already present."));
			}
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			AppDomain._principal = principal;
		}

		// Token: 0x060010BE RID: 4286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain InternalSetDomainByID(int domain_id);

		// Token: 0x060010BF RID: 4287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain InternalSetDomain(AppDomain context);

		// Token: 0x060010C0 RID: 4288
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalPushDomainRef(AppDomain domain);

		// Token: 0x060010C1 RID: 4289
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalPushDomainRefByID(int domain_id);

		// Token: 0x060010C2 RID: 4290
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalPopDomainRef();

		// Token: 0x060010C3 RID: 4291
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context InternalSetContext(Context context);

		// Token: 0x060010C4 RID: 4292
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context InternalGetContext();

		// Token: 0x060010C5 RID: 4293
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context InternalGetDefaultContext();

		// Token: 0x060010C6 RID: 4294
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InternalGetProcessGuid(string newguid);

		// Token: 0x060010C7 RID: 4295 RVA: 0x00046490 File Offset: 0x00044690
		internal static object InvokeInDomain(AppDomain domain, MethodInfo method, object obj, object[] args)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			bool flag = false;
			object obj3;
			try
			{
				AppDomain.InternalPushDomainRef(domain);
				flag = true;
				AppDomain.InternalSetDomain(domain);
				Exception ex;
				object obj2 = ((RuntimeMethodInfo)method).InternalInvoke(obj, args, out ex);
				if (ex != null)
				{
					throw ex;
				}
				obj3 = obj2;
			}
			finally
			{
				AppDomain.InternalSetDomain(currentDomain);
				if (flag)
				{
					AppDomain.InternalPopDomainRef();
				}
			}
			return obj3;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000464EC File Offset: 0x000446EC
		internal static object InvokeInDomainByID(int domain_id, MethodInfo method, object obj, object[] args)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			bool flag = false;
			object obj3;
			try
			{
				AppDomain.InternalPushDomainRefByID(domain_id);
				flag = true;
				AppDomain.InternalSetDomainByID(domain_id);
				Exception ex;
				object obj2 = ((RuntimeMethodInfo)method).InternalInvoke(obj, args, out ex);
				if (ex != null)
				{
					throw ex;
				}
				obj3 = obj2;
			}
			finally
			{
				AppDomain.InternalSetDomain(currentDomain);
				if (flag)
				{
					AppDomain.InternalPopDomainRef();
				}
			}
			return obj3;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00046548 File Offset: 0x00044748
		internal static string GetProcessGuid()
		{
			if (AppDomain._process_guid == null)
			{
				AppDomain._process_guid = AppDomain.InternalGetProcessGuid(Guid.NewGuid().ToString());
			}
			return AppDomain._process_guid;
		}

		/// <summary>Creates a new application domain with the specified name.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010CA RID: 4298 RVA: 0x0004657E File Offset: 0x0004477E
		public static AppDomain CreateDomain(string friendlyName)
		{
			return AppDomain.CreateDomain(friendlyName, null, null);
		}

		/// <summary>Creates a new application domain with the given name using the supplied evidence.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. This friendly name can be displayed in user interfaces to identify the domain. For more information, see <see cref="P:System.AppDomain.FriendlyName" />. </param>
		/// <param name="securityInfo">Evidence that establishes the identity of the code that runs in the application domain. Pass null to use the evidence of the current application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010CB RID: 4299 RVA: 0x00046588 File Offset: 0x00044788
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, null);
		}

		// Token: 0x060010CC RID: 4300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain createDomain(string friendlyName, AppDomainSetup info);

		/// <summary>Creates a new application domain using the specified name, evidence, and application domain setup information.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. This friendly name can be displayed in user interfaces to identify the domain. For more information, see <see cref="P:System.AppDomain.FriendlyName" />. </param>
		/// <param name="securityInfo">Evidence that establishes the identity of the code that runs in the application domain. Pass null to use the evidence of the current application domain.</param>
		/// <param name="info">An object that contains application domain initialization information. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010CD RID: 4301 RVA: 0x00046594 File Offset: 0x00044794
		[MonoLimitation("Currently it does not allow the setup in the other domain")]
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			AppDomain defaultDomain = AppDomain.DefaultDomain;
			if (info == null)
			{
				if (defaultDomain == null)
				{
					info = new AppDomainSetup();
				}
				else
				{
					info = defaultDomain.SetupInformation;
				}
			}
			else
			{
				info = new AppDomainSetup(info);
			}
			if (defaultDomain != null)
			{
				if (!info.Equals(defaultDomain.SetupInformation))
				{
					if (info.ApplicationBase == null)
					{
						info.ApplicationBase = defaultDomain.SetupInformation.ApplicationBase;
					}
					if (info.ConfigurationFile == null)
					{
						info.ConfigurationFile = Path.GetFileName(defaultDomain.SetupInformation.ConfigurationFile);
					}
				}
			}
			else if (info.ConfigurationFile == null)
			{
				info.ConfigurationFile = "[I don't have a config file]";
			}
			if (info.AppDomainInitializer != null && !info.AppDomainInitializer.Method.IsStatic)
			{
				throw new ArgumentException("Non-static methods cannot be invoked as an appdomain initializer");
			}
			info.SerializeNonPrimitives();
			AppDomain appDomain = (AppDomain)RemotingServices.GetDomainProxy(AppDomain.createDomain(friendlyName, info));
			if (securityInfo == null)
			{
				if (defaultDomain == null)
				{
					appDomain._evidence = null;
				}
				else
				{
					appDomain._evidence = defaultDomain.Evidence;
				}
			}
			else
			{
				appDomain._evidence = new Evidence(securityInfo);
			}
			if (info.AppDomainInitializer != null)
			{
				AppDomain.Loader loader = new AppDomain.Loader(info.AppDomainInitializer.Method.DeclaringType.Assembly.Location);
				appDomain.DoCallBack(new CrossAppDomainDelegate(loader.Load));
				AppDomain.Initializer initializer = new AppDomain.Initializer(info.AppDomainInitializer, info.AppDomainInitializerArguments);
				appDomain.DoCallBack(new CrossAppDomainDelegate(initializer.Initialize));
			}
			return appDomain;
		}

		/// <summary>Creates a new application domain with the given name, using evidence, application base path, relative search path, and a parameter that specifies whether a shadow copy of an assembly is to be loaded into the application domain.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. This friendly name can be displayed in user interfaces to identify the domain. For more information, see <see cref="P:System.AppDomain.FriendlyName" />. </param>
		/// <param name="securityInfo">Evidence that establishes the identity of the code that runs in the application domain. Pass null to use the evidence of the current application domain.</param>
		/// <param name="appBasePath">The base directory that the assembly resolver uses to probe for assemblies. For more information, see <see cref="P:System.AppDomain.BaseDirectory" />. </param>
		/// <param name="appRelativeSearchPath">The path relative to the base directory where the assembly resolver should probe for private assemblies. For more information, see <see cref="P:System.AppDomain.RelativeSearchPath" />. </param>
		/// <param name="shadowCopyFiles">If true, a shadow copy of an assembly is loaded into this application domain. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010CE RID: 4302 RVA: 0x000466F8 File Offset: 0x000448F8
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, AppDomain.CreateDomainSetup(appBasePath, appRelativeSearchPath, shadowCopyFiles));
		}

		/// <summary>Creates a new application domain using the specified name, evidence, application domain setup information, default permission set, and array of fully trusted assemblies.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. This friendly name can be displayed in user interfaces to identify the domain. For more information, see the description of <see cref="P:System.AppDomain.FriendlyName" />.</param>
		/// <param name="securityInfo">Evidence that establishes the identity of the code that runs in the application domain. Pass null to use the evidence of the current application domain.</param>
		/// <param name="info">An object that contains application domain initialization information.</param>
		/// <param name="grantSet">A default permission set that is granted to all assemblies loaded into the new application domain that do not have specific grants. </param>
		/// <param name="fullTrustAssemblies">An array of strong names representing assemblies to be considered fully trusted in the new application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The application domain is null.-or-The <see cref="P:System.AppDomainSetup.ApplicationBase" /> property is not set on the <see cref="T:System.AppDomainSetup" /> object that is supplied for <paramref name="info" />. </exception>
		// Token: 0x060010CF RID: 4303 RVA: 0x0004670A File Offset: 0x0004490A
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info, PermissionSet grantSet, params global::System.Security.Policy.StrongName[] fullTrustAssemblies)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.ApplicationTrust = new ApplicationTrust(grantSet, fullTrustAssemblies ?? EmptyArray<global::System.Security.Policy.StrongName>.Value);
			return AppDomain.CreateDomain(friendlyName, securityInfo, info);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0004673C File Offset: 0x0004493C
		private static AppDomainSetup CreateDomainSetup(string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
		{
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			appDomainSetup.ApplicationBase = appBasePath;
			appDomainSetup.PrivateBinPath = appRelativeSearchPath;
			if (shadowCopyFiles)
			{
				appDomainSetup.ShadowCopyFiles = "true";
			}
			else
			{
				appDomainSetup.ShadowCopyFiles = "false";
			}
			return appDomainSetup;
		}

		// Token: 0x060010D1 RID: 4305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalIsFinalizingForUnload(int domain_id);

		/// <summary>Indicates whether this application domain is unloading, and the objects it contains are being finalized by the common language runtime.</summary>
		/// <returns>true if this application domain is unloading and the common language runtime has started invoking finalizers; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010D2 RID: 4306 RVA: 0x00046779 File Offset: 0x00044979
		public bool IsFinalizingForUnload()
		{
			return AppDomain.InternalIsFinalizingForUnload(this.getDomainID());
		}

		// Token: 0x060010D3 RID: 4307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalUnload(int domain_id);

		// Token: 0x060010D4 RID: 4308 RVA: 0x00046786 File Offset: 0x00044986
		private int getDomainID()
		{
			return Thread.GetDomainID();
		}

		/// <summary>Unloads the specified application domain.</summary>
		/// <param name="domain">An application domain to unload. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> is null. </exception>
		/// <exception cref="T:System.CannotUnloadAppDomainException">
		///   <paramref name="domain" /> could not be unloaded. </exception>
		/// <exception cref="T:System.Exception">An error occurred during the unload process.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010D5 RID: 4309 RVA: 0x0004678D File Offset: 0x0004498D
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.MayFail)]
		public static void Unload(AppDomain domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			AppDomain.InternalUnload(domain.getDomainID());
		}

		/// <summary>Assigns the specified value to the specified application domain property.</summary>
		/// <param name="name">The name of a user-defined application domain property to create or change. </param>
		/// <param name="data">The value of the property. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010D6 RID: 4310
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetData(string name, object data);

		/// <summary>Assigns the specified value to the specified application domain property, with a specified permission to demand of the caller when the property is retrieved.</summary>
		/// <param name="name">The name of a user-defined application domain property to create or change. </param>
		/// <param name="data">The value of the property. </param>
		/// <param name="permission">The permission to demand of the caller when the property is retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="name" /> specifies a system-defined property string and <paramref name="permission" /> is not null.</exception>
		// Token: 0x060010D7 RID: 4311 RVA: 0x000467A8 File Offset: 0x000449A8
		[MonoLimitation("The permission field is ignored")]
		public void SetData(string name, object data, IPermission permission)
		{
			this.SetData(name, data);
		}

		/// <summary>Establishes the specified directory path as the base directory for subdirectories where dynamically generated files are stored and accessed.</summary>
		/// <param name="path">The fully qualified path that is the base directory for subdirectories where dynamic assemblies are stored. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010D8 RID: 4312 RVA: 0x000467B2 File Offset: 0x000449B2
		[Obsolete("Use AppDomainSetup.DynamicBase")]
		public void SetDynamicBase(string path)
		{
			this.SetupInformationNoCopy.DynamicBase = path;
		}

		/// <summary>Gets the current thread identifier.</summary>
		/// <returns>A 32-bit signed integer that is the identifier of the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060010D9 RID: 4313 RVA: 0x000467C0 File Offset: 0x000449C0
		[Obsolete("AppDomain.GetCurrentThreadId has been deprecated because it does not provide a stable Id when managed threads are running on fibers (aka lightweight threads). To get a stable identifier for a managed thread, use the ManagedThreadId property on Thread.'")]
		public static int GetCurrentThreadId()
		{
			return Thread.CurrentThreadId;
		}

		/// <summary>Obtains a string representation that includes the friendly name of the application domain and any context policies.</summary>
		/// <returns>A string formed by concatenating the literal string "Name:", the friendly name of the application domain, and either string representations of the context policies or the string "There are no context policies." </returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The application domain represented by the current <see cref="T:System.AppDomain" /> has been unloaded.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010DA RID: 4314 RVA: 0x00045963 File Offset: 0x00043B63
		public override string ToString()
		{
			return this.getFriendlyName();
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000467C8 File Offset: 0x000449C8
		private static void ValidateAssemblyName(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException("The Name of AssemblyName cannot be null or a zero-length string.");
			}
			bool flag = true;
			for (int i = 0; i < name.Length; i++)
			{
				char c = name[i];
				if (i == 0 && char.IsWhiteSpace(c))
				{
					flag = false;
					break;
				}
				if (c == '/' || c == '\\' || c == ':')
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				throw new ArgumentException("The Name of AssemblyName cannot start with whitespace, or contain '/', '\\'  or ':'.");
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00046836 File Offset: 0x00044A36
		private void DoAssemblyLoad(Assembly assembly)
		{
			if (this.AssemblyLoad == null)
			{
				return;
			}
			this.AssemblyLoad(this, new AssemblyLoadEventArgs(assembly));
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00046854 File Offset: 0x00044A54
		private Assembly DoAssemblyResolve(string name, Assembly requestingAssembly, bool refonly)
		{
			ResolveEventHandler resolveEventHandler;
			if (refonly)
			{
				resolveEventHandler = this.ReflectionOnlyAssemblyResolve;
			}
			else
			{
				resolveEventHandler = this.AssemblyResolve;
			}
			if (resolveEventHandler == null)
			{
				return null;
			}
			Dictionary<string, object> dictionary;
			if (refonly)
			{
				dictionary = AppDomain.assembly_resolve_in_progress_refonly;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					AppDomain.assembly_resolve_in_progress_refonly = dictionary;
				}
			}
			else
			{
				dictionary = AppDomain.assembly_resolve_in_progress;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					AppDomain.assembly_resolve_in_progress = dictionary;
				}
			}
			if (dictionary.ContainsKey(name))
			{
				return null;
			}
			dictionary[name] = null;
			Assembly assembly2;
			try
			{
				Delegate[] invocationList = resolveEventHandler.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Assembly assembly = ((ResolveEventHandler)invocationList[i])(this, new ResolveEventArgs(name, requestingAssembly));
					if (assembly != null)
					{
						return assembly;
					}
				}
				assembly2 = null;
			}
			finally
			{
				dictionary.Remove(name);
			}
			return assembly2;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00046918 File Offset: 0x00044B18
		internal Assembly DoTypeBuilderResolve(TypeBuilder tb)
		{
			if (this.TypeResolve == null)
			{
				return null;
			}
			return this.DoTypeResolve(tb.FullName);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00046930 File Offset: 0x00044B30
		internal Assembly DoTypeResolve(string name)
		{
			if (this.TypeResolve == null)
			{
				return null;
			}
			Dictionary<string, object> dictionary = AppDomain.type_resolve_in_progress;
			if (dictionary == null)
			{
				dictionary = (AppDomain.type_resolve_in_progress = new Dictionary<string, object>());
			}
			if (dictionary.ContainsKey(name))
			{
				return null;
			}
			dictionary[name] = null;
			Assembly assembly2;
			try
			{
				Delegate[] invocationList = this.TypeResolve.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Assembly assembly = ((ResolveEventHandler)invocationList[i])(this, new ResolveEventArgs(name));
					if (assembly != null)
					{
						return assembly;
					}
				}
				assembly2 = null;
			}
			finally
			{
				dictionary.Remove(name);
			}
			return assembly2;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000469CC File Offset: 0x00044BCC
		internal Assembly DoResourceResolve(string name, Assembly requesting)
		{
			if (this.ResourceResolve == null)
			{
				return null;
			}
			Delegate[] invocationList = this.ResourceResolve.GetInvocationList();
			for (int i = 0; i < invocationList.Length; i++)
			{
				Assembly assembly = ((ResolveEventHandler)invocationList[i])(this, new ResolveEventArgs(name, requesting));
				if (assembly != null)
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00046A1F File Offset: 0x00044C1F
		private void DoDomainUnload()
		{
			if (this.DomainUnload != null)
			{
				this.DomainUnload(this, null);
			}
		}

		// Token: 0x060010E2 RID: 4322
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void DoUnhandledException(Exception e);

		// Token: 0x060010E3 RID: 4323 RVA: 0x00046A36 File Offset: 0x00044C36
		internal void DoUnhandledException(UnhandledExceptionEventArgs args)
		{
			if (this.UnhandledException != null)
			{
				this.UnhandledException(this, args);
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00046A4D File Offset: 0x00044C4D
		internal byte[] GetMarshalledDomainObjRef()
		{
			return CADSerializer.SerializeObject(RemotingServices.Marshal(AppDomain.CurrentDomain, null, typeof(AppDomain))).GetBuffer();
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00046A70 File Offset: 0x00044C70
		internal void ProcessMessageInDomain(byte[] arrRequest, CADMethodCallMessage cadMsg, out byte[] arrResponse, out CADMethodReturnMessage cadMrm)
		{
			IMessage message;
			if (arrRequest != null)
			{
				message = CADSerializer.DeserializeMessage(new MemoryStream(arrRequest), null);
			}
			else
			{
				message = new MethodCall(cadMsg);
			}
			IMessage message2 = ChannelServices.SyncDispatchMessage(message);
			cadMrm = CADMethodReturnMessage.Create(message2);
			if (cadMrm == null)
			{
				arrResponse = CADSerializer.SerializeMessage(message2).GetBuffer();
				return;
			}
			arrResponse = null;
		}

		/// <summary>Occurs when an assembly is loaded.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060010E6 RID: 4326 RVA: 0x00046ABC File Offset: 0x00044CBC
		// (remove) Token: 0x060010E7 RID: 4327 RVA: 0x00046AF4 File Offset: 0x00044CF4
		public event AssemblyLoadEventHandler AssemblyLoad;

		/// <summary>Occurs when the resolution of an assembly fails.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060010E8 RID: 4328 RVA: 0x00046B2C File Offset: 0x00044D2C
		// (remove) Token: 0x060010E9 RID: 4329 RVA: 0x00046B64 File Offset: 0x00044D64
		public event ResolveEventHandler AssemblyResolve;

		/// <summary>Occurs when an <see cref="T:System.AppDomain" /> is about to be unloaded.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060010EA RID: 4330 RVA: 0x00046B9C File Offset: 0x00044D9C
		// (remove) Token: 0x060010EB RID: 4331 RVA: 0x00046BD4 File Offset: 0x00044DD4
		public event EventHandler DomainUnload;

		/// <summary>Occurs when the default application domain's parent process exits.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060010EC RID: 4332 RVA: 0x00046C0C File Offset: 0x00044E0C
		// (remove) Token: 0x060010ED RID: 4333 RVA: 0x00046C44 File Offset: 0x00044E44
		public event EventHandler ProcessExit;

		/// <summary>Occurs when the resolution of a resource fails because the resource is not a valid linked or embedded resource in the assembly.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060010EE RID: 4334 RVA: 0x00046C7C File Offset: 0x00044E7C
		// (remove) Token: 0x060010EF RID: 4335 RVA: 0x00046CB4 File Offset: 0x00044EB4
		public event ResolveEventHandler ResourceResolve;

		/// <summary>Occurs when the resolution of a type fails.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060010F0 RID: 4336 RVA: 0x00046CEC File Offset: 0x00044EEC
		// (remove) Token: 0x060010F1 RID: 4337 RVA: 0x00046D24 File Offset: 0x00044F24
		public event ResolveEventHandler TypeResolve;

		/// <summary>Occurs when an exception is not caught.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060010F2 RID: 4338 RVA: 0x00046D5C File Offset: 0x00044F5C
		// (remove) Token: 0x060010F3 RID: 4339 RVA: 0x00046D94 File Offset: 0x00044F94
		public event UnhandledExceptionEventHandler UnhandledException;

		/// <summary>Occurs when an exception is thrown in managed code, before the runtime searches the call stack for an exception handler in the application domain.</summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060010F4 RID: 4340 RVA: 0x00046DCC File Offset: 0x00044FCC
		// (remove) Token: 0x060010F5 RID: 4341 RVA: 0x00046E04 File Offset: 0x00045004
		public event EventHandler<FirstChanceExceptionEventArgs> FirstChanceException;

		/// <summary>Gets a value that indicates whether the current application domain has a set of permissions that is granted to all assemblies that are loaded into the application domain.</summary>
		/// <returns>true if the current application domain has a homogenous set of permissions; otherwise, false.</returns>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0000C091 File Offset: 0x0000A291
		[MonoTODO]
		public bool IsHomogenous
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether assemblies that are loaded into the current application domain execute with full trust.</summary>
		/// <returns>true if assemblies that are loaded into the current application domain execute with full trust; otherwise, false.</returns>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0000C091 File Offset: 0x0000A291
		[MonoTODO]
		public bool IsFullyTrusted
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the domain manager that was provided by the host when the application domain was initialized.</summary>
		/// <returns>An object that represents the domain manager provided by the host when the application domain was initialized, or null if no domain manager was provided.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlDomainPolicy" />
		/// </PermissionSet>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00046E39 File Offset: 0x00045039
		public AppDomainManager DomainManager
		{
			get
			{
				return this._domain_manager;
			}
		}

		/// <summary>Occurs when the resolution of an assembly fails in the reflection-only context.</summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060010F9 RID: 4345 RVA: 0x00046E44 File Offset: 0x00045044
		// (remove) Token: 0x060010FA RID: 4346 RVA: 0x00046E7C File Offset: 0x0004507C
		public event ResolveEventHandler ReflectionOnlyAssemblyResolve;

		/// <summary>Gets the activation context for the current application domain.</summary>
		/// <returns>An object that represents the activation context for the current application domain, or null if the domain has no activation context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00046EB1 File Offset: 0x000450B1
		public ActivationContext ActivationContext
		{
			get
			{
				return this._activation;
			}
		}

		/// <summary>Gets the identity of the application in the application domain.</summary>
		/// <returns>An object that identifies the application in the application domain.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00046EB9 File Offset: 0x000450B9
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._applicationIdentity;
			}
		}

		/// <summary>Gets an integer that uniquely identifies the application domain within the process. </summary>
		/// <returns>An integer that identifies the application domain.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00046EC1 File Offset: 0x000450C1
		public int Id
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.getDomainID();
			}
		}

		/// <summary>Returns the assembly display name after policy has been applied.</summary>
		/// <returns>A string containing the assembly display name after policy has been applied.</returns>
		/// <param name="assemblyName">The assembly display name, in the form provided by the <see cref="P:System.Reflection.Assembly.FullName" /> property.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010FE RID: 4350 RVA: 0x00046EC9 File Offset: 0x000450C9
		[ComVisible(false)]
		[MonoTODO("This routine only returns the parameter currently")]
		public string ApplyPolicy(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length == 0)
			{
				throw new ArgumentException("assemblyName");
			}
			return assemblyName;
		}

		/// <summary>Creates a new application domain with the given name, using evidence, application base path, relative search path, and a parameter that specifies whether a shadow copy of an assembly is to be loaded into the application domain. Specifies a callback method that is invoked when the application domain is initialized, and an array of string arguments to pass the callback method.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. This friendly name can be displayed in user interfaces to identify the domain. For more information, see <see cref="P:System.AppDomain.FriendlyName" />. </param>
		/// <param name="securityInfo">Evidence that establishes the identity of the code that runs in the application domain. Pass null to use the evidence of the current application domain.</param>
		/// <param name="appBasePath">The base directory that the assembly resolver uses to probe for assemblies. For more information, see <see cref="P:System.AppDomain.BaseDirectory" />. </param>
		/// <param name="appRelativeSearchPath">The path relative to the base directory where the assembly resolver should probe for private assemblies. For more information, see <see cref="P:System.AppDomain.RelativeSearchPath" />. </param>
		/// <param name="shadowCopyFiles">true to load a shadow copy of an assembly into the application domain. </param>
		/// <param name="adInit">An <see cref="T:System.AppDomainInitializer" /> delegate that represents a callback method to invoke when the new <see cref="T:System.AppDomain" /> object is initialized.</param>
		/// <param name="adInitArgs">An array of string arguments to be passed to the callback represented by <paramref name="adInit" />, when the new <see cref="T:System.AppDomain" /> object is initialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060010FF RID: 4351 RVA: 0x00046EF0 File Offset: 0x000450F0
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles, AppDomainInitializer adInit, string[] adInitArgs)
		{
			AppDomainSetup appDomainSetup = AppDomain.CreateDomainSetup(appBasePath, appRelativeSearchPath, shadowCopyFiles);
			appDomainSetup.AppDomainInitializerArguments = adInitArgs;
			appDomainSetup.AppDomainInitializer = adInit;
			return AppDomain.CreateDomain(friendlyName, securityInfo, appDomainSetup);
		}

		/// <summary>Executes an assembly given its display name.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001100 RID: 4352 RVA: 0x00046F1F File Offset: 0x0004511F
		public int ExecuteAssemblyByName(string assemblyName)
		{
			return this.ExecuteAssemblyByName(assemblyName, null, null);
		}

		/// <summary>Executes an assembly given its display name, using the specified evidence.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001101 RID: 4353 RVA: 0x00046F2A File Offset: 0x0004512A
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity)
		{
			return this.ExecuteAssemblyByName(assemblyName, assemblySecurity, null);
		}

		/// <summary>Executes the assembly given its display name, using the specified evidence and arguments.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <param name="args">Command-line arguments to pass when starting the process. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblySecurity" /> is not null. When legacy CAS policy is not enabled, <paramref name="assemblySecurity" /> should be null. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001102 RID: 4354 RVA: 0x00046F38 File Offset: 0x00045138
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, assemblySecurity);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly given an <see cref="T:System.Reflection.AssemblyName" />, using the specified evidence and arguments.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> object representing the name of the assembly. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <param name="args">Command-line arguments to pass when starting the process. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblySecurity" /> is not null. When legacy CAS policy is not enabled, <paramref name="assemblySecurity" /> should be null. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001103 RID: 4355 RVA: 0x00046F58 File Offset: 0x00045158
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssemblyByName(AssemblyName assemblyName, Evidence assemblySecurity, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, assemblySecurity);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly given its display name, using the specified arguments.</summary>
		/// <returns>The value that is returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="args">Command-line arguments to pass when starting the process.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-<paramref name="assemblyName" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		// Token: 0x06001104 RID: 4356 RVA: 0x00046F78 File Offset: 0x00045178
		public int ExecuteAssemblyByName(string assemblyName, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, null);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly given an <see cref="T:System.Reflection.AssemblyName" />, using the specified arguments.</summary>
		/// <returns>The value that is returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> object representing the name of the assembly.</param>
		/// <param name="args">Command-line arguments to pass when starting the process.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-<paramref name="assemblyName" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		// Token: 0x06001105 RID: 4357 RVA: 0x00046F98 File Offset: 0x00045198
		public int ExecuteAssemblyByName(AssemblyName assemblyName, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, null);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Returns a value that indicates whether the application domain is the default application domain for the process.</summary>
		/// <returns>true if the current <see cref="T:System.AppDomain" /> object represents the default application domain for the process; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001106 RID: 4358 RVA: 0x00046FB5 File Offset: 0x000451B5
		public bool IsDefaultAppDomain()
		{
			return this == AppDomain.DefaultDomain;
		}

		/// <summary>Returns the assemblies that have been loaded into the reflection-only context of the application domain.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.Assembly" /> objects that represent the assemblies loaded into the reflection-only context of the application domain.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">An operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001107 RID: 4359 RVA: 0x00046FBF File Offset: 0x000451BF
		public Assembly[] ReflectionOnlyGetAssemblies()
		{
			return this.GetAssemblies(true);
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06001108 RID: 4360 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _AppDomain.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06001109 RID: 4361 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _AppDomain.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600110A RID: 4362 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _AppDomain.GetTypeInfoCount(out uint pcTInfo)
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
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600110B RID: 4363 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _AppDomain.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a nullable Boolean value that indicates whether any compatibility switches are set, and if so, whether the specified compatibility switch is set.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) if no compatibility switches are set; otherwise, a Boolean value that indicates whether the compatibility switch that is specified by <paramref name="value" /> is set.</returns>
		/// <param name="value">The compatibility switch to test.</param>
		// Token: 0x0600110C RID: 4364 RVA: 0x00046FC8 File Offset: 0x000451C8
		public bool? IsCompatibilitySwitchSet(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return new bool?(this.compatibility_switch != null && this.compatibility_switch.Contains(value));
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00046FF4 File Offset: 0x000451F4
		internal void SetCompatibilitySwitch(string value)
		{
			if (this.compatibility_switch == null)
			{
				this.compatibility_switch = new List<string>();
			}
			this.compatibility_switch.Add(value);
		}

		/// <summary>Gets or sets a value that indicates whether CPU and memory monitoring of application domains is enabled for the current process. Once monitoring is enabled for a process, it cannot be disabled.</summary>
		/// <returns>true if monitoring is enabled; otherwise false.</returns>
		/// <exception cref="T:System.ArgumentException">The current process attempted to assign the value false to this property.</exception>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00033991 File Offset: 0x00031B91
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO("Currently always returns false")]
		public static bool MonitoringIsEnabled
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of bytes that survived the last collection and that are known to be referenced by the current application domain.</summary>
		/// <returns>The number of surviving bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The static (Shared in Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> property is set to false.</exception>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO]
		public long MonitoringSurvivedMemorySize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the total bytes that survived from the last collection for all application domains in the process.</summary>
		/// <returns>The total number of surviving bytes for the process.</returns>
		/// <exception cref="T:System.InvalidOperationException">The static (Shared in Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> property is set to false.</exception>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO]
		public static long MonitoringSurvivedProcessMemorySize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the total size, in bytes, of all memory allocations that have been made by the application domain since it was created, without subtracting memory that has been collected. </summary>
		/// <returns>The total size of all memory allocations.</returns>
		/// <exception cref="T:System.InvalidOperationException">The static (Shared in Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> property is set to false.</exception>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO]
		public long MonitoringTotalAllocatedMemorySize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the total processor time that has been used by all threads while executing in the current application domain, since the process started.</summary>
		/// <returns>Total processor time for the current application domain.</returns>
		/// <exception cref="T:System.InvalidOperationException">The static (Shared in Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> property is set to false.</exception>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO]
		public TimeSpan MonitoringTotalProcessorTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040006BE RID: 1726
		private IntPtr _mono_app_domain;

		// Token: 0x040006BF RID: 1727
		private static string _process_guid;

		// Token: 0x040006C0 RID: 1728
		[ThreadStatic]
		private static Dictionary<string, object> type_resolve_in_progress;

		// Token: 0x040006C1 RID: 1729
		[ThreadStatic]
		private static Dictionary<string, object> assembly_resolve_in_progress;

		// Token: 0x040006C2 RID: 1730
		[ThreadStatic]
		private static Dictionary<string, object> assembly_resolve_in_progress_refonly;

		// Token: 0x040006C3 RID: 1731
		private Evidence _evidence;

		// Token: 0x040006C4 RID: 1732
		private PermissionSet _granted;

		// Token: 0x040006C5 RID: 1733
		private PrincipalPolicy _principalPolicy;

		// Token: 0x040006C6 RID: 1734
		[ThreadStatic]
		private static IPrincipal _principal;

		// Token: 0x040006C7 RID: 1735
		private static AppDomain default_domain;

		// Token: 0x040006D0 RID: 1744
		private AppDomainManager _domain_manager;

		// Token: 0x040006D2 RID: 1746
		private ActivationContext _activation;

		// Token: 0x040006D3 RID: 1747
		private ApplicationIdentity _applicationIdentity;

		// Token: 0x040006D4 RID: 1748
		private List<string> compatibility_switch;

		// Token: 0x020001B6 RID: 438
		[Serializable]
		private class Loader
		{
			// Token: 0x06001114 RID: 4372 RVA: 0x00047015 File Offset: 0x00045215
			public Loader(string assembly)
			{
				this.assembly = assembly;
			}

			// Token: 0x06001115 RID: 4373 RVA: 0x00047024 File Offset: 0x00045224
			public void Load()
			{
				Assembly.LoadFrom(this.assembly);
			}

			// Token: 0x040006D5 RID: 1749
			private string assembly;
		}

		// Token: 0x020001B7 RID: 439
		[Serializable]
		private class Initializer
		{
			// Token: 0x06001116 RID: 4374 RVA: 0x00047032 File Offset: 0x00045232
			public Initializer(AppDomainInitializer initializer, string[] arguments)
			{
				this.initializer = initializer;
				this.arguments = arguments;
			}

			// Token: 0x06001117 RID: 4375 RVA: 0x00047048 File Offset: 0x00045248
			public void Initialize()
			{
				this.initializer(this.arguments);
			}

			// Token: 0x040006D6 RID: 1750
			private AppDomainInitializer initializer;

			// Token: 0x040006D7 RID: 1751
			private string[] arguments;
		}
	}
}
