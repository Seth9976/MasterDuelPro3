using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000E90 RID: 3728
	public class FusionCard : SummonCardBase
	{
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06006C48 RID: 27720 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string timelinePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06006C49 RID: 27721 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string seLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06006C4A RID: 27722 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string trailOffsetLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006C4B RID: 27723 RVA: 0x0000216A File Offset: 0x0000036A
		public static FusionCard Create(int cardID, int uniqueID, int rareID, Vector3 position, Quaternion rotation, CardRoot.ModelType modelType, Action onLoadFinished, Action onPlayFinished)
		{
			return null;
		}

		// Token: 0x06006C4C RID: 27724 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PreloadTimeline(Action onFinished)
		{
		}

		// Token: 0x06006C4D RID: 27725 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadPreloadedTimeline()
		{
		}

		// Token: 0x0400A795 RID: 42901
		public static string TimelinePath;
	}
}
