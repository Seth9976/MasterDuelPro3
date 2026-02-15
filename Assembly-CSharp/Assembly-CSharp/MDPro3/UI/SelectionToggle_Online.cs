using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013CB RID: 5067
	public class SelectionToggle_Online : SelectionToggle
	{
		// Token: 0x060092C5 RID: 37573 RVA: 0x0014662E File Offset: 0x0014482E
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
			this.toggleWhenSelected = true;
		}

		// Token: 0x060092C6 RID: 37574 RVA: 0x00149F25 File Offset: 0x00148125
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			this.targetPage.SetActive(true);
			Program.instance.online.Select(false);
		}

		// Token: 0x060092C7 RID: 37575 RVA: 0x00149F49 File Offset: 0x00148149
		protected override void CallToggleOffEvent()
		{
			base.CallToggleOffEvent();
			this.targetPage.SetActive(false);
		}

		// Token: 0x060092C8 RID: 37576 RVA: 0x00149F60 File Offset: 0x00148160
		public void OnLeftSelection()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			Selectable leftSelectable = base.Selectable.navigation.selectOnLeft;
			if (this.isOn)
			{
				if (leftSelectable != null)
				{
					leftSelectable.GetComponent<SelectionToggle_Online>().SetToggleOn(true);
					return;
				}
			}
			else if (leftSelectable != null)
			{
				leftSelectable.GetComponent<SelectionToggle_Online>().OnLeftSelection();
			}
		}

		// Token: 0x060092C9 RID: 37577 RVA: 0x00149FC4 File Offset: 0x001481C4
		public void OnRightSelection()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			Selectable rightSelectable = base.Selectable.navigation.selectOnRight;
			if (this.isOn)
			{
				if (rightSelectable != null)
				{
					rightSelectable.GetComponent<SelectionToggle_Online>().SetToggleOn(true);
					return;
				}
			}
			else if (rightSelectable != null)
			{
				rightSelectable.GetComponent<SelectionToggle_Online>().OnRightSelection();
			}
		}

		// Token: 0x0400D124 RID: 53540
		[Header("SelectionToggle Online")]
		[SerializeField]
		private GameObject targetPage;
	}
}
