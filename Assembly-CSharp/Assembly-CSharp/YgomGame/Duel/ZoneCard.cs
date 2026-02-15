using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomGame.Duel
{
	// Token: 0x02000F47 RID: 3911
	public abstract class ZoneCard
	{
		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x0600735C RID: 29532 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600735D RID: 29533 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPlaying
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x0600735E RID: 29534 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize(ZoneCard.Zone zone, ZoneCard.Mode mode, Action<ZoneCard> onLoadFinished)
		{
		}

		// Token: 0x0600735F RID: 29535 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(int cardID, int uniqueID)
		{
		}

		// Token: 0x06007360 RID: 29536 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadCardFront(int cardID, Material targetMaterial)
		{
		}

		// Token: 0x06007361 RID: 29537 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadCardBack(int sleeveID, Action<Material> onFinished)
		{
		}

		// Token: 0x06007362 RID: 29538 RVA: 0x0000216A File Offset: 0x0000036A
		private string ZoneToTimelineLabel(ZoneCard.Zone zone, ZoneCard.Mode mode)
		{
			return null;
		}

		// Token: 0x06007363 RID: 29539
		public abstract void Play(int cardID, int uniqueID, Vector3 position, Quaternion rotation, Vector3 scale, bool isFace, Action onPlayFinished);

		// Token: 0x06007364 RID: 29540 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Terminate()
		{
		}

		// Token: 0x0400AC8A RID: 44170
		protected GameObject autoReleaseObject;

		// Token: 0x0400AC8B RID: 44171
		protected PlayableDirector timeline;

		// Token: 0x0400AC8C RID: 44172
		protected ZoneCard.Mode mode;

		// Token: 0x0400AC8D RID: 44173
		protected string timelineLabel;

		// Token: 0x02000F48 RID: 3912
		public enum Zone
		{
			// Token: 0x0400AC8F RID: 44175
			Grave,
			// Token: 0x0400AC90 RID: 44176
			Exclude
		}

		// Token: 0x02000F49 RID: 3913
		public enum Mode
		{
			// Token: 0x0400AC92 RID: 44178
			Out,
			// Token: 0x0400AC93 RID: 44179
			In
		}
	}
}
