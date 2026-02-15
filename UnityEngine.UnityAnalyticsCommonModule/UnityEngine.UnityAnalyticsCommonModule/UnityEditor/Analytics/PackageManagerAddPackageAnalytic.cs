using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000015 RID: 21
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerAddPackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000231D File Offset: 0x0000051D
		public PackageManagerAddPackageAnalytic()
			: base("addPackage")
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000232C File Offset: 0x0000052C
		[RequiredByNativeCode]
		internal static PackageManagerAddPackageAnalytic CreatePackageManagerAddPackageAnalytic()
		{
			return new PackageManagerAddPackageAnalytic();
		}
	}
}
