using System;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200000D RID: 13
	internal struct FontEngineUtilities
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003510 File Offset: 0x00001710
		internal static int MaxValue(int a, int b, int c)
		{
			return (a < b) ? ((b < c) ? c : b) : ((a < c) ? c : a);
		}
	}
}
