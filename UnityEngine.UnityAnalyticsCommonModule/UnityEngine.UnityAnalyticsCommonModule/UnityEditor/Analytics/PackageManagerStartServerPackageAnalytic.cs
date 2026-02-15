using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001C RID: 28
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerStartServerPackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002433 File Offset: 0x00000633
		public PackageManagerStartServerPackageAnalytic()
			: base("startPackageManagerServer")
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002444 File Offset: 0x00000644
		[RequiredByNativeCode]
		internal static PackageManagerStartServerPackageAnalytic CreatePackageManagerStartServerPackageAnalytic()
		{
			return new PackageManagerStartServerPackageAnalytic();
		}
	}
}
