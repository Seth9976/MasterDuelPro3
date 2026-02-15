using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EB0 RID: 3760
	public class LinkCard : SummonCardBase
	{
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06006D74 RID: 28020 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string timelinePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06006D75 RID: 28021 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string trailOffsetLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06006D76 RID: 28022 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string seLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006D77 RID: 28023 RVA: 0x0000216A File Offset: 0x0000036A
		public static LinkCard Create(int cardID, int uniqueID, int rareID, Vector3 position, Quaternion rotation, CardRoot.ModelType modelType, Action onLoadFinished, Action onPlayFinished)
		{
			return null;
		}

		// Token: 0x06006D78 RID: 28024 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PreloadTimeline(Action onFinished)
		{
		}

		// Token: 0x06006D79 RID: 28025 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadPreloadedTimeline()
		{
		}

		// Token: 0x0400A88D RID: 43149
		public static string TimelinePath;
	}
}
