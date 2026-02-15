using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000D33 RID: 3379
	public class DuelChainStack : MonoBehaviour
	{
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06006208 RID: 25096 RVA: 0x0000216A File Offset: 0x0000036A
		private ElementObjectManager m_Eom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06006209 RID: 25097 RVA: 0x0000216A File Offset: 0x0000036A
		private PlayableDirector m_Pd
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600620A RID: 25098 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject GetChainSet(int layer, bool pos)
		{
			return null;
		}

		// Token: 0x0600620B RID: 25099 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject GetChainStripe(int layer0, bool pos0, int layer1, bool pos1)
		{
			return null;
		}

		// Token: 0x0600620C RID: 25100 RVA: 0x0000216A File Offset: 0x0000036A
		private MeshRenderer GetChainCard(int layer, bool pos)
		{
			return null;
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetChainStripe(int layer0, bool pos0, bool pos1)
		{
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x0000216D File Offset: 0x0000036D
		private void HideChainStripeInLayer(int layer0)
		{
		}

		// Token: 0x0600620F RID: 25103 RVA: 0x0000216D File Offset: 0x0000036D
		private void HideChainSetInLayer(int layer)
		{
		}

		// Token: 0x06006210 RID: 25104 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetChainCard(int layer, bool isleft, int cardid, int styleid, bool isresolve)
		{
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x000F5A04 File Offset: 0x000F3C04
		private ValueTuple<bool, int, int> GetDataFromLayer(int layer)
		{
			return default(ValueTuple<bool, int, int>);
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetChainNum(int layer, bool pos, int num)
		{
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x000F5A1C File Offset: 0x000F3C1C
		private ValueTuple<SpriteRenderer, SpriteRenderer, SpriteRenderer> GetChainNumSR(int layer, bool pos)
		{
			return default(ValueTuple<SpriteRenderer, SpriteRenderer, SpriteRenderer>);
		}

		// Token: 0x06006214 RID: 25108 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSpriteNum(SpriteRenderer sprite, int digit)
		{
		}

		// Token: 0x06006215 RID: 25109 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetChainStack1(List<DuelChainManager.ChainSpotData> chainstack)
		{
		}

		// Token: 0x06006216 RID: 25110 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetChainStack2(List<DuelChainManager.ChainSpotData> chainstack)
		{
		}

		// Token: 0x06006217 RID: 25111 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetChainResolve1(List<DuelChainManager.ChainSpotData> chainstack)
		{
		}

		// Token: 0x06006218 RID: 25112 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetChainResolve2(List<DuelChainManager.ChainSpotData> chainstack, bool isFirst)
		{
		}

		// Token: 0x04009CF2 RID: 40178
		private const string LABEL_EO_CHAINSET = "ChainCardSet";

		// Token: 0x04009CF3 RID: 40179
		private const string LABEL_EO_OFFSET = "Offset";

		// Token: 0x04009CF4 RID: 40180
		private const string LABEL_EO_STRIPE = "ChainStraight";

		// Token: 0x04009CF5 RID: 40181
		private const string LABEL_EO_CHAINCARDEOM = "DummyChainCard";

		// Token: 0x04009CF6 RID: 40182
		private const string LABEL_EO_CHAINCARD = "DummyCardModel_front";

		// Token: 0x04009CF7 RID: 40183
		private const string LABEL_EO_CHAINNUM = "ChainNum";

		// Token: 0x04009CF8 RID: 40184
		private const string LABEL_EO_DIGITAL = "_Digit";

		// Token: 0x04009CF9 RID: 40185
		private const string LABEL_EO_TEN = "_Tens";

		// Token: 0x04009CFA RID: 40186
		private const string LABEL_EO_ONE = "_Ones";

		// Token: 0x04009CFB RID: 40187
		private const string LABEL_EO_RESOLVETEXT = "ResolveTextSet";

		// Token: 0x04009CFC RID: 40188
		private const string LABEL_EO_CHAINEFFECT = "_Chain";

		// Token: 0x04009CFD RID: 40189
		private const string LABEL_EO_CHAINSCALE = "CardLightSetScaleC";

		// Token: 0x04009CFE RID: 40190
		private static Dictionary<bool, string>[] m_LabelTable;

		// Token: 0x04009CFF RID: 40191
		private List<DuelChainManager.ChainSpotData> m_ChaindataList;
	}
}
