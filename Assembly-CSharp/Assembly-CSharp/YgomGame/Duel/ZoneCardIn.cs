using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F4A RID: 3914
	public class ZoneCardIn : ZoneCard
	{
		// Token: 0x06007366 RID: 29542 RVA: 0x0000216A File Offset: 0x0000036A
		public static ZoneCardIn Create(ZoneCard.Zone zone, ZoneCard.Mode mode, Action<ZoneCard> onLoadFinished)
		{
			return null;
		}

		// Token: 0x06007367 RID: 29543 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Play(int cardID, int uniqueID, Vector3 position, Quaternion rotation, Vector3 scale, bool isFace, Action onPlayFinished)
		{
		}

		// Token: 0x06007368 RID: 29544 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Terminate()
		{
		}

		// Token: 0x0400AC94 RID: 44180
		private Action onPlayFinished;
	}
}
