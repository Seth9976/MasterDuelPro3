using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C39 RID: 3129
	public class DuelpassMateWidget
	{
		// Token: 0x0600592E RID: 22830 RVA: 0x00002739 File Offset: 0x00000939
		public DuelpassMateWidget(ElementObjectManager eom)
		{
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x000F4D48 File Offset: 0x000F2F48
		~DuelpassMateWidget()
		{
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator MateSwapCoroutine()
		{
			return null;
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMate(int id)
		{
		}

		// Token: 0x04009518 RID: 38168
		private Transform mateTransform;

		// Token: 0x04009519 RID: 38169
		private SelectionButton mateButton;

		// Token: 0x0400951A RID: 38170
		private List<int> mateIds;

		// Token: 0x0400951B RID: 38171
		private Character2D chara;
	}
}
