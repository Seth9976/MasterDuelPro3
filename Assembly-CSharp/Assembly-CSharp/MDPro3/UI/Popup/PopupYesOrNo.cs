using System;
using TMPro;

namespace MDPro3.UI.Popup
{
	// Token: 0x02001491 RID: 5265
	public class PopupYesOrNo : Popup
	{
		// Token: 0x06009A23 RID: 39459 RVA: 0x00170C04 File Offset: 0x0016EE04
		protected override void InitializeSelections()
		{
			base.InitializeSelections();
			base.Manager.GetElement<TextMeshProUGUI>("FrameText").text = this.args[1];
			base.Manager.GetElement<SelectionButton>("DecideButton").SetButtonText(this.args[2]);
			base.Manager.GetElement<SelectionButton>("CancelButton").SetButtonText(this.args[3]);
			if (this.args.Count > 4)
			{
				base.Manager.GetElement("Icon").SetActive(true);
			}
		}

		// Token: 0x06009A24 RID: 39460 RVA: 0x0016F7F9 File Offset: 0x0016D9F9
		public override void Show()
		{
			base.Show();
			AudioManager.PlaySE("SE_SYS_VERIFY", 1f);
		}

		// Token: 0x06009A25 RID: 39461 RVA: 0x00170C9E File Offset: 0x0016EE9E
		protected override void OnDecide()
		{
			Action action = this.decideAction;
			if (action != null)
			{
				action();
			}
			this.Hide();
		}

		// Token: 0x06009A26 RID: 39462 RVA: 0x00170CB7 File Offset: 0x0016EEB7
		protected override void OnCancel()
		{
			Action action = this.cancelAction;
			if (action != null)
			{
				action();
			}
			this.Hide();
		}

		// Token: 0x0400D7C9 RID: 55241
		public Action decideAction;

		// Token: 0x0400D7CA RID: 55242
		public Action cancelAction;
	}
}
