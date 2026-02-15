using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000018 RID: 24
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerResolvePackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002393 File Offset: 0x00000593
		public PackageManagerResolvePackageAnalytic()
			: base("resolvePackages")
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023A4 File Offset: 0x000005A4
		[RequiredByNativeCode]
		internal static PackageManagerResolvePackageAnalytic CreatePackageManagerResolvePackageAnalytic()
		{
			return new PackageManagerResolvePackageAnalytic();
		}

		// Token: 0x0400003F RID: 63
		public string[] packages;

		// Token: 0x04000040 RID: 64
		public string[] package_registries;

		// Token: 0x04000041 RID: 65
		public string[] package_signatures;

		// Token: 0x04000042 RID: 66
		public string[] package_sources;

		// Token: 0x04000043 RID: 67
		public string[] package_types;
	}
}
