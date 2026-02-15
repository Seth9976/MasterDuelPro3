using System;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013BE RID: 5054
	public class SelectionToggle_CharacterPlayer : SelectionToggle
	{
		// Token: 0x06009273 RID: 37491 RVA: 0x0014662E File Offset: 0x0014482E
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
			this.toggleWhenSelected = true;
		}

		// Token: 0x06009274 RID: 37492 RVA: 0x00147E08 File Offset: 0x00146008
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.character.GetUI<CharacterSelectorUI>().SwitchPlayer(this.playerCode);
		}

		// Token: 0x06009275 RID: 37493 RVA: 0x00147E2C File Offset: 0x0014602C
		public void OnLeftSelection()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			Selectable leftSelectable = base.Selectable.navigation.selectOnLeft;
			if (this.isOn)
			{
				if (leftSelectable != null)
				{
					leftSelectable.GetComponent<SelectionToggle_CharacterPlayer>().SetToggleOn(true);
					return;
				}
			}
			else if (leftSelectable != null)
			{
				leftSelectable.GetComponent<SelectionToggle_CharacterPlayer>().OnLeftSelection();
			}
		}

		// Token: 0x06009276 RID: 37494 RVA: 0x00147E90 File Offset: 0x00146090
		public void OnRightSelection()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			Selectable rightSelectable = base.Selectable.navigation.selectOnRight;
			if (this.isOn)
			{
				if (rightSelectable != null)
				{
					rightSelectable.GetComponent<SelectionToggle_CharacterPlayer>().SetToggleOn(true);
					return;
				}
			}
			else if (rightSelectable != null)
			{
				rightSelectable.GetComponent<SelectionToggle_CharacterPlayer>().OnRightSelection();
			}
		}

		// Token: 0x0400D0E4 RID: 53476
		[SerializeField]
		private string playerCode;
	}
}
