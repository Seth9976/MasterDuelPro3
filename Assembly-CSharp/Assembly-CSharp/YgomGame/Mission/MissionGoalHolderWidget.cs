using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Mission
{
	// Token: 0x02000A30 RID: 2608
	public class MissionGoalHolderWidget : ElementWidgetBase
	{
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06004BAC RID: 19372 RVA: 0x0000216A File Offset: 0x0000036A
		public LayoutElement layoutElement
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MissionGoalHolderWidget(ElementObjectManager eom, MissionGoalsWidget ownerGoals)
			: base(null)
		{
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick()
		{
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelected()
		{
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeselected()
		{
		}

		// Token: 0x04008998 RID: 35224
		public int idx;

		// Token: 0x04008999 RID: 35225
		public readonly MissionGoalsWidget ownerGoals;

		// Token: 0x0400899A RID: 35226
		public readonly SelectionButton button;

		// Token: 0x0400899B RID: 35227
		public readonly GameObject root;

		// Token: 0x0400899C RID: 35228
		public MissionGoalWidget goalWidget;

		// Token: 0x0400899D RID: 35229
		private LayoutElement m_LayoutElementCache;

		// Token: 0x0400899E RID: 35230
		public Action<MissionGoalHolderWidget> onClickCallback;

		// Token: 0x0400899F RID: 35231
		public Action<MissionGoalHolderWidget> onSelectedCallback;

		// Token: 0x040089A0 RID: 35232
		public Action<MissionGoalHolderWidget> onDeselectedCallback;
	}
}
