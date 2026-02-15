using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using Mono.Security;
using Unity;

namespace System
{
	/// <summary>Represents assembly binding information that can be added to an instance of <see cref="T:System.AppDomain" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C1 RID: 449
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AppDomainSetup : IAppDomainSetup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class.</summary>
		// Token: 0x0600116F RID: 4463 RVA: 0x00003CE1 File Offset: 0x00001EE1
		public AppDomainSetup()
		{
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00047EC0 File Offset: 0x000460C0
		internal AppDomainSetup(AppDomainSetup setup)
		{
			this.application_base = setup.application_base;
			this.application_name = setup.application_name;
			this.cache_path = setup.cache_path;
			this.configuration_file = setup.configuration_file;
			this.dynamic_base = setup.dynamic_base;
			this.license_file = setup.license_file;
			this.private_bin_path = setup.private_bin_path;
			this.private_bin_path_probe = setup.private_bin_path_probe;
			this.shadow_copy_directories = setup.shadow_copy_directories;
			this.shadow_copy_files = setup.shadow_copy_files;
			this.publisher_policy = setup.publisher_policy;
			this.path_changed = setup.path_changed;
			this.loader_optimization = setup.loader_optimization;
			this.disallow_binding_redirects = setup.disallow_binding_redirects;
			this.disallow_code_downloads = setup.disallow_code_downloads;
			this._activationArguments = setup._activationArguments;
			this.domain_initializer = setup.domain_initializer;
			this.application_trust = setup.application_trust;
			this.domain_initializer_args = setup.domain_initializer_args;
			this.disallow_appbase_probe = setup.disallow_appbase_probe;
			this.configuration_bytes = setup.configuration_bytes;
			this.manager_assembly = setup.manager_assembly;
			this.manager_type = setup.manager_type;
			this.partial_visible_assemblies = setup.partial_visible_assemblies;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class with the specified activation arguments required for manifest-based activation of an application domain.</summary>
		/// <param name="activationArguments">An object that specifies information required for the manifest-based activation of a new application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationArguments" /> is null.</exception>
		// Token: 0x06001171 RID: 4465 RVA: 0x00047FF3 File Offset: 0x000461F3
		public AppDomainSetup(ActivationArguments activationArguments)
		{
			this._activationArguments = activationArguments;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class with the specified activation context to use for manifest-based activation of an application domain.</summary>
		/// <param name="activationContext">The activation context to be used for an application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is null.</exception>
		// Token: 0x06001172 RID: 4466 RVA: 0x00048002 File Offset: 0x00046202
		public AppDomainSetup(ActivationContext activationContext)
		{
			this._activationArguments = new ActivationArguments(activationContext);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00048018 File Offset: 0x00046218
		private static string GetAppBase(string appBase)
		{
			if (appBase == null)
			{
				return null;
			}
			if (appBase == "")
			{
				appBase = Path.DirectorySeparatorChar.ToString();
			}
			if (appBase.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
			{
				appBase = new Uri(appBase).LocalPath;
				if (Path.DirectorySeparatorChar != '/')
				{
					appBase = appBase.Replace('/', Path.DirectorySeparatorChar);
				}
			}
			appBase = Path.GetFullPath(appBase);
			if (Path.DirectorySeparatorChar != '/')
			{
				bool flag = appBase.StartsWith("\\\\?\\", StringComparison.Ordinal);
				if (appBase.IndexOf(':', flag ? 6 : 2) != -1)
				{
					throw new NotSupportedException("The given path's format is not supported.");
				}
			}
			string directoryName = Path.GetDirectoryName(appBase);
			if (directoryName != null && directoryName.LastIndexOfAny(Path.GetInvalidPathChars()) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid path characters in path: '{0}'"), appBase), "appBase");
			}
			string fileName = Path.GetFileName(appBase);
			if (fileName != null && fileName.LastIndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid filename characters in path: '{0}'"), appBase), "appBase");
			}
			return appBase;
		}

		/// <summary>Gets or sets the name of the directory containing the application.</summary>
		/// <returns>The name of the application base directory.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00048118 File Offset: 0x00046318
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x00048125 File Offset: 0x00046325
		public string ApplicationBase
		{
			get
			{
				return AppDomainSetup.GetAppBase(this.application_base);
			}
			set
			{
				this.application_base = value;
			}
		}

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>The name of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0004812E File Offset: 0x0004632E
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x00048136 File Offset: 0x00046336
		public string ApplicationName
		{
			get
			{
				return this.application_name;
			}
			set
			{
				this.application_name = value;
			}
		}

		/// <summary>Gets or sets the name of an area specific to the application where files are shadow copied. </summary>
		/// <returns>The fully qualified name of the directory path and file name where files are shadow copied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004813F File Offset: 0x0004633F
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x00048147 File Offset: 0x00046347
		public string CachePath
		{
			get
			{
				return this.cache_path;
			}
			set
			{
				this.cache_path = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for an application domain.</summary>
		/// <returns>The name of the configuration file.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00048150 File Offset: 0x00046350
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x0004819F File Offset: 0x0004639F
		public string ConfigurationFile
		{
			get
			{
				if (this.configuration_file == null)
				{
					return null;
				}
				if (Path.IsPathRooted(this.configuration_file))
				{
					return this.configuration_file;
				}
				if (this.ApplicationBase == null)
				{
					throw new MemberAccessException("The ApplicationBase must be set before retrieving this property.");
				}
				return Path.Combine(this.ApplicationBase, this.configuration_file);
			}
			set
			{
				this.configuration_file = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the &lt;publisherPolicy&gt; section of the configuration file is applied to an application domain.</summary>
		/// <returns>true if the &lt;publisherPolicy&gt; section of the configuration file for an application domain is ignored; false if the declared publisher policy is honored.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x000481A8 File Offset: 0x000463A8
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x000481B0 File Offset: 0x000463B0
		public bool DisallowPublisherPolicy
		{
			get
			{
				return this.publisher_policy;
			}
			set
			{
				this.publisher_policy = value;
			}
		}

		/// <summary>Gets or sets the base directory where the directory for dynamically generated files is located.</summary>
		/// <returns>The directory where the <see cref="P:System.AppDomain.DynamicDirectory" /> is located.NoteThe return value of this property is different from the value assigned. See the Remarks section.</returns>
		/// <exception cref="T:System.MemberAccessException">This property cannot be set because the application name on the application domain is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x000481BC File Offset: 0x000463BC
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x0004820C File Offset: 0x0004640C
		public string DynamicBase
		{
			get
			{
				if (this.dynamic_base == null)
				{
					return null;
				}
				if (Path.IsPathRooted(this.dynamic_base))
				{
					return this.dynamic_base;
				}
				if (this.ApplicationBase == null)
				{
					throw new MemberAccessException("The ApplicationBase must be set before retrieving this property.");
				}
				return Path.Combine(this.ApplicationBase, this.dynamic_base);
			}
			set
			{
				if (this.application_name == null)
				{
					throw new MemberAccessException("ApplicationName must be set before the DynamicBase can be set.");
				}
				this.dynamic_base = Path.Combine(value, ((uint)this.application_name.GetHashCode()).ToString("x"));
			}
		}

		/// <summary>Gets or sets the location of the license file associated with this domain.</summary>
		/// <returns>The location and name of the license file.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00048250 File Offset: 0x00046450
		// (set) Token: 0x06001181 RID: 4481 RVA: 0x00048258 File Offset: 0x00046458
		public string LicenseFile
		{
			get
			{
				return this.license_file;
			}
			set
			{
				this.license_file = value;
			}
		}

		/// <summary>Specifies the optimization policy used to load an executable.</summary>
		/// <returns>An enumerated constant that is used with the <see cref="T:System.LoaderOptimizationAttribute" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00048261 File Offset: 0x00046461
		// (set) Token: 0x06001183 RID: 4483 RVA: 0x00048269 File Offset: 0x00046469
		[MonoLimitation("In Mono this is controlled by the --share-code flag")]
		public LoaderOptimization LoaderOptimization
		{
			get
			{
				return this.loader_optimization;
			}
			set
			{
				this.loader_optimization = value;
			}
		}

		/// <summary>Gets or sets the display name of the assembly that provides the type of the application domain manager for application domains created using this <see cref="T:System.AppDomainSetup" /> object.</summary>
		/// <returns>The display name of the assembly that provides the <see cref="T:System.Type" /> of the application domain manager.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00048272 File Offset: 0x00046472
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x0004827A File Offset: 0x0004647A
		public string AppDomainManagerAssembly
		{
			get
			{
				return this.manager_assembly;
			}
			set
			{
				this.manager_assembly = value;
			}
		}

		/// <summary>Gets or sets the full name of the type that provides the application domain manager for application domains created using this <see cref="T:System.AppDomainSetup" /> object.</summary>
		/// <returns>The full name of the type, including the namespace.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00048283 File Offset: 0x00046483
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x0004828B File Offset: 0x0004648B
		public string AppDomainManagerType
		{
			get
			{
				return this.manager_type;
			}
			set
			{
				this.manager_type = value;
			}
		}

		/// <summary>Gets or sets a list of assemblies marked with the <see cref="F:System.Security.PartialTrustVisibilityLevel.NotVisibleByDefault" /> flag that are made visible to partial-trust code running in a sandboxed application domain. </summary>
		/// <returns>An array of partial assembly names, where each partial name consists of the simple assembly name and the public key.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00048294 File Offset: 0x00046494
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x0004829C File Offset: 0x0004649C
		public string[] PartialTrustVisibleAssemblies
		{
			get
			{
				return this.partial_visible_assemblies;
			}
			set
			{
				if (value != null)
				{
					this.partial_visible_assemblies = (string[])value.Clone();
					Array.Sort<string>(this.partial_visible_assemblies, StringComparer.OrdinalIgnoreCase);
					return;
				}
				this.partial_visible_assemblies = null;
			}
		}

		/// <summary>Gets or sets the list of directories under the application base directory that are probed for private assemblies.</summary>
		/// <returns>A list of directory names separated by semicolons.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x000482CA File Offset: 0x000464CA
		// (set) Token: 0x0600118B RID: 4491 RVA: 0x000482D2 File Offset: 0x000464D2
		public string PrivateBinPath
		{
			get
			{
				return this.private_bin_path;
			}
			set
			{
				this.private_bin_path = value;
				this.path_changed = true;
			}
		}

		/// <summary>Gets or sets a string value that includes or excludes <see cref="P:System.AppDomainSetup.ApplicationBase" /> from the search path for the application, and searches only <see cref="P:System.AppDomainSetup.PrivateBinPath" />.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) to include the application base path when searching for assemblies; any non-null string value to exclude the path. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x000482E2 File Offset: 0x000464E2
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x000482EA File Offset: 0x000464EA
		public string PrivateBinPathProbe
		{
			get
			{
				return this.private_bin_path_probe;
			}
			set
			{
				this.private_bin_path_probe = value;
				this.path_changed = true;
			}
		}

		/// <summary>Gets or sets the names of the directories containing assemblies to be shadow copied.</summary>
		/// <returns>A list of directory names separated by semicolons.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x000482FA File Offset: 0x000464FA
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x00048302 File Offset: 0x00046502
		public string ShadowCopyDirectories
		{
			get
			{
				return this.shadow_copy_directories;
			}
			set
			{
				this.shadow_copy_directories = value;
			}
		}

		/// <summary>Gets or sets a string that indicates whether shadow copying is turned on or off.</summary>
		/// <returns>The string value "true" to indicate that shadow copying is turned on; or "false" to indicate that shadow copying is turned off.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0004830B File Offset: 0x0004650B
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x00048313 File Offset: 0x00046513
		public string ShadowCopyFiles
		{
			get
			{
				return this.shadow_copy_files;
			}
			set
			{
				this.shadow_copy_files = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether an application domain allows assembly binding redirection.</summary>
		/// <returns>true if redirection of assemblies is not allowed; false if it is allowed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0004831C File Offset: 0x0004651C
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00048324 File Offset: 0x00046524
		public bool DisallowBindingRedirects
		{
			get
			{
				return this.disallow_binding_redirects;
			}
			set
			{
				this.disallow_binding_redirects = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether HTTP download of assemblies is allowed for an application domain.</summary>
		/// <returns>true if HTTP download of assemblies is not allowed; false if it is allowed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0004832D File Offset: 0x0004652D
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x00048335 File Offset: 0x00046535
		public bool DisallowCodeDownload
		{
			get
			{
				return this.disallow_code_downloads;
			}
			set
			{
				this.disallow_code_downloads = value;
			}
		}

		/// <summary>Gets or sets a string that specifies the target version and profile of the .NET Framework for the application domain, in a format that can be parsed by the <see cref="M:System.Runtime.Versioning.FrameworkName.#ctor(System.String)" /> constructor. </summary>
		/// <returns>The target version and profile of the .NET Framework. </returns>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0004833E File Offset: 0x0004653E
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x00048346 File Offset: 0x00046546
		public string TargetFrameworkName { get; set; }

		/// <summary>Gets or sets data about the activation of an application domain.</summary>
		/// <returns>An object that contains data about the activation of an application domain.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set to an <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object whose application identity does not match the application identity of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object returned by the <see cref="P:System.AppDomainSetup.ApplicationTrust" /> property. No exception is thrown if the <see cref="P:System.AppDomainSetup.ApplicationTrust" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0004834F File Offset: 0x0004654F
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x0004836C File Offset: 0x0004656C
		public ActivationArguments ActivationArguments
		{
			get
			{
				if (this._activationArguments != null)
				{
					return this._activationArguments;
				}
				this.DeserializeNonPrimitives();
				return this._activationArguments;
			}
			set
			{
				this._activationArguments = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.AppDomainInitializer" /> delegate, which represents a callback method that is invoked when the application domain is initialized.</summary>
		/// <returns>A delegate that represents a callback method that is invoked when the application domain is initialized.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00048375 File Offset: 0x00046575
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00048392 File Offset: 0x00046592
		[MonoLimitation("it needs to be invoked within the created domain")]
		public AppDomainInitializer AppDomainInitializer
		{
			get
			{
				if (this.domain_initializer != null)
				{
					return this.domain_initializer;
				}
				this.DeserializeNonPrimitives();
				return this.domain_initializer;
			}
			set
			{
				this.domain_initializer = value;
			}
		}

		/// <summary>Gets or sets the arguments passed to the callback method represented by the <see cref="T:System.AppDomainInitializer" /> delegate. The callback method is invoked when the application domain is initialized.</summary>
		/// <returns>An array of strings that is passed to the callback method represented by the <see cref="T:System.AppDomainInitializer" /> delegate, when the callback method is invoked during <see cref="T:System.AppDomain" /> initialization.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0004839B File Offset: 0x0004659B
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x000483A3 File Offset: 0x000465A3
		[MonoLimitation("it needs to be used to invoke the initializer within the created domain")]
		public string[] AppDomainInitializerArguments
		{
			get
			{
				return this.domain_initializer_args;
			}
			set
			{
				this.domain_initializer_args = value;
			}
		}

		/// <summary>Gets or sets an object containing security and trust information.</summary>
		/// <returns>An object that contains security and trust information. </returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set to an <see cref="T:System.Security.Policy.ApplicationTrust" /> object whose application identity does not match the application identity of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object returned by the <see cref="P:System.AppDomainSetup.ActivationArguments" /> property. No exception is thrown if the <see cref="P:System.AppDomainSetup.ActivationArguments" /> property is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x000483AC File Offset: 0x000465AC
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x000483DC File Offset: 0x000465DC
		[MonoNotSupported("This property exists but not considered.")]
		public ApplicationTrust ApplicationTrust
		{
			get
			{
				if (this.application_trust != null)
				{
					return this.application_trust;
				}
				this.DeserializeNonPrimitives();
				if (this.application_trust == null)
				{
					this.application_trust = new ApplicationTrust();
				}
				return this.application_trust;
			}
			set
			{
				this.application_trust = value;
			}
		}

		/// <summary>Specifies whether the application base path and private binary path are probed when searching for assemblies to load.</summary>
		/// <returns>true if probing is not allowed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x000483E5 File Offset: 0x000465E5
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x000483ED File Offset: 0x000465ED
		[MonoNotSupported("This property exists but not considered.")]
		public bool DisallowApplicationBaseProbing
		{
			get
			{
				return this.disallow_appbase_probe;
			}
			set
			{
				this.disallow_appbase_probe = value;
			}
		}

		/// <summary>Returns the XML configuration information set by the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method, which overrides the application's XML configuration information.</summary>
		/// <returns>An array that contains the XML configuration information that was set by the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method, or null if the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method has not been called.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011A2 RID: 4514 RVA: 0x000483F6 File Offset: 0x000465F6
		[MonoNotSupported("This method exists but not considered.")]
		public byte[] GetConfigurationBytes()
		{
			if (this.configuration_bytes == null)
			{
				return null;
			}
			return this.configuration_bytes.Clone() as byte[];
		}

		/// <summary>Provides XML configuration information for the application domain, replacing the application's XML configuration information.</summary>
		/// <param name="value">An array that contains the XML configuration information to be used for the application domain.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011A3 RID: 4515 RVA: 0x00048412 File Offset: 0x00046612
		[MonoNotSupported("This method exists but not considered.")]
		public void SetConfigurationBytes(byte[] value)
		{
			this.configuration_bytes = value;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0004841C File Offset: 0x0004661C
		private void DeserializeNonPrimitives()
		{
			lock (this)
			{
				if (this.serialized_non_primitives != null)
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream memoryStream = new MemoryStream(this.serialized_non_primitives);
					object[] array = (object[])binaryFormatter.Deserialize(memoryStream);
					this._activationArguments = (ActivationArguments)array[0];
					this.domain_initializer = (AppDomainInitializer)array[1];
					this.application_trust = (ApplicationTrust)array[2];
					this.serialized_non_primitives = null;
				}
			}
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000484AC File Offset: 0x000466AC
		internal void SerializeNonPrimitives()
		{
			object[] array = new object[] { this._activationArguments, this.domain_initializer, this.application_trust };
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, array);
			this.serialized_non_primitives = memoryStream.ToArray();
		}

		/// <summary>Sets the specified switches, making the application domain compatible with previous versions of the .NET Framework for the specified issues.</summary>
		/// <param name="switches">An enumerable set of string values that specify compatibility switches, or null to erase the existing compatibility switches.</param>
		// Token: 0x060011A6 RID: 4518 RVA: 0x00002C89 File Offset: 0x00000E89
		[MonoTODO("not implemented, does not throw because it's used in testing moonlight")]
		public void SetCompatibilitySwitches(IEnumerable<string> switches)
		{
		}

		/// <summary>Gets or sets a value that indicates whether interface caching is disabled for interop calls in the application domain, so that a QueryInterface is performed on each call.</summary>
		/// <returns>true if interface caching is disabled for interop calls in application domains created with the current <see cref="T:System.AppDomainSetup" /> object; otherwise, false.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x000484FC File Offset: 0x000466FC
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x000176B9 File Offset: 0x000158B9
		public bool SandboxInterop
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Provides the common language runtime with an alternate implementation of a string comparison function. </summary>
		/// <param name="functionName">The name of the string comparison function to override.</param>
		/// <param name="functionVersion">The function version. For .NET Framework 4.5, its value must be 1 or greater.</param>
		/// <param name="functionPointer">A pointer to the function that overrides <paramref name="functionName" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="functionName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="functionVersion" /> is not 1 or greater.-or-<paramref name="functionPointer" /> is <see cref="F:System.IntPtr.Zero" />. </exception>
		// Token: 0x060011A9 RID: 4521 RVA: 0x000176B9 File Offset: 0x000158B9
		public void SetNativeFunction(string functionName, int functionVersion, IntPtr functionPointer)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000715 RID: 1813
		private string application_base;

		// Token: 0x04000716 RID: 1814
		private string application_name;

		// Token: 0x04000717 RID: 1815
		private string cache_path;

		// Token: 0x04000718 RID: 1816
		private string configuration_file;

		// Token: 0x04000719 RID: 1817
		private string dynamic_base;

		// Token: 0x0400071A RID: 1818
		private string license_file;

		// Token: 0x0400071B RID: 1819
		private string private_bin_path;

		// Token: 0x0400071C RID: 1820
		private string private_bin_path_probe;

		// Token: 0x0400071D RID: 1821
		private string shadow_copy_directories;

		// Token: 0x0400071E RID: 1822
		private string shadow_copy_files;

		// Token: 0x0400071F RID: 1823
		private bool publisher_policy;

		// Token: 0x04000720 RID: 1824
		private bool path_changed;

		// Token: 0x04000721 RID: 1825
		private LoaderOptimization loader_optimization;

		// Token: 0x04000722 RID: 1826
		private bool disallow_binding_redirects;

		// Token: 0x04000723 RID: 1827
		private bool disallow_code_downloads;

		// Token: 0x04000724 RID: 1828
		private ActivationArguments _activationArguments;

		// Token: 0x04000725 RID: 1829
		private AppDomainInitializer domain_initializer;

		// Token: 0x04000726 RID: 1830
		[NonSerialized]
		private ApplicationTrust application_trust;

		// Token: 0x04000727 RID: 1831
		private string[] domain_initializer_args;

		// Token: 0x04000728 RID: 1832
		private bool disallow_appbase_probe;

		// Token: 0x04000729 RID: 1833
		private byte[] configuration_bytes;

		// Token: 0x0400072A RID: 1834
		private byte[] serialized_non_primitives;

		// Token: 0x0400072B RID: 1835
		private string manager_assembly;

		// Token: 0x0400072C RID: 1836
		private string manager_type;

		// Token: 0x0400072D RID: 1837
		private string[] partial_visible_assemblies;
	}
}
