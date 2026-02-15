using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001A RID: 26
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerResetPackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000023E3 File Offset: 0x000005E3
		public PackageManagerResetPackageAnalytic()
			: base("resetToDefaultDependencies")
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000023F4 File Offset: 0x000005F4
		[RequiredByNativeCode]
		internal static PackageManagerResetPackageAnalytic CreatePackageManagerResetPackageAnalytic()
		{
			return new PackageManagerResetPackageAnalytic();
		}
	}
}
