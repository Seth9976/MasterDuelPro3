using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D34 RID: 3380
	public class DuelChainStripe : MonoBehaviour
	{
		// Token: 0x0600621A RID: 25114 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelChainStripe Create(DuelChainManager dcmanager)
		{
			return null;
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x000029CC File Offset: 0x00000BCC
		public int AddChain(Vector3 srcpos, Vector3 dstpos, Action<int> onfinish = null)
		{
			return 0;
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x000029CC File Offset: 0x00000BCC
		public int RemoveChain()
		{
			return 0;
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator AddChainUnit(int chainid, Transform chainunit, float targetlength)
		{
			return null;
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator RemoveChainUnit(int chainid)
		{
			return null;
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x0000216D File Offset: 0x0000036D
		private void HideChainUnit(Transform chainunit)
		{
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x0000216A File Offset: 0x0000036A
		private Transform GetAvailableChainUnit()
		{
			return null;
		}

		// Token: 0x06006221 RID: 25121 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayTransTween(Transform chainhead, float duration, float trans, Action onfinished = null)
		{
		}

		// Token: 0x04009D00 RID: 40192
		private const float DELTATIME = 0.25f;

		// Token: 0x04009D01 RID: 40193
		private const float MINFLOAT = 0.0001f;

		// Token: 0x04009D02 RID: 40194
		private const float UNITLENGTH = 0.75f;

		// Token: 0x04009D03 RID: 40195
		private const string PATH_PREHAB = "Duel/Effects/Chain/fxp_chn_001/fxp_chn_001";

		// Token: 0x04009D04 RID: 40196
		private GameObject m_ChainUnitPrehab;

		// Token: 0x04009D05 RID: 40197
		private Transform m_ShowPool;

		// Token: 0x04009D06 RID: 40198
		private Transform m_HidePool;

		// Token: 0x04009D07 RID: 40199
		private Stack<int> m_ChainIdStack;

		// Token: 0x04009D08 RID: 40200
		private Stack<Transform> m_IdolChainUnitStack;

		// Token: 0x04009D09 RID: 40201
		private Dictionary<int, ChainIndo> m_ChainInfoTable;
	}
}
