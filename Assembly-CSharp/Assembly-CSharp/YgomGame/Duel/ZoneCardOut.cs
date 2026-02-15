using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F4B RID: 3915
	public class ZoneCardOut : ZoneCard
	{
		// Token: 0x0600736A RID: 29546 RVA: 0x0000216A File Offset: 0x0000036A
		public static ZoneCardOut Create(ZoneCard.Zone zone, ZoneCard.Mode mode, Action<ZoneCard> onLoadFinished)
		{
			return null;
		}

		// Token: 0x0600736B RID: 29547 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Play(int cardID, int uniqueID, Vector3 position, Quaternion rotation, Vector3 scale, bool isFace, Action onPlayFinished)
		{
		}

		// Token: 0x0600736C RID: 29548 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator PlayFinishedCallbackExecuter()
		{
			return null;
		}

		// Token: 0x0600736D RID: 29549 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Terminate()
		{
		}

		// Token: 0x0400AC95 RID: 44181
		private List<Action> onPlayFinished;

		// Token: 0x0400AC96 RID: 44182
		private const float playedCallbackIntervalTime = 0.1f;
	}
}
