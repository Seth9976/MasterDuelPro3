using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.CardPack.Open.Widget
{
	// Token: 0x020010C1 RID: 4289
	public class CardPackFoundKey : MonoBehaviour
	{
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06007F6A RID: 32618 RVA: 0x000029CC File Offset: 0x00000BCC
		public int foundCnt
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007F6B RID: 32619 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(int idx, SpriteRenderer keyTemplate)
		{
		}

		// Token: 0x06007F6C RID: 32620 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPlay(int foundCnt, Action<CardPackFoundKey> onFinishCallback)
		{
		}

		// Token: 0x06007F6D RID: 32621 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x0400B811 RID: 47121
		private SpriteRenderer m_KeyTemplate;

		// Token: 0x0400B812 RID: 47122
		private readonly List<SpriteRenderer> m_KeyCaches;

		// Token: 0x0400B813 RID: 47123
		private int m_Idx;

		// Token: 0x0400B814 RID: 47124
		private int m_FoundCnt;

		// Token: 0x0400B815 RID: 47125
		private Action<CardPackFoundKey> m_OnFinishCallback;

		// Token: 0x0400B816 RID: 47126
		[NonSerialized]
		public int sortingOrderBase;
	}
}
