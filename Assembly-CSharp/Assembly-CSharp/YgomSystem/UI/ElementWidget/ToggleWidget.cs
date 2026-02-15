using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.Utility;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x0200069C RID: 1692
	public class ToggleWidget : ElementWidgetBase
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003545 RID: 13637 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isOn
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject badge
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06003547 RID: 13639 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06003548 RID: 13640 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<bool> onChangeValue
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

		// Token: 0x06003549 RID: 13641 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ToggleWidget(ElementObjectManager eom, bool isOn = false)
			: base(null)
		{
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetIsOn(bool isOn)
		{
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateView()
		{
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick()
		{
		}

		// Token: 0x04003071 RID: 12401
		private readonly string k_ELabelButton;

		// Token: 0x04003072 RID: 12402
		private readonly string k_ELabelOn;

		// Token: 0x04003073 RID: 12403
		private readonly string k_ELabelOff;

		// Token: 0x04003074 RID: 12404
		private readonly string k_ELabelBadge;

		// Token: 0x04003075 RID: 12405
		private readonly string k_PLabelSoundClickOn;

		// Token: 0x04003076 RID: 12406
		private readonly string k_PLabelSoundClickOff;

		// Token: 0x04003077 RID: 12407
		public readonly SelectionButton button;

		// Token: 0x04003078 RID: 12408
		public readonly GameObject onImage;

		// Token: 0x04003079 RID: 12409
		public readonly GameObject offImage;

		// Token: 0x0400307A RID: 12410
		public readonly PropertyContainer propertyContainer;

		// Token: 0x0400307B RID: 12411
		private bool m_IsOn;
	}
}
