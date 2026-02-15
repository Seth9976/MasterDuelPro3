using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000016 RID: 22
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerTestAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002343 File Offset: 0x00000543
		public PackageManagerTestAnalytic()
			: base("PackageManager")
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002354 File Offset: 0x00000554
		[RequiredByNativeCode]
		internal static PackageManagerTestAnalytic CreatePackageManagerTestAnalytic()
		{
			return new PackageManagerTestAnalytic();
		}
	}
}
