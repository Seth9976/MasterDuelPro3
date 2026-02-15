using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.Extension
{
	// Token: 0x0200076E RID: 1902
	public static class HorizontalOrVerticalLayoutGroupExtension
	{
		// Token: 0x06003B4A RID: 15178 RVA: 0x0000216A File Offset: 0x0000036A
		public static RectOffset GetPaddingWithPlatformOverrider(this HorizontalOrVerticalLayoutGroup lg)
		{
			return null;
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float GetSpacingWithPlatformOverrider(this HorizontalOrVerticalLayoutGroup lg)
		{
			return 0f;
		}
	}
}
