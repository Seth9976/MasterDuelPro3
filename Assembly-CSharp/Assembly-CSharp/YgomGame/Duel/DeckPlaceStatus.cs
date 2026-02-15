using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D26 RID: 3366
	public class DeckPlaceStatus
	{
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600619E RID: 24990 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isTerminated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x00002739 File Offset: 0x00000939
		public DeckPlaceStatus(DeckCardPlace deckPlace)
		{
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x060061A1 RID: 24993 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show(bool immediate)
		{
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide(bool immediate)
		{
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x000F55EC File Offset: 0x000F37EC
		private Vector2 World2ScreenPos(Vector3 pos)
		{
			return default(Vector2);
		}

		// Token: 0x04009C8B RID: 40075
		private PlaceStatusLabel statusLabel;

		// Token: 0x04009C8C RID: 40076
		private DeckCardPlace deckPlace;
	}
}
