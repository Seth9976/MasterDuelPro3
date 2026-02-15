using System;
using MDPro3.UI.Popup;
using MDPro3.Utility;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x020013D2 RID: 5074
	public class SelectionToggle_SearchFilter : SelectionToggle
	{
		// Token: 0x06009308 RID: 37640 RVA: 0x0014B0C4 File Offset: 0x001492C4
		protected override void Awake()
		{
			base.Awake();
			if (this.code != 0)
			{
				string originText = this.GetButtonText();
				this.SetButtonText(StringHelper.GetUnsafe(this.code, 0));
				if (this.subCode != 0)
				{
					if (this.subCode == 9999)
					{
						this.SetButtonText(InterString.Get("[?]族", this.GetButtonText(), 0));
						return;
					}
					if (this.subCode == 1051 || this.subCode == 1052)
					{
						this.SetButtonText(InterString.Get(originText, 0));
						return;
					}
					string title = this.GetButtonText();
					if (Language.NeedBlankToAddWord())
					{
						title += " ";
					}
					title += StringHelper.GetUnsafe(this.subCode, 0);
					this.SetButtonText(title);
				}
			}
		}

		// Token: 0x06009309 RID: 37641 RVA: 0x0014B187 File Offset: 0x00149387
		protected override void CallHoverOnEvent()
		{
			base.CallHoverOnEvent();
			((PopupSearchFilter)Program.instance.ui_.currentPopupB).lastSelectedToggle = this;
		}

		// Token: 0x0400D165 RID: 53605
		[Header("SelectionToggle SearchFilter")]
		public int code;

		// Token: 0x0400D166 RID: 53606
		public int subCode;

		// Token: 0x0400D167 RID: 53607
		public int group;

		// Token: 0x0400D168 RID: 53608
		public long filterCode;
	}
}
