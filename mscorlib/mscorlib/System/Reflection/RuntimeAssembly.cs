using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x0200063A RID: 1594
	[ComDefaultInterface(typeof(_Assembly))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeAssembly : Assembly
	{
		// Token: 0x06002F54 RID: 12116 RVA: 0x000B55E0 File Offset: 0x000B37E0
		protected RuntimeAssembly()
		{
			this.resolve_event_holder = new Assembly.ResolveEventHolder();
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000B55F3 File Offset: 0x000B37F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 6, this.FullName, this);
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000339FF File Offset: 0x00031BFF
		internal static RuntimeAssembly GetExecutingAssembly(ref StackCrawlMark stackMark)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000B5614 File Offset: 0x000B3814
		internal static AssemblyName CreateAssemblyName(string assemblyString, bool forIntrospection, out RuntimeAssembly assemblyFromResolveEvent)
		{
			if (assemblyString == null)
			{
				throw new ArgumentNullException("assemblyString");
			}
			if (assemblyString.Length == 0 || assemblyString[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("String cannot have zero length."));
			}
			if (forIntrospection)
			{
				AppDomain.CheckReflectionOnlyLoadSupported();
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = assemblyString;
			assemblyFromResolveEvent = null;
			return assemblyName;
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000B5667 File Offset: 0x000B3867
		internal static RuntimeAssembly InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, ref StackCrawlMark stackMark, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			if (assemblyRef.CodeBase != null)
			{
				AppDomain.CheckLoadFromSupported();
			}
			assemblyRef = (AssemblyName)assemblyRef.Clone();
			if (assemblySecurity != null)
			{
			}
			return (RuntimeAssembly)Assembly.Load(assemblyRef);
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000B56A2 File Offset: 0x000B38A2
		internal static RuntimeAssembly LoadWithPartialNameInternal(string partialName, Evidence securityEvidence, ref StackCrawlMark stackMark)
		{
			return (RuntimeAssembly)Assembly.LoadWithPartialName(partialName, securityEvidence);
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000B56B0 File Offset: 0x000B38B0
		internal static RuntimeAssembly LoadWithPartialNameInternal(AssemblyName an, Evidence securityEvidence, ref StackCrawlMark stackMark)
		{
			return RuntimeAssembly.LoadWithPartialNameInternal(an.ToString(), securityEvidence, ref stackMark);
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000B56BF File Offset: 0x000B38BF
		public override AssemblyName GetName(bool copiedName)
		{
			if (SecurityManager.SecurityEnabled)
			{
				string codeBase = this.CodeBase;
			}
			return AssemblyName.Create(this, true);
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000B56D6 File Offset: 0x000B38D6
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
			return base.InternalGetType(null, name, throwOnError, ignoreCase);
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000B5704 File Offset: 0x000B3904
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
			foreach (Module module in this.GetModules(true))
			{
				if (module.ScopeName == name)
				{
					return module;
				}
			}
			return null;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000B575D File Offset: 0x000B395D
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return Assembly.GetReferencedAssemblies(this);
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000B5768 File Offset: 0x000B3968
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

		// Token: 0x06002F60 RID: 12128 RVA: 0x000B57B6 File Offset: 0x000B39B6
		[MonoTODO("Always returns the same as GetModules")]
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000B57C0 File Offset: 0x000B39C0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, null, true, ref stackCrawlMark);
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000B57DC File Offset: 0x000B39DC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, version, true, ref stackCrawlMark);
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002F63 RID: 12131 RVA: 0x000B57F6 File Offset: 0x000B39F6
		[ComVisible(false)]
		public override Module ManifestModule
		{
			get
			{
				return this.GetManifestModule();
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000B57FE File Offset: 0x000B39FE
		public override bool GlobalAssemblyCache
		{
			get
			{
				return this.get_global_assembly_cache();
			}
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000B5806 File Offset: 0x000B3A06
		public override Type[] GetExportedTypes()
		{
			return this.GetTypes(true);
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000B5810 File Offset: 0x000B3A10
		internal static byte[] GetAotId()
		{
			byte[] array = new byte[16];
			if (RuntimeAssembly.GetAotIdInternal(array))
			{
				return array;
			}
			return null;
		}

		// Token: 0x06002F67 RID: 12135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_code_base(Assembly a, bool escaped);

		// Token: 0x06002F68 RID: 12136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string get_location();

		// Token: 0x06002F69 RID: 12137
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string get_fullname(Assembly a);

		// Token: 0x06002F6A RID: 12138
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetAotIdInternal(byte[] aotid);

		// Token: 0x06002F6B RID: 12139
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InternalImageRuntimeVersion(Assembly a);

		// Token: 0x06002F6C RID: 12140
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool get_global_assembly_cache();

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002F6D RID: 12141
		public override extern MethodInfo EntryPoint
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002F6E RID: 12142
		[ComVisible(false)]
		public override extern bool ReflectionOnly
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000B5830 File Offset: 0x000B3A30
		internal static string GetCodeBase(Assembly a, bool escaped)
		{
			string text = RuntimeAssembly.get_code_base(a, escaped);
			if (SecurityManager.SecurityEnabled && string.Compare("FILE://", 0, text, 0, 7, true, CultureInfo.InvariantCulture) == 0)
			{
				string text2 = text.Substring(7);
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text2).Demand();
			}
			return text;
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000B5877 File Offset: 0x000B3A77
		public override string CodeBase
		{
			get
			{
				return RuntimeAssembly.GetCodeBase(this, false);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x000B5880 File Offset: 0x000B3A80
		public override string EscapedCodeBase
		{
			get
			{
				return RuntimeAssembly.GetCodeBase(this, true);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x000B5889 File Offset: 0x000B3A89
		public override string FullName
		{
			get
			{
				return RuntimeAssembly.get_fullname(this);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000B5891 File Offset: 0x000B3A91
		[ComVisible(false)]
		public override string ImageRuntimeVersion
		{
			get
			{
				return RuntimeAssembly.InternalImageRuntimeVersion(this);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x000B5899 File Offset: 0x000B3A99
		internal override IntPtr MonoAssembly
		{
			get
			{
				return this._mono_assembly;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (set) Token: 0x06002F75 RID: 12149 RVA: 0x000B58A1 File Offset: 0x000B3AA1
		internal override bool FromByteArray
		{
			set
			{
				this.fromByteArray = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x000B58AC File Offset: 0x000B3AAC
		public override string Location
		{
			get
			{
				if (this.fromByteArray)
				{
					return string.Empty;
				}
				string location = this.get_location();
				if (location != string.Empty && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, location).Demand();
				}
				return location;
			}
		}

		// Token: 0x06002F77 RID: 12151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetManifestResourceInfoInternal(string name, ManifestResourceInfo info);

		// Token: 0x06002F78 RID: 12152 RVA: 0x000B58F0 File Offset: 0x000B3AF0
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (resourceName.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.");
			}
			ManifestResourceInfo manifestResourceInfo = new ManifestResourceInfo(null, null, (ResourceLocation)0);
			if (this.GetManifestResourceInfoInternal(resourceName, manifestResourceInfo))
			{
				return manifestResourceInfo;
			}
			return null;
		}

		// Token: 0x06002F79 RID: 12153
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern string[] GetManifestResourceNames();

		// Token: 0x06002F7A RID: 12154
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetManifestResourceInternal(string name, out int size, out Module module);

		// Token: 0x06002F7B RID: 12155 RVA: 0x000B5934 File Offset: 0x000B3B34
		public unsafe override Stream GetManifestResourceStream(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.", "name");
			}
			ManifestResourceInfo manifestResourceInfo = this.GetManifestResourceInfo(name);
			if (manifestResourceInfo == null)
			{
				Assembly assembly = AppDomain.CurrentDomain.DoResourceResolve(name, this);
				if (assembly != null && assembly != this)
				{
					return assembly.GetManifestResourceStream(name);
				}
				return null;
			}
			else
			{
				if (manifestResourceInfo.ReferencedAssembly != null)
				{
					return manifestResourceInfo.ReferencedAssembly.GetManifestResourceStream(name);
				}
				if (manifestResourceInfo.FileName != null && manifestResourceInfo.ResourceLocation == (ResourceLocation)0)
				{
					if (this.fromByteArray)
					{
						throw new FileNotFoundException(manifestResourceInfo.FileName);
					}
					return new FileStream(Path.Combine(Path.GetDirectoryName(this.Location), manifestResourceInfo.FileName), FileMode.Open, FileAccess.Read);
				}
				else
				{
					int num;
					Module module;
					IntPtr manifestResourceInternal = this.GetManifestResourceInternal(name, out num, out module);
					if (manifestResourceInternal == (IntPtr)0)
					{
						return null;
					}
					return new RuntimeAssembly.UnmanagedMemoryStreamForModule((byte*)(void*)manifestResourceInternal, (long)num, module);
				}
			}
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000B5A28 File Offset: 0x000B3C28
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetManifestResourceStream(type, name, false, ref stackCrawlMark);
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000B3EED File Offset: 0x000B20ED
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000B3EF6 File Offset: 0x000B20F6
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000B5A42 File Offset: 0x000B3C42
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06002F81 RID: 12161 RVA: 0x000B5A4A File Offset: 0x000B3C4A
		// (remove) Token: 0x06002F82 RID: 12162 RVA: 0x000B5A58 File Offset: 0x000B3C58
		public override event ModuleResolveEventHandler ModuleResolve
		{
			add
			{
				this.resolve_event_holder.ModuleResolve += value;
			}
			remove
			{
				this.resolve_event_holder.ModuleResolve -= value;
			}
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000B5A66 File Offset: 0x000B3C66
		internal override Module GetManifestModule()
		{
			return this.GetManifestModuleInternal();
		}

		// Token: 0x06002F84 RID: 12164
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Module GetManifestModuleInternal();

		// Token: 0x06002F85 RID: 12165
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern Module[] GetModulesInternal();

		// Token: 0x06002F86 RID: 12166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetFilesInternal(string name, bool getResourceModules);

		// Token: 0x06002F87 RID: 12167 RVA: 0x000B5A70 File Offset: 0x000B3C70
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			string[] array = (string[])this.GetFilesInternal(null, getResourceModules);
			if (array == null)
			{
				return EmptyArray<FileStream>.Value;
			}
			string location = this.Location;
			FileStream[] array2;
			if (location != string.Empty)
			{
				array2 = new FileStream[array.Length + 1];
				array2[0] = new FileStream(location, FileMode.Open, FileAccess.Read);
				for (int i = 0; i < array.Length; i++)
				{
					array2[i + 1] = new FileStream(array[i], FileMode.Open, FileAccess.Read);
				}
			}
			else
			{
				array2 = new FileStream[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = new FileStream(array[j], FileMode.Open, FileAccess.Read);
				}
			}
			return array2;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000B5B08 File Offset: 0x000B3D08
		public override FileStream GetFile(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(null, "Name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not valid");
			}
			string text = (string)this.GetFilesInternal(name, true);
			if (text != null)
			{
				return new FileStream(text, FileMode.Open, FileAccess.Read);
			}
			return null;
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000B5B52 File Offset: 0x000B3D52
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000B5B5A File Offset: 0x000B3D5A
		public override bool Equals(object o)
		{
			return this == o || (o != null && o is RuntimeAssembly && ((RuntimeAssembly)o)._mono_assembly == this._mono_assembly);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000B5B87 File Offset: 0x000B3D87
		public override string ToString()
		{
			if (this.assemblyName != null)
			{
				return this.assemblyName;
			}
			this.assemblyName = this.FullName;
			return this.assemblyName;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002F8C RID: 12172 RVA: 0x000B5BAA File Offset: 0x000B3DAA
		public override Evidence Evidence
		{
			get
			{
				return this.UnprotectedGetEvidence();
			}
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000B5BB4 File Offset: 0x000B3DB4
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

		// Token: 0x06002F8E RID: 12174 RVA: 0x000B5C04 File Offset: 0x000B3E04
		internal void Resolve()
		{
			lock (this)
			{
				this.LoadAssemblyPermissions();
				Evidence evidence = new Evidence(this.UnprotectedGetEvidence());
				evidence.AddHost(new PermissionRequestEvidence(this._minimum, this._optional, this._refuse));
				this._granted = SecurityManager.ResolvePolicy(evidence, this._minimum, this._optional, this._refuse, out this._denied);
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002F8F RID: 12175 RVA: 0x000B5C8C File Offset: 0x000B3E8C
		internal override PermissionSet GrantedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return DefaultPolicies.FullTrust;
						}
						return null;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._granted;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x000B5CBE File Offset: 0x000B3EBE
		internal override PermissionSet DeniedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return null;
						}
						return DefaultPolicies.FullTrust;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._denied;
			}
		}

		// Token: 0x06002F91 RID: 12177
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool LoadPermissions(Assembly a, ref IntPtr minimum, ref int minLength, ref IntPtr optional, ref int optLength, ref IntPtr refused, ref int refLength);

		// Token: 0x06002F92 RID: 12178 RVA: 0x000B5CF0 File Offset: 0x000B3EF0
		private void LoadAssemblyPermissions()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (RuntimeAssembly.LoadPermissions(this, ref zero, ref num, ref zero2, ref num2, ref zero3, ref num3))
			{
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(zero, array, 0, num);
					this._minimum = SecurityManager.Decode(array);
				}
				if (num2 > 0)
				{
					byte[] array2 = new byte[num2];
					Marshal.Copy(zero2, array2, 0, num2);
					this._optional = SecurityManager.Decode(array2);
				}
				if (num3 > 0)
				{
					byte[] array3 = new byte[num3];
					Marshal.Copy(zero3, array3, 0, num3);
					this._refuse = SecurityManager.Decode(array3);
				}
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002F93 RID: 12179 RVA: 0x000B5D9A File Offset: 0x000B3F9A
		public override PermissionSet PermissionSet
		{
			get
			{
				return this.GrantedPermissionSet;
			}
		}

		// Token: 0x04001850 RID: 6224
		internal IntPtr _mono_assembly;

		// Token: 0x04001851 RID: 6225
		internal Evidence _evidence;

		// Token: 0x04001852 RID: 6226
		internal Assembly.ResolveEventHolder resolve_event_holder;

		// Token: 0x04001853 RID: 6227
		internal PermissionSet _minimum;

		// Token: 0x04001854 RID: 6228
		internal PermissionSet _optional;

		// Token: 0x04001855 RID: 6229
		internal PermissionSet _refuse;

		// Token: 0x04001856 RID: 6230
		internal PermissionSet _granted;

		// Token: 0x04001857 RID: 6231
		internal PermissionSet _denied;

		// Token: 0x04001858 RID: 6232
		internal bool fromByteArray;

		// Token: 0x04001859 RID: 6233
		internal string assemblyName;

		// Token: 0x0200063B RID: 1595
		internal class UnmanagedMemoryStreamForModule : UnmanagedMemoryStream
		{
			// Token: 0x06002F94 RID: 12180 RVA: 0x000B5DA2 File Offset: 0x000B3FA2
			public unsafe UnmanagedMemoryStreamForModule(byte* pointer, long length, Module module)
				: base(pointer, length)
			{
				this.module = module;
			}

			// Token: 0x06002F95 RID: 12181 RVA: 0x000B5DB3 File Offset: 0x000B3FB3
			protected override void Dispose(bool disposing)
			{
				if (this._isOpen)
				{
					this.module = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x0400185A RID: 6234
			private Module module;
		}
	}
}
