using System;
using TMPro;

namespace MDPro3.UI.Popup
{
	// Token: 0x02001487 RID: 5255
	public class PopupConfirm : Popup
	{
		// Token: 0x060099F9 RID: 39417 RVA: 0x0016F7D0 File Offset: 0x0016D9D0
		protected override void InitializeSelections()
		{
			base.InitializeSelections();
			base.Manager.GetElement<TextMeshProUGUI>("FrameText").text = this.args[1];
		}

		// Token: 0x060099FA RID: 39418 RVA: 0x0016F7F9 File Offset: 0x0016D9F9
		public override void Show()
		{
			base.Show();
			AudioManager.PlaySE("SE_SYS_VERIFY", 1f);
		}
	}
}
