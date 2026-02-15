using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000017 RID: 23
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerRemovePackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000236B File Offset: 0x0000056B
		public PackageManagerRemovePackageAnalytic()
			: base("removePackage")
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000237C File Offset: 0x0000057C
		[RequiredByNativeCode]
		internal static PackageManagerRemovePackageAnalytic CreatePackageManagerRemovePackageAnalytic()
		{
			return new PackageManagerRemovePackageAnalytic();
		}
	}
}
