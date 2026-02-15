using System;
using System.Collections.Generic;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006F3 RID: 1779
	public static class StreamAssetList
	{
		// Token: 0x06003775 RID: 14197 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsEnable()
		{
			return false;
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LoadFromFile()
		{
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool Exists(string relativePath)
		{
			return false;
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsAssetBundle(string relativePath)
		{
			return false;
		}

		// Token: 0x04003176 RID: 12662
		private const string kStreamAssetListFileName = "streamAssetList.json";

		// Token: 0x04003177 RID: 12663
		private static HashSet<string> assetList;
	}
}
