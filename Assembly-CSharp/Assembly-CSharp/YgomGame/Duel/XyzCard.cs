using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F45 RID: 3909
	public class XyzCard : SummonCardBase
	{
		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x0600734F RID: 29519 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string timelinePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06007350 RID: 29520 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string trailOffsetLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06007351 RID: 29521 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string seLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007352 RID: 29522 RVA: 0x0000216A File Offset: 0x0000036A
		public static XyzCard Create(int cardID, int uniqueID, int rareID, Vector3 position, Quaternion rotation, CardRoot.ModelType modelType, Action onLoadFinished, Action onPlayFinished)
		{
			return null;
		}

		// Token: 0x06007353 RID: 29523 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PreloadTimeline(Action onFinished)
		{
		}

		// Token: 0x06007354 RID: 29524 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadPreloadedTimeline()
		{
		}

		// Token: 0x0400AC89 RID: 44169
		public static string TimelinePath;
	}
}
