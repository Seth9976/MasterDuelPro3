using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013D4 RID: 5076
	public class SelectionToggle_Setting : SelectionToggle
	{
		// Token: 0x06009313 RID: 37651 RVA: 0x0014662E File Offset: 0x0014482E
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
			this.toggleWhenSelected = true;
		}

		// Token: 0x06009314 RID: 37652 RVA: 0x0014B3C4 File Offset: 0x001495C4
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			if (!this.scrollRect.gameObject.activeSelf)
			{
				this.scrollRect.gameObject.SetActive(true);
				this.ScrollRectToTop();
			}
			Program.instance.setting.lastSelectedToggle = this;
		}

		// Token: 0x06009315 RID: 37653 RVA: 0x0014B410 File Offset: 0x00149610
		protected override void CallToggleOffEvent()
		{
			base.CallToggleOffEvent();
			this.scrollRect.gameObject.SetActive(false);
		}

		// Token: 0x06009316 RID: 37654 RVA: 0x0014B429 File Offset: 0x00149629
		protected override void OnSubmit()
		{
			base.OnSubmit();
			EventSystem.current.SetSelectedGameObject(this.scrollRect.content.GetChild(0).gameObject);
		}

		// Token: 0x06009317 RID: 37655 RVA: 0x0014B451 File Offset: 0x00149651
		public void ScrollRectToTop()
		{
			this.scrollRect.verticalScrollbar.value = 1f;
		}

		// Token: 0x0400D16A RID: 53610
		[SerializeField]
		private ScrollRect scrollRect;
	}
}
