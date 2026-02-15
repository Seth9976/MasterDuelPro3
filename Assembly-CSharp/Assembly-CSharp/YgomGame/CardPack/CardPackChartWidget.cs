using System;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.CardPack
{
	// Token: 0x020010A5 RID: 4261
	public class CardPackChartWidget : ElementWidgetBase
	{
		// Token: 0x06007EC0 RID: 32448 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPackChartWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06007EC1 RID: 32449 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public CardPackChartWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06007EC2 RID: 32450 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binging(int destructive, int handling, int difficult)
		{
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x0000216D File Offset: 0x0000036D
		private void BindGauge(ElementObjectManager gaugeEom, float amount)
		{
		}

		// Token: 0x06007EC4 RID: 32452 RVA: 0x0000216D File Offset: 0x0000036D
		private void BindUniqueGauge(ElementObjectManager gaugeEom, string label, float amount, Color color)
		{
		}

		// Token: 0x0400B773 RID: 46963
		private readonly string k_ELabelDestructive;

		// Token: 0x0400B774 RID: 46964
		private readonly string k_ELabelDifficult;

		// Token: 0x0400B775 RID: 46965
		private readonly string k_ELabelHandling;
	}
}
