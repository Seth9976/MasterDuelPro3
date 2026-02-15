using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001B RID: 27
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerResolveErrorPackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000240B File Offset: 0x0000060B
		public PackageManagerResolveErrorPackageAnalytic()
			: base("resolveErrorUserAction")
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000241C File Offset: 0x0000061C
		[RequiredByNativeCode]
		internal static PackageManagerResolveErrorPackageAnalytic CreatePackageManagerResolveErrorPackageAnalytic()
		{
			return new PackageManagerResolveErrorPackageAnalytic();
		}

		// Token: 0x04000044 RID: 68
		public string reason;

		// Token: 0x04000045 RID: 69
		public string action;
	}
}
