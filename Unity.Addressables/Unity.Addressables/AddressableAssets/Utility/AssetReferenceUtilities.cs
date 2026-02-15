using System;

namespace UnityEngine.AddressableAssets.Utility
{
	// Token: 0x02000042 RID: 66
	internal class AssetReferenceUtilities
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00007215 File Offset: 0x00005415
		internal static string FormatName(string name)
		{
			if (name.EndsWith("(Clone)", StringComparison.Ordinal))
			{
				name = name.Replace("(Clone)", "");
			}
			return name;
		}
	}
}
