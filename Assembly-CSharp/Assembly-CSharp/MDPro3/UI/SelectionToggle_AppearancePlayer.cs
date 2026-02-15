using System;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013B8 RID: 5048
	public class SelectionToggle_AppearancePlayer : SelectionToggle
	{
		// Token: 0x06009256 RID: 37462 RVA: 0x0014662E File Offset: 0x0014482E
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
			this.toggleWhenSelected = true;
		}

		// Token: 0x06009257 RID: 37463 RVA: 0x00147912 File Offset: 0x00145B12
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.appearance.GetUI<AppearanceUI>().SwitchPlayer(this.playerCode);
		}

		// Token: 0x06009258 RID: 37464 RVA: 0x00147934 File Offset: 0x00145B34
		public void OnLeftSelection()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			Selectable leftSelectable = base.Selectable.navigation.selectOnLeft;
			if (this.isOn)
			{
				if (leftSelectable != null)
				{
					leftSelectable.GetComponent<SelectionToggle_AppearancePlayer>().SetToggleOn(true);
					return;
				}
			}
			else if (leftSelectable != null)
			{
				leftSelectable.GetComponent<SelectionToggle_AppearancePlayer>().OnLeftSelection();
			}
		}

		// Token: 0x06009259 RID: 37465 RVA: 0x00147998 File Offset: 0x00145B98
		public void OnRightSelection()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			Selectable rightSelectable = base.Selectable.navigation.selectOnRight;
			if (this.isOn)
			{
				if (rightSelectable != null)
				{
					rightSelectable.GetComponent<SelectionToggle_AppearancePlayer>().SetToggleOn(true);
					return;
				}
			}
			else if (rightSelectable != null)
			{
				rightSelectable.GetComponent<SelectionToggle_AppearancePlayer>().OnRightSelection();
			}
		}

		// Token: 0x0400D0DA RID: 53466
		[SerializeField]
		private string playerCode;
	}
}
