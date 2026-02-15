using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Mission
{
	// Token: 0x02000A34 RID: 2612
	public class MissionGoalsWidget : ElementWidgetBase
	{
		// Token: 0x06004BC8 RID: 19400 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MissionGoalsWidget(ElementObjectManager eom, MissionPanelWidget ownerPanel)
			: base(null)
		{
		}

		// Token: 0x040089B9 RID: 35257
		private readonly int k_ELabelGoalWidgetMax;

		// Token: 0x040089BA RID: 35258
		private readonly string k_ELabelGoalHolderFormat;

		// Token: 0x040089BB RID: 35259
		private readonly string k_ELabelGauge;

		// Token: 0x040089BC RID: 35260
		private readonly string k_ELabelGaugeStartHead;

		// Token: 0x040089BD RID: 35261
		private readonly string k_ELabelGaugeGaugeStartHeadFill;

		// Token: 0x040089BE RID: 35262
		private readonly string k_ELabelGaugeExtendHead;

		// Token: 0x040089BF RID: 35263
		private readonly string k_ELabelGaugeExtendHeadFill;

		// Token: 0x040089C0 RID: 35264
		private readonly string k_ELabelGaugeExtendTail;

		// Token: 0x040089C1 RID: 35265
		private readonly string k_ELabelGaugeExtendTailFill;

		// Token: 0x040089C2 RID: 35266
		private readonly string k_ELabelSecretFilter;

		// Token: 0x040089C3 RID: 35267
		public int idx;

		// Token: 0x040089C4 RID: 35268
		public readonly MissionPanelWidget ownerPanel;

		// Token: 0x040089C5 RID: 35269
		public readonly Slider gauge;

		// Token: 0x040089C6 RID: 35270
		public readonly GameObject gaugeStartHead;

		// Token: 0x040089C7 RID: 35271
		public readonly GameObject gaugeStartHeadFill;

		// Token: 0x040089C8 RID: 35272
		public readonly GameObject gaugeExtendHeadBar;

		// Token: 0x040089C9 RID: 35273
		public readonly GameObject gaugeExtendHeadFill;

		// Token: 0x040089CA RID: 35274
		public readonly GameObject gaugeExtendTailBar;

		// Token: 0x040089CB RID: 35275
		public readonly GameObject gaugeExtendTailFill;

		// Token: 0x040089CC RID: 35276
		public readonly Slider secretFilterGauge;

		// Token: 0x040089CD RID: 35277
		public readonly List<MissionGoalHolderWidget> goalHolders;
	}
}
