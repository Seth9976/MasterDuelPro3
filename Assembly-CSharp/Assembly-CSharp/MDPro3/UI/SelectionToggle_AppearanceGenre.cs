using System;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x020013B3 RID: 5043
	public class SelectionToggle_AppearanceGenre : SelectionToggle
	{
		// Token: 0x06009231 RID: 37425 RVA: 0x0014662E File Offset: 0x0014482E
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
			this.toggleWhenSelected = true;
		}

		// Token: 0x06009232 RID: 37426 RVA: 0x0014664B File Offset: 0x0014484B
		public override void SetToggleOn(bool callEvent = true)
		{
			if (!this.isOn)
			{
				Program.instance.appearance.GetUI<AppearanceUI>().ScrollRect.verticalScrollbar.value = 1f;
			}
			base.SetToggleOn(true);
		}

		// Token: 0x06009233 RID: 37427 RVA: 0x0014667F File Offset: 0x0014487F
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.appearance.GetUI<AppearanceUI>().ShowItems(base.name.Split(" ", StringSplitOptions.None)[1]);
			Program.instance.appearance.lastSelectedToggle = this;
		}

		// Token: 0x06009234 RID: 37428 RVA: 0x001466C0 File Offset: 0x001448C0
		protected override void OnSubmit()
		{
			base.OnSubmit();
			UserInput.NextSelectionIsAxis = true;
			GameObject target = Program.instance.appearance.GetUI<AppearanceUI>().GetCurrentContentItem();
			if (target == null)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(target);
		}
	}
}
