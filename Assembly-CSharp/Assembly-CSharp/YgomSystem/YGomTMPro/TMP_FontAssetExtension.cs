using System;
using System.Collections.Generic;
using TMPro;

namespace YgomSystem.YGomTMPro
{
	// Token: 0x020004ED RID: 1261
	public static class TMP_FontAssetExtension
	{
		// Token: 0x060027F4 RID: 10228 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckClearDynamicGriph(this TMP_FontAsset self)
		{
			return false;
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SearchDependencieFonts(this TMP_FontAsset self, List<TMP_FontAsset> searchList)
		{
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool TryAddCharactersDependencies(this TMP_FontAsset self, List<TMP_FontAsset> searchList, string characters)
		{
			return false;
		}
	}
}
