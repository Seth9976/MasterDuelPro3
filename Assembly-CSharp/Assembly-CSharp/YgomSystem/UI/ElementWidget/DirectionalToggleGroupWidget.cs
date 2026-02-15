using System;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x0200068B RID: 1675
	public class DirectionalToggleGroupWidget : ElementWidgetBehaviourBase<DirectionalToggleGroupWidget>
	{
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x0000216A File Offset: 0x0000036A
		public ToggleGroupWidget toggleGroupWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x0000216A File Offset: 0x0000036A
		public static DirectionalToggleGroupWidget Create(ElementObjectManager eom, string elabelFormat, int defaultIdx = 0)
		{
			return null;
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x0000216A File Offset: 0x0000036A
		public static DirectionalToggleGroupWidget Create(ElementObjectManager eom, ToggleWidget[] toggles, int defaultIdx = 0)
		{
			return null;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedToggleCallback()
		{
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeIdx(int idx)
		{
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCurrentToggleToSelector()
		{
		}

		// Token: 0x0400300B RID: 12299
		private ToggleGroupWidget m_ToggleGroupWidget;

		// Token: 0x0400300C RID: 12300
		public bool focusIdxToSelectionTransitionItem;
	}
}
