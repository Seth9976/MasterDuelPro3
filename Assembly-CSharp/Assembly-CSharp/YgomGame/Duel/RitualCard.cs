using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EE9 RID: 3817
	public class RitualCard : SummonCardBase
	{
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06006F5A RID: 28506 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string timelinePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06006F5B RID: 28507 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string seLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06006F5C RID: 28508 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string trailOffsetLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006F5D RID: 28509 RVA: 0x0000216A File Offset: 0x0000036A
		public static RitualCard Create(int cardID, int uniqueID, int rareID, Vector3 position, Quaternion rotation, CardRoot.ModelType modelType, Action onLoadFinished, Action onPlayFinished)
		{
			return null;
		}

		// Token: 0x06006F5E RID: 28510 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PreloadTimeline(Action onFinished)
		{
		}

		// Token: 0x06006F5F RID: 28511 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadPreloadedTimeline()
		{
		}

		// Token: 0x0400AA44 RID: 43588
		public static string TimelinePath;
	}
}
