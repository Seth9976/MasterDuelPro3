using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000019 RID: 25
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerEmbedPackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000023BB File Offset: 0x000005BB
		public PackageManagerEmbedPackageAnalytic()
			: base("embedPackage")
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023CC File Offset: 0x000005CC
		[RequiredByNativeCode]
		internal static PackageManagerEmbedPackageAnalytic CreatePackageManagerEmbedPackageAnalytic()
		{
			return new PackageManagerEmbedPackageAnalytic();
		}
	}
}
