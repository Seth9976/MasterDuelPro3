using System;
using System.Collections.Generic;
using UnityEngine.UI;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000FBE RID: 4030
	public class CardStrip : DeckEditCard
	{
		// Token: 0x06007888 RID: 30856 RVA: 0x0000216D File Offset: 0x0000036D
		protected new void InitializeElemnts()
		{
		}

		// Token: 0x06007889 RID: 30857 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x0600788A RID: 30858 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600788B RID: 30859 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardInventory(bool nonPrizeOnly = false)
		{
		}

		// Token: 0x0600788C RID: 30860 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardInventory(int num)
		{
		}

		// Token: 0x0600788D RID: 30861 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInDeckSum(int numN, int alterN, int numP1, int alterP1, int numP2, int alterP2, int numRental, int alterR)
		{
		}

		// Token: 0x0600788E RID: 30862 RVA: 0x0000216D File Offset: 0x0000036D
		private void AdjustIndicator(int oldNum, int newNum, List<Image> list, Image template, bool isAlter = false)
		{
		}

		// Token: 0x0600788F RID: 30863 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInDeckIndicatorColor(bool isFull)
		{
		}

		// Token: 0x06007890 RID: 30864 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetData(CardBaseData baseData, int regulationID, DeckEditViewController2.DisplayMode mode = DeckEditViewController2.DisplayMode.Simple)
		{
		}

		// Token: 0x06007891 RID: 30865 RVA: 0x0000216D File Offset: 0x0000036D
		public new void ScalingIcons(float scale = 1.5f)
		{
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetSelectionItem()
		{
			return null;
		}

		// Token: 0x0400B04E RID: 45134
		private ExtendedTextMeshProUGUI m_CardInventory;

		// Token: 0x0400B04F RID: 45135
		private Image m_InDeckIndicatorRental;

		// Token: 0x0400B050 RID: 45136
		private Image m_InDeckIndicator1;

		// Token: 0x0400B051 RID: 45137
		private Image m_InDeckIndicator2;

		// Token: 0x0400B052 RID: 45138
		private Image m_InDeckIndicator3;

		// Token: 0x0400B053 RID: 45139
		private int inDeckR;

		// Token: 0x0400B054 RID: 45140
		private int inDeckN;

		// Token: 0x0400B055 RID: 45141
		private int inDeckP1;

		// Token: 0x0400B056 RID: 45142
		private int inDeckP2;

		// Token: 0x0400B057 RID: 45143
		private List<Image> m_IndicatorsR;

		// Token: 0x0400B058 RID: 45144
		private List<Image> m_IndicatorsN;

		// Token: 0x0400B059 RID: 45145
		private List<Image> m_IndicatorsP1;

		// Token: 0x0400B05A RID: 45146
		private List<Image> m_IndicatorsP2;

		// Token: 0x0400B05B RID: 45147
		private int inDeckAlterR;

		// Token: 0x0400B05C RID: 45148
		private int inDeckAlterN;

		// Token: 0x0400B05D RID: 45149
		private int inDeckAlterP1;

		// Token: 0x0400B05E RID: 45150
		private int inDeckAlterP2;

		// Token: 0x0400B05F RID: 45151
		private List<Image> m_IndicatorsAlterR;

		// Token: 0x0400B060 RID: 45152
		private List<Image> m_IndicatorsAlterN;

		// Token: 0x0400B061 RID: 45153
		private List<Image> m_IndicatorsAlterP1;

		// Token: 0x0400B062 RID: 45154
		private List<Image> m_IndicatorsAlterP2;

		// Token: 0x0400B063 RID: 45155
		private bool isIni;
	}
}
