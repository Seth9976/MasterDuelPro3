using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace System.Resources
{
	/// <summary>Represents a resource manager that provides convenient access to culture-specific resources at run time.</summary>
	// Token: 0x020005D0 RID: 1488
	[ComVisible(true)]
	[Serializable]
	public class ResourceManager
	{
		// Token: 0x06002BF4 RID: 11252 RVA: 0x000ADA98 File Offset: 0x000ABC98
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Init()
		{
			try
			{
				this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class with default values.</summary>
		// Token: 0x06002BF5 RID: 11253 RVA: 0x000ADACC File Offset: 0x000ABCCC
		protected ResourceManager()
		{
			this.Init();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class that looks up resources contained in files with the specified root name in the given assembly.</summary>
		/// <param name="baseName">The root name of the resource file without its extension but including any fully qualified namespace name. For example, the root name for the resource file named MyApplication.MyResource.en-US.resources is MyApplication.MyResource. </param>
		/// <param name="assembly">The main assembly for the resources. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="baseName" /> or <paramref name="assembly" /> parameter is null. </exception>
		// Token: 0x06002BF6 RID: 11254 RVA: 0x000ADB04 File Offset: 0x000ABD04
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(string baseName, Assembly assembly)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Assembly must be a runtime Assembly object."));
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			this.SetAppXConfiguration();
			this.CommonAssemblyInit();
			try
			{
				this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
				if (assembly == typeof(object).Assembly && this.m_callingAssembly != assembly)
				{
					this.m_callingAssembly = null;
				}
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class that looks up resources in satellite assemblies based on information from the specified type object.</summary>
		/// <param name="resourceSource">A type from which the resource manager derives all information for finding .resources files. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="resourceSource" /> parameter is null. </exception>
		// Token: 0x06002BF7 RID: 11255 RVA: 0x000ADBBC File Offset: 0x000ABDBC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(Type resourceSource)
		{
			if (null == resourceSource)
			{
				throw new ArgumentNullException("resourceSource");
			}
			if (!(resourceSource is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."));
			}
			this._locationInfo = resourceSource;
			this.MainAssembly = this._locationInfo.Assembly;
			this.BaseNameField = resourceSource.Name;
			this.SetAppXConfiguration();
			this.CommonAssemblyInit();
			try
			{
				this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
				if (this.MainAssembly == typeof(object).Assembly && this.m_callingAssembly != this.MainAssembly)
				{
					this.m_callingAssembly = null;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000ADC88 File Offset: 0x000ABE88
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this._resourceSets = null;
			this.resourceGroveler = null;
			this._lastUsedResourceCache = null;
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000ADCA0 File Offset: 0x000ABEA0
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			if (this.UseManifest)
			{
				this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
			}
			else
			{
				this.resourceGroveler = new FileBasedResourceGroveler(resourceManagerMediator);
			}
			if (this.m_callingAssembly == null)
			{
				this.m_callingAssembly = (RuntimeAssembly)this._callingAssembly;
			}
			if (this.UseManifest && this._neutralResourcesCulture == null)
			{
				this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
			}
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000ADD32 File Offset: 0x000ABF32
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this._callingAssembly = this.m_callingAssembly;
			this.UseSatelliteAssem = this.UseManifest;
			this.ResourceSets = new Hashtable();
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000ADD58 File Offset: 0x000ABF58
		private void CommonAssemblyInit()
		{
			this.UseManifest = true;
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
			this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
			this.ResourceSets = new Hashtable();
		}

		/// <summary>Gets the root name of the resource files that the <see cref="T:System.Resources.ResourceManager" /> searches for resources.</summary>
		/// <returns>The root name of the resource files that the <see cref="T:System.Resources.ResourceManager" /> searches for resources.</returns>
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x000ADDBE File Offset: 0x000ABFBE
		public virtual string BaseName
		{
			get
			{
				return this.BaseNameField;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the resource manager allows case-insensitive resource lookups in the <see cref="M:System.Resources.ResourceManager.GetString(System.String)" /> and <see cref="M:System.Resources.ResourceManager.GetObject(System.String)" /> methods.</summary>
		/// <returns>true to ignore case during resource lookup; otherwise, false.</returns>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000ADDC6 File Offset: 0x000ABFC6
		public virtual bool IgnoreCase
		{
			get
			{
				return this._ignoreCase;
			}
		}

		/// <summary>Gets or sets the location from which to retrieve default fallback resources.</summary>
		/// <returns>One of the enumeration values that specifies where the resource manager can look for fallback resources.</returns>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000ADDCE File Offset: 0x000ABFCE
		protected UltimateResourceFallbackLocation FallbackLocation
		{
			get
			{
				return this._fallbackLoc;
			}
		}

		/// <summary>Generates the name of the resource file for the given <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>The name that can be used for a resource file for the given <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="culture">The culture object for which a resource file name is constructed. </param>
		// Token: 0x06002BFF RID: 11263 RVA: 0x000ADDD8 File Offset: 0x000ABFD8
		protected virtual string GetResourceFileName(CultureInfo culture)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			stringBuilder.Append(this.BaseNameField);
			if (!culture.HasInvariantCultureName)
			{
				CultureInfo.VerifyCultureName(culture.Name, true);
				stringBuilder.Append('.');
				stringBuilder.Append(culture.Name);
			}
			stringBuilder.Append(".resources");
			return stringBuilder.ToString();
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000ADE3C File Offset: 0x000AC03C
		internal ResourceSet GetFirstResourceSet(CultureInfo culture)
		{
			if (this._neutralResourcesCulture != null && culture.Name == this._neutralResourcesCulture.Name)
			{
				culture = CultureInfo.InvariantCulture;
			}
			if (this._lastUsedResourceCache != null)
			{
				ResourceManager.CultureNameResourceSetPair cultureNameResourceSetPair = this._lastUsedResourceCache;
				lock (cultureNameResourceSetPair)
				{
					if (culture.Name == this._lastUsedResourceCache.lastCultureName)
					{
						return this._lastUsedResourceCache.lastResourceSet;
					}
				}
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> dictionary = resourceSets;
				lock (dictionary)
				{
					resourceSets.TryGetValue(culture.Name, out resourceSet);
				}
			}
			if (resourceSet != null)
			{
				if (this._lastUsedResourceCache != null)
				{
					ResourceManager.CultureNameResourceSetPair cultureNameResourceSetPair = this._lastUsedResourceCache;
					lock (cultureNameResourceSetPair)
					{
						this._lastUsedResourceCache.lastCultureName = culture.Name;
						this._lastUsedResourceCache.lastResourceSet = resourceSet;
					}
				}
				return resourceSet;
			}
			return null;
		}

		/// <summary>Retrieves the resource set for a particular culture.</summary>
		/// <returns>The resource set for the specified culture.</returns>
		/// <param name="culture">The culture whose resources are to be retrieved. </param>
		/// <param name="createIfNotExists">true to load the resource set, if it has not been loaded yet; otherwise, false. </param>
		/// <param name="tryParents">true to use resource fallback to load an appropriate resource if the resource set cannot be found; false to bypass the resource fallback process. (See the Remarks section.)</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="culture" /> parameter is null. </exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">
		///   <paramref name="tryParents" /> is true, no usable set of resources has been found, and there are no default culture resources. </exception>
		// Token: 0x06002C01 RID: 11265 RVA: 0x000ADF68 File Offset: 0x000AC168
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> dictionary = resourceSets;
				lock (dictionary)
				{
					ResourceSet resourceSet;
					if (resourceSets.TryGetValue(culture.Name, out resourceSet))
					{
						return resourceSet;
					}
				}
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (this.UseManifest && culture.HasInvariantCultureName)
			{
				string resourceFileName = this.GetResourceFileName(culture);
				Stream manifestResourceStream = ((RuntimeAssembly)this.MainAssembly).GetManifestResourceStream(this._locationInfo, resourceFileName, this.m_callingAssembly == this.MainAssembly, ref stackCrawlMark);
				if (createIfNotExists && manifestResourceStream != null)
				{
					ResourceSet resourceSet = ((ManifestBasedResourceGroveler)this.resourceGroveler).CreateResourceSet(manifestResourceStream, this.MainAssembly);
					ResourceManager.AddResourceSet(resourceSets, culture.Name, ref resourceSet);
					return resourceSet;
				}
			}
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents);
		}

		/// <summary>Provides the implementation for finding a resource set.</summary>
		/// <returns>The specified resource set.</returns>
		/// <param name="culture">The culture object to look for. </param>
		/// <param name="createIfNotExists">true to load the resource set, if it has not been loaded yet; otherwise, false. </param>
		/// <param name="tryParents">true to check parent <see cref="T:System.Globalization.CultureInfo" /> objects if the resource set cannot be loaded; otherwise, false.</param>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">The main assembly does not contain a .resources file, which is required to look up a resource. </exception>
		/// <exception cref="T:System.ExecutionEngineException">There was an internal error in the runtime.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The satellite assembly associated with <paramref name="culture" /> could not be located.</exception>
		// Token: 0x06002C02 RID: 11266 RVA: 0x000AE054 File Offset: 0x000AC254
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents, ref stackCrawlMark);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000AE070 File Offset: 0x000AC270
		private ResourceSet InternalGetResourceSet(CultureInfo requestedCulture, bool createIfNotExists, bool tryParents, ref StackCrawlMark stackMark)
		{
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			CultureInfo cultureInfo = null;
			Dictionary<string, ResourceSet> dictionary = resourceSets;
			lock (dictionary)
			{
				if (resourceSets.TryGetValue(requestedCulture.Name, out resourceSet))
				{
					return resourceSet;
				}
			}
			ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(requestedCulture, this._neutralResourcesCulture, tryParents);
			foreach (CultureInfo cultureInfo2 in resourceFallbackManager)
			{
				dictionary = resourceSets;
				lock (dictionary)
				{
					if (resourceSets.TryGetValue(cultureInfo2.Name, out resourceSet))
					{
						if (requestedCulture != cultureInfo2)
						{
							cultureInfo = cultureInfo2;
						}
						break;
					}
				}
				resourceSet = this.resourceGroveler.GrovelForResourceSet(cultureInfo2, resourceSets, tryParents, createIfNotExists, ref stackMark);
				if (resourceSet != null)
				{
					cultureInfo = cultureInfo2;
					break;
				}
			}
			if (resourceSet != null && cultureInfo != null)
			{
				foreach (CultureInfo cultureInfo3 in resourceFallbackManager)
				{
					ResourceManager.AddResourceSet(resourceSets, cultureInfo3.Name, ref resourceSet);
					if (cultureInfo3 == cultureInfo)
					{
						break;
					}
				}
			}
			return resourceSet;
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000AE1C8 File Offset: 0x000AC3C8
		private static void AddResourceSet(Dictionary<string, ResourceSet> localResourceSets, string cultureName, ref ResourceSet rs)
		{
			lock (localResourceSets)
			{
				ResourceSet resourceSet;
				if (localResourceSets.TryGetValue(cultureName, out resourceSet))
				{
					if (resourceSet != rs)
					{
						if (!localResourceSets.ContainsValue(rs))
						{
							rs.Dispose();
						}
						rs = resourceSet;
					}
				}
				else
				{
					localResourceSets.Add(cultureName, rs);
				}
			}
		}

		/// <summary>Returns the version specified by the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> attribute in the given assembly.</summary>
		/// <returns>The satellite contract version of the given assembly, or null if no version was found.</returns>
		/// <param name="a">The assembly to check for the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> attribute. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Version" /> found in the assembly <paramref name="a" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="a" /> is null. </exception>
		// Token: 0x06002C05 RID: 11269 RVA: 0x000AE22C File Offset: 0x000AC42C
		protected static Version GetSatelliteContractVersion(Assembly a)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a", Environment.GetResourceString("Assembly cannot be null."));
			}
			string text = null;
			if (a.ReflectionOnly)
			{
				foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(a))
				{
					if (customAttributeData.Constructor.DeclaringType == typeof(SatelliteContractVersionAttribute))
					{
						text = (string)customAttributeData.ConstructorArguments[0].Value;
						break;
					}
				}
				if (text == null)
				{
					return null;
				}
			}
			else
			{
				object[] customAttributes = a.GetCustomAttributes(typeof(SatelliteContractVersionAttribute), false);
				if (customAttributes.Length == 0)
				{
					return null;
				}
				text = ((SatelliteContractVersionAttribute)customAttributes[0]).Version;
			}
			Version version;
			try
			{
				version = new Version(text);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				if (a == typeof(object).Assembly)
				{
					return null;
				}
				throw new ArgumentException(Environment.GetResourceString("Satellite contract version attribute on the assembly '{0}' specifies an invalid version: {1}.", new object[]
				{
					a.ToString(),
					text
				}), ex);
			}
			return version;
		}

		/// <summary>Returns culture-specific information for the main assembly's default resources by retrieving the value of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> attribute on a specified assembly.</summary>
		/// <returns>The culture from the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> attribute, if found; otherwise, the invariant culture.</returns>
		/// <param name="a">The assembly for which to return culture-specific information. </param>
		// Token: 0x06002C06 RID: 11270 RVA: 0x000AE360 File Offset: 0x000AC560
		protected static CultureInfo GetNeutralResourcesLanguage(Assembly a)
		{
			UltimateResourceFallbackLocation ultimateResourceFallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
			return ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(a, ref ultimateResourceFallbackLocation);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000AE378 File Offset: 0x000AC578
		internal static bool CompareNames(string asmTypeName1, string typeName2, AssemblyName asmName2)
		{
			int num = asmTypeName1.IndexOf(',');
			if (((num == -1) ? asmTypeName1.Length : num) != typeName2.Length)
			{
				return false;
			}
			if (string.Compare(asmTypeName1, 0, typeName2, 0, typeName2.Length, StringComparison.Ordinal) != 0)
			{
				return false;
			}
			if (num == -1)
			{
				return true;
			}
			while (char.IsWhiteSpace(asmTypeName1[++num]))
			{
			}
			AssemblyName assemblyName = new AssemblyName(asmTypeName1.Substring(num));
			if (string.Compare(assemblyName.Name, asmName2.Name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (string.Compare(assemblyName.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (assemblyName.CultureInfo != null && asmName2.CultureInfo != null && assemblyName.CultureInfo.LCID != asmName2.CultureInfo.LCID)
			{
				return false;
			}
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			byte[] publicKeyToken2 = asmName2.GetPublicKeyToken();
			if (publicKeyToken != null && publicKeyToken2 != null)
			{
				if (publicKeyToken.Length != publicKeyToken2.Length)
				{
					return false;
				}
				for (int i = 0; i < publicKeyToken.Length; i++)
				{
					if (publicKeyToken[i] != publicKeyToken2[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00002C89 File Offset: 0x00000E89
		private void SetAppXConfiguration()
		{
		}

		/// <summary>Returns the value of the specified string resource.</summary>
		/// <returns>The value of the resource localized for the caller's current UI culture, or null if <paramref name="name" /> cannot be found in a resource set.</returns>
		/// <param name="name">The name of the resource to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the specified resource is not a string. </exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources has been found, and there are no resources for the default culture. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic. </exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06002C09 RID: 11273 RVA: 0x000AE470 File Offset: 0x000AC670
		public virtual string GetString(string name)
		{
			return this.GetString(name, null);
		}

		/// <summary>Returns the value of the string resource localized for the specified culture.</summary>
		/// <returns>The value of the resource localized for the specified culture, or null if <paramref name="name" /> cannot be found in a resource set.</returns>
		/// <param name="name">The name of the resource to retrieve. </param>
		/// <param name="culture">An object that represents the culture for which the resource is localized. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the specified resource is not a string. </exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources has been found, and there are no resources for a default culture. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic. </exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06002C0A RID: 11274 RVA: 0x000AE47C File Offset: 0x000AC67C
		public virtual string GetString(string name, CultureInfo culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (culture == null)
			{
				culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
			}
			ResourceSet resourceSet = this.GetFirstResourceSet(culture);
			if (resourceSet != null)
			{
				string @string = resourceSet.GetString(name, this._ignoreCase);
				if (@string != null)
				{
					return @string;
				}
			}
			foreach (CultureInfo cultureInfo in new ResourceFallbackManager(culture, this._neutralResourcesCulture, true))
			{
				ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
				if (resourceSet2 == null)
				{
					break;
				}
				if (resourceSet2 != resourceSet)
				{
					string string2 = resourceSet2.GetString(name, this._ignoreCase);
					if (string2 != null)
					{
						if (this._lastUsedResourceCache != null)
						{
							ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
							lock (lastUsedResourceCache)
							{
								this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
								this._lastUsedResourceCache.lastResourceSet = resourceSet2;
							}
						}
						return string2;
					}
					resourceSet = resourceSet2;
				}
			}
			return null;
		}

		/// <summary>Returns the value of the specified non-string resource.</summary>
		/// <returns>The value of the resource localized for the caller's current culture settings. If an appropriate resource set exists but <paramref name="name" /> cannot be found, the method returns null. </returns>
		/// <param name="name">The name of the resource to get. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of localized resources has been found, and there are no default culture resources. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06002C0B RID: 11275 RVA: 0x000AE598 File Offset: 0x000AC798
		public virtual object GetObject(string name)
		{
			return this.GetObject(name, null, true);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000AE5A4 File Offset: 0x000AC7A4
		private object GetObject(string name, CultureInfo culture, bool wrapUnmanagedMemStream)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (culture == null)
			{
				culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
			}
			ResourceSet resourceSet = this.GetFirstResourceSet(culture);
			if (resourceSet != null)
			{
				object @object = resourceSet.GetObject(name, this._ignoreCase);
				if (@object != null)
				{
					UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
					if (unmanagedMemoryStream != null && wrapUnmanagedMemStream)
					{
						return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream);
					}
					return @object;
				}
			}
			foreach (CultureInfo cultureInfo in new ResourceFallbackManager(culture, this._neutralResourcesCulture, true))
			{
				ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
				if (resourceSet2 == null)
				{
					break;
				}
				if (resourceSet2 != resourceSet)
				{
					object object2 = resourceSet2.GetObject(name, this._ignoreCase);
					if (object2 != null)
					{
						if (this._lastUsedResourceCache != null)
						{
							ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
							lock (lastUsedResourceCache)
							{
								this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
								this._lastUsedResourceCache.lastResourceSet = resourceSet2;
							}
						}
						UnmanagedMemoryStream unmanagedMemoryStream2 = object2 as UnmanagedMemoryStream;
						if (unmanagedMemoryStream2 != null && wrapUnmanagedMemStream)
						{
							return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream2);
						}
						return object2;
					}
					else
					{
						resourceSet = resourceSet2;
					}
				}
			}
			return null;
		}

		/// <summary>Specifies the root name of the resource files that the <see cref="T:System.Resources.ResourceManager" /> searches for resources.</summary>
		// Token: 0x0400164D RID: 5709
		protected string BaseNameField;

		/// <summary>Contains a <see cref="T:System.Collections.Hashtable" /> that returns a mapping from cultures to <see cref="T:System.Resources.ResourceSet" /> objects. </summary>
		// Token: 0x0400164E RID: 5710
		[Obsolete("call InternalGetResourceSet instead")]
		protected Hashtable ResourceSets;

		// Token: 0x0400164F RID: 5711
		[NonSerialized]
		private Dictionary<string, ResourceSet> _resourceSets;

		// Token: 0x04001650 RID: 5712
		private string moduleDir;

		/// <summary>Specifies the main assembly that contains the resources.</summary>
		// Token: 0x04001651 RID: 5713
		protected Assembly MainAssembly;

		// Token: 0x04001652 RID: 5714
		private Type _locationInfo;

		// Token: 0x04001653 RID: 5715
		private Type _userResourceSet;

		// Token: 0x04001654 RID: 5716
		private CultureInfo _neutralResourcesCulture;

		// Token: 0x04001655 RID: 5717
		[NonSerialized]
		private ResourceManager.CultureNameResourceSetPair _lastUsedResourceCache;

		// Token: 0x04001656 RID: 5718
		private bool _ignoreCase;

		// Token: 0x04001657 RID: 5719
		private bool UseManifest;

		// Token: 0x04001658 RID: 5720
		[OptionalField(VersionAdded = 1)]
		private bool UseSatelliteAssem;

		// Token: 0x04001659 RID: 5721
		[OptionalField]
		private UltimateResourceFallbackLocation _fallbackLoc;

		// Token: 0x0400165A RID: 5722
		[OptionalField]
		private Version _satelliteContractVersion;

		// Token: 0x0400165B RID: 5723
		[OptionalField]
		private bool _lookedForSatelliteContractVersion;

		// Token: 0x0400165C RID: 5724
		[OptionalField(VersionAdded = 1)]
		private Assembly _callingAssembly;

		// Token: 0x0400165D RID: 5725
		[OptionalField(VersionAdded = 4)]
		private RuntimeAssembly m_callingAssembly;

		// Token: 0x0400165E RID: 5726
		[NonSerialized]
		private IResourceGroveler resourceGroveler;

		/// <summary>Holds the number used to identify resource files.</summary>
		// Token: 0x0400165F RID: 5727
		public static readonly int MagicNumber = -1091581234;

		/// <summary> Specifies the version of resource file headers that the current implementation of <see cref="T:System.Resources.ResourceManager" /> can interpret and produce.</summary>
		// Token: 0x04001660 RID: 5728
		public static readonly int HeaderVersionNumber = 1;

		// Token: 0x04001661 RID: 5729
		private static readonly Type _minResourceSet = typeof(ResourceSet);

		// Token: 0x04001662 RID: 5730
		internal static readonly string ResReaderTypeName = typeof(ResourceReader).FullName;

		// Token: 0x04001663 RID: 5731
		internal static readonly string ResSetTypeName = typeof(RuntimeResourceSet).FullName;

		// Token: 0x04001664 RID: 5732
		internal static readonly string MscorlibName = typeof(ResourceReader).Assembly.FullName;

		// Token: 0x04001665 RID: 5733
		internal static readonly int DEBUG = 0;

		// Token: 0x020005D1 RID: 1489
		internal class CultureNameResourceSetPair
		{
			// Token: 0x04001666 RID: 5734
			public string lastCultureName;

			// Token: 0x04001667 RID: 5735
			public ResourceSet lastResourceSet;
		}

		// Token: 0x020005D2 RID: 1490
		internal class ResourceManagerMediator
		{
			// Token: 0x06002C0F RID: 11279 RVA: 0x000AE76F File Offset: 0x000AC96F
			internal ResourceManagerMediator(ResourceManager rm)
			{
				if (rm == null)
				{
					throw new ArgumentNullException("rm");
				}
				this._rm = rm;
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000AE78C File Offset: 0x000AC98C
			internal string ModuleDir
			{
				get
				{
					return this._rm.moduleDir;
				}
			}

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x06002C11 RID: 11281 RVA: 0x000AE799 File Offset: 0x000AC999
			internal Type LocationInfo
			{
				get
				{
					return this._rm._locationInfo;
				}
			}

			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000AE7A6 File Offset: 0x000AC9A6
			internal Type UserResourceSet
			{
				get
				{
					return this._rm._userResourceSet;
				}
			}

			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x06002C13 RID: 11283 RVA: 0x000AE7B3 File Offset: 0x000AC9B3
			internal string BaseNameField
			{
				get
				{
					return this._rm.BaseNameField;
				}
			}

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000AE7C0 File Offset: 0x000AC9C0
			internal CultureInfo NeutralResourcesCulture
			{
				get
				{
					return this._rm._neutralResourcesCulture;
				}
			}

			// Token: 0x06002C15 RID: 11285 RVA: 0x000AE7CD File Offset: 0x000AC9CD
			internal string GetResourceFileName(CultureInfo culture)
			{
				return this._rm.GetResourceFileName(culture);
			}

			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000AE7DB File Offset: 0x000AC9DB
			// (set) Token: 0x06002C17 RID: 11287 RVA: 0x000AE7E8 File Offset: 0x000AC9E8
			internal bool LookedForSatelliteContractVersion
			{
				get
				{
					return this._rm._lookedForSatelliteContractVersion;
				}
				set
				{
					this._rm._lookedForSatelliteContractVersion = value;
				}
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000AE7F6 File Offset: 0x000AC9F6
			// (set) Token: 0x06002C19 RID: 11289 RVA: 0x000AE803 File Offset: 0x000ACA03
			internal Version SatelliteContractVersion
			{
				get
				{
					return this._rm._satelliteContractVersion;
				}
				set
				{
					this._rm._satelliteContractVersion = value;
				}
			}

			// Token: 0x06002C1A RID: 11290 RVA: 0x000AE811 File Offset: 0x000ACA11
			internal Version ObtainSatelliteContractVersion(Assembly a)
			{
				return ResourceManager.GetSatelliteContractVersion(a);
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000AE819 File Offset: 0x000ACA19
			internal UltimateResourceFallbackLocation FallbackLoc
			{
				get
				{
					return this._rm.FallbackLocation;
				}
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000AE826 File Offset: 0x000ACA26
			internal RuntimeAssembly CallingAssembly
			{
				get
				{
					return this._rm.m_callingAssembly;
				}
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000AE833 File Offset: 0x000ACA33
			internal RuntimeAssembly MainAssembly
			{
				get
				{
					return (RuntimeAssembly)this._rm.MainAssembly;
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x06002C1E RID: 11294 RVA: 0x000AE845 File Offset: 0x000ACA45
			internal string BaseName
			{
				get
				{
					return this._rm.BaseName;
				}
			}

			// Token: 0x04001668 RID: 5736
			private ResourceManager _rm;
		}
	}
}
