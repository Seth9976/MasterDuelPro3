using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x0200069B RID: 1691
	public class ToggleGroupWidget : ElementWidgetBase
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003534 RID: 13620 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentIdx
		{
			get
			{
				return 0;
			}
			private set
			{
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x0000216A File Offset: 0x0000036A
		public ToggleWidget currentToggle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003537 RID: 13623 RVA: 0x0000216D File Offset: 0x0000036D
		public bool selectChildOnCurrentSelected
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<ToggleWidget> childToggles
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06003539 RID: 13625 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600353A RID: 13626 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int> onChangeIdx
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ToggleGroupWidget(ElementObjectManager eom, ToggleWidget[] toggles, int defaultIdx = 0)
			: base(null)
		{
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectCurrentToggle(bool initializeSelection = false)
		{
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool MoveIdx(int idx, bool isLoop = false)
		{
			return false;
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool MoveIdxNext(bool isLoop = false)
		{
			return false;
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool MoveIdxBack(bool isLoop = false)
		{
			return false;
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetIdx(int idx)
		{
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetAllOff()
		{
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangedToggleValue(int idx, bool value)
		{
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDefaultItem()
		{
		}

		// Token: 0x0400306F RID: 12399
		private readonly ToggleWidget[] m_ChildToggles;

		// Token: 0x04003070 RID: 12400
		private int m_CurrentIdx;
	}
}
