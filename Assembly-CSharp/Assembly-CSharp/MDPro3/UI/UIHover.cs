using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001386 RID: 4998
	public class UIHover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06009098 RID: 37016 RVA: 0x0013D122 File Offset: 0x0013B322
		public bool Hover
		{
			get
			{
				return this._hover;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06009099 RID: 37017 RVA: 0x0013D12C File Offset: 0x0013B32C
		private Image Image
		{
			get
			{
				return this.m_Image = ((this.m_Image != null) ? this.m_Image : base.GetComponent<Image>());
			}
		}

		// Token: 0x0600909A RID: 37018 RVA: 0x0013D15E File Offset: 0x0013B35E
		private void OnDisable()
		{
			this.Hide();
		}

		// Token: 0x0600909B RID: 37019 RVA: 0x0013D168 File Offset: 0x0013B368
		public void OnPointerEnter(PointerEventData eventData)
		{
			this._hover = true;
			if (UserInput.Draging && this.Image != null)
			{
				this.Image.color = new Color(1f, 1f, 1f, this.alpha);
				UIHover.HoveringLabel = this.label;
			}
		}

		// Token: 0x0600909C RID: 37020 RVA: 0x0013D1C4 File Offset: 0x0013B3C4
		public void OnPointerExit(PointerEventData eventData)
		{
			this._hover = false;
			if (this.Image != null)
			{
				this.Image.color = Color.clear;
				if (UIHover.HoveringLabel == this.label)
				{
					UIHover.HoveringLabel = string.Empty;
				}
			}
		}

		// Token: 0x0600909D RID: 37021 RVA: 0x0013D212 File Offset: 0x0013B412
		public void Hide()
		{
			if (this.Image != null)
			{
				this.Image.color = Color.clear;
			}
		}

		// Token: 0x0400CF4F RID: 53071
		public const string LABEL_LEFT = "Left";

		// Token: 0x0400CF50 RID: 53072
		public const string LABEL_RIGHT = "Right";

		// Token: 0x0400CF51 RID: 53073
		public const string LABEL_REMOVEDECK = "RemoveDeck";

		// Token: 0x0400CF52 RID: 53074
		public const string LABEL_ADDBOOKMARK = "AddBookmark";

		// Token: 0x0400CF53 RID: 53075
		public const string LABEL_CANNOTADDBOOKMARK = "CanNotAddBookmark";

		// Token: 0x0400CF54 RID: 53076
		public const string LABEL_MAINDECK = "MainDeck";

		// Token: 0x0400CF55 RID: 53077
		public const string LABEL_EXTRADECK = "ExtraDeck";

		// Token: 0x0400CF56 RID: 53078
		public const string LABEL_SIDEDECK = "SideDeck";

		// Token: 0x0400CF57 RID: 53079
		public static string HoveringLabel;

		// Token: 0x0400CF58 RID: 53080
		[SerializeField]
		private float alpha = 0.2f;

		// Token: 0x0400CF59 RID: 53081
		[SerializeField]
		private string label;

		// Token: 0x0400CF5A RID: 53082
		private bool _hover;

		// Token: 0x0400CF5B RID: 53083
		private Image m_Image;
	}
}
