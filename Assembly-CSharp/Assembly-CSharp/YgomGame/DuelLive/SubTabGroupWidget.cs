using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C6D RID: 3181
	public class SubTabGroupWidget : ElementWidgetBase, ISubTabWidget
	{
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveTabWidget tabWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06005B05 RID: 23301 RVA: 0x0000216A File Offset: 0x0000036A
		public ElementEntityFactory entityFactory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06005B06 RID: 23302 RVA: 0x0000216A File Offset: 0x0000036A
		public SubTabGroupWidget parentGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005B07 RID: 23303 RVA: 0x0000216A File Offset: 0x0000036A
		private List<Tween> GetTweenByLabel(string label)
		{
			return null;
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public SubTabGroupWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06005B09 RID: 23305 RVA: 0x0000216D File Offset: 0x0000036D
		public void CaptureTweenFrom()
		{
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayTween(bool isOn, bool immediate = false)
		{
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckCollectTween()
		{
		}

		// Token: 0x06005B0C RID: 23308 RVA: 0x0000216D File Offset: 0x0000036D
		private void StopAllTween()
		{
		}

		// Token: 0x06005B0D RID: 23309 RVA: 0x0000216D File Offset: 0x0000036D
		private void TargetGotoAndPlayLabel(string label, bool wakeup, bool immediate)
		{
		}

		// Token: 0x0400964C RID: 38476
		private const string k_ELabelHandleToggle = "HandleToggle";

		// Token: 0x0400964D RID: 38477
		private const string k_TLabelToOpen = "ToOpen";

		// Token: 0x0400964E RID: 38478
		private const string k_TLabelToClose = "ToClose";

		// Token: 0x0400964F RID: 38479
		private readonly DuelLiveTabWidget m_TabWidget;

		// Token: 0x04009650 RID: 38480
		private readonly ElementEntityFactory m_EntityFactory;

		// Token: 0x04009651 RID: 38481
		private List<Tween> m_ToOpenTweens;

		// Token: 0x04009652 RID: 38482
		private List<Tween> m_ToCloseTweens;
	}
}
