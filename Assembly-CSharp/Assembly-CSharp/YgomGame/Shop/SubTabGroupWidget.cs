using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000976 RID: 2422
	public class SubTabGroupWidget : ElementWidgetBase, ISubTabWidget
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060046E5 RID: 18149 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopTabWidget tabWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060046E6 RID: 18150 RVA: 0x0000216A File Offset: 0x0000036A
		public ElementEntityFactory entityFactory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060046E7 RID: 18151 RVA: 0x0000216A File Offset: 0x0000036A
		public SubTabGroupWidget parentGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060046E8 RID: 18152 RVA: 0x000029CC File Offset: 0x00000BCC
		public int dataCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x0000216A File Offset: 0x0000036A
		private List<Tween> GetTweenByLabel(string label)
		{
			return null;
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public SubTabGroupWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x0000216D File Offset: 0x0000036D
		public void CaptureTweenFrom()
		{
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayTween(bool isOn, bool immediate = false)
		{
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckCollectTween()
		{
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x0000216D File Offset: 0x0000036D
		private void StopAllTween()
		{
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x0000216D File Offset: 0x0000036D
		private void TargetGotoAndPlayLabel(string label, bool wakeup, bool immediate)
		{
		}

		// Token: 0x0400851F RID: 34079
		private const string k_ELabelHandleToggle = "HandleToggle";

		// Token: 0x04008520 RID: 34080
		private const string k_TLabelToOpen = "ToOpen";

		// Token: 0x04008521 RID: 34081
		private const string k_TLabelToClose = "ToClose";

		// Token: 0x04008522 RID: 34082
		private readonly ShopTabWidget m_TabWidget;

		// Token: 0x04008523 RID: 34083
		private readonly ElementEntityFactory m_EntityFactory;

		// Token: 0x04008524 RID: 34084
		private List<Tween> m_ToOpenTweens;

		// Token: 0x04008525 RID: 34085
		private List<Tween> m_ToCloseTweens;
	}
}
