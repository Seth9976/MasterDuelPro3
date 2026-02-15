using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200139B RID: 5019
	public class PopupText : PopupBase
	{
		// Token: 0x06009108 RID: 37128 RVA: 0x00140798 File Offset: 0x0013E998
		public override void InitializeSelections()
		{
			base.InitializeSelections();
			this.text.text = this.selections[1];
			this.text.GetComponent<ContentSizeFitter>().SetLayoutVertical();
			float height = this.text.GetComponent<RectTransform>().rect.height;
			if (height > 825f)
			{
				height = 825f;
			}
			if (height < 300f)
			{
				height = 300f;
			}
			Vector2 sizeDelta = new Vector2(-50f, height);
			this.scrollRect.GetComponent<RectTransform>().sizeDelta = sizeDelta;
			this.backTop.sizeDelta = new Vector2(this.backTop.sizeDelta.x, (1100f - sizeDelta.y) / 2f);
			this.backBotton.sizeDelta = new Vector2(this.backBotton.sizeDelta.x, (1100f - sizeDelta.y) / 2f);
			this.scrollRect.verticalScrollbar.value = 1f;
			this.text.horizontalAlignment = this.alignment;
		}

		// Token: 0x06009109 RID: 37129 RVA: 0x001408AF File Offset: 0x0013EAAF
		public override void OnCancel()
		{
			base.OnCancel();
			this.Hide();
		}

		// Token: 0x0400CFD5 RID: 53205
		[Header("Popup Select Reference")]
		public RectTransform backTop;

		// Token: 0x0400CFD6 RID: 53206
		public RectTransform backBotton;

		// Token: 0x0400CFD7 RID: 53207
		public ScrollRect scrollRect;

		// Token: 0x0400CFD8 RID: 53208
		public TextMeshProUGUI text;

		// Token: 0x0400CFD9 RID: 53209
		public HorizontalAlignmentOptions alignment = HorizontalAlignmentOptions.Center;
	}
}
