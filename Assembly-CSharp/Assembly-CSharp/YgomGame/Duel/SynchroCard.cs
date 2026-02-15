using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F2F RID: 3887
	public class SynchroCard : SummonCardBase
	{
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06007268 RID: 29288 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string timelinePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06007269 RID: 29289 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string seLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x0600726A RID: 29290 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string trailOffsetLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600726B RID: 29291 RVA: 0x0000216A File Offset: 0x0000036A
		public static SynchroCard Create(int cardID, int uniqueID, int rareID, Vector3 position, Quaternion rotation, CardRoot.ModelType modelType, Action onLoadFinished, Action onPlayFinished)
		{
			return null;
		}

		// Token: 0x0600726C RID: 29292 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PreloadTimeline(Action onFinished)
		{
		}

		// Token: 0x0600726D RID: 29293 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadPreloadedTimeline()
		{
		}

		// Token: 0x0400AC0A RID: 44042
		public static string TimelinePath;
	}
}
