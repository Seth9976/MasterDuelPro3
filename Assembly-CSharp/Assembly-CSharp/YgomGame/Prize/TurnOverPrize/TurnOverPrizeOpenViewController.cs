using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomGame.Shop;

namespace YgomGame.Prize.TurnOverPrize
{
	// Token: 0x02000A1A RID: 2586
	public class TurnOverPrizeOpenViewController : BaseMenuViewController
	{
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06004B09 RID: 19209 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int prizeId, ShopBuyViewController.PurchaseHandler purchaseHandler, Dictionary<string, object> args)
		{
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yMainRoutine()
		{
			return null;
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayShuffle()
		{
			return null;
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ySelectPrize()
		{
			return null;
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayResult()
		{
			return null;
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAllPrizes(bool setArrowVisible = false)
		{
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPrizeByPos(int pos, bool setArrowVisible = false)
		{
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPrizesAsBeforeSuffle()
		{
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPrizeOnUnlock(int idx)
		{
		}

		// Token: 0x0400892B RID: 35115
		private const string k_VCPath = "Prize/TurnOverPrize/TurnOverPrizeOpen";

		// Token: 0x0400892C RID: 35116
		private const string k_ArgKeyPrizeId = "prizeId";

		// Token: 0x0400892D RID: 35117
		internal const string k_ArgKeyPurchaseHandler = "priceContext";

		// Token: 0x0400892E RID: 35118
		private bool m_BeforePostProcessing;

		// Token: 0x0400892F RID: 35119
		private ShopBuyViewController.PurchaseHandler m_PurchaseHandler;

		// Token: 0x04008930 RID: 35120
		private string m_CoverPath;

		// Token: 0x04008931 RID: 35121
		private List<object> m_PrizeDatas;

		// Token: 0x04008932 RID: 35122
		private List<string> m_LoadedAssetsPath;

		// Token: 0x04008933 RID: 35123
		private GameObject m_Root3D;

		// Token: 0x04008934 RID: 35124
		private ActorRoot m_ActorRoot;

		// Token: 0x04008935 RID: 35125
		private int m_DecidedIdx;
	}
}
