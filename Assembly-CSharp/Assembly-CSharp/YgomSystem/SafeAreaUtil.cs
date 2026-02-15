using System;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004C5 RID: 1221
	public static class SafeAreaUtil
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x000F1700 File Offset: 0x000EF900
		public static Rect safeArea
		{
			get
			{
				return default(Rect);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x000F1718 File Offset: 0x000EF918
		public static Vector2 anchorMin
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x000F1730 File Offset: 0x000EF930
		public static Vector2 anchorMax
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Setup(bool force = false)
		{
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000F1748 File Offset: 0x000EF948
		public static Rect GetSafeArea()
		{
			return default(Rect);
		}

		// Token: 0x0400281C RID: 10268
		private static bool setup;

		// Token: 0x0400281D RID: 10269
		private static Rect _safeArea;

		// Token: 0x0400281E RID: 10270
		private static Vector2 _anchorMin;

		// Token: 0x0400281F RID: 10271
		private static Vector2 _anchorMax;
	}
}
