using System;
using MDPro3.UI.PropertyOverride;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.Popup
{
	// Token: 0x02001490 RID: 5264
	public class PopupText : Popup
	{
		// Token: 0x06009A20 RID: 39456 RVA: 0x00170AD0 File Offset: 0x0016ECD0
		protected override void InitializeSelections()
		{
			base.InitializeSelections();
			TextMeshProUGUI element = base.Manager.GetElement<TextMeshProUGUI>("Text");
			element.text = this.args[1];
			element.horizontalAlignment = this.alignment;
			element.GetComponent<ContentSizeFitter>().SetLayoutVertical();
			float preferredHeight = element.GetComponent<RectTransform>().rect.height + (PropertyOverrider.NeedMobileLayout() ? 40f : 32f);
			base.Manager.GetElement<LayoutElement>("EntryButtonsScrollView").preferredHeight = preferredHeight;
		}

		// Token: 0x06009A21 RID: 39457 RVA: 0x00170B5C File Offset: 0x0016ED5C
		protected override void Update()
		{
			if (!this.NeedResponseInput())
			{
				return;
			}
			if ((UserInput.MouseRightDown || UserInput.WasCancelPressed) && this.cancelCallHide)
			{
				AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
				this.Hide();
			}
			if (UserInput.RightScrollWheel.y != 0f)
			{
				this.scrollbar.value = Mathf.Clamp01(this.scrollbar.value + UserInput.RightScrollWheel.y * 1000f * Time.unscaledDeltaTime / this.content.rect.height);
			}
		}

		// Token: 0x0400D7C6 RID: 55238
		[Header("Popup Text")]
		public HorizontalAlignmentOptions alignment = HorizontalAlignmentOptions.Center;

		// Token: 0x0400D7C7 RID: 55239
		[SerializeField]
		private Scrollbar scrollbar;

		// Token: 0x0400D7C8 RID: 55240
		[SerializeField]
		private RectTransform content;
	}
}
